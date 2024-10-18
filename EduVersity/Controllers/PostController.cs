using AutoMapper;
using EduVersity.Data.Models;
using EduVersity.Managers.PostManager;
using EduVersity.ViewModels.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduVersity.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostManager _postManager;
        public PostController(IPostManager postManager)
        {
            _postManager = postManager;
        }

        public IActionResult Index()
        {
            var posts = _postManager.GetAllPostsOrdered();
            return View(posts);
        }

        [HttpGet]
        [Authorize(Roles ="Admin,Employee")]
        public IActionResult Create()
        {
            ViewBag.Username = User.Identity.Name;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PostAddVm model ,string Username)
        {
            if (ModelState.IsValid)
            {
                _postManager.Add(model,Username);
                return RedirectToAction("Index"); 
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Update(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int Id, PostUpdateVm model)
        {
            if (ModelState.IsValid)
            {
                _postManager.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Delete(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        public IActionResult DeletePost(int Id)
        {
            _postManager.Delete(Id);
            return View();
        }



    }

}

