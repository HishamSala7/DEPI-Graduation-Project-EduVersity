using EduVersity.Models.AppContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using EduVersity.Managers.AccountManager;
using EduVersity.Repos.DepartmentRepo;
using EduVersity.Managers.DepartmentManager;
using EduVersity.ViewModels.AutoMapperProfile;
using EduVersity.Data.Models;
using EduVersity.Repos.StudentRepo;
using EduVersity.Managers.StudentManager;
using EduVersity.Repos.CourseRepo;
using EduVersity.Managers.CourseManager;
using EduVersity.Repos.SemesterRepo;
using EduVersity.Managers.SemesterManager;
using EduVersity.Repos.UserDepartmentRepo;
using EduVersity.Managers.UserDepartmentManager;
using EduVersity.Repos.PostRepo;
using EduVersity.Managers.PostManager;

namespace EduVersity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<WebAppContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("con"));
            });

            builder.Services.AddScoped<IAccountManager, AccountManager>();
            builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            builder.Services.AddScoped<IDepartmentManager, DepartmentManager>();
            builder.Services.AddScoped<IStudentRepo, StudentRepo>();
            builder.Services.AddScoped<IStudentManager, StudentManager>();
            builder.Services.AddScoped<ICourseRepo, CourseRepo>();
            builder.Services.AddScoped<ICourseManager, CourseManager>();
            builder.Services.AddScoped<ISemesterRepo, SemesterRepo>();
            builder.Services.AddScoped<ISemesterManager, SemesterManager>();
            builder.Services.AddScoped<IUserDepartmentRepo, UserDepartmentRepo>();
            builder.Services.AddScoped<IUserDepartmentManager, UserDepartmentManager>();
            builder.Services.AddScoped<IPostRepo, PostRepo>();
            builder.Services.AddScoped<IPostManager, PostManager>();
            


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                   .AddEntityFrameworkStores<WebAppContext>();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            builder.Services.Configure<IdentityOptions>(options=>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;

            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
