using AutoMapper;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Dto.Acccount.ForgotPassword;
using RealStateApp.Core.Application.Dto.Acccount.Register;
using RealStateApp.Core.Application.Dto.Acccount.ResetPassword;
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
        #region MyRegion
        CreateMap<LoginViewModel, AuthenticationRequest>()
            .ReverseMap()
            .ForMember(l => l.Error, opt => opt.Ignore())
            .ForMember(l => l.HasError, opt => opt.Ignore());

        CreateMap<RegisterRequest, SaveUserViewModel>()
            .ForMember(r => r.Error, opt => opt.Ignore())
            .ForMember(r => r.HasError, opt => opt.Ignore())
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
        #endregion

        #region SellingType

        CreateMap<SellingType, SellingTypeVeiwModel>()
            .ReverseMap()
            .ForMember(x => x.Properties, opt => opt.Ignore());

        CreateMap<SellingType, SellingTypeAddViewModel>()
            .ReverseMap()
            .ForMember(x => x.Properties, opt => opt.Ignore());
        #endregion

        #region Improvements

        CreateMap<Improvements, ImprovementsAddViewModel>()
            .ReverseMap()
            .ForMember(x => x.PropertyImprovements, opt => opt.Ignore());

        CreateMap<Improvements, ImprovemetnsViewModel>()
            .ReverseMap()
            .ForMember(x => x.PropertyImprovements, opt => opt.Ignore());

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