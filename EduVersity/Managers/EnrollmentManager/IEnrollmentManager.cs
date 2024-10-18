using EduVersity.ViewModels.Course;
using EduVersity.ViewModels.Enrollment;
using EduVersity.ViewModels.Student;
using EduVersity.ViewModels.User;
using EduVersity.ViewModels.UserDepartment;

namespace EduVersity.Managers.EnrollmentManager
{
    public interface IEnrollmentManager
    {
        StudentDetailsVm GetLoggedInStudent(string Username);
        List<CourseLevelSemesterDepartmentVm> GetSemesterCourses(int SemesterId, int DepartmentId, int LevelId);
        void AddEnrolledCourses(string Username, List<CourseLevelSemesterDepartmentVm> model);
        List<EnrollmentDetailsVm> GetStudentEnrollmentsDetails(int StudentId);
        List<CourseLevelSemesterDepartmentVm> GetUnEnrolledCourses(string Username, int SemesterId, int DepartmentId, int LevelId);
        List<EnrollCourseDepartmentVm> GetEnrolledCourses();
        List<ProfessorDepartmentVm> GetAllProfessors();
        void AssignTeachersToCourses(List<EnrollCourseDepartmentVm> model);
        List<ProfessorCourseReadVm> GetCoursesTeacher();
        ProfessorLoggedInVm GetLoggedInProfessor(string Username);
        List<ProfessorCourseReadVm> GetProfessorEnrollmetsDetails(string ProfessorId);
        List<StudentGradeEditVm> GetStudentsDataInCourse(int CourseId);
        void UpdateStudentsGrades(int CourseId, List<StudentGradeEditVm> model);




    }
}
