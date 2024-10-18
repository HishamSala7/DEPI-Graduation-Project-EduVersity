using EduVersity.Managers.CourseManager;
using EduVersity.Managers.DepartmentManager;
using EduVersity.ViewModels.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EduVersity.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CourseController : Controller
    {
        private readonly ICourseManager _courseManager;

        public CourseController(ICourseManager courseManager)
        {
            _courseManager = courseManager;
        }
        public IActionResult Index()
        {
            var CoursesWithDepartmentName = _courseManager.GetCourseListWithDepartmentName();
            
            return View(CoursesWithDepartmentName);
        }

        [HttpGet]
        public IActionResult AddCourse()
        {
            ViewBag.depts = _courseManager.GetDepartments();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCourse(int Id, CourseAddVm model)
        {
            if (ModelState.IsValid)
            {
                _courseManager.Add(model);
                return RedirectToAction("Index");
            }
            ViewBag.depts = _courseManager.GetDepartments();
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var crs = _courseManager.GetCourseById(Id);
            ViewBag.depts = _courseManager.GetDepartments();
            return View(crs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, CourseEditVm model)
        {
            if (ModelState.IsValid)
            {
                _courseManager.Update(model);
                return RedirectToAction("Index");
            }
            ViewBag.depts = _courseManager.GetDepartments();
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var crs = _courseManager.GetCourseById(Id);
            return View(crs);   
        }

        public IActionResult DeleteCourse(int Id)
        {
            _courseManager.Delete(Id);
            return View();
        }

    }
}
