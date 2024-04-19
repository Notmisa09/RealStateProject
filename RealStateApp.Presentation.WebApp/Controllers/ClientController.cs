using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.Services;
using RealStateApp.Core.Application.ViewModels.Filter;
using RealStateApp.Core.Application.ViewModels.Properties;

namespace RealStateApp.Presentation.WebApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;

        public ClientController(IUserService userService, 
            IPropertyService propertyService, 
            IPropertyTypeService propertyTypeService)
        {
            _userService = userService;
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
        }
        public async Task<IActionResult> PropertyFilter(FilterViewModel vm)
        {
            ViewBag.PropertyType = await _propertyTypeService.GetAll();
            var result = await _propertyService.GeAllWithFilterInclude(vm);
            return View("Index", result);
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PropertyType = await _propertyTypeService.GetAll();
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
