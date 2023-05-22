using System.Net.Mime;
using IMS.Shared.Models.Dto.Friends;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebAPIMockup.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/friendships")]
[Produces(MediaTypeNames.Application.Json)]
public class FriendshipController: ControllerBase
{
    /// <summary>
    /// Return lists of friends
    /// </summary>
    /// <returns>List of user friend</returns>
    /// <response code="200">When user is authorized their friendships</response>
    /// <response code="401">If user is unauthorized</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FriendshipsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<IEnumerable<FriendshipsDto>> GetFriendships()
    {
        return Ok(new List<FriendshipsDto>() {new FriendshipsDto(), new FriendshipsDto()});
    }

    /// <summary>
    /// Return lists of friendship request
    /// </summary>
    /// <returns>List of friendship requests</returns>
    /// <response code="200">If friendship list is returned</response>
    /// <response code="401">If user is unauthorized</response>
    [ProducesResponseType(typeof(IEnumerable<FriendshipsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("requests")]
    public ActionResult<IEnumerable<FriendshipsDto>> GetRequests()
    {
        return Ok(new List<FriendshipsDto>() {new FriendshipsDto(), new FriendshipsDto()});
    }

    /// <summary>
    /// Method to create friend request
    /// </summary>
    /// <param name="createFriendshipDto">Data needed to create friendship</param>
    /// <returns>Newly created relationship</returns>
    /// <response code="200">If user send request to other user</response>
    /// <response code="401">If user is unauthorized</response>
    [ProducesResponseType(typeof(FriendshipsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost]
    public ActionResult<FriendshipsDto> SendRequest([FromBody] CreateFriendshipDto createFriendshipDto)
    {
        return Created("", new FriendshipsDto());
    }

    /// <summary>
    /// Method to accept or reject friendship request
    /// </summary>
    /// <param name="friendshipId">Is UUID format id of friendship</param>
    /// <param name="status">Is Enum value in range of Waiting = 0, Accepted = 1, Rejected = 2</param>
    /// <returns>Updated friendship data</returns>
    /// <response code="200">If friendship status was changed</response>
    /// <response code="401">If user is unauthorized</response>
    /// <response code="404">If relation is notfound</response> 
    [ProducesResponseType(typeof(FriendshipsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{friendshipId}/{status}")]
    public ActionResult<FriendshipsDto> ChangeStatus(
        [FromRoute(Name = "friendshipId")] string friendshipId,
        [FromRoute(Name = "status")] int status)
    {
        return Ok(new FriendshipsDto());
    }
}