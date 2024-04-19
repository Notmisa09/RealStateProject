using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Porfavor introduzca su correo electronico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Porfavor su contraseña")]
        public string Password { get; set; }
        public bool HasError { get; set; } = false;
        public string? Error { get; set; }
    }
}
