using AutoMapper;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.SellingTypes;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services
{
    public class SellingTypeService : GenericService<SellingTypeVeiwModel, SellingTypeAddViewModel, SellingType>, ISellingTypeService
    {
        private readonly IMapper _mapper;
        private readonly ISellingTypeRepository _repository;
        public SellingTypeService(ISellingTypeRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
    }
}
