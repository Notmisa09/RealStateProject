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

        public PropertyService(IPropertyRepository repository,
            IMapper mapper, 
            IHttpContextAccessor contextAccesor) : base(repository, mapper)
        {
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
            } while (propertie == null);
            
            vm.AgentId = user.Id;
            vm.AgentEmail = user.Email;
            vm.AgentPhoneNumber = user.PhoneNumber;
            
            return await base.Add(vm);
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
                PropertyType = x.PropertyType.PropertyName,
                SellingType = x.SellingType.SellingTypeName,
                SellingTypeId = x.SellingTypeId,
                PropertyTypeId = x.PropertyTypeId,
                BathroomsAmount = x.BathroomsAmount,
                RoomsAmount = x.RoomsAmount,
                Description = x.Description,
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
                PropertyType = x.PropertyType.PropertyName,
                SellingType = x.SellingType.SellingTypeName,
                SellingTypeId = x.SellingTypeId,
                PropertyTypeId = x.PropertyTypeId,
                BathroomsAmount = x.BathroomsAmount,
                RoomsAmount = x.RoomsAmount,
                Description = x.Description,
                Price = x.Price,

            }).ToList();
        }

    }
}
