using AutoMapper;
using EduVersity.Data.Models;
using EduVersity.Managers.DepartmentManager;
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

        public CourseManager(ICourseRepo courseRepo, IMapper mapper, IDepartmentManager departmentManager)
        {
            _courseRepo = courseRepo;
            _mapper = mapper;
            _departmentManager = departmentManager;
        }

        public List<CourseEditVm> GetAllCourse()
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

        public List<CourseReadVm> GetCoursesWithDepartmentName()
        {
            var courses = GetAllCourse();
            var depts = GetDepartments();

            List<CourseReadVm> res = (from course in courses
                                                join dept in depts on
                                                course.DepartmentId equals dept.Id
                                                select new CourseReadVm 
                                                { 
                                                    CourseId = course.Id,
                                                    Code = course.Code,
                                                    CreditHours = course.CreditHours,
                                                    CourseName = course.Name,
                                                    DepartmentName = dept.Name
                                                }).ToList();
            return res;
        }

        public List<DepartmentReadVm> GetDepartments()
        {
            return _departmentManager.GetAllDepartments();
        }
    }
}
