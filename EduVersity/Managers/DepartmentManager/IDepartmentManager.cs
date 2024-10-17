using EduVersity.ViewModels.Department;

namespace EduVersity.Managers.DepartmentManager
{
    public interface IDepartmentManager
    {
        List<DepartmentReadVm> GetAllDepartments();
        DepartmentReadVm? GetDepartmentById(int id);
        void AddDepartment(DepartmentAddVm model);
        void UpdateDepartment(DepartmentUpdateVm model);
        void DeleteDepartment(int Id);
    }
}
