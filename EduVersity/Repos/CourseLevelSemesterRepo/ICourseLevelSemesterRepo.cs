using EduVersity.Repos.GenericRepo;
using EduVersity.Data.Models;

namespace EduVersity.Repos.CourseLevelSemesterRepo
{
    public interface ICourseLevelSemesterRepo : IGenericRepo<CourseLevelSemester>
    {
        void DeleteAllCoursesInSemester(int SemesterId);
        void AddList(List<CourseLevelSemester> list);
        List<CourseLevelSemester> GetCoursesToExclude(int SemesterId);
    }
}
