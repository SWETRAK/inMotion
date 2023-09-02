using System.Security.Policy;
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
    private readonly IUserProfileVideoService _userProfileVideoService;
    private readonly IMapper _mapper;
    private readonly ILogger<UserSoapService> _logger;

    public UserSoapService(
        IUserService userService,
        ILogger<UserSoapService> logger,
        IMapper mapper, IUserProfileVideoService userProfileVideoService)
    {
        _userService = userService;
        _logger = logger;
        _mapper = mapper;
        _userProfileVideoService = userProfileVideoService;
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

    public async Task<bool> UpdateUserProfileVideo(string userId, UpdateUserProfileVideoSoapDto updateUserProfileVideoSoapDto)
    {
        var requestData = _mapper.Map<UpdateUserProfileVideoDto>(updateUserProfileVideoSoapDto);
        try
        {
            await _userProfileVideoService.UpdateUserProfileVideo(userId, requestData);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user profile video");
            return false;
        }
    }
}