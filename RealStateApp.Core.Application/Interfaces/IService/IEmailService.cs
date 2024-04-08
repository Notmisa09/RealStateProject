using RealStateApp.Core.Application.Dto.Email;

namespace RealStateApp.Core.Application.Interfaces.IService
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
