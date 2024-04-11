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
    public static class AgentSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AppUser agent = new();
            agent.UserName = "agent";
            agent.Name = "agent";
            agent.LastName = "user";
            agent.PhoneNumber = "809-225-1541";
            agent.PhoneNumberConfirmed = true;
            agent.Email = "agent@gmail.com";
            agent.EmailConfirmed = true;
            agent.IsActive = true;

            if (userManager.Users.All(u => u.Id != agent.Id))
            {
                var user = await userManager.FindByEmailAsync(agent.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(agent, "123Pa$$word");
                    await userManager.AddToRoleAsync(agent, RolesEnum.Agent.ToString());
                }
            }
        }
    }
}
