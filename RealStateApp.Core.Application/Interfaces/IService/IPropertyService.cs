using RealStateApp.Core.Application.ViewModels.Filter;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.IService
{
    public interface IPropertyService : IGenericService<PropertyViewModel, PropertyAddViewModel , Properties>
    {

        Task RemoveAllByAgent(string Id);
        List<string> GetImagesForProperties(int Id);
        Task<List<PropertyViewModel>> GeAllWithFilterInclude(FilterViewModel vm);
        Task<List<PropertyAddViewModel>> GetAllByUserId(string Id);
        Task RemoveFavProp(int Id);
        Task<List<PropertyViewModel>> GetAllFav();
        Task AddFavProp(PropertyAddViewModel vm);
        Task<List<PropertyAddViewModel>> GetPropertyById(int Id);
        Task<int> PropertiesCount(string Id);
        Task<List<PropertyAddViewModel>> GeAllWithIncludeByAgent();
        Task<List<PropertyViewModel>> GeAllWithInclude();
        Task<List<PropertyAddViewModel>> GetPropertyByCode(string Code);
    }
}
