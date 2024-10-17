using System.ComponentModel.DataAnnotations;

namespace EduVersity.ViewModels.User
{
    public class UserLoginVm
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]  
        public string Password { get; set; }
        public bool RemeberMe {  get; set; }
    }
}
