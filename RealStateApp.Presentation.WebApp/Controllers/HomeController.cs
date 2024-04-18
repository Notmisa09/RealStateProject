using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.Filter;
using RealStateApp.Infrastructure.Identity.Seeds;

namespace RealStateApp.Presentation.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IPropertyService _propertyService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, IPropertyService propertyService)
        {
            _propertyService = propertyService;
            _userService = userService;
            _logger = logger;
        }

        public async Task<IActionResult> AgentFilter(FilterUserViewModel vm)
        {
            var result = await _userService.FilterForAgents(vm, RolesEnum.Agent.ToString());
            return View("Agents",result);  
        }

        public async Task<IActionResult> Index()
        {
            return View(await _propertyService.GeAllWithInclude());
        }

        public async Task<IActionResult> Agents()
        {
            return View(await _userService.GeAllByUsers(RolesEnum.Agent.ToString()));
        }

        public async Task<IActionResult> Details(int Id)
        {
            var property = await _propertyService.GetPropertyById(Id);
            foreach (var item in property)
            {
                ViewBag.User = await _userService.GetById(item.AgentId);
            }
            return View(property);
        }
    }
}
