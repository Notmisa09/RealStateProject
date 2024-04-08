namespace RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos
{
    public class AuthenticationRequest
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}