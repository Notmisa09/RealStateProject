using Microsoft.AspNetCore.Identity;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Infrastructure.Identity.Entities;

namespace RealStateApp.Infrastructure.Identity.Seeds
{
    public static class AdminSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            AppUser adminuser = new();
            adminuser.UserName = "adminuser";
            adminuser.Name = "admin";
            adminuser.LastName = "user";
            adminuser.PhoneNumber = "809-115-1941";
            adminuser.PhoneNumberConfirmed = true;
            adminuser.Email = "adminuser@gmail.com";
            adminuser.EmailConfirmed = true;
            adminuser.IsActive = true;
            adminuser.Identification = "4-3235-1618";


            if (userManager.Users.All(u => u.Id != adminuser.Id))
            {
                var user = await userManager.FindByEmailAsync(adminuser.Email);
                if(user == null)
                {
                    await userManager.CreateAsync(adminuser, "123Pa$$word");
                    await userManager.AddToRoleAsync(adminuser, RolesEnum.Admin.ToString());
                }
            }
            
        }
    }
}
