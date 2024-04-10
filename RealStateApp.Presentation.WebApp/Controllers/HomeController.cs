using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Presentation.WebApp.Models;
using System.Diagnostics;

namespace RealStateApp.Presentation.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Agents()
        {
            return View(await _userService.GeAllByUsers(RolesEnum.Agent.ToString()));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
