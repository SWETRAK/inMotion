using IMS.Shared.Models.Dto.User;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebAPIMockup.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController: Controller
{
    [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("login")]
    public ActionResult<UserInfoDto> LoginUserWithEmailAndPassword()
    {
        return Ok(new UserInfoDto());
    }

    [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("register")]
    public ActionResult<UserInfoDto> RegisterUserWithEmailAndPassword()
    {
        return Ok(new UserInfoDto());
    }
    
    [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("provider")]
    public ActionResult<UserInfoDto> AccessWithProvider()
    {
        return Ok(new UserInfoDto());
    }
}