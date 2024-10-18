using EduVersity.ViewModels.Course;

namespace EduVersity.ViewModels.Semester
{
    public class SemesterDetailsVm
    {
        public string Name { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public List<string> LevelsName { get; set; }
        public List<IGrouping<string,CourseReadVm>> Courses { get; set; }
    }
}
