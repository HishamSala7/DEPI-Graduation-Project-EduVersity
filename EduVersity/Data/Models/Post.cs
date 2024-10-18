using Microsoft.AspNetCore.SignalR;

namespace EduVersity.Data.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Content { get; set; }
        public DateOnly Date { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
