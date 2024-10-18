using EduVersity.Data.Models;
using EduVersity.Repos.GenericRepo;

namespace EduVersity.Repos.MaterialRepo
{
    public interface IMaterialRepo : IGenericRepo<Material>
    {
        List<Material> GetCourseMaterials(int CourseId);
    }

}
