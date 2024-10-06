using EduVersity.ViewModels.Level;
using EduVersity.ViewModels.Semester;

namespace EduVersity.Managers.SemesterManager
{
    public interface ISemesterManager
    {
        List<SemesterReadVm> GetAllSemesters();
        SemesterUpdateVm? GetSemesterById(int Id);
        void Add(SemesterAddVm model);
        void Update(SemesterUpdateVm model);
        void Delete(int id);
        bool CheckDates(DateOnly EndDate, DateOnly StartDate);
        IEnumerable<IGrouping<string, CourseDepartmentVm>> GetCoursesPerDepartment();
        List<LevelReadVm> GetLevels();
    }
}
