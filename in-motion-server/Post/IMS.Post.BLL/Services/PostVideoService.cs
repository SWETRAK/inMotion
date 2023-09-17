using AutoMapper;
using IMS.Post.Domain.Consts;
using IMS.Post.Domain.Entities.Post;
using IMS.Post.IBLL.Services;
using IMS.Post.IDAL.Repositories.Post;
using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Exceptions;
using IMS.Shared.Utils.Parsers;
using Microsoft.Extensions.Logging;

namespace IMS.Post.BLL.Services;

public class PostVideoService: IPostVideoService
{
    private readonly IPostVideoRepository _postVideoRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ILogger<PostVideoService> _logger;

    public PostVideoService(IPostVideoRepository postVideoRepository,
        IPostRepository postRepository,
        IUserService userService,
        IMapper mapper,
        ILogger<PostVideoService> logger)
    {
        _postVideoRepository = postVideoRepository;
        _postRepository = postRepository;
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task SaveUploadedVideo(UploadVideoMetaDataDto uploadVideoMetaDataDto)
    {
        var authorIdGuid = uploadVideoMetaDataDto.AuthorId.ParseGuid();
        var postIdGuid = uploadVideoMetaDataDto.PostId.ParseGuid();
        
        var post = await _postRepository.GetByIdAsync(postIdGuid);
        
        if (post is null)
            throw new PostNotFoundException(postIdGuid.ToString());

        if (!Enum.TryParse<PostVideoType>(uploadVideoMetaDataDto.Type, out var videoType))
            throw new PostVideoTypeEnumParseException();

        var videos = post.Videos.ToArray();
        
        if (videos.Length < 2 && !Array.Exists(videos,x => x.Type.Equals(videoType)))
        {
            post.Videos = videos.Append(new PostVideo
            {
                ExternalAuthorId = authorIdGuid,
                Filename = uploadVideoMetaDataDto.Filename,
                BucketName = uploadVideoMetaDataDto.BucketName,
                BucketLocation = uploadVideoMetaDataDto.BucketLocation,
                ContentType = uploadVideoMetaDataDto.ContentType,
                Type = videoType
            }).ToArray();
        }
        
        if (post.Videos.Count().Equals(2) && 
            Array.Exists(videos, x => x.Type.Equals(PostVideoType.Front)) &&
            Array.Exists(videos, x => x.Type.Equals(PostVideoType.Rear)))
        {
            post.Visibility = PostVisibility.Public;
        }

        await _postVideoRepository.SaveAsync();
    }
}