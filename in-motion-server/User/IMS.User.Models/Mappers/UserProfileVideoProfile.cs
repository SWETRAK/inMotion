using AutoMapper;
using IMS.Shared.Messaging.Messages.Users;
using IMS.Shared.Messaging.Messages.Users.Nested;
using IMS.User.Domain.Entities;
using IMS.User.Models.Dto.Incoming;
using IMS.User.Models.Dto.Outgoing;

namespace IMS.User.Models.Mappers;

public class UserProfileVideoProfile: Profile
{
    public UserProfileVideoProfile()
    {
        CreateMap<UpdateUserProfileVideoMessage, UpdateUserProfileVideoDto>()
            .ForMember(c => c.BucketLocation, opt => opt.MapFrom(src => src.BucketLocation))
            .ForMember(c => c.BucketName, opt => opt.MapFrom(src => src.BucketName))
            .ForMember(c => c.Filename, opt => opt.MapFrom(src => src.Filename))
            .ForMember(c => c.ContentType, opt => opt.MapFrom(src => src.ContentType));
        
        CreateMap<UserProfileVideo, UserProfileVideoDto>()
            .ForMember(c => c.Id, opt => opt.MapFrom(p => p.Id.ToString()))
            .ForMember(c => c.Filename, opt => opt.MapFrom(p => p.Filename))
            .ForMember(c => c.BucketLocation, opt => opt.MapFrom(p => p.BucketLocation))
            .ForMember(c => c.BucketName, opt => opt.MapFrom(p => p.BucketName))
            .ForMember(c => c.UserId, opt => opt.MapFrom(p => p.AuthorExternalId.ToString()))
            .ForMember(c => c.ContentType, opt => opt.MapFrom(p => p.ContentType));
        
        CreateMap<UserProfileVideoDto, UserProfileVideoVM>()
            .ForMember(c => c.Id, opt => opt.MapFrom(p => p.Id))
            .ForMember(c => c.Filename, opt => opt.MapFrom(p => p.Filename))
            .ForMember(c => c.BucketLocation, opt => opt.MapFrom(p => p.BucketLocation))
            .ForMember(c => c.BucketName, opt => opt.MapFrom(p => p.BucketName))
            .ForMember(c => c.ContentType, opt => opt.MapFrom(p => p.ContentType));
        
        CreateMap<UserProfileVideo, UpdatedUserProfileVideoDto>()
            .ForMember(c => c.Filename, opt => opt.MapFrom(p => p.Filename))
            .ForMember(c => c.ContentType, opt => opt.MapFrom(p => p.ContentType))
            .ForMember(c => c.BucketLocation, opt => opt.MapFrom(p => p.BucketLocation))
            .ForMember(c => c.BucketName, opt => opt.MapFrom(p => p.BucketName))
            .ForMember(c => c.UserId, opt => opt.MapFrom(p => p.AuthorExternalId.ToString()))
            .ForMember(c => c.Id, opt => opt.MapFrom(p => p.Id.ToString()));
    }
}