namespace EduVersity.Data.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string? ApplicationUserId { get; set; }
        public float Grade { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public ApplicationUser ApplicationUser { get; set; }



    }
}
