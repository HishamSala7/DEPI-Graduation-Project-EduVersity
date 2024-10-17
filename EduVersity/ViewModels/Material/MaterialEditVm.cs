namespace EduVersity.ViewModels.Material
{
    public class MaterialEditVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly UploadDate { get; set; }
        public byte[]? FileContent { get; set; }
        public string? FileLink { get; set; }
        public int CourseId { get; set; }
    }


}
