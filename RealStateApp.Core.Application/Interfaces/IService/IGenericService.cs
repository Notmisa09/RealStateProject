namespace RealStateApp.Core.Application.Interfaces.IService
{
    public interface IGenericService <ViewModel , AddViewModel , Entity> 
        where ViewModel : class
        where AddViewModel : class
        where Entity : class
    {
        Task<List<ViewModel>> GetAll();
        Task<AddViewModel> Add(AddViewModel vm);
        Task Update(AddViewModel vm, int Id);
        Task<AddViewModel> GetById (dynamic id);
        Task Remove(int Id);

    }
}
