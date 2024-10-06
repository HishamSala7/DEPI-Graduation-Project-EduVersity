using EduVersity.ViewModels.Student;
using Microsoft.AspNetCore.Identity;

namespace EduVersity.Managers.StudentManager
{
    public interface IStudentManager
    {
        List<StudentReadVm> GetAllStudents();
        StudentDetailsVm? GetStudentDetails(int Id);
        Task<IdentityResult> AddStudent(StudentAddVm model);
        Task<IdentityResult> UpdateStudent(StudentUpdateVm model);
        Task<IdentityResult> DeleteStudent(int Id, string Username);
        StudentUpdateVm GetStudentUpdateVmFromDetails(StudentDetailsVm model);
        StudentDeleteVm GetStudentDeleteVmFromDetails(StudentDetailsVm model);
    }
}
