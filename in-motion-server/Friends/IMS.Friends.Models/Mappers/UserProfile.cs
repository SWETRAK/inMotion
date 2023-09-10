using AutoMapper;
using IMS.Friends.Models.Dto.Outgoing;
using IMS.Friends.Models.Models;
using IMS.Shared.Messaging.Messages.Users;
using IMS.Shared.Messaging.Messages.Users.Nested;

namespace IMS.Friends.Models.Mappers;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<GetUserInfoResponseMessage, UserInfo>()
            .ForMember(u => u.Id, opt => opt.MapFrom(p => p.Id))
            .ForMember(u => u.Nickname, opt => opt.MapFrom(p => p.Nickname))
            .ForMember(u => u.Bio, opt => opt.MapFrom(p => p.Bio))
            .ForMember(u => u.FrontVideo, opt => opt.MapFrom(p => p.UserProfileVideo));
        
        CreateMap<UserProfileVideoVM, UserProfileVideo>()
            .ForMember(u => u.Id, opt => opt.MapFrom(p => Guid.Parse(p.Id)))
            .ForMember(u => u.ContentType, opt => opt.MapFrom(p => p.ContentType))
            .ForMember(u => u.Filename, opt => opt.MapFrom(p => p.Filename))
            .ForMember(u => u.BucketLocation, opt => opt.MapFrom(p => p.BucketLocation))
            .ForMember(u => u.BucketName, opt => opt.MapFrom(p => p.BucketName));
        
        CreateMap<UserInfo, FriendInfoDto>()
            .ForMember(fid => fid.Id, opt => opt.MapFrom(p => p.Id.ToString()))
            .ForMember(fid => fid.Bio, opt => opt.MapFrom(p => p.Bio))
            .ForMember(fid => fid.Nickname, opt => opt.MapFrom(p => p.Nickname))
            .ForMember(fid => fid.FrontVideo, opt => opt.MapFrom(p => p.FrontVideo));

        CreateMap<UserProfileVideo, FriendProfileVideoDto>()
            .ForMember(u => u.Id, opt => opt.MapFrom(p => Guid.Parse(p.Id)))
            .ForMember(u => u.ContentType, opt => opt.MapFrom(p => p.ContentType))
            .ForMember(u => u.Filename, opt => opt.MapFrom(p => p.Filename))
            .ForMember(u => u.BucketLocation, opt => opt.MapFrom(p => p.BucketLocation))
            .ForMember(u => u.BucketName, opt => opt.MapFrom(p => p.BucketName));
    }
}