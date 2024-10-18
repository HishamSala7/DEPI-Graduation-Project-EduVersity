using EduVersity.Models.AppContext;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace EduVersity.ValidationAttributes

{
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            WebAppContext context = new WebAppContext();

            if (context.Users.IsNullOrEmpty())
                return ValidationResult.Success;
                 
            var UserNames = context.Users.Select(x => x.UserName).ToList();

            string username = value.ToString();

            if(UserNames.Any(x=> x == username))
            {
                return new ValidationResult("Username is already taken");
            }
            return ValidationResult.Success;

            //return base.IsValid(value, validationContext);
        }
    }

}
