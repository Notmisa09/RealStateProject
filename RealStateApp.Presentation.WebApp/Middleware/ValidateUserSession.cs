using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Helpers;

namespace RealStateApp.Presentation.WebApp.Middleware
{
    public class ValidateUserSession
    {
        private IHttpContextAccessor _contextAccessor;

        public ValidateUserSession(IHttpContextAccessor  contextAccessor)
        {
                _contextAccessor = contextAccessor;
        }

        public bool HasUser()
        {
            AuthenticationResponse userViewModel = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

            if (userViewModel == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
