using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Context;
using RealStateApp.Infrastructure.Persistence.Repositories;
using RealStateApp.Infrastructure.Persistence.Repository;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class PropertyRepository : BaseRepository<Properties>, IPropertyRepository
    {
        private readonly RealStateContext _context;
        public PropertyRepository(RealStateContext context) : base(context) { _context = context; }
    }
}
