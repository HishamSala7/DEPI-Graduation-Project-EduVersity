using EduVersity.Models.AppContext;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace EduVersity.ValidationAttributes
{
    public class UniqueCourseCodeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            WebAppContext context = new WebAppContext();

            if (context.Courses.IsNullOrEmpty())
                return ValidationResult.Success;

            var Codes = context.Courses.Select(x => x.Code).ToList();

            string crsCode = value.ToString();

            if (Codes.Any(x => x == crsCode))
            {
                return new ValidationResult("Course Code is already Exists");
            }
            return ValidationResult.Success;

            //return base.IsValid(value, validationContext);
        }
    }
}
