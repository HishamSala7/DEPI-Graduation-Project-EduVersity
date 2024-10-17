using EduVersity.ValidationAttributes;

namespace EduVersity.ViewModels.Course
{
    public class CourseAddVm
    {
        public int Id { get; set; }
        [UniqueCourseName]     
        public string Name { get; set; }
        [UniqueCourseCode]
        public string Code { get; set; }
        public int CreditHours { get; set; }
        public int? DepartmentId { get; set; }
    }
}
