using IMS.Post.IBLL.Services;
using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;
using IMS.Shared.Utils.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Post.API.Controllers;

[ApiController]
[Route("api/posts/comments")]
public class PostCommentController: ControllerBase
{
    private readonly IPostCommentService _postCommentService;

    public PostCommentController(IPostCommentService postCommentService)
    {
        _postCommentService = postCommentService;
    }

    [Authorize]
    [HttpGet("{postId}")]
    public async Task<ActionResult<ImsHttpMessage<IEnumerable<PostCommentDto>>>> GetPostCommentsPaginatedAsync(
        [FromRoute(Name = "postId")] string postId)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var response =
            await _postCommentService.GetPostCommentsAsync(postId);

        return Ok(new ImsHttpMessage<IEnumerable<PostCommentDto>>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = response,
        });
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ImsHttpMessage<PostCommentDto>>> AddPostComment(
        [FromBody] CreatePostCommentDto createPostCommentDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var response =
            await _postCommentService.CreatePostCommentDtoAsync(userIdString,
                createPostCommentDto);

        return Ok(new ImsHttpMessage<PostCommentDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = response,
        });
    }

    [Authorize]
    [HttpPut("{commentId}")]
    public async Task<ActionResult<ImsHttpMessage<PostCommentDto>>> EditPostComment(
        [FromRoute(Name = "commentId")] string commentId, 
        [FromBody] UpdatePostCommentDto updatePostCommentDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var response =
            await _postCommentService.EditPostCommentDtoAsync(userIdString, commentId, updatePostCommentDto);

        return Ok(new ImsHttpMessage<PostCommentDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = response,
        });
    }

    [Authorize]
    [HttpDelete("commentId")]
    public async Task<ActionResult<ImsHttpMessage<bool>>> DeleteComment([FromRoute(Name = "commentId")] string commentId)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        await _postCommentService.DeletePostCommentAsync(userIdString, commentId);

        return Ok(new ImsHttpMessage<bool>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = true,
        });
    }
}