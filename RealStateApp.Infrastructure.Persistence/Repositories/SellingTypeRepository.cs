using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Context;
using RealStateApp.Infrastructure.Persistence.Repository;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class SellingTypeRepository : BaseRepository<SellingType>, ISellingTypeRepository
    {
        private readonly RealStateContext _context;
        public SellingTypeRepository(RealStateContext context) : base(context) { _context = context; }
    }
}
