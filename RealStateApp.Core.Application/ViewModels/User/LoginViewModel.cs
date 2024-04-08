namespace RealStateApp.Core.Application.ViewModels.User
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool HasError {  get; set; }
        public string? Error { get; set; }
    }
}
