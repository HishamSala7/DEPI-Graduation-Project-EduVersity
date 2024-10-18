using AutoMapper;
using EduVersity.Data.Models;
using EduVersity.Managers.CourseLevelSemesterManager;
using EduVersity.Managers.DepartmentManager;
using EduVersity.Repos.CourseLevelSemesterRepo;
using EduVersity.Repos.CourseRepo;
using EduVersity.ViewModels.Course;
using EduVersity.ViewModels.Department;
using System.Collections.Immutable;

namespace EduVersity.Managers.CourseManager
{
    public class CourseManager : ICourseManager
    {
        private readonly ICourseRepo _courseRepo;
        private readonly IMapper _mapper;
        private readonly IDepartmentManager _departmentManager;
        private readonly ICourseLevelSemesterManager _courseLevelSemesterManager;

        public CourseManager(ICourseRepo courseRepo, IMapper mapper, IDepartmentManager departmentManager,
            ICourseLevelSemesterManager courseLevelSemesterManager)
        {
            _courseRepo = courseRepo;
            _mapper = mapper;
            _departmentManager = departmentManager;
            _courseLevelSemesterManager = courseLevelSemesterManager;
        }

        public List<CourseEditVm> GetAllCourses()
        {
            var courses = _courseRepo.GetAll();
            return _mapper.Map<List<CourseEditVm>>(courses);
        }

        public CourseEditVm? GetCourseById(int id)
        {
            var course = _courseRepo.GetById(id);
            
            if (course == null)
                return null;

            return _mapper.Map<CourseEditVm>(course);
        }


        public void Add(CourseAddVm model)
        {
            _courseRepo.Add(_mapper.Map<Course>(model));
            _courseRepo.SaveChanges();
        }

        public void Delete(int Id)
        {
            _courseRepo.Delete(Id);
            _courseRepo.SaveChanges();
        }


        public void Update(CourseEditVm model)
        {
            var course = _courseRepo.GetById(model.Id);

            if (course == null)
                return ;

            _mapper.Map(model, course);
            _courseRepo.Update(course);
            _courseRepo.SaveChanges();
        }

        public List<DepartmentReadVm> GetDepartments()
        {
            return _departmentManager.GetAllDepartments();
        }

        public List<CourseReadVm>  GetCourseListWithDepartmentName()
        {
            return _courseRepo.GetCourseListWithDepartmentName();
        }

        public List<CourseLevelSemesterDepartmentVm> GetCoursesInDepartmentInSemester(int DepartmentId,
            int SemesterId, int LevelId)
        {
            var courses = _courseLevelSemesterManager.GetCoursesByLevelAndSemester(LevelId, SemesterId);

            var coursesInDept = (from crs in GetAllCourses()
                                join item in courses
                                on crs.Id equals item.CourseId
                                where crs.DepartmentId == DepartmentId
                                select new CourseLevelSemesterDepartmentVm
                                {
                                    CourseId = item.CourseId,
                                    CourseName = crs.Name,
                                    DepartmentId = (int)crs.DepartmentId
                                }).ToList();

            return coursesInDept;

        }



    }
}
