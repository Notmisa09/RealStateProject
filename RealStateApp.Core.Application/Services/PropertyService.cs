using AutoMapper;
using Microsoft.AspNetCore.Http;
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
            PropertyAddViewModel propertie = new();
            do
            {
                vm.Id = CodeGenerator.GenerateCode(vm.Id);
                propertie = await GetById(vm.Id);

            } while (propertie != null);

            vm.AgentId = user.Id;
            vm.AgentEmail = user.Email;

            var property = await base.Add(vm);
            await AddImprovements(vm);

            return property;
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


        public async Task<List<PropertyViewModel>> GeAllWithIncludeByAgent()
        {
            var list = await _repository.GetAllWithInclude(new List<string> { "PropertyType", "SellingType" });
            return list.Where(x => x.AgentId == user.Id).Select(x => new PropertyViewModel
            {
                Id = x.Id,
                AgentEmail = x.AgentEmail,
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

            }).ToList();
        }

        public async Task<List<PropertyViewModel>> GeAllWithInclude()
        {
            var list = await _repository.GetAllWithInclude(new List<string> { "PropertyType", "SellingType" });
            return list.Select(x => new PropertyViewModel
            {
                Id = x.Id,
                AgentEmail = x.AgentEmail,
                AgentPhoneNumber = x.AgentPhoneNumber,
                AgentId = x.AgentId,
                PropertyTypeName = x.PropertyType.PropertyTypeName,
                SellingTypeName = x.SellingType.SellingTypeName,
                SellingTypeId = x.SellingTypeId,
                Location = x.Location,
                PropertyTypeId = x.PropertyTypeId,
                BathroomsAmount = x.BathroomsAmount,
                BedroomsAmount = x.BedroomsAmount,
                Description = x.Description,
                Meters = x.Meters,
                Price = x.Price,

            }).ToList();
        }
    }
}
