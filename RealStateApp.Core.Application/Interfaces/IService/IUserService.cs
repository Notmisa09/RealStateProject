using RealStateApp.Core.Application.Dto.Acccount;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Dto.Acccount.ForgotPassword;
using RealStateApp.Core.Application.Dto.Acccount.Register;
using RealStateApp.Core.Application.ViewModels.User;

namespace RealStateApp.Core.Application.Interfaces.IService
{
    public interface IUserService
    {
        Task<ServiceResult> UpdateUserAsync(SaveUserViewModel vm);
        Task<SaveUserViewModel> GetById(string Id);
        Task<List<UserViewModel>> GeAllByUsers(string Roles);
        Task SignOutAsync();
        Task<AuthenticationResponse> LoginAync(LoginViewModel vm);
        Task<string> ConfrimEmailAsync(string UserId, string token);
        Task<ServiceResult> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin);
        Task<ServiceResult> RegigsterAsync(SaveUserViewModel vm, string origin, string UserRole);
        Task<string> ConfirmEmailAsync(string UserId, string token);
        Task<ServiceResult> ResetPasswordAsync(ResetPasswordViewModel vm);
    }
}
