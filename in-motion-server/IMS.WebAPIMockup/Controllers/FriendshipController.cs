using IMS.Shared.Models.Dto.Friends;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebAPIMockup.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/friendships")]
public class FriendshipController: ControllerBase
{
    [HttpGet]
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