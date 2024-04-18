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
        private readonly IHttpContextAccessor _httcontextAccessor;
        public UserController(IUserService userService, IHttpContextAccessor httcontextAccessor)
        {
            _httcontextAccessor = httcontextAccessor;
            _userInSession = _httcontextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _userService = userService;
        }

        //LOGIN
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
                if (result.Roles.Contains(RolesEnum.Agent.ToString()))
                {
                    return RedirectToRoute(new { controller = "Agent", action = "Index" });
                }
                else if (result.Roles.Contains(RolesEnum.Admin.ToString()))
                {
                    return RedirectToRoute(new { controller = "Admin", action = "Index" });
                }
                else
                {
                    return RedirectToRoute(new { controller = "Client", action = "Index" });
                }
            }

            if (result.HasError)
            {
                vm.Error = result.Error;
                vm.HasError = true;
                return View ("Index",vm);
            }
            return RedirectToRoute("Home", "Index");
        }

        //REGISTER USER
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
            ServiceResult response = await _userService.UserRegisterSelector(vm, Role, origin);
            if(response.HasError)
            {
                vm.Error = response.Error;
                vm.HasError = response.HasError;
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        //CONFIRM EMAIL
        public async Task<IActionResult> ConfirmEmailAsync(string UserId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(UserId, token);
            return View("ConfirmEmail", response);
        }

        //LOGOUT
        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }


        //RESET PASSWORD
        public IActionResult ResetPassword(string Token)
        {
            return View(new ResetPasswordViewModel { Token = Token });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("ResetPassword", vm);
            }
            ServiceResult response = await _userService.ResetPasswordAsync(vm);
            if (response.HasError)
            {
                vm.Error = response.Error;
                vm.HasError = response.HasError;
                return View("ResetPassword", vm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }


        //FORGOT PASSWORD
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            ForgotPasswordViewModel vm = new();
            vm.Email = email;
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var origin = Request.Headers["origin"];
            ServiceResult response = await _userService.ForgotPasswordAsync(vm, origin);
            if (response.HasError)
            {
                vm.Error = response.Error;
                vm.HasError = response.HasError;
                return View("ForgotPassword", vm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }   
    }
}
