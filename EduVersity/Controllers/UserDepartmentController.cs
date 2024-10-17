using EduVersity.Managers.UserDepartmentManager;
using EduVersity.ViewModels.UserDepartment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduVersity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserDepartmentController : Controller
    {
        private readonly IUserDepartmentManager _userDepartmentManager;

        public UserDepartmentController(IUserDepartmentManager userDepartmentManager)
        {
            _userDepartmentManager = userDepartmentManager;
        }


        [HttpGet]
        public IActionResult AssignToDepartment(string UserId)
        {
            ViewBag.depts = _userDepartmentManager.GetDepartments();
            ViewBag.UserId = UserId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignToDepartment(string UserId, UserDepartmentAddVm model)
        {
            if (ModelState.IsValid)
            {
                if (!_userDepartmentManager.AddProfessorToDepartment(model))
                {
                    ModelState.AddModelError("msg", "The user is already exists in department, if you want to update  go to Details");
                    ViewBag.depts = _userDepartmentManager.GetDepartments();
                    ViewBag.UserId = model.UserId;
                    return View(model);
                }
            }

            return Redirect("~/Account/Index");
        }

        [HttpGet]
        public IActionResult UpdateDepartment(string UserId)
        {
            var user = _userDepartmentManager.GetUserDepartmentById(UserId);
            ViewBag.depts = _userDepartmentManager.GetDepartments();
            ViewBag.UserId = UserId;
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateDepartment(string UserId , UserDepartmentUpdateVm model)
        {
            if (ModelState.IsValid)
            {
                _userDepartmentManager.Update(model, UserId);
                return Redirect("~/Account/Index");
            }
            ViewBag.depts = _userDepartmentManager.GetDepartments();
            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteDepartment(string UserId, string Username)
        {
            _userDepartmentManager.Delete(UserId);

            return Redirect($"~/Account/Details?Username={Username}");
        }

    }
}
