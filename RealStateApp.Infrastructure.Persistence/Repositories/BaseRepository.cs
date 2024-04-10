using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Infrastructure.Persistence.Context;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly RealStateContext _context;
        private DbSet<T> entity;
        public BaseRepository(RealStateContext context)
        {
                _context = context;
                entity =  _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> GetAllWithInclude(List<string> properties)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var property in properties)
            {
                query = query.Include(property);
            }
            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
           return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAync(dynamic Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public async Task RemoveAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity, int Id)
        {
            var entry = _context.Set<T>().FindAsync(Id);
            _context.Entry(entry).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();  
        }
    }
}
