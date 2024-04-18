using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.PropertyType;

namespace RealStateApp.Presentation.WebApp.Controllers
{
    public class PropertyTypeController : Controller
    {
        private readonly IPropertyTypeService _propertyTypeService;

        public PropertyTypeController(IPropertyTypeService propertyTypeService)
        {
            _propertyTypeService = propertyTypeService;
        }

        //PROPERTY GETALL
        public async Task<IActionResult> Index()
        {
            return View(await _propertyTypeService.GeallWithPropertiesAmount());
        }

        //PROPERTY SAVE
        public IActionResult Save()
        {
            return View("Save",new PropertyTypeAddViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(PropertyTypeAddViewModel vm)
        {
            await _propertyTypeService.Add(vm);
            return RedirectToRoute(new { controller= "PropertyType", action="Index" });
        }


        //PROPERTY TYPE EDIT
        public async Task<IActionResult> Edit(int Id)
        {
            return View("Save",await _propertyTypeService.GetById(Id)); 
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PropertyTypeAddViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Save", vm);
            }
            await _propertyTypeService.Update(vm, vm.Id);
            return RedirectToAction("Index");
        }
        
        //PROPERTY TYPE REMOVE
        public async Task<IActionResult> Remove(int Id)
        {
            return View(await _propertyTypeService.GetById(Id));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTrue(int Id)
        {
            await _propertyTypeService.Remove(Id);
            return RedirectToAction("Index");
        }
    }
}
