using EduVersity.ViewModels.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduVersity.Controllers
{
	[Authorize(Roles = "Admin")]
    public class RoleController : Controller
	{
		private readonly RoleManager<IdentityRole> _RoleManager;

		public RoleController(RoleManager<IdentityRole> roleManager)
        {
			this._RoleManager = roleManager;
		}
        public IActionResult Index()
		{
			List<IdentityRole> roles =  _RoleManager.Roles.ToList();
			return View(roles);
		}
		[HttpGet]
		public IActionResult AddRole()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddRole(RoleVm model)
		{
			if (ModelState.IsValid)
			{
				IdentityRole identityRole = new IdentityRole();
				identityRole.Name = model.RoleName;
				IdentityResult result = await _RoleManager.CreateAsync(identityRole);

				if (result.Succeeded)
					return RedirectToAction("Index");

				else

					foreach(var item in result.Errors)
                        ModelState.AddModelError("msg", item.Description);

			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Edit(string Id)
		{
			IdentityRole Role = _RoleManager.Roles.FirstOrDefault(x => x.Id == Id);
			RoleVm roleVm = new RoleVm();
			roleVm.RoleName = Role.Name;
			ViewBag.Id = Role.Id;
			return View(roleVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(RoleVm model, string Id)
		{
            
            if (ModelState.IsValid)
			{
                var Role = await _RoleManager.FindByIdAsync(Id);
				Role.Name = model.RoleName;
                IdentityResult result = await _RoleManager.UpdateAsync(Role);
                
				if (result.Succeeded)
                    return RedirectToAction("Index");

                else

                    foreach (var item in result.Errors)
                        ModelState.AddModelError("msg", item.Description);
            }
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string Id)
		{
            var Role = await _RoleManager.FindByIdAsync(Id);
            RoleVm roleVm = new RoleVm();
            roleVm.RoleName = Role.Name;
            ViewBag.Id = Role.Id;
            return View(roleVm);
        }

		
		public async Task<IActionResult> DeleteRole(string Id)
		{
            var Role = await _RoleManager.FindByIdAsync(Id);
            RoleVm roleVm = new RoleVm();
            roleVm.RoleName = Role.Name;
            IdentityResult result = await _RoleManager.DeleteAsync(Role);

			if (result.Succeeded)
				return View();

			else
				foreach (var item in result.Errors)
					ModelState.AddModelError("msg", item.Description);

			return View("Delete", roleVm);
        }

    }
}
