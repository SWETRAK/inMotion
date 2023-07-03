using System.Net.Mime;
using IMS.Shared.Models.Dto.Post;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebAPIMockup.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/wall")]
[Produces(MediaTypeNames.Application.Json)]
public class WallController: ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Returns founded item</response>
    /// <response code="403">If user is unauthorized</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(IEnumerable<PostDetailsDto>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<PostDetailsDto>> GetWallPosts()
    {
        return Ok(new List<PostDetailsDto>() { new PostDetailsDto(), new PostDetailsDto() });
    }
}