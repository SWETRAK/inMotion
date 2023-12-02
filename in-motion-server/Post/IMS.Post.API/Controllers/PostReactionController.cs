using IMS.Post.IBLL.Services;
using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;
using IMS.Shared.Utils.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Post.API.Controllers;

[ApiController]
[Route("api/posts/reactions")]
public class PostReactionController: ControllerBase
{
    private readonly IPostReactionService _postReactionService;

    public PostReactionController(IPostReactionService postReactionService)
    {
        _postReactionService = postReactionService;
    }

    [Authorize]
    [HttpGet("{postId}")]
    public async Task<ActionResult<ImsHttpMessage<IEnumerable<PostReactionDto>>>> GetPostReactionsAsync(
        [FromRoute(Name = "postId")] string postId)
    {
        var serverRequestTime = DateTime.UtcNow;
        var response =
            await _postReactionService.GetForPostAsync(postId);

        return Ok(new ImsHttpMessage<IEnumerable<PostReactionDto>>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = response,
        });
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ImsHttpMessage<PostReactionDto>>> CreatePostReaction(
        [FromBody] CreatePostReactionDto createPostReactionDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var response = await _postReactionService.CreatePostReactionAsync(userIdString, createPostReactionDto);

        return Created("", new ImsHttpMessage<PostReactionDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = response,
        });
    }

    [Authorize]
    [HttpPut("{reactionId}")]
    public async Task<ActionResult<ImsHttpMessage<PostReactionDto>>> EditPostReactionAsync(
        [FromRoute(Name = "reactionId")] string reactionId, 
        [FromBody] EditPostReactionDto editPostReactionDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var response = await _postReactionService.EditPostReactionAsync(userIdString, reactionId, editPostReactionDto);

        return Ok(new ImsHttpMessage<PostReactionDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = response,
        });
    }

    [Authorize]
    [HttpDelete("{reactionId}")]
    public async Task<ActionResult<ImsHttpMessage<bool>>> DeletePostReactionAsync(
        [FromRoute(Name = "reactionId")] string reactionId)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        await _postReactionService.DeletePostReactionAsync(userIdString, reactionId);

        return Ok(new ImsHttpMessage<bool>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = true,
        });
    }
}