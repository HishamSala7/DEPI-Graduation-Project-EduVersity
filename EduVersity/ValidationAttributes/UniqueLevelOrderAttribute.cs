using EduVersity.Models.AppContext;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace EduVersity.ValidationAttributes
{
    public class UniqueLevelOrderAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            WebAppContext context = new WebAppContext();

            if (context.Courses.IsNullOrEmpty())
                return ValidationResult.Success;

            var LevelsOrder = context.Levels.Select(x => x.LevelOrder).ToList();

            int LvlOrder = (int)value;

            if (LevelsOrder.Any(x => x == LvlOrder))
            {
                return new ValidationResult("Level Order is already Exists");
            }
            return ValidationResult.Success;

            //return base.IsValid(value, validationContext);
        }
    }
}
