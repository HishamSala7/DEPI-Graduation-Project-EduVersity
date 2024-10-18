namespace EduVersity.Data.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int LectureId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public bool IsAttend { get; set; }
        public Lecture Lecture { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }

    }
}
