using EduVersity.Data.Models;
using EduVersity.Models.AppContext;
using EduVersity.Repos.GenericRepo;

namespace EduVersity.Repos.EnrollmentRepo
{
    public class EnrollmentRepo : GenericRepo<Enrollment>,IEnrollmentRepo
    {
        private readonly WebAppContext _context;

        public EnrollmentRepo(WebAppContext context) : base(context)
        {
            _context = context;
        }

        public void AddEnrollmentList(List<Enrollment> enrollments )
        {
            _context.Enrollments.AddRange(enrollments);
        }

        public List<Enrollment> GetEnrollments(int StudentId)
        {
            return _context.Enrollments.Where(x=>x.StudentId == StudentId).ToList();
        }

        public void AssignProfessorsToCourses(List<Enrollment> enrollments)
        {

        }

        public void UpdateStudentsGradesList(List<Enrollment> enrollments)
        {

        }

    }
}
