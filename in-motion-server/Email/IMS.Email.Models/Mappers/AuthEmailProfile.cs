using AutoMapper;
using IMS.Email.Models.Models;
using IMS.Shared.Messaging.Messages.Email;

namespace IMS.Email.Models.Mappers;

public class AuthEmailProfile: Profile
{
    public AuthEmailProfile()
    {
        CreateMap<UserLoggedInEmailMessage, SendUserLoggedInEmail>()
            .ForMember(model => model.Email, opt => opt.MapFrom(p => p.Email))
            .ForMember(model => model.DateTime, opt => opt.MapFrom(p => p.LoggedDate));

        CreateMap<ActivateAccountEmailMessage, SendAccountActivation>()
            .ForMember(model => model.Email, opt => opt.MapFrom(p => p.Email))
            .ForMember(model => model.RegisterTime, opt => opt.MapFrom(p => p.DateTime))
            .ForMember(model => model.ActivationCode, opt => opt.MapFrom(p => p.ActivationCode));

        CreateMap<FailureLoginAttemptEmailMessage, SendFailedLoginAttempt>()
            .ForMember(model => model.Email, opt => opt.MapFrom(p => p.Email))
            .ForMember(model => model.AttemptDateTime, opt => opt.MapFrom(p => p.DateTime));
    }
}