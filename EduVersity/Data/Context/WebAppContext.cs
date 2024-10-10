using EduVersity.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;

namespace EduVersity.Models.AppContext
{
    public class WebAppContext : IdentityDbContext<ApplicationUser>
    {
       public WebAppContext() : base() { }
        public WebAppContext(DbContextOptions optionsBuilder) : base(optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Data Seeding
            /*

             
             */

            foreach (var fk in modelBuilder.Model.GetEntityTypes()
                                          .SelectMany(e => e.GetForeignKeys()))
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.UserName).IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(e=>e.Email).IsRequired(); 

            modelBuilder.Entity<ApplicationUser>()
                .Property(e=>e.PasswordHash).IsRequired();

            modelBuilder.Entity<IdentityRole>()
                .Property(p => p.Name).IsRequired();

            modelBuilder.Entity<IdentityRole>()
                .HasIndex(i => i.Name).IsUnique();

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(x => x.UserName).IsUnique();

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(x => x.Email).IsUnique();

            modelBuilder.Entity<ApplicationUser>()
                .Property(x => x.Email).IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;Database=EduVersity;Trusted_Connection=true;TrustServerCertificate=true");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<UserDepartment> UserDepartments { get; set; }

    }
}

