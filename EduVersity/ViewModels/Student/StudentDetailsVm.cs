namespace EduVersity.ViewModels.Student
{
    public class StudentDetailsVm
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string MidName { get; set; } 
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public float GPA { get; set; }
        public int SSN { get; set; }
        public char Gender { get; set; }
        public string Status { get; set; }
        public DateOnly EnrollDate { get; set; }
        public string LevelName { get; set; }
        public int SemesterId { get; set; }
        public int DepartmentId { get; set; }
        public int LevelId { get; set; }
    }
}
