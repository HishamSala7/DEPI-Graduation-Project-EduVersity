using EduVersity.Models.AppContext;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace EduVersity.ValidationAttributes
{
    public class UniqueDepartmentNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            WebAppContext context = new WebAppContext();

            if (context.Departments.IsNullOrEmpty())
                return ValidationResult.Success;

            var depts = context.Departments.Select(x => x.Name).ToList();

            string deptName = value.ToString();

            if (depts.Any(x => x == deptName))
            {
                return new ValidationResult("Department Name is already exists");
            }
            return ValidationResult.Success;

            //return base.IsValid(value, validationContext);
        }
    }
}
