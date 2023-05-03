using IMS.Shared.Models.Dto.Post;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebAPIMockup.Controllers;

// TODO: Finish documentation
[ApiController]
[Route("api/posts")]
[Produces("application/json")]
public class PostController: Controller
{
    [HttpGet]
    public ActionResult<IEnumerable<PostDetailsDto>> GetPosts()
    {
        return Ok(new List<PostDetailsDto>());
    }

    [HttpGet("{postId}")]
    public ActionResult<PostDetailsDto> GetSpecificPost([FromRoute] string postId)
    {
        return Ok(new PostDetailsDto());
    }

    /// <summary>
    /// Creates post
    /// </summary>
    /// <param name="videos">Gets array of video files, where they have names "frontvideo" and "rearvideo"</param>
    /// <param name="data">Gets json string of representation of CreatePostDto</param>
    /// <returns>PostDetailsDto when new post created</returns>
    /// <response code="201"></response>
    /// <response code="403"></response>
    /// <response code="400"></response>
    [ProducesResponseType(typeof(PostDetailsDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public ActionResult<PostDetailsDto> CreatePost(
        [FromForm(Name = "videos")] IFormFile[] videos,
        [FromForm(Name = "data")] string data)
    {
        //var jsonData = JsonSerializer.Deserialize<CreatePostDto>(data); // can throw JsonException
        // foreach (var video in videos)
        // {
        //     Console.WriteLine(video.FileName);
        // }

        return Created("", new PostDetailsDto());
    }

    [HttpPut("{postId}")]
    public ActionResult<PostDetailsDto> UpdatePost([FromBody] CreatePostDto createPostDto)
    {
        return Ok(new PostDetailsDto());
    }
}