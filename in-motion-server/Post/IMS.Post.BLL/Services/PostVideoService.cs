using IMS.Post.Domain.Consts;
using IMS.Post.Domain.Entities.Post;
using IMS.Post.IBLL.Services;
using IMS.Post.IDAL.Repositories.Post;
using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Exceptions;
using IMS.Shared.Utils.Parsers;

namespace IMS.Post.BLL.Services;

public class PostVideoService: IPostVideoService
{
    private readonly IPostVideoRepository _postVideoRepository;
    private readonly IPostRepository _postRepository;

    public PostVideoService(IPostVideoRepository postVideoRepository,
        IPostRepository postRepository)
    {
        _postVideoRepository = postVideoRepository;
        _postRepository = postRepository;
    }
    
    public async Task SaveUploadedVideos(UploadVideosMetaDataDto uploadVideosMetaDataDto)
    {
        var authorIdGuid = uploadVideosMetaDataDto.AuthorId.ParseGuid();
        var postIdGuid = uploadVideosMetaDataDto.PostId.ParseGuid();
        
        var post = await _postRepository.GetByIdAsync(postIdGuid);
        
        if (post is null)
            throw new PostNotFoundException(postIdGuid.ToString());

        var videos = post.Videos.ToList();
        
        if (videos.Count.Equals(0))
        {
            uploadVideosMetaDataDto.VideosMetaData.ToList().ForEach(video =>
            {
                if (!Enum.TryParse<PostVideoType>(video.Type, out var videoType))
                    throw new PostVideoTypeEnumParseException();
                
                videos.Add(new PostVideo
                {
                    ExternalAuthorId = authorIdGuid,
                    Filename = video.Filename,
                    BucketName = video.BucketName,
                    BucketLocation = video.BucketLocation,
                    ContentType = video.ContentType,
                    Type = videoType
                });
            });
        }
        
        if (videos.Count.Equals(2) && 
            Array.Exists(videos.ToArray(), x => x.Type.Equals(PostVideoType.Front)) &&
            Array.Exists(videos.ToArray(), x => x.Type.Equals(PostVideoType.Rear)))
        {
            post.Videos = videos;
            post.Visibility = PostVisibility.Public;
        }
        await _postVideoRepository.SaveAsync();
    }
}