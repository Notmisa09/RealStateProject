using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.Acccount;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.User;

namespace RealStateApp.Presentation.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Index",vm);
            }
            var result = await _userService.LoginAync(vm);
            if (result.HasError)
            {
                result.HasError = vm.HasError;
                result.Error = vm.Error;
                return View ("Index",result);
            }
            return RedirectToAction("Home", "Index");
        }

        public IActionResult Register()
        {
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm, string Role)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", vm);
            }
            var origin = Request.Headers["origin"];
            ServiceResult response = await _userService.RegigsterAsync(vm, origin , Role);
            if(!response.HasError)
            {
                vm.Error = response.Error;
                vm.HasError = response.HasError;
                return View(vm);
            }
            var instance = Singleton.GetInstance("");
            Singleton.GetInstance(response.Error);
            return RedirectToAction("Index");
        }
    }
}
