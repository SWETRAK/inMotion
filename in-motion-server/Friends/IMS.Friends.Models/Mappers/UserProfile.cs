using AutoMapper;
using IMS.Friends.Models.Dto.Outgoing;
using IMS.Friends.Models.Models;
using IMS.Shared.Messaging.Messages.Users;

namespace IMS.Friends.Models.Mappers;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<UserInfoMessage, UserInfo>()
            .ForMember(ui => ui.Id, opt => opt.MapFrom(p => Guid.Parse(p.Id)))
            .ForMember(ui => ui.Email, opt => opt.MapFrom(p => p.Email))
            .ForMember(ui => ui.Nickname, opt => opt.MapFrom(p => p.Nickname))
            .ForMember(ui => ui.FrontVideo, opt => opt.MapFrom(p => p.FrontVideo));
        
        CreateMap<UserInfo, FriendInfoDto>()
            .ForMember(fid => fid.Id, opt => opt.MapFrom(p => p.Id.ToString()))
            .ForMember(fid => fid.Email, opt => opt.MapFrom(p => p.Email))
            .ForMember(fid => fid.Nickname, opt => opt.MapFrom(p => p.Nickname))
            .ForMember(fid => fid.FrontVideo, opt => opt.MapFrom(p => p.FrontVideo));

    }
}