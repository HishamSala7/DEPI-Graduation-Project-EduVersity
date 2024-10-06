using EduVersity.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace EduVersity.ViewModels.Department
{
    public class DepartmentAddVm
    {
        [Required]
        [UniqueDepartmentName]
        public string Name { get; set; }
    }

}
