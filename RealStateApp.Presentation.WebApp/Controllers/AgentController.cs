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
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly ISellingTypeService _sellingTypeService;
        private readonly IimprovementsService _improvementsService;

        public AgentController(IUserService userService,
            //
            IPropertyService propertieService,
            //
            IHttpContextAccessor contextAccessor,
            //
            IPropertyTypeService propertyTypeService,
            //
            IimprovementsService improvementsService,
            //
            ISellingTypeService sellingTypeService)
        {
            _improvementsService = improvementsService;
            _sellingTypeService = sellingTypeService;
            _propertyTypeService = propertyTypeService;
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
        public async Task<IActionResult> AddProperties()
        {
            ViewBag.PropertyTypeList = await _propertyTypeService.GetAll();
            ViewBag.SellingTypeList = await _sellingTypeService.GetAll();
            ViewBag.Improvements = await _improvementsService.GetAll();
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
