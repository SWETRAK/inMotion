using IMS.Post.IBLL.Services;
using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;
using IMS.Shared.Utils.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Post.API.Controllers;

[ApiController]
[Route("api/postCommentReactions")]
public class PostCommentReactionController: ControllerBase
{
    private readonly IPostCommentReactionService _postCommentReactionService;

    public PostCommentReactionController(IPostCommentReactionService postCommentReactionService)
    {
        _postCommentReactionService = postCommentReactionService;
    }

    [Authorize]
    [HttpGet("{postCommentId}")]
    public async Task<ActionResult<ImsHttpMessage<ImsPagination<IEnumerable<PostCommentReactionDto>>>>> GetAllReactions(
        [FromRoute(Name = "postCommentId")] string postCommentId, [FromBody] ImsPaginationRequestDto paginationRequestDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var response =
            await _postCommentReactionService.GetAllPostCommentReactionsPaginatedAsync(postCommentId,
                paginationRequestDto);

        return Ok(new ImsHttpMessage<ImsPagination<IEnumerable<PostCommentReactionDto>>>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = response,
        });
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ImsHttpMessage<PostCommentReactionDto>>> CreatePostCommentReaction(
        [FromBody] CreatePostCommentReactionDto createPostCommentReactionDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var response =
            await _postCommentReactionService.AddPostCommentReaction(userIdString, createPostCommentReactionDto);

        return Created("", new ImsHttpMessage<PostCommentReactionDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = response,
        });
    }

    [Authorize]
    [HttpPut("{postCommentReactionId}")]
    public async Task<ActionResult<ImsHttpMessage<PostCommentReactionDto>>> EditReaction(
        [FromRoute(Name = "postCommentReactionId")] string postCommentReactionId,
        [FromBody] EditPostCommentReactionDto editPostCommentReactionDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var response =
            await _postCommentReactionService.EditPostCommentReaction(userIdString, postCommentReactionId,
                editPostCommentReactionDto);

        return Ok(new ImsHttpMessage<PostCommentReactionDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = response,
        });
    }

    [Authorize]
    [HttpDelete("{postCommentReactionId}")]
    public async Task<ActionResult<ImsHttpMessage<bool>>> DeleteReaction(
        [FromRoute(Name = "postCommentReactionId")] string postCommentReactionId)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        await _postCommentReactionService.RemovePostCommentReaction(userIdString, postCommentReactionId);

        return Ok(new ImsHttpMessage<bool>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = true
        });
    }
}