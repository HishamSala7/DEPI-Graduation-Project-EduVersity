using System.ComponentModel.DataAnnotations;

namespace EduVersity.Data.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public DateOnly BirthDate { get; set; }
        public float? GPA { get; set; }
        public int SSN { get; set; }
        public char Gender { get; set; }
        public string Status { get; set; }
        public DateOnly EnrollDate { get; set; }
        public int? DepartmentId { get; set; }
        public int? LevelId { get; set; }
        public int? SemesterId {  get; set; }
        public string? ApplicationUserId { get; set; }
        public Department Department { get; set; }
        public Level Level { get; set; }
        public Semester Semester { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<StudentSubmissions> StudentSubmissions { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


    }
}
