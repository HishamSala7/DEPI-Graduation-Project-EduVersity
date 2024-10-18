using System.Net.NetworkInformation;

namespace EduVersity.Data.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int? SemesterOrder { get; set; }
        public ICollection<CourseLevelSemester> Semesters { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
