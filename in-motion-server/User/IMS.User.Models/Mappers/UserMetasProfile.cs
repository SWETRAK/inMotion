using AutoMapper;
using IMS.Shared.Messaging.Messages.JWT;
using IMS.Shared.Messaging.Messages.Users;
using IMS.User.Models.Dto.Outgoing;

namespace IMS.User.Models.Mappers;

public class UserMetasProfile: Profile
{
    public UserMetasProfile()
    {
        CreateMap<GetBaseUserInfoResponseMessage, FullUserInfoDto>()
            .ForMember(u => u.Bio, opt => opt.Ignore())
            .ForMember(u => u.UserProfileVideo, opt => opt.Ignore())
            .ForMember(u => u.Id, opt => opt.MapFrom(p => p.Id))
            .ForMember(u => u.Nickname, opt => opt.MapFrom(p => p.Nickname));

        CreateMap<FullUserInfoDto, GetUserInfoResponseMessage>()
            .ForMember(u => u.Id, opt => opt.MapFrom(p => p.Id))
            .ForMember(u => u.Nickname, opt => opt.MapFrom(p => p.Nickname))
            .ForMember(u => u.Bio, opt => opt.MapFrom(p => p.Bio))
            .ForMember(u => u.UserProfileVideo, opt => opt.MapFrom(p => p.UserProfileVideo));
    }
}