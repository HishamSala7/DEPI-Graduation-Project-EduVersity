namespace EduVersity.Data.Models
{
    public class CourseLevelSemester
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int LevelId { get; set; }
        public int SemesterId { get; set; }
        public Course Course { get; set; }
        public Level Level { get; set; }
        public Semester Semester { get; set; }

    }
}
