using EduVersity.ViewModels.CourseLevelSemester;

namespace EduVersity.ViewModels.Course
{
    public class CourseReadVm  
    {
        public int CourseId { get; set; }  
        public string Code { get; set; }
        public int CreditHours { get; set; }
        public string CourseName { get; set; }
        public string DepartmentName { get; set; }
        public string LevelName { get; set; }
        public int LevelId { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            CourseReadVm other = (CourseReadVm)obj;
            return this.CourseId == other.CourseId;
        }

        public override int GetHashCode()
        {
            return CourseId.GetHashCode();
        }
    }
}
