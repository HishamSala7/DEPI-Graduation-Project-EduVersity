using EduVersity.Data.Models;
using EduVersity.ViewModels.Department;
using EduVersity.ViewModels.Student;
using Microsoft.AspNetCore.Identity;

namespace EduVersity.Managers.StudentManager
{
    public interface IStudentManager
    {
        List<StudentReadVm> GetAllStudents();
        StudentDetailsVm? GetStudentDetails(int Id);
        StudentDetailsVm? GetStudentDetails(string Username);
        Task<IdentityResult> AddStudent(StudentAddVm model);
        Task<IdentityResult> UpdateStudent(StudentUpdateVm model);
        Task<IdentityResult> DeleteStudent(int Id, string Username);
        StudentUpdateVm GetStudentUpdateVmFromDetails(StudentDetailsVm model);
        StudentDeleteVm GetStudentDeleteVmFromDetails(StudentDetailsVm model);
        string GetDepartmentName(int StudentId);
        List<DepartmentReadVm> GetDepartments();
        void AssignToDepartment(int StudentId, AssignToDepartmentVm model);
        void DeleteDepartment(int StudentId);
        StudentSemesterAddVm GetStudentSemesterAddVm();
        void AddStudentsInSemester(StudentSemesterAddVm model);

        //List<Student> GetStudentsByLevelId(int LevelId);

    }
}
