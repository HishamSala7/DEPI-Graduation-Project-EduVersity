using EduVersity.Managers.DepartmentManager;
using EduVersity.ViewModels.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduVersity.Controllers
{
    [Authorize(Roles ="Admin")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentManager _departmentManager;

        public DepartmentController(IDepartmentManager departmentManager)
        {
            _departmentManager = departmentManager;
        }
        public IActionResult Index()
        {
            var depts = _departmentManager.GetAllDepartments();
            return View(depts);
        }

        [HttpGet]
        public IActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDepartment(DepartmentAddVm model)
        {
            if (ModelState.IsValid)
            {
                _departmentManager.AddDepartment(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var dept = _departmentManager.GetDepartmentById(Id);
            return View(dept);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, DepartmentUpdateVm model)
        {
            if (ModelState.IsValid)
            {
                _departmentManager.UpdateDepartment(model);
                return RedirectToAction("Index");
            }

            DepartmentReadVm dept = new DepartmentReadVm();
            dept.Name = model.Name;
            dept.Id = model.Id;
            
            return View(dept);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var dept = _departmentManager.GetDepartmentById(Id);
            return View(dept);
        }

        public IActionResult DeleteDepartment(int Id)
        {
            _departmentManager.DeleteDepartment(Id);
            return View();
        }


    }
}
