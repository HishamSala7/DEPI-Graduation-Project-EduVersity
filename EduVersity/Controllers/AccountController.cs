using EduVersity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using EduVersity.ViewModels.User;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System.Reflection.Metadata.Ecma335;
using EduVersity.Managers.AccountManager;
using EduVersity.ViewModels.UserDepartment;
using Microsoft.AspNetCore.Authorization;

namespace EduVersity.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly IAccountManager _AccountManager;

        public AccountController(IAccountManager accountManager)
        {
            this._AccountManager = accountManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_AccountManager.GetAllUsersExceptStudents());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(string Username)
        {
            UserDetailsVm user =  _AccountManager.GetUserDetails(Username);
            ViewBag.dept = await _AccountManager.GetDepartmentName(Username);
            return View(user);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(UserAddVm user)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _AccountManager.Add(user);

                if (result.Succeeded)
                    return RedirectToAction("Index");

                else
                    foreach (var item in result.Errors)
                        ModelState.AddModelError("msg", item.Description);
                
            }
            return View(user);
        }

        

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string Username)
        {
            UserEditVm userEdit = _AccountManager.GetUserEditVm(Username);
            return View(userEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditVm userEdit)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _AccountManager.Update(userEdit);

                if (result.Succeeded)
                    return RedirectToAction("Index");

                else
                    foreach (var item in result.Errors)
                        ModelState.AddModelError("msg", item.Description);

            }
            return View(userEdit);

        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string Username)
        {
            return View("Delete",Username);
        }

        public async Task<IActionResult> DeleteUser(string Username)
        {
            IdentityResult result =  await _AccountManager.Delete(Username);

            if (result.Succeeded)
                return View();

            else
                foreach (var item in result.Errors)
                    ModelState.AddModelError("msg", item.Description);

            return View("Delete",Username);
        }
    
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginVm model)
        {
            if (ModelState.IsValid)
            {
                if (await _AccountManager.Login(model))
                    return Redirect("~/Post/Index");
            }
            ModelState.AddModelError("msg","Username or Password is incorrect");
            return View(); 
        }

        public IActionResult Logout()
        {
             _AccountManager.Logout();
            return RedirectToAction("Login");
        }
    }
}
