using Microsoft.AspNetCore.Mvc;

namespace EduVersity.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
