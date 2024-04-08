using RealStateApp.Core.Application.Dto.Acccount;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Dto.Acccount.ForgotPassword;
using RealStateApp.Core.Application.ViewModels.User;

namespace RealStateApp.Core.Application.Interfaces.IService
{
    public interface IUserService
    {
        Task SignOutAsync();
        Task<AuthenticationResponse> LoginAync(LoginViewModel vm);
        Task<string> ConfrimEmailAsync(string UserId, string token);
        Task<ServiceResult> ForgotPasswordAsync(ForgotPasswordRequest vm, string origin);
        Task<ServiceResult> RegigsterAsync(SaveUserViewModel vm, string origin, string UserRole);
        Task<string> ConfirmEmailAsync(string UserId, string token);
        Task<ServiceResult> ResetPasswordAsync(ResetPasswordViewModel vm, string origin);
    }
}
