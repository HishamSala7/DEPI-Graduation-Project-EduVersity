using EduVersity.Data.Models;
using EduVersity.Repos.GenericRepo;
using EduVersity.ViewModels.Course;

namespace EduVersity.Repos.CourseRepo
{
    public interface ICourseRepo : IGenericRepo<Course>
    {
        List<CourseReadVm> GetCourseListWithDepartmentName();
    }
}
