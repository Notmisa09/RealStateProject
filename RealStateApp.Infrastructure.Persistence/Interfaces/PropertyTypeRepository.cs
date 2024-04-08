using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Context;
using RealStateApp.Infrastructure.Persistence.Repositories;

namespace RealStateApp.Infrastructure.Persistence.Interfaces
{
    public class PropertyTypeRepository : BaseRepository<PropertyType> , IPropertyTypeRepository
    {
        private readonly RealStateContext _context;
        public PropertyTypeRepository(RealStateContext context) : base(context) { _context = context; }
    }
}
