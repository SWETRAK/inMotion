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
    }
}