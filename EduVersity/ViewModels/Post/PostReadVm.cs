namespace EduVersity.ViewModels.Post
{
    public class PostReadVm
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Content { get; set; }
        public DateOnly Date { get; set; }
    }
}
