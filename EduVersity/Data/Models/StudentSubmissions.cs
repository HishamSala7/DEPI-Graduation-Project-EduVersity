namespace EduVersity.Data.Models
{
    public class StudentSubmissions
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int AssignmentId { get; set; }
        public float? Mark { get; set; }
        public string FileLink { get; set; }
        public Student Student { get; set; }
        public Assignment Assignment { get; set; }

    }
}
