using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.IRepository
{
    public interface IPropertyImprovementsRepository : IBaseRepository<PropertyImprovements>
    {
        Task RemoveUpdateWithUserId(int Id);
        List<int> GetImprovements(int Id);
    }
}
