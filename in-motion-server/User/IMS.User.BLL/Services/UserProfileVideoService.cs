using AutoMapper;
using IMS.Shared.Models.Exceptions;
using IMS.User.IBLL.Services;
using IMS.User.IDAL.Repositories;
using IMS.User.Models.Dto.Incoming;
using Microsoft.Extensions.Logging;

namespace IMS.User.BLL.Services;

public class UserProfileVideoService: IUserProfileVideoService
{
    private readonly IUserProfileVideoRepository _userProfileVideoRepository;
    private readonly IUserMetasRepository _userMetasRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UserProfileVideoService> _logger;

    public UserProfileVideoService(
        IUserProfileVideoRepository userProfileVideoRepository, 
        IMapper mapper, 
        ILogger<UserProfileVideoService> logger, 
        IUserMetasRepository userMetasRepository)
    {
        _userProfileVideoRepository = userProfileVideoRepository;
        _mapper = mapper;
        _logger = logger;
        _userMetasRepository = userMetasRepository;
    }
    
    public async Task UpdateUserProfileVideo(string userIdString, UpdateUserProfileVideoDto updateUserProfileVideoDto)
    {
        if (Guid.TryParse(userIdString, out var userIdGuid))
            throw new InvalidUserGuidStringException();

        // Check if user exists in database;
        var userMetas = await _userMetasRepository.GetByExternalUserIdWithProfileVideoAsync(userIdGuid);
        if (userMetas is null)
            throw new UserNotFoundException();
        
        
        
        throw new NotImplementedException();
    }
}