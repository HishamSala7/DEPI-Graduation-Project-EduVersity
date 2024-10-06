using EduVersity.ViewModels.Department;
using EduVersity.ViewModels.User;
using EduVersity.ViewModels.UserDepartment;

namespace EduVersity.Managers.UserDepartmentManager
{
    public interface IUserDepartmentManager
    {
        bool AddProfessorToDepartment(UserAddToDepartmentVm model);
        List<DepartmentReadVm> GetDepartments();
        string GetProfessorDepartmentName(string UserId);
    }
}
