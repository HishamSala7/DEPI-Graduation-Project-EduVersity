using EduVersity.Models.AppContext;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace EduVersity.ValidationAttributes
{
    public class UniqueCourseNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            WebAppContext context = new WebAppContext();

            if (context.Courses.IsNullOrEmpty())
                return ValidationResult.Success;

            var Names = context.Courses.Select(x => x.Name).ToList();

            string crsName = value.ToString();

            if (Names.Any(x => x == crsName))
            {
                return new ValidationResult("Course Name is already Exists");
            }
            return ValidationResult.Success;

            //return base.IsValid(value, validationContext);
        }
    }
}
