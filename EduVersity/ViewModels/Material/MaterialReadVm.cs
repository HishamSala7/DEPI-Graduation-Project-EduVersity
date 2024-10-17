namespace EduVersity.ViewModels.Material
{
    public class MaterialReadVM
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateOnly UploadDate { get; set; }
        public byte[] FileContent { get; set; }
        public string? FileLink { get; set; }
    }
}
