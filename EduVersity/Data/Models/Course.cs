using System.ComponentModel.DataAnnotations;

namespace EduVersity.Data.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CreditHours { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Material> Materials { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<CourseLevelSemester> Semesters { get; set; }

    }
}
