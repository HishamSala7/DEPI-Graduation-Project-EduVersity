namespace EduVersity.Data.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateOnly UploadDate { get; set; }
        public string FileLink { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
