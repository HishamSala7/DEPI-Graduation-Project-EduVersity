using Microsoft.AspNetCore.Components.Web;

namespace EduVersity.ViewModels.CourseLevelSemester
{
    public class CourseLevelSemesterVm
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public int LevelId { get; set; }
        public int SemesterId { get; set; }
        public string DepartmentName { get; set; }
        public bool IsSelected { get; set; }

        // Override Equals method
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (CourseLevelSemesterVm)obj;
            return CourseId == other.CourseId;
        }

        // Override GetHashCode method
        public override int GetHashCode()
        {
            return CourseId.GetHashCode();
        }

    }
}
