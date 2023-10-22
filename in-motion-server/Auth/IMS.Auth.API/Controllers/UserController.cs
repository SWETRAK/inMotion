using IMS.Auth.IBLL.Services;
using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;
using IMS.Shared.Utils.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Auth.App.Controllers;

[ApiController]
[Route("api/user")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [Authorize]
    [HttpPut("email")]
    public async Task<ActionResult<ImsHttpMessage<UserInfoDto>>> UpdateUserEmail(UpdateEmailDto updateEmailDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var result = await _userService.UpdateUserEmail(updateEmailDto, userIdString);

        return Ok(new ImsHttpMessage<UserInfoDto>
        {
            Data = result,
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status204NoContent
        });
    }

    [Authorize]
    [HttpPut("nickname")]
    public async Task<ActionResult<ImsHttpMessage<UserInfoDto>>> UpdateUserNickname(UpdateNicknameDto updateNicknameDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var result = await _userService.UpdateUserNickname(updateNicknameDto, userIdString);

        return Ok(new ImsHttpMessage<UserInfoDto>
        {
            Data = result,
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status204NoContent
        });
    }
}