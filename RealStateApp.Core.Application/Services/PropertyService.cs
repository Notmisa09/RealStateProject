using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.Filter;
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
        private readonly IPropertyImagesRepository _propimagesrepository;
        private readonly IClientPropertyFavRepository _clientpropertyfavrepository;

        public PropertyService(IPropertyRepository repository,
            IMapper mapper,
            IHttpContextAccessor contextAccesor,
            IPropertyImprovementsRepository propimprovemetns,
            IPropertyImagesRepository propimagerespository,
            IClientPropertyFavRepository clientpropertyfavrepository) : base(repository, mapper)
        {
            _propimagesrepository = propimagerespository;
            _propimprovemetns = propimprovemetns;
            _mapper = mapper;
            _repository = repository;
            _contextAccessor = contextAccesor;
            user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _clientpropertyfavrepository = clientpropertyfavrepository;
        }

        #region Saves

        public override async Task<PropertyAddViewModel> Add(PropertyAddViewModel vm)
        {
            var validateresult = Validations(vm);
            if (validateresult.HasError)
            {
                return vm;
            }

            PropertyAddViewModel propertie = new();
            do
            {
                vm.PropertyCode = CodeGenerator.GenerateCode(vm.PropertyCode);
                propertie = await GetById(vm.Id);

            } while (propertie != null);

            vm.AgentId = user.Id;
            vm.AgentEmail = user.Email;

            var property = await base.Add(vm);
            vm.Id = property.Id;

            await AddImprovements(vm);
            await AddImages(vm);
            return property;
        }

        //ADDFAVPROPERTIES
        public async Task AddFavProp(PropertyAddViewModel vm)
        {
            ClientPropertyFav favprop = new();
            favprop.PropertyId = vm.Id.Value;
            favprop.UserId = user.Id;
            await _clientpropertyfavrepository.AddAsync(favprop);
        }

        #endregion

        #region Update

        //UPDATE
        public override async Task Update(PropertyAddViewModel vm, int Id)
        {
            if (vm.Improvements != null)
            {
                await _propimprovemetns.RemoveUpdateWithUserId(Id);
                await AddImprovements(vm);
            }
            if (vm.formFile != null)
            {
                await _propimagesrepository.RemoveImages(Id);
                await AddImages(vm);
            }
            await base.Update(vm, Id);
        }

        #endregion

        #region Remove

        //REMOVEFAPROPERTIES
        public async Task RemoveFavProp(int Id)
        {
            var prop = await _clientpropertyfavrepository.GetByIdAync(Id);
            await _clientpropertyfavrepository.RemoveAsync(prop);
        }

        public override async Task Remove(int Id)
        {
            await _propimagesrepository.RemoveImages(Id);
            await base.Remove(Id);
        }
        #endregion

        //PROPERTIESCOUNTER
        public async Task<int> PropertiesCount(string Id)
        {
            var properties = await _repository.GetAllAsync();
            var count = properties.Where(x => x.AgentId == Id).Count();
            return count;
        }

        #region private methods

        //ADDIMGAES
        private async Task AddImages(PropertyAddViewModel vm)
        {
            foreach (var item in vm.formFile)
            {
                var image = FileHelpers.UploadFile(item, vm.Id, "Properties", false);
                PropertyImages images = new()
                {
                    ImageURL = image,
                    PropertyId = vm.Id.Value,
                };
                await _propimagesrepository.AddAsync(images);
            }
        }

        //VALIDATIONS
        private PropertyAddViewModel Validations(PropertyAddViewModel vm)
        {
            if (vm.formFile.Count() > 4)
            {
                vm.HasError = true;
                vm.Error = "La cantidad de imagenes excede el limite";
                return vm;
            }
            if (vm.Improvements == null)
            {
                vm.HasError = true;
                vm.Error = "La propiedad debe tener al menos 1 mejora";
            }
            return vm;
        }

        //ADDIMPROVEMENTS
        private async Task AddImprovements(PropertyAddViewModel vm)
        {
            PropertyImprovements propimprovements = new();

            foreach (var item in vm.Improvements)
            {
                propimprovements.ImprovementId = item;
                propimprovements.PropertyId = vm.Id.Value;

                await _propimprovemetns.AddAsync(propimprovements);
            }
        }

        #endregion 

        #region Gets

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
                    FrontImage = _propimagesrepository.GetFirstImage(x.Id)
                }).FirstOrDefault();

                prop.Add(newlist);
            }
            return prop;
        }


        //GETBYID
        public async Task<List<PropertyAddViewModel>> GeAllWithIncludeByAgent()
        {
            var list = await _repository.GetAllWithInclude(new List<string> { "PropertyType", "SellingType" });
            return list.Where(x => x.AgentId == user.Id).OrderByDescending(x => x.Id).Select(x => new PropertyAddViewModel
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
                FrontImage = _propimagesrepository.GetFirstImage(x.Id)

            }).ToList();
        }


        //GETBYID
        public async Task<List<PropertyAddViewModel>> GetAllByUserId(string Id)
        {
            var list = await _repository.GetAllWithInclude(new List<string> { "PropertyType", "SellingType" });
            return list.Where(x => x.AgentId == Id).OrderByDescending(x => x.Id).Select(x => new PropertyAddViewModel
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
                FrontImage = _propimagesrepository.GetFirstImage(x.Id)

            }).ToList();
        }

        public async Task<List<PropertyViewModel>> GeAllWithInclude()
        {
            var list = await _repository.GetAllWithInclude(new List<string> { "PropertyType", "SellingType" });
            return list.OrderByDescending(x => x.Id).Select(x => new PropertyViewModel
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
                FrontImage = _propimagesrepository.GetFirstImage(x.Id)
            }).ToList();
        }

        //GETBYID
        public async Task<List<PropertyAddViewModel>> GetPropertyById(int Id)
        {
            var list = await _repository.GetAllWithInclude(new List<string> { "PropertyType", "SellingType", "PropertyImprovements.Improvements" });
            return list.Where(x => x.Id == Id).OrderBy(x => x.Id).Select(x => new PropertyAddViewModel
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
                FrontImage = _propimagesrepository.GetFirstImage(x.Id),
                ImprovementsName = x.PropertyImprovements.Select(x => x.Improvements.ImprovementName).ToList()
            }).ToList();
        }

        //GETBYID
        public async Task<List<PropertyAddViewModel>> GetPropertyByCode(string Code)
        {
            var list = await _repository.GetAllWithInclude(new List<string> { "PropertyType", "SellingType", "PropertyImprovements.Improvements" });
            return list.Where(x => x.PropertyCode == Code).Select(x => new PropertyAddViewModel
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
                FrontImage = _propimagesrepository.GetFirstImage(x.Id),
                ImprovementsName = x.PropertyImprovements.Select(x => x.Improvements.ImprovementName).ToList()
            }).ToList();
        }


        public async Task<List<PropertyViewModel>> GeAllWithFilterInclude(FilterViewModel vm)
        {
            var list = await _repository.GetAllWithInclude(new List<string> { "PropertyType", "SellingType" });
            list = list.OrderByDescending(x => x.Id).ToList();

            if (vm.PropertyCode != null)
            {
                list = list.Where(x => x.PropertyCode == vm.PropertyCode).ToList();
            }
            else
            {
                if (vm.MaxValue != null)
                {
                    list = list.Where(x => x.Price <= vm.MaxValue).ToList();
                }
                if (vm.MinValue != null)
                {
                    list = list.Where(x => x.Price >= vm.MinValue).ToList();
                }
                if (vm.BedRoomAmount != null)
                {
                    list = list.Where(x => x.BedroomsAmount == vm.BedRoomAmount).ToList();
                }
                if (vm.BathroomAmount != null)
                {
                    list = list.Where(x => x.BathroomsAmount == vm.BathroomAmount).ToList();
                }
                if (vm.PropertyType != null)
                {
                    list = list.Where(x => x.PropertyTypeId == vm.PropertyType).ToList();
                }
            }
            return list.Select(x => new PropertyViewModel
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
                FrontImage = _propimagesrepository.GetFirstImage(x.Id)
            }).ToList();
        }
        #endregion
    }
}
