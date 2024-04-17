using Microsoft.AspNetCore.Identity;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infrastructure.Identity.Seeds
{
    public static class ClientUser
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            AppUser clientuser = new();
            clientuser.UserName = "clientuser";
            clientuser.Email = "clientuser@gmail.com";
            clientuser.Name = "client";
            clientuser.LastName = "user";
            clientuser.PhoneNumber = "829-123-9811";
            clientuser.EmailConfirmed = true;
            clientuser.PhoneNumberConfirmed = true;
            clientuser.IsActive = true;

            if (userManager.Users.All(u => u.Id != clientuser.Id))
            {
                var user = await userManager.FindByEmailAsync(clientuser.Email);
                if (user != null)
                {
                    await userManager.CreateAsync(clientuser, "123Pa$$word");
                    await userManager.AddToRoleAsync(clientuser, RolesEnum.Client.ToString());
                }
            }

        }
    }
}
