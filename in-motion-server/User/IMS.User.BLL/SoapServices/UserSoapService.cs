using AutoMapper;
using IMS.User.IBLL.Services;
using IMS.User.IBLL.SoapServices;
using IMS.User.Models.Dto.Incoming;
using IMS.User.Models.Dto.Incoming.Soap;
using IMS.User.Models.Dto.Outgoing.Soap;
using Microsoft.Extensions.Logging;

namespace IMS.User.BLL.SoapServices;

public class UserSoapService : IUserSoapService
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ILogger<UserSoapService> _logger;

    public UserSoapService(
        IUserService userService,
        ILogger<UserSoapService> logger, IMapper mapper)
    {
        _userService = userService;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<FullUserInfoSoapDto> GetFullUserInfo(string userId)
    {
        try
        {
            var user = await _userService.GetFullUserInfoAsync(userId);
            return _mapper.Map<FullUserInfoSoapDto>(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting full user info");
            return null;
        }
    }

    public async Task<IEnumerable<FullUserInfoSoapDto>> GetFullUsersInfoByEmail(IEnumerable<string> userIds)
    {
        try
        {
            var users = await _userService.GetFullUsersInfoAsync(userIds.ToList());
            return _mapper.Map<IEnumerable<FullUserInfoSoapDto>>(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting full users info by email");
            return null;
        }
    }

    public async Task<UpdateUserProfileVideoSoapDto> UpdateUserProfileVideo(string userId, UpdateUserProfileVideoSoapDto updateUserProfileVideoSoapDto)
    {
        var requestData = _mapper.Map<UpdateUserProfileVideoDto>(updateUserProfileVideoSoapDto);
        try
        {
            var updatedUserProfileVideo = await _userService.UpdateUserProfileVideo(userId, requestData);
            var responseMessage = _mapper.Map<UpdateUserProfileVideoSoapDto>(updatedUserProfileVideo);
            return responseMessage;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user profile video");
            return null;
        }
    }
}