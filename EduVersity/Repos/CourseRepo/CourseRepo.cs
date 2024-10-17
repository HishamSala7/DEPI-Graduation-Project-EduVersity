using EduVersity.Data.Models;
using EduVersity.Models.AppContext;
using EduVersity.Repos.GenericRepo;
using EduVersity.ViewModels.Course;

namespace EduVersity.Repos.CourseRepo
{
    public class CourseRepo : GenericRepo<Course>, ICourseRepo
    {
        private readonly WebAppContext _context;

        public CourseRepo(WebAppContext context) : base(context)
        {
            _context = context;
        }

        public List<CourseReadVm> GetCourseListWithDepartmentName()
        {
            return  (from course in _context.Courses
                     join dept in _context.Departments
                     on course.DepartmentId equals dept.Id
                     select new CourseReadVm
                     {
                         CourseId = course.Id,
                         Code = course.Code,
                         CreditHours = course.CreditHours,
                         CourseName = course.Name,
                         DepartmentName = dept.Name
                     }).ToList();
        }

        
    }
}
