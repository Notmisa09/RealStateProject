using Microsoft.AspNetCore.Mvc;

namespace RealStateApp.Presentation.WebApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
