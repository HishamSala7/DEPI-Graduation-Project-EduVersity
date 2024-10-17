using EduVersity.Data.Models;
using EduVersity.Repos.GenericRepo;

namespace EduVersity.Repos.StudentRepo
{
    public interface IStudentRepo : IGenericRepo<Student>
    {
        List<Student> GetStudentsByLevelId(int LevelId);
    }
}
