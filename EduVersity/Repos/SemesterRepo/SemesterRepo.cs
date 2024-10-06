using EduVersity.Data.Models;
using EduVersity.Models.AppContext;
using EduVersity.Repos.GenericRepo;

namespace EduVersity.Repos.SemesterRepo
{
    public class SemesterRepo : GenericRepo<Semester>, ISemesterRepo
    {
        private readonly WebAppContext _context;

        public SemesterRepo(WebAppContext context):base(context)
        {
            _context = context;
        }
    }
}
