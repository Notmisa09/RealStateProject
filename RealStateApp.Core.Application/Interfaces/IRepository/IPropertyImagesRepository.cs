using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.IRepository
{
    public interface IPropertyImagesRepository : IBaseRepository<PropertyImages>
    {
        string GetFirstImage(int PropertyId);
        List<string> GetImagesForProperties(int PropertyId);
    }
}
