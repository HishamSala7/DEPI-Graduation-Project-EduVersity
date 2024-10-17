using EduVersity.Data.Models;
using EduVersity.Models.AppContext;
using EduVersity.Repos.GenericRepo;
using Microsoft.EntityFrameworkCore;

namespace EduVersity.Repos.CourseLevelSemesterRepo
{
    public class CourseLevelSemesterRepo : GenericRepo<CourseLevelSemester> , ICourseLevelSemesterRepo
    {
        private readonly WebAppContext _context;

        public CourseLevelSemesterRepo(WebAppContext context) : base(context)
        {
            _context = context;
        }

        public void AddList(List<CourseLevelSemester> list)
        {
            _context.AddRange(list);
        }

        public void DeleteAllCoursesInSemester(int SemesterId)
        {
            var CoursesToDelete = _context.CourseLevelSemesters.Where(c=>c.SemesterId == SemesterId).ToList();
            _context.CourseLevelSemesters.RemoveRange(CoursesToDelete);
        }

        public List<CourseLevelSemester> GetCoursesToExclude(int SemesterId)
        {
            return _context.CourseLevelSemesters.Where(x => x.SemesterId == SemesterId).ToList();
        }

    }
}
