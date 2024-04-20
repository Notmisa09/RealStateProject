using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos
{
    public class AuthenticationRequest
    {
        [SwaggerParameter(Description = "User email for login")]
        public string Email { get; set; }
        [SwaggerParameter(Description = "User password for login")]
        public string Password { get; set; }
    }
}