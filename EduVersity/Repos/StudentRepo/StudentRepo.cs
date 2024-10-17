using EduVersity.Data.Models;
using EduVersity.Models.AppContext;
using EduVersity.Repos.GenericRepo;

namespace EduVersity.Repos.StudentRepo
{
    public class StudentRepo : GenericRepo<Student> , IStudentRepo
    {
        private readonly WebAppContext _context;

        public StudentRepo(WebAppContext context) : base(context)
        {
            _context = context;
        }

        public List<Student> GetStudentsByLevelId(int LevelId)
        {
            return _context.Students.Where(x => x.LevelId == LevelId).ToList();
        }

    }
}
