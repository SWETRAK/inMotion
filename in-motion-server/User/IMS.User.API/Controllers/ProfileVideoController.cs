using IMS.Shared.Models.Dto;
using IMS.User.IBLL.Services;
using IMS.User.Models.Dto.Outgoing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.User.API.Controllers;

[ApiController]
[Route("users/profileVideos")]
public class ProfileVideoController: ControllerBase
{
    private readonly ILogger<ProfileVideoController> _logger;
    private readonly IUserProfileVideoService _userProfileVideoService;

    public ProfileVideoController(
        ILogger<ProfileVideoController> logger, 
        IUserProfileVideoService userProfileVideoService)
    {
        _logger = logger;
        _userProfileVideoService = userProfileVideoService;
    }

    [Authorize]
    [HttpGet("byVideo/{videoId}")]
    public async Task<ActionResult<ImsHttpMessage<UserProfileVideoDto>>> GetProfileVideoByUserId([FromRoute(Name = "videoId")] string videoId)
    {
        var serverRequestTime = DateTime.UtcNow;
        var responseData = await _userProfileVideoService.GetUserProfileVideoByVideoIdAsync(videoId);
        
        return Ok(new ImsHttpMessage<UserProfileVideoDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = responseData
        });
    }
    
    [Authorize]
    [HttpGet("byUser/{userId}")]
    public async Task<ActionResult<ImsHttpMessage<UserProfileVideoDto>>> GetProfileVideoByProfileId([FromRoute(Name = "userId")] string userId)
    {
        var serverRequestTime = DateTime.UtcNow;
        var responseData = await _userProfileVideoService.GetUserProfileVideoByAuthorAsync(userId);
        
        return Ok(new ImsHttpMessage<UserProfileVideoDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = responseData
        });
    }
 }