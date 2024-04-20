using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Context;
using RealStateApp.Infrastructure.Persistence.Repository;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class ClientPropertyFavRepository : BaseRepository<ClientPropertyFav>, IClientPropertyFavRepository
    {
        private readonly RealStateContext _context;
        public ClientPropertyFavRepository(RealStateContext context) : base(context) {_context = context;}


        public async Task RemoveByPropertyId(int propertyId)
        {
            var favclient = _context.ClinetPropertyFav.Where(x => x.PropertyId == propertyId);
            _context.RemoveRange(favclient);
            await _context.SaveChangesAsync();
        }
    }
}
