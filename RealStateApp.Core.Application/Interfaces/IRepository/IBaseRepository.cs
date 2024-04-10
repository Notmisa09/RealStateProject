namespace RealStateApp.Core.Application.Interfaces.IRepository
{
    public interface IBaseRepository <T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAync(dynamic Id);
        Task RemoveAsync(T entity);   
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity, int Id);
    }
}
