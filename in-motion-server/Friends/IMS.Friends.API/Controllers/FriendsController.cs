using IMS.Friends.IBLL.Services;
using IMS.Friends.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;
using IMS.Shared.Utils.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Friends.API.Controllers;

[ApiController]
[Route("api/friends")]
public class FriendsController : ControllerBase
{
    private readonly ILogger<FriendsController> _logger;
    private readonly IFriendsService _friendsService;

    public FriendsController(
        ILogger<FriendsController> logger,
        IFriendsService friendsService
    )
    {
        _logger = logger;
        _friendsService = friendsService;
    }

    [Authorize]
    [HttpPost("{externalUserIdString}")]
    public async Task<ActionResult<ImsHttpMessage<RequestFriendshipDto>>> SendFriendshipInvitation(
        [FromRoute(Name = "externalUserIdString")]
        string externalUserIdString)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);

        var response = await _friendsService.CreateFriendshipRequest( userIdString, externalUserIdString);
        var result = new ImsHttpMessage<RequestFriendshipDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status201Created,
            Data = response
        };

        return Created("", result);
    }

    [Authorize]
    [HttpPut("accept/{friendshipIdString}")]
    public async Task<ActionResult<ImsHttpMessage<AcceptedFriendshipDto>>> AcceptedFriendship(
        [FromRoute(Name = "friendshipIdString")]
        string friendshipIdString)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);

        var response = await _friendsService.AcceptFriendshipInvitation(userIdString, friendshipIdString);
        var result = new ImsHttpMessage<AcceptedFriendshipDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = response
        };

        return Ok(result);
    }

    [Authorize]
    [HttpPatch("reject/{friendshipIdString}")]
    public async Task<ActionResult<ImsHttpMessage<RejectedFriendshipDto>>> RejectFriendship(
        [FromRoute(Name = "friendshipIdString")]
        string friendshipIdString)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);

        var response = await _friendsService.RejectFriendshipInvitation(userIdString, friendshipIdString);
        var result = new ImsHttpMessage<RejectedFriendshipDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = response
        };

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("revert/{friendshipIdString}")]
    public async Task<ActionResult<ImsHttpMessage<bool>>> RevertFriendshipRequest(
        [FromRoute(Name = "friendshipIdString")]
        string friendshipIdString)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);

        await _friendsService.RevertFriendshipInvitation(userIdString, friendshipIdString);
        var result = new ImsHttpMessage<bool>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status204NoContent,
            Data = true
        };

        return result;
    }

    [Authorize]
    [HttpDelete("unfriend/{friendshipIdString}")]
    public async Task<ActionResult<ImsHttpMessage<RejectedFriendshipDto>>> Unfriend(
        [FromRoute(Name = "friendshipIdString")] string friendshipIdString)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);

        var response = await _friendsService.UnfriendExistingFriendship(userIdString, friendshipIdString);
        var result = new ImsHttpMessage<RejectedFriendshipDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = response
        };

        return Ok(result);
    }
}