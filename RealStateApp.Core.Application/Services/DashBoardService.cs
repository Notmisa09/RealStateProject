using RealStateApp.Core.Application.Enum;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.DashBoard;

namespace RealStateApp.Core.Application.Services
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IPropertyService _propertyService;
        private readonly IAccountService _accountService;
        
        public DashBoardService(IPropertyService propertyService, IAccountService accountService)
        {
            _accountService = accountService;
            _propertyService = propertyService;
        }

        public async Task<DashBoardViewModel> GetDashBoardInfo(DashBoardViewModel vm)
        {
            var PropertiesAmount = await _propertyService.GetAll();
            var Users = await _accountService.GetAllUsers();

            vm.AgentsInactive = Users.Where(x => x.IsActive == false && x.Roles.Contains(RolesEnum.Agent.ToString())).Count();
            vm.AgentsActive = Users.Where(x => x.IsActive == true && x.Roles.Contains(RolesEnum.Agent.ToString())).Count();

            vm.ClientInactive = Users.Where(x => x.IsActive == false && x.Roles.Contains(RolesEnum.Client.ToString())).Count();
            vm.ClientActive = Users.Where(x => x.IsActive == true && x.Roles.Contains(RolesEnum.Client.ToString())).Count();

            vm.DevInactive = Users.Where(x => x.IsActive == false && x.Roles.Contains(RolesEnum.Developer.ToString())).Count();
            vm.DevActive = Users.Where(x => x.IsActive == true && x.Roles.Contains(RolesEnum.Developer.ToString())).Count();

            vm.PropertiesRegisterAmount = PropertiesAmount.Count();

            return vm;
        }
    }
}
