using EduVersity.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace EduVersity.ViewModels.Department
{
    public class DepartmentUpdateVm
    {
        public int Id { get; set; }
        [Required]
        [UniqueDepartmentName]
        public string Name  { get; set; }
        


    }
}
