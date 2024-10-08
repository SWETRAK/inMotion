using AutoMapper;
using IMS.Post.Domain.Entities.Post;
using IMS.Post.IBLL.Services;
using IMS.Post.IDAL.Repositories.Post;
using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Dto.Outgoing;
using IMS.Post.Models.Exceptions;
using IMS.Shared.Utils.Parsers;
using Microsoft.IdentityModel.Tokens;

namespace IMS.Post.BLL.Services;

public class PostReactionService: IPostReactionService
{
    private readonly IPostReactionRepository _postReactionRepository;
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public PostReactionService(IPostReactionRepository postReactionRepository,
        IPostRepository postRepository,
        IMapper mapper,
        IUserService userService)
    {
        _postReactionRepository = postReactionRepository;
        _postRepository = postRepository;
        _mapper = mapper;
        _userService = userService;
    }
    
    public async Task<IEnumerable<PostReactionDto>> GetForPostAsync(string postId)
    {
        var postIdGuid = postId.ParseGuid();
        
        var post = await _postRepository.GetByIdAsync(postIdGuid);
        if (post is null)
            throw new PostNotFoundException(postId);

        var postReactions = await _postReactionRepository.GetRangeByPostIdAsync(post.Id);

        if (postReactions.IsNullOrEmpty())
        {
            return new List<PostReactionDto>();
        }

        var authors = await _userService.GetUsersByIdsArray(postReactions.Select(x => x.ExternalAuthorId));
        
        var responseData = _mapper.Map<IEnumerable<PostReaction>, List<PostReactionDto>>(
            postReactions,
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
    
    public async Task<PostReactionDto> CreatePostReactionAsync(string userId, CreatePostReactionDto createPostReactionDto)
    {
        var userIdGuid = userId.ParseGuid();
        var postIdGuid = createPostReactionDto.PostId.ParseGuid();
        
        var post = await _postRepository.GetByIdAsync(postIdGuid);
        
        if (post is null) 
            throw new PostNotFoundException();

        var postReaction = await _postReactionRepository.GetByAuthorIdAndPostIdAsync(userIdGuid, post.Id);
        if (postReaction is not null) throw new PostReactionAlreadyExistsException();

        postReaction = new PostReaction
        {
            Post = post,
            ExternalAuthorId = userIdGuid,
            Emoji = createPostReactionDto.Emoji
        };

        await _postReactionRepository.AddAsync(postReaction);
        await _postReactionRepository.SaveAsync();
        
        var author = await _userService.GetUserById(postReaction.ExternalAuthorId);
                
        return _mapper.Map<PostReaction, PostReactionDto>(
            postReaction,
            f => f.AfterMap((src, dest) =>
            {
                dest.Author = _mapper.Map<PostAuthorDto>(author);
            })
        );
    }
    
    public async Task<PostReactionDto> EditPostReactionAsync(string userId,
        string reactionId,
        EditPostReactionDto editPostRequestDto)
    {
        var userIdGuid = userId.ParseGuid();
        var postReactionIdGuid = reactionId.ParseGuid();
        var postIdGuid = editPostRequestDto.PostId.ParseGuid();
        
        var postReaction = await _postReactionRepository.GetByIdAndUserIdAndPostIdAsync(
            postReactionIdGuid, postIdGuid, userIdGuid);
        if (postReaction is null)
            throw new PostReactionNotFoundException();
        
        postReaction.Emoji = editPostRequestDto.Emoji;
        postReaction.LastModificationDate = DateTime.UtcNow;
        await _postReactionRepository.SaveAsync();
        
        var author = await _userService.GetUserById(postReaction.ExternalAuthorId);
                
        return _mapper.Map<PostReaction, PostReactionDto>(
            postReaction,
            f => f.AfterMap((src, dest) =>
            {
                dest.Author = _mapper.Map<PostAuthorDto>(author);
            })
        );
    }
    
    public async Task DeletePostReactionAsync(string userId, string reactionId)
    {
        var userIdGuid = userId.ParseGuid();
        var postReactionIdGuid = reactionId.ParseGuid();

        var postReaction = await _postReactionRepository.GetByIdAndAuthorId(postReactionIdGuid, userIdGuid);
        
        if (postReaction is null)
            throw new PostReactionNotFoundException();

        _postReactionRepository.Delete(postReaction);
        await _postReactionRepository.SaveAsync();
    }
}