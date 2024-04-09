using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.Services;
using System.Reflection;

namespace RealStateApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPropertyTypeService , PropertyTypeService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<ISellingTypeService, SellingTypeService>();
            services.AddTransient<IimprovementsService, ImprovementsService>();
        }
    }
}
