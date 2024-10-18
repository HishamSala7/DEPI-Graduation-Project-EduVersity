using EduVersity.ValidationAttributes;

namespace EduVersity.ViewModels.User
{
    public class UserDetailsVm
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public float? Salary { get; set; }
        public string Username { get; set; }
        public DateOnly? HireDate { get; set; }
        public char Gender { get; set; }
        public int SSN { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Email { get; set; }
        public char Discriminator { get; set; }
    }
}
