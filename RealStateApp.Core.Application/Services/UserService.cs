using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Dto.Acccount;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Dto.Acccount.ForgotPassword;
using RealStateApp.Core.Application.Dto.Acccount.Register;
using RealStateApp.Core.Application.Dto.Acccount.ResetPassword;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.Filter;
using RealStateApp.Core.Application.ViewModels.User;

namespace RealStateApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IPropertyService _propertyService;
        private readonly AuthenticationResponse user;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(IAccountService accountService, IMapper mapper, 
            IPropertyService propertyService, IHttpContextAccessor contextAccesor)
        {
            _contextAccessor = contextAccesor;
            _propertyService = propertyService;
            _accountService = accountService;
            _mapper = mapper;
            user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<ServiceResult> Remove(string Id)
        {
            var result = await _accountService.Remove(Id);
            return result;
        }
        
        public async Task<ServiceResult> ChangeUserStatus(SaveUserViewModel vm)
        {
            var user = _mapper.Map<RegisterRequest>(vm);
            var response = await _accountService.ChangeUserStatus(user);
            return response;
        }


        public async Task<ServiceResult> UserRegisterSelector(SaveUserViewModel request, string Role, string origin = "")
        {
            if (Role.Contains(RolesEnum.Agent.ToString()) || Role.Contains(RolesEnum.Client.ToString()))
            {
                var response = await RegisterLowUserRoles(request, origin, Role);
                return response;
            }
            else
            {
                var response = await RegisterHighUserRoles(request, Role);
                return response;
            }
        }

        public async Task<ServiceResult> UpdateUserAsync(SaveUserViewModel vm)
        {
            ServiceResult response = new();
            var user = _mapper.Map<RegisterRequest>(vm);
            response = await _accountService.Update(user);
            return response;

        }

        public async Task<SaveUserViewModel> GetById(string Id)
        {
            var user = await _accountService.GetUserById(Id);
            var vm = _mapper.Map<SaveUserViewModel>(user);
            return vm;
        }


        public async Task<List<UserViewModel>> FilterForAgents(FilterUserViewModel vm , string Roles)
        {
            var list = await _accountService.GetAllUsers();
            list = list.Where(u => u.Roles.Contains(Roles) && u.IsActive == true)
                .OrderBy(x => x.FirstName).ToList();
            var newlist = _mapper.Map<List<UserViewModel>>(list);

            if (!string.IsNullOrEmpty(vm.AgentName))
            {
                newlist = newlist.Where(x => x.FirstName.Contains(vm.AgentName) || x.LastName.Contains(vm.AgentName)).ToList();
            }
            return newlist;
        }


        public async Task<List<UserViewModel>> GeAllByUsers(string Roles)
        {
            var list = await _accountService.GetAllUsers();
            list = list.Where(u => u.Roles.Contains(Roles) && u.IsActive == true)
                .OrderBy(x => x.FirstName).ToList();
            var newlist = _mapper.Map<List<UserViewModel>>(list);

            return newlist;
        }

        //GETALL FOR ADMIN
        public async Task<List<UserViewModel>> GetAdminUsers(string Roles)
        {
            var list = await _accountService.GetAllUsers();
            list = list.Where(u => u.Roles.Contains(Roles) && u.Id != user.Id)
            .OrderBy(x => x.FirstName).ToList();
            var newlist = _mapper.Map<List<UserViewModel>>(list);
            return newlist;
        }

        //GET ALL USER IGNORING THE USERSTATUS 
        public async Task<List<UserViewModel>> GetUsersIsActiveIgnore(string Roles)
        {
            var list = await _accountService.GetAllUsers();
            list = list.Where(u => u.Roles.Contains(Roles))
            .OrderBy(x => x.FirstName).ToList();
            var newlist = _mapper.Map<List<UserViewModel>>(list);

            if(Roles == RolesEnum.Agent.ToString())
            {
                foreach (var item in newlist)
                {
                    item.PropertiesAmount = await _propertyService.PropertiesCount(item.Id);
                }
            }
            return newlist;
        }

        //LOGIN
        public async Task<AuthenticationResponse> LoginAync(LoginViewModel vm)
        {
            AuthenticationRequest loginrequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginrequest);
            return userResponse;

        }
        
        //LOGOUT
        public async Task SignOutAsync()
        {
            await _accountService.SignOutAync();
        }

        //CONFIRMEMAILASYNC
        public async Task<string> ConfrimEmailAsync(string UserId, string token)
        {
            return await _accountService.ConfirmAccountAysnc(UserId, token);
        }
        
        //FORGOTPASSWORDASYNC
        public async Task<ServiceResult> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin)
        {
            ForgotPasswordRequest resetRequest = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _accountService.ForgotPasswordAsync(resetRequest, origin);
        }

        public async Task<ServiceResult> RegisterLowUserRoles(SaveUserViewModel vm, string origin, string UserRole)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            if (registerRequest != null && string.IsNullOrEmpty(registerRequest.Id))
            {
                if (vm.FormFile != null)
                {
                    registerRequest.ImageURL = FileHelpers.UploadFile(vm.FormFile, vm.UserName, "User", false);
                }
            }
            var result = await _accountService.RegisterLowRolesUser(registerRequest, origin, UserRole);
            return result;
        }

        public async Task<ServiceResult> RegisterHighUserRoles(SaveUserViewModel vm, string RoleUser)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            var result = await _accountService.RegisterHighRolesUsers(registerRequest, RoleUser);
            return result;
        }

        public async Task<string> ConfirmEmailAsync(string UserId, string token)
        {
            return await _accountService.ConfirmAccountAysnc(UserId, token);
        }

        public async Task<ServiceResult> ResetPasswordAsync(ResetPasswordViewModel vm)
        {
            ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(vm);
            return await _accountService.ResetPasswordAsync(resetRequest);
        }
    }
}
