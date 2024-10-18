using System.ComponentModel.DataAnnotations;

namespace EduVersity.ViewModels.User
{
    public class UserEditVm
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MidName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public float? Salary { get; set; }
        public string Username { get; set; }
        public DateOnly BirthDate { get; set; }

    }
}
