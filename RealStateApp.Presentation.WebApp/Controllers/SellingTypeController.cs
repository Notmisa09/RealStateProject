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
            return View(await _sellingTypeService.GeAll());
        }

        //SAVE
        public async Task<IActionResult> Save(int Id)
        {
            return View(await _sellingTypeService.GetById(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Save(SellingTypeAddViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);    
            }
            await _sellingTypeService.Add(vm);
            return RedirectToRoute(new { controller= "SellingType" , action="Index"});
        }

        //EDIT
        public async Task<IActionResult> Edit(int Id)
        {
            return View(await _sellingTypeService.GetById(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SellingTypeAddViewModel vm)
        {
            if (ModelState.IsValid)
            {
                return View("Register", vm);
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
