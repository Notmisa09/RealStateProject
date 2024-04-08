namespace RealStateApp.Core.Application.ViewModels.User
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
