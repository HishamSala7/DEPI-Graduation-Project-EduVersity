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

            if(professor != null)
                return View("ProfessorIndex", professor);

            return View();
        }

        [HttpGet]
        public IActionResult GetSemesterCourses(int SemesterId, int DepartmentId,int LevelId, string Username)
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
        public IActionResult StudentEnrollmentsDetails(int Id)
        {
            var details = _enrollmentManager.GetStudentEnrollmentsDetails(Id);
            
            if (details.Count == 0 || details == null)
                return View("NoEnrollmets");

            return View(details);
        }

        [HttpGet]
        public IActionResult ProfessorEnrollmentsDetails(string Id)
        {
            var professorEnrollments = _enrollmentManager.GetProfessorEnrollmetsDetails(Id);
            if (professorEnrollments.Count == 0 || professorEnrollments == null)
                return View("NotAssigned");

            return View(professorEnrollments);
        }

        [HttpGet]
        public IActionResult EditStudentsGrades(int CourseId, string CourseName)
        {
            var StudentsData = _enrollmentManager.GetStudentsDataInCourse(CourseId);
            ViewBag.CourseName = CourseName;
            ViewBag.CourseId = CourseId;
            return View(StudentsData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveStudentsGrades(int CourseId, List<StudentGradeEditVm> model)
        {
            _enrollmentManager.UpdateStudentsGrades(CourseId,model);
            return View();
        }

        [HttpGet]
        public IActionResult AssignTeachersToCourses()
        {
            var courses = _enrollmentManager.GetEnrolledCourses();
            var professors = _enrollmentManager.GetAllProfessors();
            ViewBag.professors = professors.Select(p => new { p.DepartmentName ,
                    p.UserId, FullName = p.FirstName + " " + p.LastName }).ToList();

            return View(courses);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignTeachersToCourses(List<EnrollCourseDepartmentVm> model)
        {
            if (ModelState.IsValid)
            {
                _enrollmentManager.AssignTeachersToCourses(model);
                return View("SuccessProcess");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult GetTeachersEnrolledCourses()
        {
            var CoursesTeachers = _enrollmentManager.GetCoursesTeacher();
            return View(CoursesTeachers);
        }

        
    }
}
