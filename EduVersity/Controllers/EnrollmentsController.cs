using EduVersity.Managers.CourseLevelSemesterManager;
using EduVersity.Managers.EnrollmentManager;
using EduVersity.ViewModels.Course;
using EduVersity.ViewModels.Enrollment;
using Microsoft.AspNetCore.Mvc;

namespace EduVersity.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly IEnrollmentManager _enrollmentManager;

        public EnrollmentsController(IEnrollmentManager enrollmentManager)
        {
            _enrollmentManager = enrollmentManager;
        }
        public IActionResult Index()
        {
            var stud =  _enrollmentManager.GetLoggedInStudent(User.Identity.Name);
            if(stud != null)
                return View("StudentIndex",stud);

            var professor = _enrollmentManager.GetLoggedInProfessor(User.Identity.Name);
            return View("ProfessorIndex", professor);
        }

        [HttpGet]
        public IActionResult SemesterCourses(int SemesterId, int DepartmentId,int LevelId, string Username)
        {
            var courses = _enrollmentManager.GetUnEnrolledCourses(Username, SemesterId, DepartmentId, LevelId);
            
            if (courses == null || courses.Count == 0)
                return View("success");
            
            ViewBag.Username = Username;
            return View(courses);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCoursesEnrollment(string Username, List<CourseLevelSemesterDepartmentVm> model)
        {
            _enrollmentManager.AddEnrolledCourses(Username, model);
            return View();
        }

        [HttpGet]
        public IActionResult StudentDetails(int Id)
        {
            var details = _enrollmentManager.GetStudentEnrollmentsDetails(Id);
            
            if (details.Count == 0 || details == null)
                return View("NoEnrollmets");

            return View(details);
        }

        [HttpGet]
        public IActionResult ProfessorDetails(string Id)
        {
            var professorEnrollments = _enrollmentManager.GetProfessorEnrollmetsDetails(Id);
            return View(professorEnrollments);
        }

        [HttpGet]
        public IActionResult AssignTeacher()
        {
            var courses = _enrollmentManager.GetEnrollCourses();
            var professors = _enrollmentManager.GetAllProfessors();
            ViewBag.professors = professors.Select(p => new { p.DepartmentName ,
                    p.UserId, FullName = p.FirstName + " " + p.LastName }).ToList();

            return View(courses);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignTeacher(List<EnrollCourseDepartmentVm> model)
        {
            if (ModelState.IsValid)
            {
                _enrollmentManager.AssignTeachersToCourses(model);
                return View("SuccessProcess");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult CoursesTeachers()
        {
            var CoursesTeachers = _enrollmentManager.GetCoursesTeacher();
            return View(CoursesTeachers);
        }


    }
}
