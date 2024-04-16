using AutoMapper;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services
{
    public class PropertyTypeService : GenericService<PropertyTypeViewModel, PropertyTypeAddViewModel, PropertyType>, IPropertyTypeService
    {
        private readonly IMapper _mapper;
        private readonly IPropertyTypeRepository _repository;
        private readonly IPropertyRepository _propertyRepository;
        public PropertyTypeService(IPropertyTypeRepository repository, IMapper mapper, IPropertyRepository propertyRepository) : base(repository, mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _propertyRepository = propertyRepository;
        }

        public async Task<List<PropertyTypeViewModel>> GeallWithPropertiesAmount()
        {
            var properties = await _propertyRepository.GetAllAsync();
            var propertyType = await GetAll();
            List<PropertyTypeViewModel> propertyTypelist = new();
            foreach (var item in propertyType)
            {
                var propertyCount = properties.Where(x => x.PropertyTypeId == item.Id).Count();
                var propertyTypeWithCount = new PropertyTypeViewModel
                {
                    Id = item.Id,
                    PropertyTypeName = item.PropertyTypeName,
                    Description = item.Description,
                    PropertiesAmount = propertyCount
                };

                propertyTypelist.Add(propertyTypeWithCount);
            }
            return propertyTypelist;
        }
    }
}
