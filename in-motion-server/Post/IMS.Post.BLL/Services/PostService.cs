using AutoMapper;
using IMS.Post.Domain.Entities.Other;
using IMS.Post.IBLL.Services;
using IMS.Post.IDAL.Repositories.Other;
using IMS.Post.IDAL.Repositories.Post;
using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Dto.Outgoing;
using IMS.Post.Models.Exceptions;
using IMS.Post.Models.Models;
using IMS.Shared.Models.Dto;
using IMS.Shared.Models.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace IMS.Post.BLL.Services;

// TODO: Test this logic
public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ILocalizationRepository _localizationRepository;
    private readonly ILogger<PostService> _logger;
    private readonly IMapper _mapper;

    public PostService(IPostRepository postRepository,
        ILogger<PostService> logger,
        IMapper mapper,
        ITagRepository tagRepository,
        ILocalizationRepository localizationRepository)
    {
        _postRepository = postRepository;
        _logger = logger;
        _mapper = mapper;
        _tagRepository = tagRepository;
        _localizationRepository = localizationRepository;
    }

    public async Task<ImsPagination<IEnumerable<GetPostResponseDto>>> GetPublicPostsFromCurrentDay(
        ImsPaginationRequestDto paginationRequestDto)
    {
        var posts = await _postRepository.GetPublicFormDayPaginatedAsync(DateTime.UtcNow,
            paginationRequestDto.PageNumber, paginationRequestDto.PageSize);
        var getPostResponseDtos = _mapper.Map<IEnumerable<Domain.Entities.Post.Post>, IEnumerable<GetPostResponseDto>>(
            posts,
            f => f.AfterMap((src, dest) =>
            {
                // TODO: Add users into response
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
        ImsPaginationRequestDto paginationRequestDto)
    {
        //TODO: Add consumer/service for getting userInfo with image
        var friendsList = new List<Author>() { };

        var posts = await _postRepository.GetFriendsPublicFromDayPaginatedAsync(DateTime.UtcNow,
            friendsList.Select(y => y.Id), paginationRequestDto.PageNumber, paginationRequestDto.PageSize);

        var getPostResponseDtos = _mapper.Map<IEnumerable<Domain.Entities.Post.Post>, IEnumerable<GetPostResponseDto>>(
            posts,
            f => f.AfterMap((src, dest) =>
            {
                // TODO: Add users into response
            })
        );

        return new ImsPagination<IEnumerable<GetPostResponseDto>>
        {
            PageSize = paginationRequestDto.PageSize,
            PageNumber = paginationRequestDto.PageNumber,
            Data = getPostResponseDtos
        };
    }

    // TODO: Add validator for CreatePostRequestDto and nested classes
    // TODO: Add mapper for CreatePostResponseDto 
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

    // TODO: Change return structure to dedicated for this endpoint
    public async Task<GetPostResponseDto> GetCurrentUserPost(string userId)
    {
        if (!Guid.TryParse(userId, out var userIdGuid))
            throw new InvalidGuidStringException();

        var post = await _postRepository.GetByExternalAuthorIdAsync(DateTime.UtcNow, userIdGuid);

        if (post is null)
            throw new PostNotFoundException();

        return _mapper.Map<GetPostResponseDto>(post);
    }

    // TODO: Edit your post meats
    // TODO: Implement return structure to proper file 
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
