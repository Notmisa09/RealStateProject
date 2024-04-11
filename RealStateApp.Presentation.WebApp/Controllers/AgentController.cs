using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.ViewModels.Properties;

namespace RealStateApp.Presentation.WebApp.Controllers
{
    public class AgentController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPropertyService _propertyService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AuthenticationResponse user;

        public AgentController(IUserService userService, 
            IPropertyService propertieService,
            IHttpContextAccessor contextAccessor)
        {
            _propertyService = propertieService;
            _userService = userService;
            _contextAccessor = contextAccessor;
            user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        //GETALL PROPERTIES
        public async Task<IActionResult> Index()
        {
            return View(await _propertyService.GeAllWithIncludeByAgent());
        }

        //EDIT PROFILE
        public async Task<IActionResult> Profile()
        {
            return View(await _userService.GetById(user.Id));
        }

        [HttpPost]
        public async Task<IActionResult> Profile(SaveUserViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            var response = await _userService.UpdateUserAsync(vm);
            return View();
        }

        //ADDPROPERTIES
        public IActionResult AddProperties()
        {
            return View(new PropertyAddViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddProperties(PropertyAddViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            await _propertyService.Add(vm);
            return RedirectToAction("Index");
        }
    }
}
