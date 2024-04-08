using Microsoft.AspNetCore.Mvc.Filters;
using RealStateApp.Presentation.WebApp.Controllers;

namespace RealStateApp.Presentation.WebApp.Middleware
{
    public class LoginAuthorize
    {
        private readonly ValidateUserSession _validateUserSession;
        public LoginAuthorize(ValidateUserSession validationUserSession)
        {
              _validateUserSession = validationUserSession;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_validateUserSession.HasUser())
            {
                var controller = (UserController)context.Controller;
                context.Result = controller.RedirectToAction("index", "home");
            }
            else
            {
                await next();
            }
        }
    }
}
