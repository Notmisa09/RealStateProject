using AutoMapper;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services
{
    public class PorpertyTypeService : GenericService<PropertyTypeViewModel, PropertyTypeAddViewModel, PropertyType>, IPropertyTypeService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<PropertyType> _repository;
        public PorpertyTypeService(IBaseRepository<PropertyType> repository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
    }
}
