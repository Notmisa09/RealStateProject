using Microsoft.AspNetCore.Identity;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Infrastructure.Identity.Entities;

namespace RealStateApp.Infrastructure.Identity.Seeds
{
    public class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(RolesEnum.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(RolesEnum.Client.ToString()));
            await roleManager.CreateAsync(new IdentityRole(RolesEnum.Developer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(RolesEnum.Agent.ToString()));
        }
    }
}
