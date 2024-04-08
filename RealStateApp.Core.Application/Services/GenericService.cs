using AutoMapper;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IService;

namespace RealStateApp.Core.Application.Services
{
    public class GenericService<ViewModel, AddViewModel, Entity> : IGenericService<ViewModel, AddViewModel, Entity>
        where ViewModel : class
        where AddViewModel : class
        where Entity : class
    {
        private readonly IBaseRepository<Entity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IBaseRepository<Entity>  repository , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<AddViewModel> Add(ViewModel vm)
        {
            Entity entity = _mapper.Map<Entity>(vm);
            entity = await _repository.AddAsync(entity);
            throw new NotImplementedException();
        }

        public virtual async Task<List<ViewModel>> GeAll()
        {
           var list = await _repository.GetAllAsync();
           return _mapper.Map<List<ViewModel>>(list);
        }

        public virtual async Task<AddViewModel> GetById(int id)
        {
            Entity entity = await _repository.GetByIdAync(id);
            AddViewModel vm = _mapper.Map<AddViewModel>(entity);
            return vm;
        }

        public virtual async Task Remove(int Id)
        {
            var entity = await _repository.GetByIdAync(Id);
            await _repository.RemoveAsync(entity);
        }

        public virtual async Task Update(AddViewModel vm, int Id)
        {
            Entity entity = _mapper.Map<Entity>(vm);
            await _repository.UpdateAsync(entity, Id);
        }
    }
}
