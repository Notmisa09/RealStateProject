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
        private readonly IClientPropertyFavRepository _clientpropertyfavrepository;

        public PropertyService(IPropertyRepository repository,
            IMapper mapper, 
            IHttpContextAccessor contextAccesor,
            IPropertyImprovementsRepository propimprovemetns,
            IPropertyImagesRepository imagesrepository,
            IClientPropertyFavRepository clientpropertyfavrepository) : base(repository, mapper)
        {
            _imagesrepository = imagesrepository;
            _propimprovemetns = propimprovemetns;
            _mapper = mapper;
            _repository = repository;
            _contextAccessor = contextAccesor;
            user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _clientpropertyfavrepository = clientpropertyfavrepository;
        }

        public override async Task<PropertyAddViewModel> Add(PropertyAddViewModel vm)
        {    
            PropertyAddViewModel propertie = new();
            do
            {
                vm.PropertyCode = CodeGenerator.GenerateCode(vm.PropertyCode);
                propertie = await GetById(vm.Id);

            } while (propertie != null);

            vm.AgentId = user.Id;
            vm.AgentEmail = user.Email;
            vm.Id = 0;

            var property = await base.Add(vm);
            vm.Id = property.Id;

            if (vm.formFile != null)
            {
               var result =  await AddImages(vm);
            }

            if(vm.Improvements != null)
            {
                await AddImprovements(vm);
            }
            return property;
        }

        //UPDATE
        public override async Task Update(PropertyAddViewModel vm, int Id)
        {
            if(vm.Improvements != null)
            {
                await _propimprovemetns.RemoveUpdateWithUserId(Id);
                await AddImprovements(vm);
            }
            if(vm.formFile != null)
            {
                await AddImages(vm);
            }
            await base.Update(vm, Id);
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
                        PropertyId = vm.Id,
                    };
                    await _imagesrepository.AddAsync(images);
                }
            }
            return result;
        }

        //ADDFAVPROPERTIES
        public async Task AddFavProp(PropertyAddViewModel vm)
        {
            ClientPropertyFav favprop = new();
            favprop.PropertyId = vm.Id;
            favprop.UserId = user.Id;
            await _clientpropertyfavrepository.AddAsync(favprop);
        }

        //REMOVEFAPROPERTIES
        public async Task RemoveFavProp(int Id)
        {
           var prop = await _clientpropertyfavrepository.GetByIdAync(Id);
           await _clientpropertyfavrepository.RemoveAsync(prop);
        }

        //ADDIMPROVEMENTS
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

        //PROPERTIESCOUNTER
        public async Task<int> PropertiesCount(string Id)
        {
            var properties = await _repository.GetAllAsync();
            var count = properties.Where(x => x.AgentId == Id).Count();
            return count;
        }


        //GETALLFAVPROPERTIES

        public async Task<List<PropertyViewModel>> GetAllFav()
        {
            var fav = await _clientpropertyfavrepository.GetAllAsync();
            var favnewlist = fav.Where(x => x.UserId == user.Id).ToList();
            var list = await _repository.GetAllWithInclude(new List<string> { "PropertyType", "SellingType" });
            List<PropertyViewModel> prop = new();

            foreach (var item in favnewlist)
            {
                var newlist = list.Where(x => x.Id == item.PropertyId).Select(x => new PropertyViewModel
                {
                    Id = x.Id,
                    favprop = item.Id,
                    PropertyCode = x.PropertyCode,
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
                    FrontImage = _imagesrepository.GetFirstImage(x.Id)
                }).FirstOrDefault();

                prop.Add(newlist);
            }

            return prop;
        } 


        //GETBYID
        public async Task<List<PropertyAddViewModel>> GeAllWithIncludeByAgent()
        {
            var list = await _repository.GetAllWithInclude(new List<string> { "PropertyType", "SellingType" });
            return list.Where(x => x.AgentId == user.Id).OrderBy(x => x.CreatedDate).Select(x => new PropertyAddViewModel
            {
                Id = x.Id,
                PropertyCode = x.PropertyCode,
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
                FrontImage = _imagesrepository.GetFirstImage(x.Id)

            }).ToList();
        }

        public async Task<List<PropertyViewModel>> GeAllWithInclude()
        {
            var list = await _repository.GetAllWithInclude(new List<string> { "PropertyType", "SellingType" });
            return list.OrderBy(x => x.CreatedDate).Select(x => new PropertyViewModel
            {
                Id = x.Id,
                PropertyCode = x.PropertyCode,
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

        //GETBYID
        public async Task<List<PropertyAddViewModel>> GetPropertyById(int Id)
        {
            var list = await _repository.GetAllWithInclude(new List<string> { "PropertyType", "SellingType", "PropertyImprovements.Improvements"});
            return list.Where(x => x.Id == Id).Select(x => new PropertyAddViewModel
            {
                Id = x.Id,
                PropertyCode = x.PropertyCode,
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
                FrontImage = _imagesrepository.GetFirstImage(x.Id),
                ImprovementsName = x.PropertyImprovements.Select(x => x.Improvements.ImprovementName).ToList()
            }).ToList();
        }
    }
}
