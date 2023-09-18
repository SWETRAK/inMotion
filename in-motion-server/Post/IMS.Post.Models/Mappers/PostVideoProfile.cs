using AutoMapper;
using IMS.Post.Domain.Entities.Post;
using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Dto.Outgoing;
using IMS.Post.Models.Models.Soap;

namespace IMS.Post.Models.Mappers;

public class PostVideoProfile: Profile
{
    public PostVideoProfile()
    {
        CreateMap<PostVideo, PostVideoDto>()
            .ForMember(dto => dto.Id, opt => opt.MapFrom( p => p.Id.ToString()))
            .ForMember(dto => dto.Filename, opt => opt.MapFrom(p => p.Filename))
            .ForMember(dto => dto.BucketLocation, opt => opt.MapFrom(p => p.BucketLocation))
            .ForMember(dto => dto.ContentType, opt => opt.MapFrom(p => p.ContentType))
            .ForMember(dto => dto.BucketName, opt => opt.MapFrom(p => p.BucketName))
            .ForMember(dto => dto.VideoType, opt => opt.MapFrom(p => p.Type.ToString().ToLower()));

        CreateMap<UploadVideosMetaData, UploadVideosMetaDataDto>()
            .ForMember(dto => dto.PostId, opt => opt.MapFrom(p => p.PostId))
            .ForMember(dto => dto.AuthorId, opt => opt.MapFrom(p => p.AuthorId))
            .ForMember(dto => dto.VideosMetaData, opt => opt.MapFrom(p => p.VideosMetaData));

        CreateMap<VideoMetaData, VideoMetaDataDto>();
    }
}