using RealStateApp.Core.Application.Services;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.IService
{
    public interface IPropertyTypeService : IGenericService<PropertyTypeViewModel, PropertyTypeAddViewModel, PropertyType>
    {
        Task<List<PropertyTypeViewModel>> GeallWithPropertiesAmount();
    }
}
