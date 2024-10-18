namespace EduVersity.Data.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateOnly? UploadDate { get; set; }
        public byte[] FileContent { get; set; }  //Store the file as binary data
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string? FileLink { get; set; }

    }
}
