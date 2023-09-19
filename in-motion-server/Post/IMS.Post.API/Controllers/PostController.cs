using IMS.Post.IBLL.Services;
using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;
using IMS.Shared.Utils.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Post.API.Controllers;

[Route("api/posts")]
[ApiController]
public class PostController: ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ImsHttpMessage<GetPostResponseDto>>> GetCurrentPost()
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var response = await _postService.GetCurrentUserPost(userIdString);

        return Ok(new ImsHttpMessage<GetPostResponseDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = response,
        });
    }
    
    [Authorize]
    [HttpGet("{authorId}")]
    public async Task<ActionResult<ImsHttpMessage<GetPostResponseDto>>> GetPostByAuthorId(
        [FromRoute(Name = "authorId")] string authorId)
    {
        var serverRequestTime = DateTime.UtcNow;
        var response = await _postService.GetCurrentUserPost(authorId);

        return Ok(new ImsHttpMessage<GetPostResponseDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = response,
        });
    }

    [Authorize]
    [HttpGet("public")]
    public async Task<ActionResult<ImsHttpMessage<ImsPagination<IList<GetPostResponseDto>>>>> GetPublicPostsAsync(
        [FromBody] ImsPaginationRequestDto imsPaginationRequestDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var response = await _postService.GetPublicPostsFromCurrentIteration(imsPaginationRequestDto);

        return Ok(new ImsHttpMessage<ImsPagination<IList<GetPostResponseDto>>>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = response,
        });
    }

    [Authorize]
    [HttpGet("friends")]
    public async Task<ActionResult<ImsHttpMessage<ImsPagination<IList<GetPostResponseDto>>>>> GetPublicFriendsPostsAsync(
        [FromBody] ImsPaginationRequestDto imsPaginationRequestDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var response =
            await _postService.GetFriendsPublicPostsFromCurrentIteration(userIdString, imsPaginationRequestDto);

        return Ok(new ImsHttpMessage<ImsPagination<IList<GetPostResponseDto>>>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = response,
        });
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ImsHttpMessage<CreatePostResponseDto>>> CreatePost(CreatePostRequestDto createPostRequestDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var response = await _postService.CreatePost(userIdString, createPostRequestDto);

        return Created("", new ImsHttpMessage<CreatePostResponseDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status201Created,
            Data = response
        });
    }

    [Authorize]
    [HttpPut("{postId}")]
    public async Task<ActionResult<ImsHttpMessage<GetPostResponseDto>>> EditPostMetaData([FromRoute(Name = "postId")] string postId,
        [FromBody] EditPostRequestDto editPostRequestDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var response = await _postService.EditPostsMetas(userIdString, postId, editPostRequestDto);

        return Ok(new ImsHttpMessage<GetPostResponseDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = response,
        });
    }

}