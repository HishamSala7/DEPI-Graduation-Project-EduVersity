namespace EduVersity.ViewModels.Material
{
    public class MaterialAddVM
    {
        public string Title { get; set; }
        public DateOnly? UploadDate { get; set; }
        public byte[]? FileContent { get; set; }
        public int CourseId { get; set; }
        public IFormFile? FileLink { get; set; }
    }
}
