using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.SellingTypes;

namespace RealStateApp.Presentation.WebApp.Controllers
{
    public class SellingTypeController : Controller
    {
        private readonly ISellingTypeService _sellingTypeService;

        public SellingTypeController(ISellingTypeService sellingTypeService)
        {
            _sellingTypeService = sellingTypeService;
        }

        //INDEX
        public async Task<IActionResult> Index()
        {
            return View(await _sellingTypeService.GetAll());
        }

        //SAVE
        public IActionResult Save()
        {
            return View("Save",new SellingTypeAddViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(SellingTypeAddViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);    
            }
            await _sellingTypeService.Add(vm);
            return RedirectToRoute(new { controller= "SellingTypeName" , action="Index"});
        }

        //EDIT
        public async Task<IActionResult> Edit(int Id)
        {
            return View("Save",await _sellingTypeService.GetById(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SellingTypeAddViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Save", vm);
            }
            await _sellingTypeService.Update(vm, vm.Id);
            return RedirectToAction("Index");
        }

        //REMOVE
        public async Task<IActionResult> Remove(int Id)
        {
            return View(await _sellingTypeService.GetById(Id));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTrue(int Id)
        {
            await _sellingTypeService.Remove(Id);
            return RedirectToAction("Index");
        }
    }
}
