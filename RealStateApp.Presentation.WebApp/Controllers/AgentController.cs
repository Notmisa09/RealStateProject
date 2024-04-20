using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.ViewModels.Location;

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
        private readonly HttpClient _httpClient;

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
            ISellingTypeService sellingTypeService
            )
        {
            _improvementsService = improvementsService;
            _sellingTypeService = sellingTypeService;
            _propertyTypeService = propertyTypeService;
            _propertyService = propertieService;
            _userService = userService;
            _httpClient = new HttpClient(new HttpClientHandler());
            _contextAccessor = contextAccessor;
            user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        //GET PROVINCES
        private async Task<List<string>> GetProvinces()
        {
            LocationResponse baseres = new();
            using (var response = await _httpClient.GetAsync("https://api.digital.gob.do/v1/territories/provinces"))
            {
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        baseres = JsonConvert.DeserializeObject<LocationResponse>(content);
                        List<string> provincesName = new List<string>();
                        foreach (var item in baseres.Data)
                        {
                            provincesName.Add(item.Name);
                        }
                        return provincesName;
                    }
                }
                catch (Exception ex)
                {
                    baseres.HasError = true;
                    throw new Exception(ex.Message.ToString());
                }
            }
            return null;
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
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var response = await _userService.UpdateUserAsync(vm);
            return View();
        }
        
        //LIST PROPERTIES
        public async Task<IActionResult> PropertyList()
        {
            ViewBag.PropertyTypeList = await _propertyTypeService.GetAll();
            ViewBag.SellingTypeList = await _sellingTypeService.GetAll();
            ViewBag.Improvements = await _improvementsService.GetAll();
            ViewBag.Provinces = await GetProvinces();
            return View(await _propertyService.GeAllWithIncludeByAgent());
        }

        //ADDPROPERTIES
        public async Task<IActionResult> AddProperties()
        {
            ViewBag.PropertyTypeList = await _propertyTypeService.GetAll();
            ViewBag.SellingTypeList = await _sellingTypeService.GetAll();
            ViewBag.Improvements = await _improvementsService.GetAll();
            ViewBag.Provinces = await GetProvinces();

            return View("AddProperties", new PropertyAddViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddProperties(PropertyAddViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PropertyTypeList = await _propertyTypeService.GetAll();
                ViewBag.SellingTypeList = await _sellingTypeService.GetAll();
                ViewBag.Improvements = await _improvementsService.GetAll();
                ViewBag.Provinces = await GetProvinces();
                return View(vm);
            }
            var property = await _propertyService.Add(vm);
            if (property.HasError)
            {
                vm.HasError = true;
                vm.Error = property.Error;
                ViewBag.PropertyTypeList = await _propertyTypeService.GetAll();
                ViewBag.SellingTypeList = await _sellingTypeService.GetAll();
                ViewBag.Improvements = await _improvementsService.GetAll();
                ViewBag.Provinces = await GetProvinces();
                return View("AddProperties",vm);
            }
            return RedirectToAction("Index");
        }

        //EDITREALSTATE
        public async Task<IActionResult> Edit(int Id)
        {
            ViewBag.PropertyTypeList = await _propertyTypeService.GetAll();
            ViewBag.SellingTypeList = await _sellingTypeService.GetAll();
            ViewBag.Improvements = await _improvementsService.GetAll();
            ViewBag.Provinces = await GetProvinces();
            return View("AddProperties", await _propertyService.GetById(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PropertyAddViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PropertyTypeList = await _propertyTypeService.GetAll();
                ViewBag.SellingTypeList = await _sellingTypeService.GetAll();
                ViewBag.Improvements = await _improvementsService.GetAll();
                ViewBag.Provinces = await GetProvinces();
                return View("AddProperties",vm);
            }
            await _propertyService.Update(vm, vm.Id.Value);
            return RedirectToRoute(new { controller = "Agent", Action = "PropertyList" });
        }

        //CHANGESTATUS
        public async Task<IActionResult> ChangeStatus(string Id)
        {
            var adduservm = await _userService.GetById(Id);
            await _userService.ChangeUserStatus(adduservm);
            return RedirectToRoute(new { controller = "Admin" , action= "AgentList" });
        }

        //REMOVE AGENTS
        public async Task<IActionResult> Remove(string Id)
        {
            return View("Remove", await _userService.GetById(Id));
        }

        public async Task<IActionResult> RemoveTrue(string Id)
        {
            await _propertyService.RemoveAllByAgent(Id);
            await _userService.Remove(Id);
            return RedirectToRoute(new { controller = "Admin", action = "AgentList" });
        }
    }
}
