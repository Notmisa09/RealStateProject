namespace RealStateApp.Core.Application.Interfaces.IService
{
    public interface IGenericService <ViewModel , AddViewModel , Entity> 
        where ViewModel : class
        where AddViewModel : class
        where Entity : class
    {
        Task<List<ViewModel>> GeAll();
        Task<AddViewModel> Add(AddViewModel vm);
        Task Update(AddViewModel vm, int Id);
        Task<AddViewModel> GetById (int id);
        Task Remove(int Id);

    }
}
