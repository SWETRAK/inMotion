using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebAPIMockup.Controllers;

// TODO: Finish documentation
[ApiController]
[Route("api/v{version:apiVersion}/posts/videos")]
[Produces(MediaTypeNames.Application.Json)]
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