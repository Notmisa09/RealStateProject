using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.IRepository
{
    public interface IPropertyImprovementsRepository : IBaseRepository<PropertyImprovements>
    {
        List<int> GetImprovements(int Id);
    }
}
