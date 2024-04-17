using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Dto.Acccount;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
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
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AuthenticationResponse user;
        private readonly IPropertyImprovementsRepository _propimprovemetns; 
        private readonly IPropertyImagesRepository _imagesrepository;

        public PropertyService(IPropertyRepository repository,
            IMapper mapper, 
            IHttpContextAccessor contextAccesor,
            IPropertyImprovementsRepository propimprovemetns,
            IPropertyImagesRepository imagesrepository) : base(repository, mapper)
        {
            _imagesrepository = imagesrepository;
            _propimprovemetns = propimprovemetns;
            _mapper = mapper;
            _repository = repository;
            _contextAccessor = contextAccesor;
            user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public override async Task<PropertyAddViewModel> Add(PropertyAddViewModel vm)
        {

            if (vm.formFile != null)
            {
                await AddImages(vm);
            }
            
            PropertyAddViewModel propertie = new();
            do
            {
                vm.Id = CodeGenerator.GenerateCode(vm.Id);
                propertie = await GetById(vm.Id);

            } while (propertie != null);

            vm.AgentId = user.Id;
            vm.AgentEmail = user.Email;

            var property = await base.Add(vm);
            await AddImprovements(property);

            return property;

        }



        private async Task<ServiceResult> AddImages(PropertyAddViewModel vm)
        {
            ServiceResult result = new();
            if (vm.formFile.Count() > 4)
            {
                result.HasError = true;
                result.Error = "La cantidad de imagenes excede el limite";
                return result; 
            }
            else
            {
                foreach (var item in vm.formFile)
                {
                    var image = FileHelpers.UploadFile(item, vm.Id ,"Properties", false);
                    PropertyImages images = new()
                    {
                        ImageURL = image,
                        PropertyId = vm.Id
                    };
                    await _imagesrepository.AddAsync(images);
                }
            }
            return result;
        }

        private async Task AddImprovements(PropertyAddViewModel vm)
        {
            PropertyImprovements propimprovements = new();

            foreach (var item in vm.Improvements)
            {
                propimprovements.ImprovementId = item;
                propimprovements.PropertyId = vm.Id;

                await _propimprovemetns.AddAsync(propimprovements);
            }
        }

        public async Task<int> PropertiesCount(string Id)
        {
            var properties = await _repository.GetAllAsync();
            var count = properties.Where(x => x.AgentId == Id).Count();
            return count;
        }

        public async Task<List<PropertyAddViewModel>> GeAllWithIncludeByAgent()
        {
            var list = await _repository.GetAllWithInclude(new List<string> { "PropertyType", "SellingType" });
            return list.Where(x => x.AgentId == user.Id).Select(x => new PropertyAddViewModel
            {
                Id = x.Id,
                AgentEmail = x.AgentEmail,
                AgentPhoneNumber = x.AgentPhoneNumber,
                AgentId = x.AgentId,
                PropertyTypeName = x.PropertyType.PropertyTypeName,
                SellingTypeName = x.SellingType.SellingTypeName,
                Location = x.Location,
                SellingTypeId = x.SellingTypeId,
                PropertyTypeId = x.PropertyTypeId,
                BathroomsAmount = x.BathroomsAmount,
                BedroomsAmount = x.BedroomsAmount,
                Description = x.Description,
                Meters = x.Meters,
                Price = x.Price,

            }).ToList();
        }

        public async Task<List<PropertyViewModel>> GeAllWithInclude()
        {
            var list = await _repository.GetAllWithInclude(new List<string> { "PropertyType", "SellingType" });
            return list.Select(x => new PropertyViewModel
            {
                Id = x.Id,
                AgentEmail = x.AgentEmail,
                Location = x.Location,
                AgentPhoneNumber = x.AgentPhoneNumber,
                AgentId = x.AgentId,
                PropertyTypeName = x.PropertyType.PropertyTypeName,
                SellingTypeName = x.SellingType.SellingTypeName,
                SellingTypeId = x.SellingTypeId,
                PropertyTypeId = x.PropertyTypeId,
                BathroomsAmount = x.BathroomsAmount,
                BedroomsAmount = x.BedroomsAmount,
                Description = x.Description,
                Meters = x.Meters,
                Price = x.Price,
                FrontImage = _imagesrepository.GetFirstImage(x.Id)
            }).ToList();
        }
    }
}
