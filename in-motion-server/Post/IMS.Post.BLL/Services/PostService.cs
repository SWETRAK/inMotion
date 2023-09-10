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
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace IMS.Post.BLL.Services;

// TODO: Test this logic
// TODO: Change logic to get by date and current iteration, not by only date
public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ILocalizationRepository _localizationRepository;
    private readonly ILogger<PostService> _logger;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IRequestClient<ImsBaseMessage<GetUserFriendsMessage>> _getUserFriendsRequestClient;

    public PostService(IPostRepository postRepository,
        ILogger<PostService> logger,
        IMapper mapper,
        ITagRepository tagRepository,
        ILocalizationRepository localizationRepository,
        IUserService userService, 
        IRequestClient<ImsBaseMessage<GetUserFriendsMessage>> getUserFriendsRequestClient)
    {
        _postRepository = postRepository;
        _logger = logger;
        _mapper = mapper;
        _tagRepository = tagRepository;
        _localizationRepository = localizationRepository;
        _userService = userService;
        _getUserFriendsRequestClient = getUserFriendsRequestClient;
    }

    public async Task<ImsPagination<IEnumerable<GetPostResponseDto>>> GetPublicPostsFromCurrentDay(
        ImsPaginationRequestDto paginationRequestDto)
    {
        var posts = await _postRepository.GetPublicFormDayPaginatedAsync(DateTime.UtcNow,
            paginationRequestDto.PageNumber, paginationRequestDto.PageSize);

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
                });
            })
        );
        var result = new ImsPagination<IEnumerable<GetPostResponseDto>>
        {
            PageSize = paginationRequestDto.PageSize,
            PageNumber = paginationRequestDto.PageNumber,
            Data = getPostResponseDtos
        };

        return result;
    }

    public async Task<ImsPagination<IEnumerable<GetPostResponseDto>>> GetFriendsPublicPostsFromCurrentDay(
        string userId, ImsPaginationRequestDto paginationRequestDto)
    {
        if (!Guid.TryParse(userId, out var userIdGuid))
            throw new InvalidGuidStringException();
        
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

        var friendsIdGuids = friendsResponse.Message.Data.FriendsIds.Select(Guid.Parse);

        var posts = await _postRepository.GetFriendsPublicFromDayPaginatedAsync(DateTime.UtcNow,
            friendsIdGuids, paginationRequestDto.PageNumber, paginationRequestDto.PageSize);

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
                });
            })
        );

        return new ImsPagination<IEnumerable<GetPostResponseDto>>
        {
            PageSize = paginationRequestDto.PageSize,
            PageNumber = paginationRequestDto.PageNumber,
            Data = getPostResponseDtos
        };
    }
    
    public async Task<CreatePostResponseDto> CreatePost(string userId, CreatePostRequestDto createPostRequestDto)
    {
        if (!Guid.TryParse(userId, out var userIdGuid))
            throw new InvalidGuidStringException();

        var tags = await CalculateTags(userIdGuid, createPostRequestDto.Description);
        var localization = await GetLocalization(createPostRequestDto.Localization.Latitude,
            createPostRequestDto.Localization.Longitude,
            createPostRequestDto.Localization.Name);

        var post = new Domain.Entities.Post.Post
        {
            ExternalAuthorId = userIdGuid,
            Description = createPostRequestDto.Description,
            Title = createPostRequestDto.Title,
            Tags = tags,
            Localization = localization
        };

        await _postRepository.SaveAsync();

        var result = _mapper.Map<CreatePostResponseDto>(post);
        return result;
    }
    
    public async Task<GetPostResponseDto> GetCurrentUserPost(string userId)
    {
        if (!Guid.TryParse(userId, out var userIdGuid))
            throw new InvalidGuidStringException();

        var post = await _postRepository.GetByExternalAuthorIdAsync(DateTime.UtcNow, userIdGuid);

        if (post is null)
            throw new PostNotFoundException();

        return _mapper.Map<GetPostResponseDto>(post);
    }
    
    public async Task<GetPostResponseDto> EditPostsMetas(string userId, string postId,
        EditPostRequestDto editPostRequestDto)
    {
        if (Guid.TryParse(userId, out var userIdGuid))
            throw new InvalidGuidStringException();
        if (Guid.TryParse(postId, out var postIdGuid))
            throw new InvalidGuidStringException();

        var post = await _postRepository.GetByIdAndAuthorIdAsync(DateTime.UtcNow, postIdGuid, userIdGuid);

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

    private async Task<Localization> GetLocalization(double latitude, double longitude, string name)
    {
        var localization = await _localizationRepository.GetByCoordinatesOrNameAsync(latitude, longitude, name);
        if (localization is not null) return localization;

        localization = new Localization
        {
            Name = name,
            Latitude = latitude,
            Longitude = longitude
        };
        await _localizationRepository.SaveAsync();
        return localization;
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
