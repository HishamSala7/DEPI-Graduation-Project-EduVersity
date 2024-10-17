using EduVersity.Data.Models;
using EduVersity.Models.AppContext;
using EduVersity.Repos.GenericRepo;

namespace EduVersity.Repos.LevelRepo
{
    public class LevelRepo : GenericRepo<Level> , ILevelRepo
    {
        private readonly WebAppContext _context;

        public LevelRepo(WebAppContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
