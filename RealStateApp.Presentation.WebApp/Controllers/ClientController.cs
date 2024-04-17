using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.IService;

namespace RealStateApp.Presentation.WebApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPropertyService _propertyService;

        public ClientController(IUserService userService, IPropertyService propertyService)
        {
            _userService = userService;
            _propertyService = propertyService;

        }

        public async Task<IActionResult> Index()
        {
            return View(await _propertyService.GeAllWithInclude());
        }

        //LOGOUT
        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Client", action = "HomeClient" });
        }

    }
}
