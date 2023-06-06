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
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reactionId"></param>
    /// <returns></returns>
    /// <response code="200">Returns founded item</response>
    /// <response code="404">If the item is not existing</response>
    /// <response code="403">If user is unauthorized</response>
    [HttpGet("{reactionId}")]
    public ActionResult<UserProfileVideoReactionDto> GetReaction([FromRoute] string reactionId)
    {
        return Ok(new UserProfileVideoReactionDto());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="createUserProfileVideoReactionDto"></param>
    /// <returns></returns>
    /// <response code="200">Returns founded item</response>
    /// <response code="404">If the item is not existing</response>
    /// <response code="403">If user is unauthorized</response>
    [HttpPost]
    public ActionResult<UserProfileVideoReactionDto> CreateReaction([FromBody] CreateUserProfileVideoReactionDto createUserProfileVideoReactionDto)
    {
        return Created("",new UserProfileVideoReactionDto());
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
    public ActionResult<UserProfileVideoReactionDto> UpdateReaction(
        [FromRoute] string reactionId, 
        [FromBody] CreateUserProfileVideoReactionDto createUserProfileVideoReactionDto)
    {
        return Ok(new UserProfileVideoReactionDto());
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