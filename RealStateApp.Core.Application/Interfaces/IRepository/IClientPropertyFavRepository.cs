using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.IRepository
{
    public interface IClientPropertyFavRepository : IBaseRepository<ClientPropertyFav>
    {
        Task RemoveByPropertyId(int propertyId);
    }
}
