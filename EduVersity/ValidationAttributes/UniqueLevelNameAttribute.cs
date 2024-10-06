using EduVersity.Models.AppContext;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace EduVersity.ValidationAttributes
{
    public class UniqueLevelNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            WebAppContext context = new WebAppContext();

            if (context.Levels.IsNullOrEmpty())
                return ValidationResult.Success;

            var Names = context.Levels.Select(x => x.Name).ToList();

            string LevelName = value.ToString();

            if (Names.Any(x => x == LevelName))
            {
                return new ValidationResult("Level Name is already Exists");
            }
            return ValidationResult.Success;

            //return base.IsValid(value, validationContext);
        }
    }
}
