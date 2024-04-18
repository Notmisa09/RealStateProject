using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.Acccount;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.User;

namespace RealStateApp.Presentation.WebApp.Controllers
{
    public class DevController : Controller
    {
        private readonly IUserService _userService;

        public DevController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //CHANGESTATUS
        public async Task<IActionResult> ChangeStatus(string Id)
        {
            var adduservm = await _userService.GetById(Id);
            await _userService.ChangeUserStatus(adduservm);
            return RedirectToRoute(new { controller = "Admin", action = "DevList" });
        }

        //REGISTER DEV
        public IActionResult RegisterDev()
        {
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm, string Role)
        {
            if (!ModelState.IsValid)
            {
                return View("RegisterDev", vm);
            }
            var origin = Request.Headers["origin"];
            ServiceResult response = await _userService.UserRegisterSelector(vm, Role, origin);
            if (!response.HasError)
            {
                vm.Error = response.Error;
                vm.HasError = response.HasError;
                return View("RegisterDev", vm);
            }
            return RedirectToRoute(new { controller = "Admin" , action= "DevList" });
        }

        //EDIT DEV
        public async Task<IActionResult> EditDev(string Id)
        {
            return View("RegisterDev", await _userService.GetById(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            await _userService.UpdateUserAsync(vm);
            return RedirectToRoute(new { controller = "Admin", action = "DevList" });
        }


        //REMOVE AGENTS
        public async Task<IActionResult> Remove(string Id)
        {
            return View("Remove", await _userService.GetById(Id));
        }

        public async Task<IActionResult> RemoveTrue(string Id)
        {
            await _userService.Remove(Id);
            return RedirectToRoute(new { controller = "Admin", action = "DevList" });
        }
    }
}
