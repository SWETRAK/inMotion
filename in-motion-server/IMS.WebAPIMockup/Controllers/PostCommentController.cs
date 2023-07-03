using System.Net.Mime;
using System.Security;
using IMS.Shared.Models.Dto.Post.Comment;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebAPIMockup.Controllers;

// TODO: Finish documentation
[ApiController]
[Route("api/v{version:apiVersion}/posts/comments")]
[Produces(MediaTypeNames.Application.Json)]
public class PostCommentController: ControllerBase
{
    
    /// <summary>
    /// Get comment from id
    /// </summary>
    /// <param name="commentId"></param>
    /// <response code="200">Returns founded item</response>
    /// <response code="404">If the item is not existing</response>
    /// <response code="403">If user is unauthorized</response>
    /// <returns>PostCommentDto as comment result</returns>
    [ProducesResponseType(typeof(PostCommentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{commentId}")]
    public ActionResult<PostCommentDto> GetPostComment([FromRoute] string commentId)
    {
        return Ok(new PostCommentDto());
    }
    
    /// <summary>
    /// Create comment for post
    /// </summary>
    /// <param name="createPostCommentDto"></param>
    /// <returns>PostCommentDto with new post content</returns>
    /// <response code="200">Returns founded item</response>
    /// <response code="404">If the item is not existing</response>
    /// <response code="403">If user is unauthorized</response>
    [ProducesResponseType(typeof(PostCommentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost]
    public ActionResult<PostCommentDto> CreatePostComment([FromBody] CreatePostCommentDto createPostCommentDto)
    {
        return Created("", new PostCommentDto());
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="commentId"></param>
    /// <param name="createPostCommentDto"></param>
    /// <returns></returns>
    /// <response code="200">Returns founded item</response>
    /// <response code="404">If the item is not existing</response>
    /// <response code="403">If user is unauthorized</response>
    [ProducesResponseType(typeof(PostCommentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{commentId}")]
    public ActionResult<PostCommentDto> UpdatePostComment([FromRoute] string commentId, [FromBody] CreatePostCommentDto createPostCommentDto)
    {
        return Ok(new PostCommentDto());
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="commentId"></param>
    /// <returns></returns>
    /// <response code="200">Returns founded item</response>
    /// <response code="404">If the item is not existing</response>
    /// <response code="403">If user is unauthorized</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{commentId}")] 
    public ActionResult RemovePostComment([FromRoute] SecureString commentId)
    {
        return Ok();
    }
}