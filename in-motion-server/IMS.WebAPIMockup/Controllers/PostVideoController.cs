using Microsoft.AspNetCore.Mvc;

namespace IMS.WebAPIMockup.Controllers;

// TODO: Finish documentation
[ApiController]
[Route("api/v{version:apiVersion}/posts/videos")]
public class PostVideoController: ControllerBase
{
    [Produces("video/mp4")]
    [HttpGet("{videoId}")]
    public IActionResult GetVideo([FromRoute] string videoId)
    {
        var stream = new MemoryStream();
        return File(stream, "video/mp4", "filename");
    }
}