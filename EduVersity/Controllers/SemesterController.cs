using EduVersity.Managers.SemesterManager;
using EduVersity.ViewModels.CourseLevelSemester;
using EduVersity.ViewModels.Level;
using EduVersity.ViewModels.Semester;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EduVersity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SemesterController : Controller
    {
        private readonly ISemesterManager _semesterManager;

        public SemesterController(ISemesterManager semesterManager)
        {
            _semesterManager = semesterManager;
        }
        public IActionResult Index()
        {
            var semesters = _semesterManager.GetAllSemesters();
            return View(semesters);
        }

        public IActionResult Details(int Id)
        {
            var SemesterDetails = _semesterManager.GetSemesterDetails(Id);
            return View(SemesterDetails);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(SemesterAddVm model)
        {
            if (ModelState.IsValid)
            {
                _semesterManager.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Next(int Id)
        {
            LevelReadVm CurrentLevel = null;

            var CoursesNotSelected = _semesterManager.GetCoursesNotSelected(Id,ref CurrentLevel);
            var model = _semesterManager.CoursesIntoGroups(CoursesNotSelected);
            ViewBag.level = CurrentLevel;
            ViewBag.SemesterId = Id;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Next(List<CourseLevelSemesterVm> model)
        {
            _semesterManager.AddSelectedCourses(model);
            return RedirectToAction("Next", model[0].SemesterId);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var semester = _semesterManager.GetSemesterById(Id);
            return View(semester);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SemesterUpdateVm model)
        {
            if (ModelState.IsValid)
            {
                _semesterManager.Update(model);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("msg", "Invalid Dates");
            return View(model);
        }


        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var Semester = _semesterManager.GetSemesterById(Id);
            return View(Semester);
        }

        public IActionResult DeleteSemester(int Id)
        {
            _semesterManager.Delete(Id);
            return View();
        }

        public IActionResult CheckDates(DateOnly EndDate, DateOnly StartDate)
        {
            bool res = _semesterManager.CheckDates(EndDate, StartDate);

            return Json(res);
        }


    }
}
