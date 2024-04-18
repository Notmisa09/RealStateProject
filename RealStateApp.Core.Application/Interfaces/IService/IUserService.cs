using RealStateApp.Core.Application.Dto.Acccount;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.ViewModels.Filter;
using RealStateApp.Core.Application.ViewModels.User;

namespace RealStateApp.Core.Application.Interfaces.IService
{
    public interface IUserService
    {
        Task<List<UserViewModel>> FilterForAgents(FilterUserViewModel vm, string Roles);
        Task<List<UserViewModel>> GetAdminUsers(string Roles);
        Task<List<UserViewModel>> GetUsersIsActiveIgnore(string Roles);
        Task<ServiceResult> Remove(string Id);
        Task<ServiceResult> ChangeUserStatus(SaveUserViewModel vm);
        Task<ServiceResult> UserRegisterSelector(SaveUserViewModel request, string Role, string origin = "");
        Task<ServiceResult> UpdateUserAsync(SaveUserViewModel vm);
        Task<SaveUserViewModel> GetById(string Id);
        Task<List<UserViewModel>> GeAllByUsers(string Roles);
        Task SignOutAsync();
        Task<ServiceResult> RegisterHighUserRoles(SaveUserViewModel vm, string RoleUser);
        Task<AuthenticationResponse> LoginAync(LoginViewModel vm);
        Task<string> ConfrimEmailAsync(string UserId, string token);
        Task<ServiceResult> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin);
        Task<ServiceResult> RegisterLowUserRoles(SaveUserViewModel vm, string origin, string UserRole);
        Task<string> ConfirmEmailAsync(string UserId, string token);
        Task<ServiceResult> ResetPasswordAsync(ResetPasswordViewModel vm);
    }
}
