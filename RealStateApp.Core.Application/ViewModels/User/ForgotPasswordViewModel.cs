using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.ViewModels.User
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Please type in an email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool HasError { get; set; } = false;
        public string? Error { get; set; }
    }
}
