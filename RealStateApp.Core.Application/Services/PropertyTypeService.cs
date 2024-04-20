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
        private readonly IPropertyService _propertyService;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IClientPropertyFavRepository _clientPropertyFavRepository;
        private readonly IPropertyImagesRepository _propertyImagesRepository;
        public PropertyTypeService(IPropertyTypeRepository repository,
            IMapper mapper, 
            IPropertyRepository propertyRepository,
            IPropertyService propertyService,
            IClientPropertyFavRepository clientPropertyFavRepository,
            IPropertyImagesRepository propertyImagesRepository) : base(repository, mapper)
        {
            _propertyService = propertyService;
            _mapper = mapper;
            _repository = repository;
            _propertyRepository = propertyRepository;
            _clientPropertyFavRepository = clientPropertyFavRepository;
            _propertyImagesRepository = propertyImagesRepository;
        }

        #region GetAllPropertiesCount

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

        #endregion

        public override async Task Remove(int Id)
        {
            var properties = await _propertyRepository.GetAllPropByPropType(Id);
            foreach (var item in properties)
            {
                await _propertyImagesRepository.RemoveImages(item.Id);
                await _clientPropertyFavRepository.RemoveByPropertyId(item.Id);
            }
            await _propertyRepository.RemoveRangeByPropertyType(Id);
            await base.Remove(Id);
        }
    }
}
