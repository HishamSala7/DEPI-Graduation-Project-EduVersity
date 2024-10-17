using EduVersity.ValidationAttributes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using EduVersity.ViewModels.User;
using EduVersity.Data.Models;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using EduVersity.Managers.DepartmentManager;
using EduVersity.ViewModels.Department;
using EduVersity.ViewModels.UserDepartment;
using EduVersity.Repos.UserDepartmentRepo;
using EduVersity.Managers.UserDepartmentManager;

namespace EduVersity.Managers.AccountManager
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly IUserDepartmentManager _userDepartmentManager;

        public AccountManager(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IUserDepartmentManager userDepartmentManager)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _userDepartmentManager = userDepartmentManager;
        }

        public List<UserReadVm> GetAll()
        {
            List<ApplicationUser> applicationUsers = _UserManager.Users.ToList();

            var Users = applicationUsers.Select(x => new UserReadVm()
            {
                UserId = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Username = x.UserName,
                Discriminator = x.Discriminator
            });

            return Users.ToList();
        }

        public UserDetailsVm GetUserDetails(string Username)
        {
            ApplicationUser? user = _UserManager.Users.FirstOrDefault(x => x.UserName == Username);

            UserDetailsVm userRead = new UserDetailsVm();

            userRead.Id = user.Id;
            userRead.FirstName = user.FirstName;
            userRead.LastName = user.LastName;
            userRead.MidName = user.MidName;
            userRead.Salary = user.Salary;
            userRead.HireDate = user.HireDate;
            userRead.Gender = user.Gender;
            userRead.SSN = user.SSN;
            userRead.BirthDate = user.BirthDate;
            userRead.Discriminator = user.Discriminator;
            userRead.Username = user.UserName;
            userRead.Email = user.Email;

            return userRead;
        }

        public async Task<string> GetUserId(string Username)
        {
            var user = await _UserManager.FindByNameAsync(Username);
            return user.Id;
        }

        public UserEditVm GetUserEditVm(string Username)
        {
            ApplicationUser? user = _UserManager.Users.FirstOrDefault(x => x.UserName == Username);

            UserEditVm userEdit = new UserEditVm();

            userEdit.FirstName = user.FirstName;
            userEdit.LastName = user.LastName;
            userEdit.MidName = user.MidName;
            userEdit.Salary = user.Salary;
            userEdit.BirthDate = user.BirthDate;
            userEdit.Username = user.UserName;

            return userEdit;
        }

        public async Task<IdentityResult> Add(UserAddVm userVm)
        {
            ApplicationUser applicationUser = MapUserAddVmToDbUser(userVm);

            IdentityResult result = await _UserManager.CreateAsync(applicationUser, applicationUser.PasswordHash);

            if (result.Succeeded)
            {
                await _UserManager.AddToRoleAsync(applicationUser, IdentifyRole(userVm.Discriminator));
            }

            return result;

        }

        public async Task<IdentityResult> Update(UserEditVm userEdit)
        {
            ApplicationUser? user = _UserManager.Users.FirstOrDefault(x => x.UserName == userEdit.Username);

            if (user == null)
                return null;

            user.FirstName = userEdit.FirstName;
            user.MidName = userEdit.MidName;
            user.LastName = userEdit.LastName;
            user.Salary = userEdit.Salary;
            user.BirthDate = userEdit.BirthDate;

            return await _UserManager.UpdateAsync(user);

        }

        public async Task<IdentityResult> Delete(string Username)
        {
            ApplicationUser? user = _UserManager.Users.FirstOrDefault(x => x.UserName == Username);

            return await _UserManager.DeleteAsync(user);
        }

        public async Task<bool> Login(UserLoginVm model)
        {
            var user = await _UserManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                bool IsCorrect = await _UserManager.CheckPasswordAsync(user, model.Password);

                if (IsCorrect)
                {
                    await _SignInManager.SignInAsync(user, model.RemeberMe);
                    return true;
                }

            }
            return false;
        }

        public async void Logout()
        {
            await _SignInManager.SignOutAsync();
        }

        public List<UserReadVm> GetAllUsersExceptStudents()
        {
            var users = GetAll();
            var Students = users.Where(x => x.Discriminator == 'S');
            return users.Except(Students).ToList();
        }

        public async Task<string> GetDepartmentName(string Username)
        {
            var UserId = await GetUserId(Username);
            return  _userDepartmentManager.GetProfessorDepartmentName(UserId);
        }


        private ApplicationUser MapUserAddVmToDbUser(UserAddVm userVm)
        {
            ApplicationUser applicationUser = new ApplicationUser();

            applicationUser.FirstName = userVm.FirstName;
            applicationUser.MidName = userVm.MidName;
            applicationUser.LastName = userVm.LastName;
            applicationUser.Salary = userVm.Salary;
            applicationUser.HireDate = userVm.HireDate;
            applicationUser.Gender = userVm.Gender;
            applicationUser.SSN = userVm.SSN;
            applicationUser.BirthDate = userVm.BirthDate;
            applicationUser.Discriminator = userVm.Discriminator;
            applicationUser.UserName = userVm.Username;
            applicationUser.PasswordHash = applicationUser.UserName;

            return applicationUser;
        }

        private string IdentifyRole(char Disc)
        {
            switch (Disc)
            {
                case 'A':
                    return "Admin";
                case 'E':
                    return "Employee";
                case 'P':
                    return "Professor";
                case 'S':
                    return "Student";
            }
            return string.Empty;
        }

        //public async Task<string> GetFullName(string UserId)
        //{
        //    var user = await _UserManager.FindByIdAsync(UserId);
        //    return user.FirstName+ " " +user.LastName;  
        //}

    }
}
