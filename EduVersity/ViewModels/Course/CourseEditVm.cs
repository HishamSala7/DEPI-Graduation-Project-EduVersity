using EduVersity.Data.Models;
using EduVersity.ValidationAttributes;

namespace EduVersity.ViewModels.Course
{
    public class CourseEditVm
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Code { get; set; }
        public int CreditHours { get; set; }
        public int? DepartmentId { get; set; }
    }
}
