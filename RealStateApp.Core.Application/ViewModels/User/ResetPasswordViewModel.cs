using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.ViewModels.User
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Porfavor digite un correo electronico")]
        public string Email { get; set; }
        public string Token { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).+$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords does not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
