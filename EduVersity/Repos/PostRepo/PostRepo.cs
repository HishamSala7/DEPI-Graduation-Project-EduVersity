using EduVersity.Data.Models;
using EduVersity.Models.AppContext;
using EduVersity.Repos.GenericRepo;

namespace EduVersity.Repos.PostRepo
{
    public class PostRepo : GenericRepo<Post> , IPostRepo
    {
        private readonly WebAppContext _context;

        public PostRepo(WebAppContext context) : base(context)
        {
            _context = context;
        }
    }
}
