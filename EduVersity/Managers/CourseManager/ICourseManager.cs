using EduVersity.Data.Models;
using EduVersity.ViewModels.Course;
using EduVersity.ViewModels.Department;

namespace EduVersity.Managers.CourseManager
{
    public interface ICourseManager
    {
        List<CourseEditVm> GetAllCourses();
        CourseEditVm? GetCourseById(int id);
        void Add(CourseAddVm model);
        void Update(CourseEditVm model);
        void Delete(int Id);
        List<DepartmentReadVm> GetDepartments();
        List<CourseReadVm> GetCourseListWithDepartmentName();
        List<CourseLevelSemesterDepartmentVm> GetCoursesInDepartmentInSemester(int DepartmentId, int SemesterId, int LevelId);
        
    }
}
