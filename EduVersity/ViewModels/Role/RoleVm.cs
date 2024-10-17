
using System.ComponentModel.DataAnnotations;

namespace EduVersity.ViewModels.Role
{
    public class RoleVm
    {
        [Required]
        public string RoleName { get; set; }
    }
}
