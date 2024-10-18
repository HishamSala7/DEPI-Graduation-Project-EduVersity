using EduVersity.ViewModels.CourseLevelSemester;
using EduVersity.ViewModels.Level;
using EduVersity.ViewModels.Semester;

namespace EduVersity.Managers.SemesterManager
{
    public interface ISemesterManager
    {
        List<SemesterReadVm> GetAllSemesters();
        SemesterUpdateVm? GetSemesterById(int Id);
        SemesterDetailsVm? GetSemesterDetails(int SemesterId);
        void Add(SemesterAddVm model);
        void Update(SemesterUpdateVm model);
        void Delete(int id);
        bool CheckDates(DateOnly EndDate, DateOnly StartDate);
        List<IGrouping<string, CourseLevelSemesterVm>> CoursesIntoGroups(List<CourseLevelSemesterVm> courses);
        void AddSelectedCourses(List<CourseLevelSemesterVm> courses);
        List< CourseLevelSemesterVm> GetCoursesNotSelected(int SemesterId, ref LevelReadVm? CurrentLevel);

    }
}
