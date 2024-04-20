using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Context;
using RealStateApp.Infrastructure.Persistence.Repository;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class PropertyRepository : BaseRepository<Properties>, IPropertyRepository
    {
        private readonly RealStateContext _context;
        public PropertyRepository(RealStateContext context) : base(context) { _context = context; }

        public async Task RemoveRange(string AgentId)
        {
           var properties = await _context.Properties.Where(x => x.AgentId == AgentId).ToListAsync();
            _context.RemoveRange(properties);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeByPropertyType(int T)
        {
            var properties = await _context.Properties.Where(x => x.PropertyTypeId == T).ToListAsync();
            _context.RemoveRange(properties);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeBySellingType(int T)
        {
            var properties = await _context.Properties.Where(x => x.SellingTypeId == T).ToListAsync();
            _context.RemoveRange(properties);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Properties>> GetAllPropByPropType(int PropertyTypeId)
        {
            var proplist = await _context.Properties.Where(x => x.PropertyTypeId == PropertyTypeId).ToListAsync();
            return proplist;
        }


        public async Task<List<Properties>> GetAllPropBySellingType(int SellingTypeId)
        {
            var proplist = await _context.Properties.Where(x => x.SellingTypeId == SellingTypeId).ToListAsync();
            return proplist;
        }
    }
}
