using EduVersity.Data.Models;
using EduVersity.ViewModels.Post;

namespace EduVersity.Managers.PostManager
{
    public interface IPostManager
    {
        List<PostReadVm> GetAllPostsOrdered();
        void Add(PostAddVm model,string Username);
        void Update(PostUpdateVm model);
        void Delete(int Id);
        
    }
}
