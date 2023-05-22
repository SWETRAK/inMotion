using System.Net.Mime;
using IMS.Shared.Models.Dto.User.Video.Reaction;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebAPIMockup.Controllers;

// TODO: Finish documentation
[ApiController]
[Route("user/v{version:apiVersion}/video/reactions")]
[Produces(MediaTypeNames.Application.Json)]
public class UserProfileVideoReactionController: ControllerBase
{
    [HttpGet("{reactionId}")]
    public ActionResult<UserProfileVideoReactionDto> GetReaction([FromRoute] string reactionId)
    {
        return Ok(new UserProfileVideoReactionDto());
    }

    [HttpPost]
    public ActionResult<UserProfileVideoReactionDto> CreateReaction([FromBody] CreateUserProfileVideoReactionDto createUserProfileVideoReactionDto)
    {
        return Created("",new UserProfileVideoReactionDto());
    }

    [HttpPut("{reactionId}")]
    public ActionResult<UserProfileVideoReactionDto> UpdateReaction(
        [FromRoute] string reactionId, 
        [FromBody] CreateUserProfileVideoReactionDto createUserProfileVideoReactionDto)
    {
        return Ok(new UserProfileVideoReactionDto());
    }

    [HttpDelete("{reactionId}")]
    public IActionResult RemoveReaction([FromRoute] string reactionId)
    {
        return Ok();
    }
}