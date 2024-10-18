using EduVersity.Data.Models;
using EduVersity.Models.AppContext;
using EduVersity.Repos.GenericRepo;

namespace EduVersity.Repos.MaterialRepo
{
    public class MaterialRepo : GenericRepo<Material>, IMaterialRepo
    {
        private readonly WebAppContext _context;

        public MaterialRepo(WebAppContext context):base(context) 
        {
            _context = context;
        }

        public List<Material> GetCourseMaterials(int CourseId)
        {
            return _context.Materials.Where(x=>x.CourseId == CourseId).ToList();    
        }
    }
}
