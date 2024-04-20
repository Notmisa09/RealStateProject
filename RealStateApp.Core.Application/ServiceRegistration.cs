using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.Services;
using System.Reflection;
using MediatR;

namespace RealStateApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services , IConfiguration configuration)
        {
            #region Mediator

            services.AddMediatR(Assembly.GetExecutingAssembly());

            #endregion
            
            #region AutoMapper

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #endregion
          
            #region Services

            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPropertyTypeService , PropertyTypeService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<ISellingTypeService, SellingTypeService>();
            services.AddTransient<IimprovementsService, ImprovementsService>();
            services.AddTransient<IDashBoardService, DashBoardService>();

            #endregion
        
        }
    }
}
