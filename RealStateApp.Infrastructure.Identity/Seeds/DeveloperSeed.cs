using Microsoft.AspNetCore.Identity;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Infrastructure.Identity.Entities;

namespace RealStateApp.Infrastructure.Identity.Seeds
{
    public static class DeveloperSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            AppUser developerUser = new();
            developerUser.UserName = "developer";
            developerUser.Email = "developer@email.com";
            developerUser.Name = "John";
            developerUser.LastName = "Doe";
            developerUser.PhoneNumber = "829-456-7890";
            developerUser.EmailConfirmed = true;
            developerUser.PhoneNumberConfirmed = true;
            developerUser.IsActive = true;
            developerUser.Identification = "1-1911-9112";


            if (userManager.Users.All(u => u.Id != developerUser.Id))
            { 
                var user = await userManager.FindByEmailAsync(developerUser.Email);
                if(user == null)
                {
                    await userManager.CreateAsync(developerUser, "123Pa$$word");
                    await userManager.AddToRoleAsync(developerUser, RolesEnum.Developer.ToString());
                }
            }
        }
    }
}
