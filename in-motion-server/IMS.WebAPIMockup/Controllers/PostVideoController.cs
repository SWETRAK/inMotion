using Microsoft.AspNetCore.Mvc;

namespace IMS.WebAPIMockup.Controllers;

[ApiController]
[Route("api/posts/videos")]
public class PostVideoController: Controller
{
    [Produces("video/mp4")]
    [HttpGet("{videoId}")]
    public IActionResult GetVideo([FromRoute] string videoId)
    {
        var stream = new MemoryStream();
        return File(stream, "video/mp4", "filename");
    }
}