using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduVersity.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public float? Salary { get; set; }
        public DateOnly? HireDate { get; set; }
        public char Gender { get; set; }
        public int SSN { get; set; }
        public DateOnly BirthDate { get; set; }
        public char Discriminator { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int UserNumber { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<UserDepartment> userDepartments { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public Student? Student { get; set; }

    }
}
