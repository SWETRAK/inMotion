using AutoMapper;
using IMS.Post.Domain.Entities.Post;
using IMS.Post.IBLL.Services;
using IMS.Post.IDAL.Repositories.Post;
using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Dto.Outgoing;
using IMS.Post.Models.Exceptions;
using IMS.Shared.Utils.Parsers;

namespace IMS.Post.BLL.Services;

public class PostCommentService: IPostCommentService
{
    private readonly IPostCommentRepository _postCommentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public PostCommentService(IPostCommentRepository postCommentRepository,
        IPostRepository postRepository,
        IMapper mapper, 
        IUserService userService)
    {
        _postCommentRepository = postCommentRepository;
        _postRepository = postRepository;
        _mapper = mapper;
        _userService = userService;
    }
    
    public async Task<PostCommentDto> CreatePostCommentDtoAsync(string userId, CreatePostCommentDto createPostCommentDto)
    {
        var userIdGuid = userId.ParseGuid();
        var postIdGuid = createPostCommentDto.PostId.ParseGuid();

        var post = await _postRepository.GetByIdAsync(postIdGuid);

        if (post is null)
            throw new PostNotFoundException();

        var postComment = new PostComment
        {
            ExternalAuthorId = userIdGuid,
            Post = post,
            Content = createPostCommentDto.Content
        };

        await _postCommentRepository.SaveAsync();

        return _mapper.Map<PostCommentDto>(postComment);
    }
    
    public async Task<PostCommentDto> EditPostCommentDtoAsync(string userId, string commentId, UpdatePostCommentDto updatePostCommentDto)
    {
        var userIdGuid = userId.ParseGuid();
        var commentIdGuid = commentId.ParseGuid();
        var postIdGuid = updatePostCommentDto.PostId.ParseGuid();

        var postComment = await _postCommentRepository.GetByIdAndAuthorIdAndPostIdAsync(commentIdGuid, userIdGuid, postIdGuid);

        if (postComment is null)
            throw new PostCommentNotFoundException();

        postComment.Content = updatePostCommentDto.Content;
        postComment.LastModifiedDate = DateTime.UtcNow;
        await _postCommentRepository.SaveAsync();
        
        return _mapper.Map<PostCommentDto>(postComment);
    }
    
    public async Task<IEnumerable<PostCommentDto>> GetPostCommentsAsync(
        string postId)
    {
        var postIdGuid = postId.ParseGuid();
        var post = await _postRepository.GetByIdAsync(postIdGuid);
        
        if (post is null)
            throw new PostNotFoundException();
        
        var postComments = await _postCommentRepository.GetRangeByPostIdAsync(post.Id);

        var authors = await _userService.GetUsersByIdsArray(postComments.Select(x => x.ExternalAuthorId).Distinct());
        
        var responseData = _mapper.Map<IEnumerable<PostComment>, List<PostCommentDto>>(
            postComments,
            f => f.AfterMap((src, dest) =>
            {
                dest.ForEach(s =>
                {
                    var originalData = src.First(c => c.Id.Equals(Guid.Parse(s.Id)));
                    var author = authors.FirstOrDefault(a => a.Id.Equals(originalData.ExternalAuthorId));
                    if (author is not null) s.Author = _mapper.Map<PostAuthorDto>(author);
                });
            })
        );

        return responseData;
    }
    
    public async Task DeletePostCommentAsync(string userId, string commentId)
    {
        var userIdGuid = userId.ParseGuid();
        var commentIdGuid = commentId.ParseGuid();
        
        var postComment = await _postCommentRepository.GetByIdAndAuthorIdAsync(commentIdGuid, userIdGuid);
        
        if (postComment is null)
            throw new PostCommentNotFoundException();

        _postCommentRepository.Delete(postComment);

        await _postCommentRepository.SaveAsync();
    }
}