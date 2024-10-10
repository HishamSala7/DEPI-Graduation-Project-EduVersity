using EduVersity.ViewModels.Post;

namespace EduVersity.Managers.PostManager
{
    public interface IPostManager
    {
        List<PostReadVm> GetAllPosts();
        void Add(PostAddVm model);
        void Update(PostUpdateVm model);
        void Delete(int Id);
    }
}
