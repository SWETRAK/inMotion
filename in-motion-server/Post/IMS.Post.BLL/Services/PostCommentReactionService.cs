using AutoMapper;
using IMS.Post.Domain.Entities.Post;
using IMS.Post.IBLL.Services;
using IMS.Post.IDAL.Repositories.Post;
using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Dto.Outgoing;
using IMS.Post.Models.Exceptions;
using IMS.Shared.Models.Dto;
using IMS.Shared.Utils.Parsers;

namespace IMS.Post.BLL.Services;

public class PostCommentReactionService: IPostCommentReactionService
{
    private readonly IPostCommentReactionRepository _postCommentReactionRepository;
    private readonly IPostCommentRepository _postCommentRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public PostCommentReactionService(IPostCommentReactionRepository postCommentReactionRepository,
        IPostCommentRepository postCommentRepository,
        IMapper mapper,
        IUserService userService)
    {
        _postCommentReactionRepository = postCommentReactionRepository;
        _postCommentRepository = postCommentRepository;
        _mapper = mapper;
        _userService = userService;
    }

    // TODO: Add post comment reaction
    public async Task<PostCommentReactionDto> AddPostCommentReaction(string userId, CreatePostCommentReactionDto createPostCommentReactionDto)
    {
        var userIdGuid = userId.ParseGuid();
        var postCommentIdGuid = createPostCommentReactionDto.PostCommentId.ParseGuid();
        var postComment = await _postCommentRepository.GetByIdAsync(postCommentIdGuid);

        if (postComment is null)
            throw new PostCommentNotFoundException();
        
        var postCommentReaction = new PostCommentReaction
        {
            PostComment = postComment,
            ExternalAuthorId = userIdGuid,
            Emoji = createPostCommentReactionDto.Emoji
        };

        await _postCommentReactionRepository.SaveAsync();
        //TODO: Write mapper for this and add data to PostCommentReactionDto
        return _mapper.Map<PostCommentReactionDto>(postCommentReaction);
    }
    
    // TODO: Edit post comment reaction
    public async Task<PostCommentReactionDto> EditPostCommentReaction(string userId,
        string postCommentReactionId,
        EditPostCommentReactionDto editPostCommentReactionDto)
    {
        var userIdGuid = userId.ParseGuid();
        var postCommentReactionIdGuid = postCommentReactionId.ParseGuid();
        var postCommentIdGuid = editPostCommentReactionDto.PostCommentId.ParseGuid();

        var postCommentReaction = await _postCommentReactionRepository.GetByIdAndAuthorIdAndPostCommentIdAsync(
            postCommentReactionIdGuid, userIdGuid, postCommentIdGuid);

        if (postCommentReaction is null)
            throw new PostCommentReactionNotFoundException();

        postCommentReaction.Emoji = editPostCommentReactionDto.Emoji;
        postCommentReaction.LastModificationDate = DateTime.UtcNow;

        await _postCommentReactionRepository.SaveAsync();
        
        return _mapper.Map<PostCommentReactionDto>(postCommentReaction);
    }

    // TODO: Remove post comment reaction
    public async Task RemovePostCommentReaction(string userId, string postCommentReactionId)
    {
        var userIdGuid = userId.ParseGuid();
        var postCommentReactionIdGuid = postCommentReactionId.ParseGuid();

        var postCommentReaction = await _postCommentReactionRepository.GetByIdAndAuthorIdAsync(
            postCommentReactionIdGuid, userIdGuid);

        if (postCommentReaction is null)
            throw new PostCommentReactionNotFoundException();

        _postCommentReactionRepository.Remove(postCommentReaction);
        await _postCommentReactionRepository.SaveAsync();
    }

    // TODO: Get all post comment reactions (paginated, sort by date, descending)
    public async Task<ImsPagination<IEnumerable<PostCommentReactionDto>>> GetAllPostCommentReactionsPaginatedAsync(
        string postCommentId, ImsPaginationRequestDto paginationRequestDto)
    {
        var postCommentIdGuid = postCommentId.ParseGuid();

        var postComment = await _postCommentRepository.GetByIdAsync(postCommentIdGuid);
        
        if (postComment is null)
            throw new PostCommentNotFoundException();
        
        var postCommentsReactions =
            await _postCommentReactionRepository.GetByPostCommentIdPaginatedAsync(postCommentIdGuid,
                paginationRequestDto.PageNumber, paginationRequestDto.PageSize);

        var postCommentsReactionsCount =
            await _postCommentReactionRepository.GetByPostCommentIdCountAsync(postCommentIdGuid);
            
        var authors = await _userService.GetUsersByIdsArray(postCommentsReactions.Select(x => x.ExternalAuthorId).Distinct());

        var responseData = _mapper.Map<IEnumerable<PostCommentReaction>, List<PostCommentReactionDto>>(
            postCommentsReactions,
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

        return new ImsPagination<IEnumerable<PostCommentReactionDto>>
        {
            PageNumber = paginationRequestDto.PageNumber,
            PageSize = paginationRequestDto.PageSize,
            TotalCount = postCommentsReactionsCount,
            Data = responseData
        };
    }
}