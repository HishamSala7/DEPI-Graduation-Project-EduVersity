using EduVersity.Models;
using EduVersity.ViewModels.Department;
using EduVersity.ViewModels.User;
using EduVersity.ViewModels.UserDepartment;
using Microsoft.AspNetCore.Identity;

namespace EduVersity.Managers.AccountManager
{
    public interface IAccountManager
    {
        List<UserReadVm> GetAll();
        UserDetailsVm GetUserDetails(string Username);
        Task<string> GetUserId(string Username);
        UserEditVm GetUserEditVm(string Username);
        Task<IdentityResult> Add(UserAddVm userVm);
        Task<IdentityResult> Update(UserEditVm userEdit);
        Task<IdentityResult> Delete(string Username);
        Task<bool> Login(UserLoginVm model);
        void Logout();
        List<UserReadVm> GetAllUsersExceptStudents();
        Task<string> GetDepartmentName(string Username);
    }
}
