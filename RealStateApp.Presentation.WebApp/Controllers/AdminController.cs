using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.Acccount;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.DashBoard;
using RealStateApp.Core.Application.ViewModels.User;

namespace RealStateApp.Presentation.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IDashBoardService _boardService;
        public AdminController(IUserService userService, IDashBoardService boardService)
        {
            _boardService = boardService;
            _userService = userService;
        }

        public async Task<IActionResult> AgentList()
        {
            return View("AgentList", await _userService.GetUsersIsActiveIgnore(RolesEnum.Agent.ToString()));
        }

        public async Task<IActionResult> DevList()
        {
            return View("DevList", await _userService.GetUsersIsActiveIgnore(RolesEnum.Developer.ToString()));
        }

        public async Task<IActionResult> AdminList()
        {
            return View("AdminList", await _userService.GetAdminUsers(RolesEnum.Admin.ToString()));
        }

        //DASHBOARDS
        public async Task<IActionResult> DashBoard(DashBoardViewModel vm)
        {
            return View(await _boardService.GetDashBoardInfo(vm));
        }

        //CHANGESTATUS
        public async Task<IActionResult> ChangeStatus(string Id)
        {
            var adduservm = await _userService.GetById(Id);
            await _userService.ChangeUserStatus(adduservm);
            return RedirectToAction("AdminList");
        }

        //REGISTER ADMIN
        public IActionResult RegisterAdmin()
        {
            return View(new SaveUserViewModel());
        }

        public async Task<IActionResult> Register(SaveUserViewModel vm, string Role)
        {
            if (!ModelState.IsValid)
            {
                return View("RegisterAdmin", vm);
            }
            var origin = Request.Headers["origin"];
            ServiceResult response = await _userService.UserRegisterSelector(vm, Role, origin);
            if (response.HasError)
            {
                vm.Error = response.Error;
                vm.HasError = response.HasError;
                return View("RegisterAdmin",vm);
            }
                return RedirectToAction("AdminList");
        }

        //EDIT ADMIN
        public async Task<IActionResult> EditAdmin(string Id)
        {
            return View("RegisterAdmin", await _userService.GetById(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("RegisterAdmin",vm);
            }
            await _userService.UpdateUserAsync(vm);
            return RedirectToAction("AdminList");
        }

        //REMOVE USERS
        public async Task<IActionResult> Remove(string Id)
        {
            return View("Remove", await _userService.GetById(Id));
        }

        public async Task<IActionResult> RemoveTrue(string Id)
        {
            var result = await _userService.Remove(Id);
            return RedirectToAction("AdminList");
        }
    }
}
