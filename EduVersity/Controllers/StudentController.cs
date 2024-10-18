using EduVersity.Managers.StudentManager;
using EduVersity.ViewModels.Semester;
using EduVersity.ViewModels.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace EduVersity.Controllers
{
    [Authorize(Roles ="Admin,Employee")]
    public class StudentController : Controller
    {
        private readonly IStudentManager _studentManager;

        public StudentController(IStudentManager studentManager)
        {
            _studentManager = studentManager;
        }
        public IActionResult Index()
        {
            var studs = _studentManager.GetAllStudents();
            return View(studs);
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            var stud = _studentManager.GetStudentDetails(Id);
            ViewBag.dept = _studentManager.GetDepartmentName(Id);
            return View(stud);
        }

        [HttpGet]
        public IActionResult AssignToDepartment(int Id)
        {
            ViewBag.depts = _studentManager.GetDepartments();
            ViewBag.StudentId = Id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignToDepartment(int Id, AssignToDepartmentVm model)
        {
            if(ModelState.IsValid)
            {
                _studentManager.AssignToDepartment(Id, model);
                return Redirect($"~/Student/Details?Id={Id}");

            }
            ViewBag.depts = _studentManager.GetDepartments();
            ViewBag.StudentId = Id;
            return View(model);
        }

        [HttpGet]
        public IActionResult UpdateDepartment(int Id)
        {
            ViewBag.depts = _studentManager.GetDepartments();
            ViewBag.StudentId = Id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateDepartment(int Id, AssignToDepartmentVm model)
        {
            if (ModelState.IsValid)
            {
                _studentManager.AssignToDepartment(Id, model);
                return Redirect($"~/Student/Details?Id={Id}");
            }
            ViewBag.depts = _studentManager.GetDepartments();
            ViewBag.StudentId = Id;
            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteDepartment(int Id)
        {
            _studentManager.DeleteDepartment(Id);
            return Redirect($"~/Student/Details?Id={Id}");
        }



        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(StudentAddVm model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _studentManager.AddStudent(model);
                
                if(result.Succeeded) 
                    return RedirectToAction("Index");

                foreach (var item in result.Errors)
                    ModelState.AddModelError("msg", item.Description);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var stud = _studentManager.GetStudentUpdateVmFromDetails(_studentManager.GetStudentDetails(Id));
            return View(stud);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, StudentUpdateVm model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _studentManager.UpdateStudent(model);
                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var item in result.Errors)
                    ModelState.AddModelError("msg", item.Description);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var stud = _studentManager.GetStudentDeleteVmFromDetails(_studentManager.GetStudentDetails(Id));
            return View(stud);
        }

        public async Task<IActionResult> DeleteStudent(int Id, string Username)
        {
            IdentityResult result = await  _studentManager.DeleteStudent(Id,Username);

            if (result.Succeeded)
                return View();

			foreach (var item in result.Errors)
				ModelState.AddModelError("msg", item.Description);

            StudentDeleteVm deleteVm = new StudentDeleteVm();
            deleteVm.Id = Id;   
            deleteVm.Username = Username;

            return View(deleteVm);
		}

        [HttpGet]
        public IActionResult AddStudentsToSemester()
        {
            var model = _studentManager.GetStudentSemesterAddVm();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStudentsToSemester(StudentSemesterAddVm model)
        {
            _studentManager.AddStudentsInSemester(model);
            return Redirect("~/Student/Index");
        }



    }
}
