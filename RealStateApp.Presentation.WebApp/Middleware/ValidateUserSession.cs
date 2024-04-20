using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Helpers;

namespace RealStateApp.Presentation.WebApp.Middleware
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            AuthenticationResponse authenticationResponse = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

            if (authenticationResponse == null)
            {
                return false;
            }
            return true;
        }
    }
}
