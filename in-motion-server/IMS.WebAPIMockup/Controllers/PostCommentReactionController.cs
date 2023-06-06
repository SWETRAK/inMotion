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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reactionId"></param>
    /// <returns></returns>
    [HttpGet("{reactionId}")]
    public ActionResult<PostCommentReactionDto> GetReaction([FromRoute] string reactionId)
    {
        return Ok(new PostCommentReactionDto());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="createPostCommentReactionDto"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<PostCommentReactionDto> CreateReaction([FromBody] CreatePostCommentReactionDto createPostCommentReactionDto)
    {
        return Created("",new PostCommentReactionDto());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reactionId"></param>
    /// <param name="createPostCommentReactionDto"></param>
    /// <returns></returns>
    /// <response code="200">Returns founded item</response>
    /// <response code="404">If the item is not existing</response>
    /// <response code="403">If user is unauthorized</response>
    [HttpPut("{reactionId}")]
    public ActionResult<PostCommentReactionDto> UpdateReaction(
        [FromRoute] string reactionId, 
        [FromBody] CreatePostCommentReactionDto createPostCommentReactionDto)
    {
        return Ok(new PostCommentReactionDto());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reactionId"></param>
    /// <returns></returns>
    /// <response code="200">Returns founded item</response>
    /// <response code="404">If the item is not existing</response>
    /// <response code="403">If user is unauthorized</response>
    [HttpDelete("{reactionId}")]
    public IActionResult RemoveReaction([FromRoute] string reactionId)
    {
        return Ok();
    }
}