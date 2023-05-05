using IMS.Shared.Models.Dto.Friends;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebAPIMockup.Controllers;

// TODO: Finish documentation
[ApiController]
[Route("api/v{version:apiVersion}/friendships")]
public class FriendshipController: ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FriendshipsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<IEnumerable<FriendshipsDto>> GetFriendships()
    {
        return Ok(new List<FriendshipsDto>() {new FriendshipsDto(), new FriendshipsDto()});
    }

    [HttpGet("requests")]
    public ActionResult<IEnumerable<FriendshipsDto>> GetRequests()
    {
        return Ok(new List<FriendshipsDto>() {new FriendshipsDto(), new FriendshipsDto()});
    }

    [HttpPost]
    public ActionResult<FriendshipsDto> SendRequest([FromBody] CreateFriendshipDto createFriendshipDto)
    {
        return Created("", new FriendshipsDto());
    }

    [HttpPut("{friendshipId}/{status}")]
    public ActionResult<FriendshipsDto> ChangeStatus(
        [FromRoute(Name = "friendshipId")] string friendshipId,
        [FromRoute(Name = "status")] int status)
    {
        return Ok(new FriendshipsDto());
    }
}