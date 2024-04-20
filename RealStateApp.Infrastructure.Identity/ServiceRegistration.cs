using Autofac.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Infrastructure.Identity.Context;
using RealStateApp.Infrastructure.Identity.Entities;
using RealStateApp.Infrastructure.Identity.Services;

namespace RealStateApp.Infrastructure.Identity
{
    public static class ServiceRegistration
    {
        public  static void AddIdentityLayer(this IServiceCollection service , IConfiguration configuration)
        {
            #region Identity 
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                service.AddDbContext<RealStateIdentityContext>(options => options.UseInMemoryDatabase("UserInMemoryIdentityDatabase"));
            }
            else
            {
                service.AddDbContext<RealStateIdentityContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                       m => m.MigrationsAssembly(typeof(RealStateIdentityContext).Assembly.FullName));
                });
            }

            service.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<RealStateIdentityContext>().AddDefaultTokenProviders();

            service.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User";
                options.AccessDeniedPath = "/User/AccessDenied";
            });
            service.AddAuthentication();
            #endregion

            #region Dependencies
            service.AddTransient<IAccountService, AccountService>();
            #endregion
        }
    }
}
