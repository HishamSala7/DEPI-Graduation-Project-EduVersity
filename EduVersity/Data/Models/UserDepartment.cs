using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduVersity.Data.Models
{
    public class UserDepartment
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public int DepartmentId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Department Department { get; set; }

    }
}
