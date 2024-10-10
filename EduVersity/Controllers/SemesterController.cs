using EduVersity.Managers.SemesterManager;
using EduVersity.ViewModels.Semester;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks.Sources;

namespace EduVersity.Controllers
{
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

        [HttpGet]
        public IActionResult Add()
        {
            //ViewBag.CoursesPerDepartment = _semesterManager.GetCoursesPerDepartment();
            //ViewBag.levels = _semesterManager.GetLevels();
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
            ModelState.AddModelError("msg", "Invalid Dates");
            return View(model);
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

        //public IActionResult test()
        //{
        //    ViewBag.dept = "IT";
        //    ViewBag.level = "Grade 1";
        //    List<CourseSelectionVm> courses = new List<CourseSelectionVm>
        //    {
        //        new CourseSelectionVm { CourseCode = "1", CourseName = "crs1", IsChecked = false },
        //        new CourseSelectionVm { CourseCode = "2", CourseName = "crs1", IsChecked = false },
        //        new CourseSelectionVm { CourseCode = "3", CourseName = "crs1", IsChecked = false }
        //    };
        //    return View(courses);
        //}

        //public IActionResult testRes(List<CourseSelectionVm> IsChecked)
        //{

        //    return Content("Ok");
        //}

        //[HttpGet]
        //public IActionResult Delete(int Id)
        //{
        //    var Semester = _semesterManager.GetSemesterById(Id);
        //    return View(Semester);
        //}

        //public IActionResult DeleteSemester(int Id)
        //{
        //    _semesterManager.Delete(Id);
        //    return View();
        //}

        //public IActionResult CheckDates(DateOnly EndDate, DateOnly StartDate)
        //{
        //    bool res = _semesterManager.CheckDates(EndDate, StartDate);

        //    return Json(res);
        //}

    }
}
