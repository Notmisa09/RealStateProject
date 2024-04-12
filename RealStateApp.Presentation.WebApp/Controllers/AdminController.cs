using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Core.Application.Interfaces.IService;

namespace RealStateApp.Presentation.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        public AdminController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AgentList()
        {
            return View("AgentList",await _userService.GeAllByUsers(RolesEnum.Agent.ToString()));
        }
    }
}
