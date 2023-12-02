using AutoMapper;
using IMS.Post.Domain.Entities.Other;
using IMS.Post.IBLL.Services;
using IMS.Post.IDAL.Repositories.Other;
using IMS.Post.IDAL.Repositories.Post;
using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Dto.Outgoing;
using IMS.Post.Models.Exceptions;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.Friendship;
using IMS.Shared.Models.Dto;
using IMS.Shared.Models.Exceptions;
using IMS.Shared.Utils.Parsers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace IMS.Post.BLL.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IPostIterationRepository _postIterationRepository;
    private readonly ILogger<PostService> _logger;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IRequestClient<ImsBaseMessage<GetUserFriendsMessage>> _getUserFriendsRequestClient;

    public PostService(IPostRepository postRepository,
        ILogger<PostService> logger,
        IMapper mapper,
        ITagRepository tagRepository,
        IUserService userService, 
        IRequestClient<ImsBaseMessage<GetUserFriendsMessage>> getUserFriendsRequestClient, 
        IPostIterationRepository postIterationRepository)
    {
        _postRepository = postRepository;
        _logger = logger;
        _mapper = mapper;
        _tagRepository = tagRepository;
        _userService = userService;
        _getUserFriendsRequestClient = getUserFriendsRequestClient;
        _postIterationRepository = postIterationRepository;
    }

    public async Task<ImsPagination<IList<GetPostResponseDto>>> GetPublicPostsFromCurrentIteration(
        string userIdString,
        ImsPaginationRequestDto paginationRequestDto)
    {
        var userId = userIdString.ParseGuid(); 
        var postIteration = await _postIterationRepository.GetNewest();
        
        if (postIteration is null)
            throw new PostIterationNotFoundException();
        
        var posts = await _postRepository.GetPublicFormIterationPaginatedAsync(postIteration.Id,
            paginationRequestDto.PageNumber, paginationRequestDto.PageSize);
        
        var result = new ImsPagination<IList<GetPostResponseDto>>
        {
            PageSize = paginationRequestDto.PageSize,
            PageNumber = paginationRequestDto.PageNumber
        };

        if (posts.IsNullOrEmpty())
        {
            result.Data = new List<GetPostResponseDto>();
            return result;
        }

        var authors = await _userService.GetUsersByIdsArray(posts.Select(u => u.ExternalAuthorId));

        var getPostResponseDtos = _mapper.Map<IEnumerable<Domain.Entities.Post.Post>, List<GetPostResponseDto>>(
            posts,
            f => f.AfterMap((src, dest) =>
            {
                dest.ForEach(s =>
                {
                    var originalData = src.First<Domain.Entities.Post.Post>(c => c.Id.Equals(Guid.Parse(s.Id)));
                    var author = authors.FirstOrDefault(a => a.Id.Equals(originalData.ExternalAuthorId));
                    if (author is not null) s.Author = _mapper.Map<PostAuthorDto>(author);
                    var reaction = originalData.PostReactions.FirstOrDefault(x => x.ExternalAuthorId.Equals(userId));
                    if (reaction is null) return;
                    s.PostReaction = _mapper.Map<PostReactionDto>(reaction);
                    s.IsLikedByUser = true;
                });
            })
        );

        result.Data = getPostResponseDtos;
        return result;
    }

    public async Task<IList<GetPostResponseDto>> GetFriendsPublicPostsFromCurrentIteration(
        string userId)
    {
        var userIdGuid = userId.ParseGuid();
        
        var postIteration = await _postIterationRepository.GetNewest();
        
        if (postIteration is null)
            throw new PostIterationNotFoundException();
        
        var friendsRequest = new ImsHttpMessage<GetUserFriendsMessage>
        {
            Data = new GetUserFriendsMessage
            {
                UserId = userId
            }
        };

        var friendsResponse = await _getUserFriendsRequestClient.GetResponse<ImsBaseMessage<GetUserFriendsResponseMessage>>(friendsRequest);

        if (friendsResponse.Message.Error)
            throw new NestedRabbitMqRequestException();

        var friendsIds = friendsResponse.Message.Data.FriendsIds;

        if (friendsIds.IsNullOrEmpty())
        {
            return new List<GetPostResponseDto>();
        }

        var friendsIdGuids = friendsResponse.Message.Data.FriendsIds.Select(Guid.Parse);

        var posts = await _postRepository.GetFriendsPublicAsync(postIteration.Id,
            friendsIdGuids);
        
        if (posts.IsNullOrEmpty())
        {
            return new List<GetPostResponseDto>();
        }

        var authors = await _userService.GetUsersByIdsArray(posts.Select(u => u.ExternalAuthorId));

        var getPostResponseDtos = _mapper.Map<IEnumerable<Domain.Entities.Post.Post>, List<GetPostResponseDto>>(
            posts,
            f => f.AfterMap((src, dest) =>
            {
                dest.ForEach(s =>
                {
                    var originalData = src.First(c => c.Id.Equals(Guid.Parse(s.Id)));
                    var author = authors.FirstOrDefault(a => a.Id.Equals(originalData.ExternalAuthorId));
                    if (author is not null) s.Author = _mapper.Map<PostAuthorDto>(author);
                    var reaction = originalData.PostReactions.FirstOrDefault(x => x.ExternalAuthorId.Equals(userIdGuid));
                    if (reaction is null) return;
                    s.PostReaction = _mapper.Map<PostReactionDto>(reaction);
                    s.IsLikedByUser = true;
                });
            })
        );

        return getPostResponseDtos;
    }
    
    public async Task<CreatePostResponseDto> CreatePost(string userId, CreatePostRequestDto createPostRequestDto)
    {
        var userIdGuid = userId.ParseGuid();
        var postIteration = await _postIterationRepository.GetNewest();
        
        if (postIteration is null)
            throw new PostIterationNotFoundException();
        
        var tags = await CalculateTags(userIdGuid, createPostRequestDto.Description);

        var post = new Domain.Entities.Post.Post
        {
            ExternalAuthorId = userIdGuid,
            Description = createPostRequestDto.Description,
            Title = createPostRequestDto.Title,
            Tags = tags,
            Iteration = postIteration
        };

        await _postRepository.AddAsync(post);
        await _postRepository.SaveAsync();

        var result = _mapper.Map<CreatePostResponseDto>(post);
        return result;
    }
    
    public async Task<GetPostResponseDto> GetCurrentUserPost(string userId)
    {
        var userIdGuid = userId.ParseGuid();
        var postIteration = await _postIterationRepository.GetNewest();

        if (postIteration is null)
            throw new PostIterationNotFoundException();

        var post = await _postRepository.GetByExternalAuthorIdAsync(postIteration.Id, userIdGuid);

        if (post is null)
            throw new PostNotFoundException();
        
        
        var author = await _userService.GetUserById(post.ExternalAuthorId);

        return _mapper.Map<Domain.Entities.Post.Post, GetPostResponseDto>(
            post,
            f => f.AfterMap((src, dest) =>
            {
                if (author is not null) dest.Author = _mapper.Map<PostAuthorDto>(author);
                var reaction = src.PostReactions.FirstOrDefault(x => x.ExternalAuthorId.Equals(userIdGuid));
                if (reaction is null) return;
                dest.PostReaction = _mapper.Map<PostReactionDto>(reaction);
                dest.IsLikedByUser = true;
            })
        );
    }
    
    public async Task<GetPostResponseDto> EditPostsMetas(string userId, string postId,
        EditPostRequestDto editPostRequestDto)
    {
        var userIdGuid = userId.ParseGuid();
        var postIdGuid = postId.ParseGuid();
        var postIteration = await _postIterationRepository.GetNewest();
        
        if (postIteration is null)
            throw new PostIterationNotFoundException();
        
        var post = await _postRepository.GetByIdAndAuthorIdAsync(postIteration.Id, postIdGuid, userIdGuid);

        if (post is null)
            throw new PostNotFoundException();

        if (!editPostRequestDto.Title.IsNullOrEmpty())
        {
            post.Title = editPostRequestDto.Title;
        }

        if (!editPostRequestDto.Description.IsNullOrEmpty())
        {
            post.Description = editPostRequestDto.Description;
            post.Tags = await CalculateTags(userIdGuid, editPostRequestDto.Description);
        }

        await _postRepository.SaveAsync();
        return _mapper.Map<GetPostResponseDto>(post);
    }
    
    private async Task<IList<Tag>> CalculateTags(Guid authorId, string description)
    {
        await using var dbContextTransaction = await _tagRepository.StartTransactionAsync();
        var descriptionWords =
            description.Split(new[] { ' ', ',', '.', ';', '?', '!' }, StringSplitOptions.RemoveEmptyEntries);

        var tagsString = descriptionWords.Where(x => x.StartsWith("#")).ToList();

        var tags = await _tagRepository.GetByNamesAsync(tagsString);

        tagsString.ForEach(x =>
        {
            var existingTag = tags.FirstOrDefault(y => y.Name.ToLower().Equals(x.ToLower()));
            if (existingTag == null)
            {
                tags.Add(new Tag
                {
                    Name = x,
                    ExternalAuthorId = authorId
                });
            }
        });

        await _tagRepository.SaveAsync();
        await dbContextTransaction.CommitAsync();

        return tags;
    }
}
