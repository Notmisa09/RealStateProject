using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.Services;
using RealStateApp.Core.Application.ViewModels.Improvements;
using RealStateApp.Core.Application.ViewModels.SellingTypes;

namespace RealStateApp.Presentation.WebApp.Controllers
{
    public class ImprovementsController : Controller
    {
        private readonly IimprovementsService _imprevementService;

        public ImprovementsController(IimprovementsService imprementsService)
        {
             _imprevementService = imprementsService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _imprevementService.GeAll());
        }

        public async Task<IActionResult> Save(int Id)
        {
            return View(await _imprevementService.GetById(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ImprovementsAddViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            await _imprevementService.Add(vm);
            return RedirectToRoute(new { controller = "SellingType", action = "Index" });
        }

        public async Task<IActionResult> Edit(int Id)
        {
            return View("Save",await _imprevementService.GetById(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ImprovementsAddViewModel vm)
        {
            if (ModelState.IsValid)
            {
                return View("Save", vm);
            }
            await _imprevementService.Update(vm, vm.Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(int Id)
        {
            return View(await _imprevementService.GetById(Id));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTrue(int Id)
        {
            await _imprevementService.Remove(Id);
            return RedirectToAction("Index");
        }
    }
}
