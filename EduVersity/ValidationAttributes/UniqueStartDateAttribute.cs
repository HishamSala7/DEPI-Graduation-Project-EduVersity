using EduVersity.Models.AppContext;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace EduVersity.ValidationAttributes
{
    public class UniqueStartDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            WebAppContext context = new WebAppContext();

            if (context.Semesters.IsNullOrEmpty())
                return ValidationResult.Success;

            var StartDates = context.Semesters.Select(x => x.StartDate).ToList();

            DateOnly sDate = (DateOnly)value;

            if (StartDates.Any(x => x == sDate))
            {
                return new ValidationResult("Start Date is already Exists");
            }
            return ValidationResult.Success;

            //return base.IsValid(value, validationContext);
        }
    }
}
