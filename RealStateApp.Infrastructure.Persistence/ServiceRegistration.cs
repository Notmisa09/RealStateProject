using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Infrastructure.Persistence.Context;
using RealStateApp.Infrastructure.Persistence.Repositories;

namespace RealStateApp.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void InfraStructureLayer(this IServiceCollection services , IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("InMemoryDtatabase"))
            {
                services.AddDbContext<RealStateContext>(options => options.UseInMemoryDatabase("UseInMemoryDatabase"));
            }
            else
            {
                services.AddDbContext<RealStateContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                m => m.MigrationsAssembly(typeof(RealStateContext).Assembly.FullName)));
            }

            #region Injecions
            services.AddTransient<IimprovementsRepository, ImprovementsRepository>();
            services.AddTransient<IPropertyRepository, PropertyRepository>();
            services.AddTransient<ISellingTypeRepository, SellingTypeRepository>();
            services.AddTransient<IPropertyTypeRepository, PropertyTypeRepository>();
            services.AddTransient<IPropertyImagesRepository, PropertyImagesRepository>();
            services.AddTransient<IClientPropertyFavRepository,  ClientPropertyFavRepository>();   
            services.AddTransient<IPropertyImprovementsRepository, PropertyImprovementsRepository>();
            #endregion

        }
    }
}
