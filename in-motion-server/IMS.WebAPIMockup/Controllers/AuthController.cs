using IMS.Shared.Models.Dto.Auth;
using IMS.Shared.Models.Dto.User;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebAPIMockup.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController: ControllerBase
{
    /// <summary>
    /// Login user with email and password
    /// </summary>
    /// <returns>UserLoginWithEmailAndPassword</returns>
    /// <response code="200">If login goes successfully and user is authorised </response>
    /// <response code="401">If user is not authorized</response>
    [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("login")]
    public ActionResult<UserSuccessLoginResultDto> LoginUserWithEmailAndPassword()
    {
        return Ok(new UserSuccessLoginResultDto());
    }

    /// <summary>
    /// Register user with email and password 
    /// </summary>
    /// <returns>UserLoginWithEmailAndPassword with jwt and basic user info</returns>
    /// <response code="200">If registration goes successfully and user is authorized</response>
    /// <response code="401">If user is not registered or/and unauthorized</response>
    [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("register")]
    public ActionResult<UserSuccessLoginResultDto> RegisterUserWithEmailAndPassword()
    {
        return Ok(new UserSuccessLoginResultDto());
    }
    /// <summary>
    /// Login and register user with oAuth providers
    /// </summary>
    /// <returns>UserLoginWithEmailAndPassword with jwt and basic user info</returns>
    /// <response code="200">If user is successfully login</response>
    /// <response code="401">If user is unauthorized</response>
    [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("provider")]
    public ActionResult<UserSuccessLoginResultDto> AccessWithProvider()
    {
        return Ok(new UserSuccessLoginResultDto());
    }

    /// <summary>
    /// Logout user removed cookie authorization key from frontend
    /// </summary>
    /// <returns></returns>
    /// <response code="204">If user is successfully logged out</response>
    /// <response code="401">If user haven't got access</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult LogoutUser()
    {
        return NoContent();
    }
}