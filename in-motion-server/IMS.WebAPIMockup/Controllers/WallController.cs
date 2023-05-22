using System.Net.Mime;
using IMS.Shared.Models.Dto.Post;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebAPIMockup.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/wall")]
[Produces(MediaTypeNames.Application.Json)]
public class WallController: ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<PostDetailsDto>> GetWallPosts()
    {
        return Ok(new List<PostDetailsDto>() { new PostDetailsDto(), new PostDetailsDto() });
    }
}