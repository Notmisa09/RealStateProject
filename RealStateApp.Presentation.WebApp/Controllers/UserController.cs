using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.Acccount;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.User;

namespace RealStateApp.Presentation.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly AuthenticationResponse _userInSession;
        public UserController(IUserService userService, IHttpContextAccessor httcontextAccessor)
        {
            _userInSession = httcontextAccessor.HttpContext.Session.Get<AuthenticationResponse>("");
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

            if(result != null && result.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", result);
                if (result.Roles.Contains(RolesEnum.Client.ToString()))
                {
                    return RedirectToRoute(new { controller = "Client", action = "Home" });
                }
                else if (result.Roles.Contains(RolesEnum.Admin.ToString()))
                {
                    return RedirectToRoute(new { controller = "Admin", action = "DashBoard" });
                }
            }

            if (result.HasError)
            {
                vm.Error = result.Error;
                vm.HasError = true;
                return View ("Index",vm);
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
            Singleton.SetString(response.Error);
            var instance = Singleton.GetInstance("");
            return RedirectToAction("Index");
        }



    }
}
