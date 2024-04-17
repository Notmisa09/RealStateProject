using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Context;
using RealStateApp.Infrastructure.Persistence.Repositories;

namespace RealStateApp.Infrastructure.Persistence.Interfaces
{
    public class PropertyImprovementsRepository : BaseRepository<PropertyImprovements> , IPropertyImprovementsRepository
    {
        private readonly RealStateContext _context;
        public PropertyImprovementsRepository(RealStateContext context) : base(context) { _context = context; }

        public List<int> GetImprovements(int Id)
        {
           List<int> PropertyImprovements = new();
           var property =  _context.PropertyImprovements.Where(x => x.PropertyId == Id).ToList();
            foreach (var item in property)
            {
                PropertyImprovements.Add(item.ImprovementId);
            }
            return PropertyImprovements;
        }

    }
}
