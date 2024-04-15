using AutoMapper;
using RealStateApp.Core.Application.Dto.Acccount;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Dto.Acccount.ForgotPassword;
using RealStateApp.Core.Application.Dto.Acccount.Register;
using RealStateApp.Core.Application.Dto.Acccount.ResetPassword;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.User;

namespace RealStateApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IPropertyService _propertyService;

        public UserService(IAccountService accountService, IMapper mapper, IPropertyService propertyService)
        {
            _propertyService = propertyService;
            _accountService = accountService;
            _mapper = mapper;
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

        public async Task<List<UserViewModel>> GeAllByUsers(string Roles)
        {
            var list = await _accountService.GetAllUsers();
            list = list.Where(u => u.Roles.Contains(Roles) && u.IsActive == true)
                .OrderBy(x => x.FirstName).ToList();
            var newlist = _mapper.Map<List<UserViewModel>>(list);
            return newlist;
        }

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

        public async Task<AuthenticationResponse> LoginAync(LoginViewModel vm)
        {
            AuthenticationRequest loginrequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginrequest);
            return userResponse;

        }
        public async Task SignOutAsync()
        {
            await _accountService.SignOutAync();
        }

        public async Task<string> ConfrimEmailAsync(string UserId, string token)
        {
            return await _accountService.ConfirmAccountAysnc(UserId, token);
        }

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
