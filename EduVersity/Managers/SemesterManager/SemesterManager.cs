using AutoMapper;
using EduVersity.Data.Models;
using EduVersity.Managers.CourseManager;
using EduVersity.Managers.DepartmentManager;
using EduVersity.Managers.LevelManager;
using EduVersity.Repos.SemesterRepo;
using EduVersity.ViewModels.Level;
using EduVersity.ViewModels.Semester;

namespace EduVersity.Managers.SemesterManager
{
    public class SemesterManager : ISemesterManager
    {
        private readonly ISemesterRepo _semesterRepo;
        private readonly IMapper _mapper;
        private readonly ICourseManager _courseManager;
        private readonly IDepartmentManager _departmentManager;
        private readonly ILevelManager _levelManager;

        public SemesterManager(ISemesterRepo semesterRepo, IMapper mapper,
            ICourseManager courseManager, IDepartmentManager departmentManager, ILevelManager levelManager)
        {
            _semesterRepo = semesterRepo;
            _mapper = mapper;
            _courseManager = courseManager;
            _departmentManager = departmentManager;
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

        public void Add(SemesterAddVm model)
        {
            if (model.Name != null)
            {
                _semesterRepo.Add(_mapper.Map<Semester>(model));
                _semesterRepo.SaveChanges();
            }
        }

        public void Delete(int Id)
        {
            _semesterRepo.Delete(Id);
            _semesterRepo.SaveChanges();
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

        public bool CheckDates(DateOnly EndDate, DateOnly StartDate)
        {
            return EndDate > StartDate;
        }

        public IEnumerable<IGrouping<string,CourseDepartmentVm>> GetCoursesPerDepartment()
        {
            var depts = _departmentManager.GetAllDepartments();
            var courses = _courseManager.GetAllCourse();

            var res = (from dept in depts
                       join course in courses
                       on dept.Id equals course.DepartmentId
                       select new CourseDepartmentVm
                       {
                           CourseId = course.Id,
                           CourseName = course.Name,
                           DepartmentName = dept.Name
                       })
                      .GroupBy(x => x.DepartmentName);

            return res;  
        }

        public List<LevelReadVm> GetLevels()
        {
            var levels = _levelManager.GetAllLevels();
            return levels;
        }


    }
}
