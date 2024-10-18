using EduVersity.Models.AppContext;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace EduVersity.ValidationAttributes
{
    public class UniqueEndDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            WebAppContext context = new WebAppContext();

            if (context.Semesters.IsNullOrEmpty())
                return ValidationResult.Success;

            var EndDates = context.Semesters.Select(x => x.EndDate).ToList();

            DateOnly eDate = (DateOnly)value;

            if (EndDates.Any(x => x == eDate))
            {
                return new ValidationResult("End Date is already Exists");
            }
            return ValidationResult.Success;

            //return base.IsValid(value, validationContext);
        }
    }
}
