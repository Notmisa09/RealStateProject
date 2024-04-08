using Microsoft.AspNetCore.Identity;

namespace RealStateApp.Infrastructure.Identity.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName {  get; set; }   
        public string? ImageURl {  get; set; }
        public bool IsActive { get; set; } = false;
    }
}
