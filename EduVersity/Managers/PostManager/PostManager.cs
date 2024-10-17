using AutoMapper;
using EduVersity.Data.Models;
using EduVersity.Repos.PostRepo;
using EduVersity.ViewModels.Post;
using Microsoft.AspNetCore.Identity;

namespace EduVersity.Managers.PostManager
{
    public class PostManager : IPostManager
    {
        private readonly IPostRepo _postRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostManager(IPostRepo postRepo, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _postRepo = postRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        public List<PostReadVm> GetAllPostsOrdered()
        {
            var posts = _postRepo.GetAll().OrderByDescending(p => p.Date).ToList();
            return _mapper.Map<List<PostReadVm>>(posts);
        }
        
        public void Add(PostAddVm model, string Username)
        {
            var userId = _userManager.Users.FirstOrDefault(x => x.UserName == Username).Id;

            Post post = new Post
            {
                Title = model.Title,
                Content = model.Content,
                Date = DateOnly.FromDateTime(DateTime.Now),
                ApplicationUserId = userId
            };

            _postRepo.Add(post); 
            _postRepo.SaveChanges(); 
        }
        public void Delete(int Id)
        {
            _postRepo.Delete(Id);
            _postRepo.SaveChanges();
        }

        public void Update(PostUpdateVm model)
        {
            var post = _postRepo.GetById(model.Id);
            
            if (post == null)
                return;

            _mapper.Map(model,post);
            _postRepo.Update(post);
            _postRepo.SaveChanges();
        }

    }
}
