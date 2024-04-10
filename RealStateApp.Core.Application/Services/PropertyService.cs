using AutoMapper;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services
{
    public class PropertyService : GenericService<PropertyViewModel, PropertyAddViewModel, Properties>, IPropertyService
    {
        private readonly IMapper _mapper;
        private readonly IPropertyRepository _repository;

        public PropertyService(IPropertyRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public override async Task<PropertyAddViewModel> Add(PropertyAddViewModel vm)
        {
            PropertyAddViewModel propertie = new();
            do
            {
                vm.Id = CodeGenerator.GenerateCode(vm.Id);
                propertie = await GetById(vm.Id);
            } while (propertie == null);
            return await base.Add(vm);
        }

    }
}
