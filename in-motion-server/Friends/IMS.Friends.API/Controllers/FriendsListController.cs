using IMS.Friends.IBLL.Services;
using IMS.Friends.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;
using IMS.Shared.Utils.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Friends.API.Controllers;

[Route("api/friends/lists")]
[ApiController]
public class FriendsListController: ControllerBase
{
    private readonly IFriendsListsService _friendsListsService;
    private readonly ILogger<FriendsListController> _logger;

    public FriendsListController(
        IFriendsListsService friendsListsService, 
        ILogger<FriendsListController> logger
        )
    {
        _friendsListsService = friendsListsService;
        _logger = logger;
    }

    [Authorize]
    [HttpGet("accepted")]
    public async Task<ActionResult<ImsHttpMessage<IEnumerable<AcceptedFriendshipDto>>>> GetAccepted()
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var responseData = await _friendsListsService.GetFriendsAsync(userIdString);
        return Ok(new ImsHttpMessage<IEnumerable<AcceptedFriendshipDto>>
        {
            Status = StatusCodes.Status200OK,
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.Now,
            Data = responseData
        });
    }

    [Authorize]
    [HttpGet("requested")]
    public async Task<ActionResult<ImsHttpMessage<IEnumerable<RequestFriendshipDto>>>> GetRequested()
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var responseData = await _friendsListsService.GetRequestsAsync(userIdString);
        return Ok(new ImsHttpMessage<IEnumerable<RequestFriendshipDto>>
        {
            Status = StatusCodes.Status200OK,
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.Now,
            Data = responseData
        });
    }
    
    [Authorize]
    [HttpGet("invited")]
    public async Task<ActionResult<ImsHttpMessage<IEnumerable<InvitationFriendshipDto>>>> GetInvited()
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var responseData = await _friendsListsService.GetInvitationsAsync(userIdString);
        return Ok(new ImsHttpMessage<IEnumerable<InvitationFriendshipDto>>
        {
            Status = StatusCodes.Status200OK,
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.Now,
            Data = responseData
        });
    }
}
