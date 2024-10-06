using EduVersity.Data.Models;
using EduVersity.ViewModels.Course;
using EduVersity.ViewModels.Department;

namespace EduVersity.Managers.CourseManager
{
    public interface ICourseManager
    {
        List<CourseEditVm> GetAllCourse();
        CourseEditVm? GetCourseById(int id);
        void Add(CourseAddVm model);
        void Update(CourseEditVm model);
        void Delete(int Id);
        List<CourseReadVm> GetCoursesWithDepartmentName();
        List<DepartmentReadVm> GetDepartments();

    }
}
