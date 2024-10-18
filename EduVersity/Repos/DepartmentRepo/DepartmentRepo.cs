using EduVersity.Data.Models;
using EduVersity.Models.AppContext;
using EduVersity.Repos.GenericRepo;

namespace EduVersity.Repos.DepartmentRepo
{
    public class DepartmentRepo : GenericRepo<Department>, IDepartmentRepo
    {
        private readonly WebAppContext _context;

        public DepartmentRepo(WebAppContext context) :base(context)
        {
            _context = context;
        }


    }
}
