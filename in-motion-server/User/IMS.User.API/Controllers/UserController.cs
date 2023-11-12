using IMS.Shared.Models.Dto;
using IMS.Shared.Utils.Authentication;
using IMS.User.IBLL.Services;
using IMS.User.Models.Dto.Incoming;
using IMS.User.Models.Dto.Outgoing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.User.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet("search/{nickname}")]
    public async Task<ActionResult<ImsHttpMessage<IEnumerable<FullUserInfoDto>>>> GetUsersByNickname(
        [FromRoute(Name = "nickname")] string nickname)
    {
        var serverRequestTime = DateTime.UtcNow;
        var response = await _userService.GetFullUsersInfoByNicknameAsync(nickname);
        
        return Ok(new ImsHttpMessage<IEnumerable<FullUserInfoDto>>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = response
        });
    }

    [Authorize]
    [HttpGet("{userId}")]
    public async Task<ActionResult<ImsHttpMessage<FullUserInfoDto>>> GetById([FromRoute(Name = "userId")] string userId)
    {
        var serverRequestTime = DateTime.UtcNow;
        var responseUser = await _userService.GetFullUserInfoAsync(userId);
        return Ok(new ImsHttpMessage<FullUserInfoDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = responseUser
        });
    }

    [Authorize]
    [HttpPut("update/bio")]
    public async Task<ActionResult<ImsHttpMessage<UpdatedUserBioDto>>> UpdateBio([FromBody] UpdateUserBioDto updateUserBioDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var responseUser = await _userService.UpdateBioAsync(userIdString, updateUserBioDto);
        return Ok(new ImsHttpMessage<UpdatedUserBioDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = responseUser
        });
    }
}
