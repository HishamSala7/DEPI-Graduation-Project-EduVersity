namespace EduVersity.ViewModels.User
{
    public class UserReadVm
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        //public string Email { get; set; }
        public char Discriminator { get; set; }

    }
}
