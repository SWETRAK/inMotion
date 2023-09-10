using AutoMapper;
using IMS.Post.Models.Models.Author;
using IMS.Shared.Messaging.Messages.Users;
using IMS.Shared.Messaging.Messages.Users.Nested;

namespace IMS.Post.Models.Mappers;

public class AuthorProfile: Profile
{
    public AuthorProfile()
    {
        CreateMap<GetUserInfoResponseMessage, AuthorInfo>()
            .ForMember(u => u.Id, opt => opt.MapFrom(p => Guid.Parse(p.Id)))
            .ForMember(u => u.Nickname, opt => opt.MapFrom(p => p.Nickname))
            .ForMember(u => u.FrontVideo, opt => opt.MapFrom(p => p.UserProfileVideo));
        
        CreateMap<UserProfileVideoVM, AuthorProfileVideo>()
            .ForMember(u => u.Id, opt => opt.MapFrom(p => Guid.Parse(p.Id)))
            .ForMember(u => u.ContentType, opt => opt.MapFrom(p => p.ContentType))
            .ForMember(u => u.Filename, opt => opt.MapFrom(p => p.Filename))
            .ForMember(u => u.BucketLocation, opt => opt.MapFrom(p => p.BucketLocation))
            .ForMember(u => u.BucketName, opt => opt.MapFrom(p => p.BucketName));
    }
}