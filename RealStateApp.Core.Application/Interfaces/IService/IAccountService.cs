using RealStateApp.Core.Application.Dto.Acccount;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Dto.Acccount.ForgotPassword;
using RealStateApp.Core.Application.Dto.Acccount.Register;
using RealStateApp.Core.Application.Dto.Acccount.ResetPassword;

namespace RealStateApp.Core.Application.Interfaces.IService
{
    public interface IAccountService
    {
        Task<ServiceResult> ChangeUserStatus(RegisterRequest request);
        Task<ServiceResult> Update(RegisterRequest request);
        Task<DtoAccount> GetUserById(string Id);
        Task<List<DtoAccount>> GetAllUsers();
        Task<string> ConfirmAccountAysnc(string uesrId, string token);
        Task<ServiceResult> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<ServiceResult> ResetPasswordAsync(ResetPasswordRequest request);
        Task<AuthenticateResponseJWT> AuthenticateAysncAPI(AuthenticationRequest request);
        Task<ServiceResult> RegisterHighRolesUsers(RegisterRequest request, string RoleUser);
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<ServiceResult> RegisterLowRolesUser(RegisterRequest request, string origin, string UserRole);
        Task SignOutAync();
        Task<ServiceResult> Remove(string Id);
    }
}
