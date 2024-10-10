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
        //bool CheckDates(DateOnly EndDate, DateOnly StartDate);
        //List<IGrouping<string, CourseSelectionVm>> GetCoursesPerDepartment();
       // List<LevelReadVm> GetLevels();
    }
}
