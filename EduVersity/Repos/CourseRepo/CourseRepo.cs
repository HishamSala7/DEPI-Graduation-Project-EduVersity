using EduVersity.Data.Models;
using EduVersity.Models.AppContext;
using EduVersity.Repos.GenericRepo;

namespace EduVersity.Repos.CourseRepo
{
    public class CourseRepo : GenericRepo<Course>, ICourseRepo
    {
        private readonly WebAppContext _context;

        public CourseRepo(WebAppContext context) : base(context)
        {
            _context = context;
        }
    }
}
