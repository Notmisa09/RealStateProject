using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.IService
{
    public interface IPropertyService : IGenericService<PropertyViewModel, PropertyAddViewModel , Properties>
    {
        Task RemoveFavProp(int Id);
        Task<List<PropertyViewModel>> GetAllFav();
        Task AddFavProp(PropertyAddViewModel vm);
        Task<List<PropertyAddViewModel>> GetPropertyById(int Id);
        Task<int> PropertiesCount(string Id);
        Task<List<PropertyAddViewModel>> GeAllWithIncludeByAgent();
        Task<List<PropertyViewModel>> GeAllWithInclude();
        List<string> GetImagesForProperties(int Id);
    }
}
