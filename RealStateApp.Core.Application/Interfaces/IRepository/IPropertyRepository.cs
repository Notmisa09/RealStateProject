using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.IRepository
{
    public interface IPropertyRepository : IBaseRepository<Properties>
    {
        Task<List<Properties>> GetAllPropBySellingType(int Id);
        Task<List<Properties>> GetAllPropByPropType(int Id);
        Task RemoveRange(string AgentId);
        Task RemoveRangeBySellingType(int T);
        Task RemoveRangeByPropertyType(int T);
    }
}
