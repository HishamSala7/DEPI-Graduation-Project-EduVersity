namespace EduVersity.ViewModels.Student
{
    public class StudentUpdateVm
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Status { get; set; }
        public char Gender { get; set; }
    }
}
