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
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reactionId"></param>
    /// <returns></returns>
    /// <response code="200">Returns founded item</response>
    /// <response code="404">If the item is not existing</response>
    /// <response code="403">If user is unauthorized</response>
    [HttpGet("{reactionId}")]
    public ActionResult<PostReactionDto> GetReaction([FromRoute] string reactionId)
    {
        return Ok(new PostReactionDto());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="createPostReactionDto"></param>
    /// <returns></returns>
    /// <response code="200">Returns founded item</response>
    /// <response code="404">If the item is not existing</response>
    /// <response code="403">If user is unauthorized</response>
    [HttpPost]
    public ActionResult<PostReactionDto> CreateReaction([FromBody] CreatePostReactionDto createPostReactionDto)
    {
        return Created("",new PostReactionDto());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reactionId"></param>
    /// <param name="createUserProfileVideoReactionDto"></param>
    /// <returns></returns>
    /// <response code="200">Returns founded item</response>
    /// <response code="404">If the item is not existing</response>
    /// <response code="403">If user is unauthorized</response>
    [HttpPut("{reactionId}")]
    public ActionResult<PostReactionDto> UpdateReaction(
        [FromRoute] string reactionId, 
        [FromBody] CreatePostReactionDto createUserProfileVideoReactionDto)
    {
        return Ok(new PostReactionDto());
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