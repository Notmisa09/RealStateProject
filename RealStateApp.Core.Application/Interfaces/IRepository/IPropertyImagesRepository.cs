using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.IRepository
{
    public interface IPropertyImagesRepository : IBaseRepository<PropertyImages>
    {
        List<string> GetAllImages(int PropertyId);
        Task RemoveImages(int PropertyId);
        string GetFirstImage(int PropertyId);
    }
}
