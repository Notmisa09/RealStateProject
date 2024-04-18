using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.Properties;

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
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        public async Task<IActionResult> AddFav(int Id)
        {
            var property = await _propertyService.GetById(Id);
            await _propertyService.AddFavProp(property);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MyProperties()
        {
            return View(await _propertyService.GetAllFav());
        }

        public async Task<IActionResult> RemoveFavProp(int favprop)
        {
            await _propertyService.RemoveFavProp(favprop);
            return RedirectToAction("MyProperties");
        }
    }
}
