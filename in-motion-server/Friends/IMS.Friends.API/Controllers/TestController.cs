using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Friends.API.Controllers;

[Route("friends/test")]
[ApiController]
public class TestController: ControllerBase
{

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult Test()
    {
        return Ok("Test");
    }
}