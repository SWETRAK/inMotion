using AutoMapper;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Auth.Models.Models;
using IMS.Shared.Domain.Entities.User;
using IMS.Shared.Messaging.Messages.JWT;

namespace IMS.Auth.DAL.Mappers;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<User, UserInfoDto>()
            .ForMember(dto => dto.Email, opt => opt.MapFrom(u => u.Email))
            .ForMember(dto => dto.Nickname, opt => opt.MapFrom(u => u.Nickname))
            .ForMember(dto => dto.Id, opt => opt.MapFrom(u => u.Id.ToString()))
            .ForMember(dto => dto.Token, opt => opt.Ignore());

        CreateMap<UserSuccessfulJwtValidation, ValidatedUserInfoMessage>()
            .ForMember(ch => ch.Id, opt => opt.MapFrom(p => p.Id.ToString()))
            .ForMember(ch => ch.Email, opt => opt.MapFrom(p => p.Email))
            .ForMember(ch => ch.Nickname, opt => opt.MapFrom(p => p.Nickname));

    }
}