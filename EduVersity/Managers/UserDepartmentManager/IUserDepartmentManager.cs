using EduVersity.ViewModels.Department;
using EduVersity.ViewModels.User;
using EduVersity.ViewModels.UserDepartment;

namespace EduVersity.Managers.UserDepartmentManager
{
    public interface IUserDepartmentManager
    {
        List<UserDepartmentReadVm> GetAll();
        bool AddProfessorToDepartment(UserDepartmentAddVm model);
        List<DepartmentReadVm> GetDepartments();
        string GetProfessorDepartmentName(string UserId);
        UserDepartmentUpdateVm GetUserDepartmentById(string Id);
        void Update(UserDepartmentUpdateVm model, string UserId);
        void Delete(string UserId);

        //List<UserDepartmentVm> GetPrfessorsNameWithDepartmentName();

    }
}
