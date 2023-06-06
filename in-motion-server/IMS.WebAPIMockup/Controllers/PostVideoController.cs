using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebAPIMockup.Controllers;

// TODO: Finish documentation
[ApiController]
[Route("api/v{version:apiVersion}/posts/videos")]
public class PostVideoController: ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="videoId"></param>
    /// <returns></returns>
    /// <response code="200">Returns founded item</response>
    /// <response code="404">If the item is not existing</response>
    /// <response code="403">If user is unauthorized</response>
    [Produces("video/mp4")]
    [HttpGet("{videoId}")]
    public IActionResult GetVideo([FromRoute] string videoId)
    {
        var stream = new MemoryStream();
        return File(stream, "video/mp4", "filename");
    }
}