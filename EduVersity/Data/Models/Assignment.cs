namespace EduVersity.Data.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateOnly DueDate { get; set; }
        public float FullMark { get; set; }
        public string FileLink { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<StudentSubmissions> StudentSubmissions { get; set; }


    }

}
