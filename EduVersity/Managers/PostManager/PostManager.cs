using AutoMapper;
using EduVersity.Data.Models;
using EduVersity.Repos.PostRepo;
using EduVersity.ViewModels.Post;

namespace EduVersity.Managers.PostManager
{
    public class PostManager : IPostManager
    {
        private readonly IPostRepo _postRepo;
        private readonly IMapper _mapper;

        public PostManager(IPostRepo postRepo, IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }

        public List<PostReadVm> GetAllPosts()
        {
            var posts = _postRepo.GetAll();
            return _mapper.Map<List<PostReadVm>>(posts);
        }

        public void Add(PostAddVm model)
        {
            _postRepo.Add(_mapper.Map<Post>(model));
            _postRepo.SaveChanges();
        }

        public void Delete(int Id)
        {

        }

        public void Update(PostUpdateVm model)
        {

        }
    }
}
