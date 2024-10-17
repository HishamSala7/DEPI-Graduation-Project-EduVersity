using AutoMapper;
using EduVersity.Data.Models;
using EduVersity.Managers.CourseLevelSemesterManager;
using EduVersity.Managers.CourseManager;
using EduVersity.Managers.DepartmentManager;
using EduVersity.Managers.LevelManager;
using EduVersity.Repos.SemesterRepo;
using EduVersity.ViewModels.Course;
using EduVersity.ViewModels.CourseLevelSemester;
using EduVersity.ViewModels.Level;
using EduVersity.ViewModels.Semester;
using Microsoft.IdentityModel.Tokens;

namespace EduVersity.Managers.SemesterManager
{
    public class SemesterManager : ISemesterManager
    {
        private readonly ISemesterRepo _semesterRepo;
        private readonly IMapper _mapper;
        private readonly ICourseManager _courseManager;
        private readonly ICourseLevelSemesterManager _courseLevelSemesterManager;
        private readonly ILevelManager _levelManager;

        public SemesterManager(ISemesterRepo semesterRepo, IMapper mapper, ICourseManager courseManager,
             ICourseLevelSemesterManager courseLevelSemesterManager, ILevelManager levelManager)
        {
            _semesterRepo = semesterRepo;
            _mapper = mapper;
            _courseManager = courseManager;
            _courseLevelSemesterManager = courseLevelSemesterManager;
            _levelManager = levelManager;
        }

        public List<SemesterReadVm> GetAllSemesters()
        {
            var semesters = _semesterRepo.GetAll();
            return _mapper.Map<List<SemesterReadVm>>(semesters);
        }

        public SemesterUpdateVm? GetSemesterById(int Id)
        {
            var semester = _semesterRepo.GetById(Id);

            if (semester == null)
                return null;

            return _mapper.Map<SemesterUpdateVm>(semester);
        }

        public SemesterDetailsVm? GetSemesterDetails(int SemesterId)
        {
            var semester = GetSemesterById(SemesterId);
            if (semester == null)
                return null;

            var AllCoursesInSemester = _courseLevelSemesterManager.GetCoursesToExclude(SemesterId);
            List<CourseReadVm> Allcourses = _courseManager.GetCourseListWithDepartmentName();
            List<CourseReadVm> coursesInSemester = (from c in AllCoursesInSemester
                                                    join cc in Allcourses
                                                    on c.CourseId equals cc.CourseId
                                                    select new CourseReadVm
                                                    {
                                                        CourseId = c.CourseId,
                                                        LevelId = c.LevelId,
                                                        CourseName = cc.CourseName,
                                                        DepartmentName = cc.DepartmentName,
                                                        Code = cc.Code,
                                                        CreditHours = cc.CreditHours
                                                    }).ToList();

            var res = coursesInSemester.Intersect(Allcourses).ToList();

            var result = (from c in res
                          join l in GetLevels()
                          on c.LevelId equals l.Id
                          select new CourseReadVm
                          {
                              CourseName = c.CourseName,
                              DepartmentName = c.DepartmentName,
                              LevelName = l.Name,
                              Code = c.Code,
                              CreditHours = c.CreditHours
                          }
                          ).ToList();


            SemesterDetailsVm SemesterDetails = _mapper.Map<SemesterDetailsVm>(semester);
            SemesterDetails.Courses = result.GroupBy(x => x.DepartmentName).ToList();
            SemesterDetails.LevelsName = GetLevels().Select(x => x.Name).ToList();

            return SemesterDetails;
        }

        public void Add(SemesterAddVm model)
        {
            if (model.Name != null)
            {
                _semesterRepo.Add(_mapper.Map<Semester>(model));
                _semesterRepo.SaveChanges();
            }
        }

        public void Update(SemesterUpdateVm model)
        {
            var dbSemester = _semesterRepo.GetById(model.Id);

            if (dbSemester == null)
                return;

            _mapper.Map(model, dbSemester);
            _semesterRepo.Update(dbSemester);
            _semesterRepo.SaveChanges();
        }

        public void Delete(int Id)
        {
            _courseLevelSemesterManager.DeleteCoursesInSemester(Id);
            _semesterRepo.Delete(Id);
            _semesterRepo.SaveChanges();
        }

        public bool CheckDates(DateOnly EndDate, DateOnly StartDate)
        {
            return EndDate > StartDate;
        }
        
        public void AddSelectedCourses(List<CourseLevelSemesterVm> courses)
        {
            _courseLevelSemesterManager.AddList(courses);
        }

        public List<CourseLevelSemesterVm> GetCoursesNotSelected(int SemesterId, ref LevelReadVm? CurrentLevel)
        {
            var Levels = GetLevels();
            var courses = _courseManager.GetCourseListWithDepartmentName();
            var CoursesToExclude = _courseLevelSemesterManager.GetCoursesToExclude(SemesterId);

            foreach(var level in Levels)
            {
                var check = CoursesToExclude.Any(x => x.LevelId == level.Id && x.SemesterId == SemesterId);
                if (!check)
                {
                    CurrentLevel = level;
                    break;
                }
            }

            return _mapper.Map<List<CourseLevelSemesterVm>>(courses).Except(CoursesToExclude).ToList();
        }

        public List<IGrouping<string, CourseLevelSemesterVm>> CoursesIntoGroups(List<CourseLevelSemesterVm> courses)
        {
            return courses.GroupBy(x => x.DepartmentName).ToList();
        }
        private List<LevelReadVm> GetLevels()
        {
            var levels = _levelManager.GetAllLevels();
            return levels;
        }

        

    }
}
