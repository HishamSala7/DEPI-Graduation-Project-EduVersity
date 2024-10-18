
using EduVersity.ViewModels.CourseLevelSemester;

namespace EduVersity.Managers.CourseLevelSemesterManager
{
    public interface ICourseLevelSemesterManager
    {
        void AddList(List<CourseLevelSemesterVm> model);
        List<CourseLevelSemesterVm> GetAll();
        void DeleteCoursesInSemester(int SemesterId);
        List<CourseLevelSemesterVm> GetCoursesToExclude(int SemesterId);
        List<CourseLevelSemesterVm> GetCoursesByLevelAndSemester(int LevelId, int SemesterId);

    }
}
