using System.Net.Mime;
using IMS.Shared.Models.Dto.Post.Reaction;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebAPIMockup.Controllers;

// TODO: Finish documentation
[ApiController]
[Route("api/v{version:apiVersion}/post/reactions")]
[Produces(MediaTypeNames.Application.Json)]
public class PostReactionController: ControllerBase
{
    [HttpGet("{reactionId}")]
    public ActionResult<PostReactionDto> GetReaction([FromRoute] string reactionId)
    {
        return Ok(new PostReactionDto());
    }

    [HttpPost]
    public ActionResult<PostReactionDto> CreateReaction([FromBody] CreatePostReactionDto createPostReactionDto)
    {
        return Created("",new PostReactionDto());
    }

    [HttpPut("{reactionId}")]
    public ActionResult<PostReactionDto> UpdateReaction(
        [FromRoute] string reactionId, 
        [FromBody] CreatePostReactionDto createUserProfileVideoReactionDto)
    {
        return Ok(new PostReactionDto());
    }

    [HttpDelete("{reactionId}")]
    public IActionResult RemoveReaction([FromRoute] string reactionId)
    {
        return Ok();
    }
}