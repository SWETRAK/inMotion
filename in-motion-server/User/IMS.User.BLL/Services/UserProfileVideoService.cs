using AutoMapper;
using IMS.Shared.Models.Exceptions;
using IMS.User.IBLL.Services;
using IMS.User.IDAL.Repositories;
using IMS.User.Models.Dto.Outgoing;
using Microsoft.Extensions.Logging;

namespace IMS.User.BLL.Services;

public class UserProfileVideoService: IUserProfileVideoService
{
    private readonly IUserProfileVideoRepository _userProfileVideoRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UserProfileVideoService> _logger;

    public UserProfileVideoService(
        IMapper mapper, 
        ILogger<UserProfileVideoService> logger, 
        IUserProfileVideoRepository userProfileVideoRepository)
    {

        _mapper = mapper;
        _logger = logger;
        _userProfileVideoRepository = userProfileVideoRepository;
    }
    
    public async Task<UserProfileVideoDto> GetUserProfileVideoByAuthorAsync(string userId)
    {
        if (!Guid.TryParse(userId, out var userIdGuid))
            throw new InvalidGuidStringException();

        var image = await _userProfileVideoRepository.GetByAuthorIdAsync(userIdGuid);

        if (image is null)
            
            throw new Exception();

        return _mapper.Map<UserProfileVideoDto>(image);
    }

    public async Task<UserProfileVideoDto> GetUserProfileVideoByVideoIdAsync(string videoId)
    {
        if (!Guid.TryParse(videoId, out var videoIdGuid))
            throw new InvalidGuidStringException();

        var image = await _userProfileVideoRepository.GetByIdAsync(videoIdGuid);
        
        if (image is null)
            throw new Exception();

        return _mapper.Map<UserProfileVideoDto>(image);
    }
}