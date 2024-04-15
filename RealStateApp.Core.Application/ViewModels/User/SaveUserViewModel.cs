using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        public string? Id {  get; set; }
        public bool? IsActive { get; set; }
        public IFormFile? FormFile { get; set; }

        [Required(ErrorMessage = "Please enter a First Name")]
        public string FirstName {  get; set; }

        [Required(ErrorMessage = "Please enter a LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please entar a UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please type in an email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please type in a Phone number")]
        [DataType(DataType.Text)]
        public string PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).+$" , ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords does not match")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
