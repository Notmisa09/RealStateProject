using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.IRepository
{
    public interface IPropertyImagesRepository : IBaseRepository<PropertyImages>
    {
        Task RemoveImages(int PropertyId);
        string GetFirstImage(int PropertyId);
    }
}
