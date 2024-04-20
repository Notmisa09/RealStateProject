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
        private readonly IPropertyImagesRepository _propertyImagesRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IClientPropertyFavRepository _clientPropertyFavRepository;

        public SellingTypeService(ISellingTypeRepository repository, IMapper mapper, 
            IPropertyImagesRepository propertyImagesRepository, 
            IPropertyRepository propertyRepository,
            IClientPropertyFavRepository clientPropertyFavRepository) : base(repository, mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _propertyImagesRepository = propertyImagesRepository;
            _propertyRepository = propertyRepository;
            _clientPropertyFavRepository = clientPropertyFavRepository;
        }

        public override async Task Remove(int Id)
        {
            var properties = await _propertyRepository.GetAllPropBySellingType(Id);
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
