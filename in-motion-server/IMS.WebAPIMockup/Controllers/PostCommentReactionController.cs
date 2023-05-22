using System.Net.Mime;
using IMS.Shared.Models.Dto.Post.Comment.Reaction;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebAPIMockup.Controllers;
// TODO: Finish documentation
[ApiController]
[Route("api/v{version:apiVersion}/posts/comments/reactions")]
[Produces(MediaTypeNames.Application.Json)]
public class PostCommentReactionController: ControllerBase
{
    [HttpGet("{reactionId}")]
    public ActionResult<PostCommentReactionDto> GetReaction([FromRoute] string reactionId)
    {
        return Ok(new PostCommentReactionDto());
    }

    [HttpPost]
    public ActionResult<PostCommentReactionDto> CreateReaction([FromBody] CreatePostCommentReactionDto createPostCommentReactionDto)
    {
        return Created("",new PostCommentReactionDto());
    }

    [HttpPut("{reactionId}")]
    public ActionResult<PostCommentReactionDto> UpdateReaction(
        [FromRoute] string reactionId, 
        [FromBody] CreatePostCommentReactionDto createPostCommentReactionDto)
    {
        return Ok(new PostCommentReactionDto());
    }

    [HttpDelete("{reactionId}")]
    public IActionResult RemoveReaction([FromRoute] string reactionId)
    {
        return Ok();
    }
}