using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.Properties;

namespace RealStateApp.Presentation.WebApp.Controllers
{

    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public async Task<IActionResult> Remove(int Id)
        {
            return View(await _propertyService.GetPropertyById(Id));
        }
        
        public async Task<IActionResult> RemoveTrue(int Id)
        {
            await _propertyService.Remove(Id);
            return RedirectToRoute(new { controller = "Agent", action = "PropertyList" });
        }
    }
}
