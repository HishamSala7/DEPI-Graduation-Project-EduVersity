using EduVersity.Managers.CourseManager;
using EduVersity.Managers.LevelManager;
using EduVersity.ViewModels.Level;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace EduVersity.Controllers
{
    [Authorize(Roles ="Admin")]
    public class LevelController : Controller
    {
        private readonly ILevelManager _levelManager;

        public LevelController(ILevelManager levelManager)
        {
            _levelManager = levelManager;
        }
        public IActionResult Index()
        {
            var levels = _levelManager.GetAllLevels();  
            return View(levels);
        }

        [HttpGet]
        public IActionResult AddLevel()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddLevel(LevelAddVm model)
        {
            if(ModelState.IsValid)
            {
                _levelManager.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var levelReadVm = _levelManager.GetLevelById(Id);
            var LevelUpdateVm = _levelManager.MapLevelReadVmToLevelUpdateVm(levelReadVm);
            return View(LevelUpdateVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, LevelUpdateVm model)
        {
            if (ModelState.IsValid)
            {
                _levelManager.Update(model);
                return RedirectToAction("Index");
            }
            return View(model); 
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var level =  _levelManager.GetLevelById(id);
            return View(level);
        }

        public IActionResult DeleteLevel(int id)
        {
             _levelManager.Delete(id);
            return View();
        }
    }
}
