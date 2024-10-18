using EduVersity.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace EduVersity.ViewModels.Level
{
    public class LevelAddVm
    {
        [UniqueLevelName]
        [MinLength(3,ErrorMessage ="The Name Length must be at least 3 characters")]
        public string Name { get; set; }
        [UniqueLevelOrder]
        [Range(1,7)]
        public int LevelOrder { get; set; }
    }
}
