using EduVersity.ViewModels.UserDepartment;
using System.ComponentModel.DataAnnotations;

namespace EduVersity.ViewModels.Enrollment
{
    public class EnrollCourseDepartmentVm
    {
        [Required]
        public int CourseId { get; set; }   
        public string? CourseName { get; set; }
        public string? DepartmentName { get; set; }
        [Required]
        public string ProfessorId { get; set; }
        
    }
}
