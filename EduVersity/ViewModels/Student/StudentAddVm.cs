using EduVersity.ValidationAttributes;

namespace EduVersity.ViewModels.Student
{
    public class StudentAddVm
    {
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        [UniqueUsername]
        public string Username { get; set; }
        public DateOnly BirthDate { get; set; }
        public int SSN { get; set; }
        public char Gender { get; set; }
        public string Status { get; set; }
        public DateOnly EnrollDate { get; set; }
    }
}
