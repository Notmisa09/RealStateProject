using AutoMapper;
using RealStateApp.Core.Application.Dto.Acccount;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Dto.Acccount.ForgotPassword;
using RealStateApp.Core.Application.Dto.Acccount.Register;
using RealStateApp.Core.Application.Dto.Acccount.ResetPassword;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.User;

namespace RealStateApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<List<UserViewModel>> GeAllByUsers(string Roles)
        {
           var list = await _accountService.FilterByUser(Roles);
           var newlist = _mapper.Map<List<UserViewModel>>(list);
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

        public async Task<string> ConfrimEmailAsync(string UserId , string token)
        {
            return await _accountService.ConfirmAccountAysnc(UserId , token);
        }

        public async Task<ServiceResult> ForgotPasswordAsync(ForgotPasswordRequest vm , string origin)
        {
            ForgotPasswordRequest resetRequest = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _accountService.ForgotPasswordAsync(resetRequest, origin);
        }

        public async Task<ServiceResult> RegigsterAsync(SaveUserViewModel vm , string origin , string UserRole)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            var result = await _accountService.RegisterLowRolesUser(registerRequest, origin, UserRole);
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
