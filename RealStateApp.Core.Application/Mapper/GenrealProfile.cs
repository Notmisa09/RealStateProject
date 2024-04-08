using AutoMapper;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Dto.Acccount.ForgotPassword;
using RealStateApp.Core.Application.Dto.Acccount.Register;
using RealStateApp.Core.Application.Dto.Acccount.ResetPassword;
using RealStateApp.Core.Application.ViewModels.User;

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
            .ReverseMap();

        CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
            .ForMember(r => r.Error, opt => opt.Ignore())
            .ForMember(r => r.HasError, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
            .ForMember(rp => rp.Error, opt => opt.Ignore())
            .ForMember(rp => rp.HasError, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(rp => rp.Token, src => src.MapFrom(x => x.Token));
        #endregion
    }
}