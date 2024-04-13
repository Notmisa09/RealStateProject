using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.DashBoard;
using RealStateApp.Core.Application.ViewModels.User;

namespace RealStateApp.Presentation.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IDashBoardService _boardService;
        public AdminController(IUserService userService, IDashBoardService boardService)
        {
            _boardService = boardService;
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

        public async Task<IActionResult> DevList()
        {
            return View("AgentList",await _userService.GeAllByUsers(RolesEnum.Developer.ToString()));
        }

        public async Task<IActionResult> DashBoard(DashBoardViewModel vm)
        {
            return View(await _boardService.GetDashBoardInfo(vm));
        }

        public IActionResult Register()
        {
            return View(new SaveUserViewModel());
        }
        
        public async Task<IActionResult> Register(SaveUserViewModel vm, string origin, string UserRole)
        {
            if (ModelState.IsValid)
            {
                return View(vm);
            }
            await _userService.RegigsterAsync(vm, origin, UserRole);
            return RedirectToRoute(new { controller = "Agent", action = "Index" });
        }
    }
}
