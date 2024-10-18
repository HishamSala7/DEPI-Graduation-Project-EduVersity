using EduVersity.Data.Models;
using EduVersity.Repos.GenericRepo;

namespace EduVersity.Repos.EnrollmentRepo
{
    public interface IEnrollmentRepo : IGenericRepo<Enrollment>
    {
        void AddEnrollmentList(List<Enrollment> enrollments);
        List<Enrollment> GetEnrollments(int StudentId);
        void AssignProfessorsToCourses(List<Enrollment> enrollments);
        void UpdateStudentsGradesList(List<Enrollment> enrollments);

    }
}
