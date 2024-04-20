using Microsoft.AspNetCore.Mvc.Filters;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Presentation.WebApp.Controllers;

namespace RealStateApp.Presentation.WebApp.Middleware
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        private readonly ValidateUserSession _validateUserSession;
        private readonly AuthenticationResponse user;
        private readonly IHttpContextAccessor _contextAccessor;
        public LoginAuthorize(ValidateUserSession validateUserSession, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _validateUserSession = validateUserSession;
            user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_validateUserSession.HasUser())
            {
                var controller = (UserController)context.Controller;
                if(user.Roles.Contains(RolesEnum.Admin.ToString()))
                {
                    context.Result = controller.RedirectToAction("DashBoard", "Admin");
                }
                if (user.Roles.Contains(RolesEnum.Client.ToString()))
                {
                    context.Result = controller.RedirectToAction("Index", "Client");
                }
                if (user.Roles.Contains(RolesEnum.Agent.ToString()))
                {
                    context.Result = controller.RedirectToAction("Index", "Agent");
                }
            }
            else
            {
                await next();
            }
        }
    }
}
