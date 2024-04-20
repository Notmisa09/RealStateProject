using AutoMapper;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Dto.Acccount.ForgotPassword;
using RealStateApp.Core.Application.Dto.Acccount.Register;
using RealStateApp.Core.Application.Dto.Acccount.ResetPassword;
using RealStateApp.Core.Application.Dto.API.Improvements;
using RealStateApp.Core.Application.Dto.API.PropertyType;
using RealStateApp.Core.Application.Dto.API.SellingType;
using RealStateApp.Core.Application.Features.Improvements.Commands.CreateImprovements;
using RealStateApp.Core.Application.Features.Improvements.Commands.UpdateImprovements;
using RealStateApp.Core.Application.Features.PropertyType.Commands.CreatePropertyType;
using RealStateApp.Core.Application.Features.PropertyType.Commands.UpdatePropertyType;
using RealStateApp.Core.Application.Features.SellingTypes.Commands.CreateSellingTypes;
using RealStateApp.Core.Application.Features.SellingTypes.Commands.UpdateSellingTypes;
using RealStateApp.Core.Application.ViewModels.Improvements;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.SellingTypes;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Mapper;

public class GenrealProfile : Profile
{
    public GenrealProfile()
    {
        #region Users
        CreateMap<LoginViewModel, AuthenticationRequest>()
            .ReverseMap()
            .ForMember(l => l.Error, opt => opt.Ignore())
            .ForMember(l => l.HasError, opt => opt.Ignore());

        CreateMap<RegisterRequest, SaveUserViewModel>()
            .ForMember(r => r.Error, opt => opt.Ignore())
            .ForMember(r => r.HasError, opt => opt.Ignore())
            .ForMember(r => r.IsActive, src => src.MapFrom(x => x.IsActive))
            .ReverseMap()
            .ForMember(r => r.FirstName, src => src.MapFrom(x => x.FirstName));

        CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
            .ForMember(r => r.Error, opt => opt.Ignore())
            .ForMember(r => r.HasError, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
            .ForMember(rp => rp.Error, opt => opt.Ignore())
            .ForMember(rp => rp.HasError, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(rp => rp.Token, src => src.MapFrom(x => x.Token));

        CreateMap<DtoAccount, SaveUserViewModel>()
            .ForMember(u => u.HasError, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<DtoAccount, UserViewModel>()
        .ForMember(u => u.Roles, opt => opt.Ignore())
        .ReverseMap();

        #endregion

        #region PropertyType

        CreateMap<PropertyType, PropertyTypeAddViewModel>()
            .ReverseMap()
            .ForMember(p => p.Property, opt => opt.Ignore());

        CreateMap<PropertyType, PropertyTypeViewModel>()
            .ReverseMap()
            .ForMember(p => p.Property, opt => opt.Ignore());


        CreateMap<PropertyType, PropertyTypeDTO>().ReverseMap();
        CreateMap<PropertyType, PropertyTypeAddDTO>().ReverseMap();
        CreateMap<PropertyType, CreatePropertyTypeCommand>().ReverseMap();
        CreateMap<PropertyType, UpdatePropertyTypeCommand>().ReverseMap();
        #endregion

        #region SellingType

        CreateMap<SellingType, SellingTypeVeiwModel>()
            .ReverseMap()
            .ForMember(x => x.Properties, opt => opt.Ignore());

        CreateMap<SellingType, SellingTypeAddViewModel>()
            .ReverseMap()
            .ForMember(x => x.Properties, opt => opt.Ignore());

        CreateMap<SellingType, SellingTypeDTO>().ReverseMap();
        CreateMap<SellingType, SellingTypeAddDTO>().ReverseMap();
        CreateMap<SellingType, CreateSellingTypesCommand>().ReverseMap();
        CreateMap<SellingType, UpdateSellingTypesCommand>().ReverseMap();
        #endregion

        #region Improvements

        CreateMap<Improvements, ImprovementsAddViewModel>()
            .ReverseMap()
            .ForMember(x => x.PropertyImprovements, opt => opt.Ignore());

        CreateMap<Improvements, ImprovemetnsViewModel>()
            .ReverseMap()
            .ForMember(x => x.PropertyImprovements, opt => opt.Ignore());

            CreateMap<Improvements, ImprovementsDTO>().ReverseMap();
            CreateMap<Improvements, ImprovementsAddDTO>().ReverseMap();
            CreateMap<Improvements, CreateImprovementsCommand>().ReverseMap();
            CreateMap<Improvements, UpdateImprovementsCommand>().ReverseMap();

        #endregion

        #region Properties

        CreateMap<Properties, PropertyAddViewModel>()
            .ForMember(p => p.Improvements, opt => opt.Ignore())
            .ForMember(p => p.formFile, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<Properties, PropertyViewModel>()
       .ForMember(p => p.SellingTypeName, opt => opt.Ignore())
       .ForMember(p => p.PropertyTypeName, opt => opt.Ignore())
       .ReverseMap();

        #endregion

    }
}