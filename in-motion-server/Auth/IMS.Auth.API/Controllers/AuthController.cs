using System.Net;
using IMS.Auth.IBLL.Services;
using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Auth.App.Controllers;

[ApiController]
[Route("")]
public class AuthController: ControllerBase
{
    private readonly IEmailAuthService _emailAuthService;

    public AuthController(IEmailAuthService emailAuthService)
    {
        _emailAuthService = emailAuthService;
    }

    [HttpGet("confirm/{email}/{activationCode}")]
    public async Task<ActionResult> ConfirmAccount(
        [FromRoute(Name = "email")] string email, 
        [FromRoute(Name="activationCode")]string activationCode
    )
    {
        await _emailAuthService.ConfirmRegisterWithEmail(email, activationCode);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<ImsHttpMessage<UserInfoDto>>> LoginWithEmailAndPassword(LoginUserWithEmailAndPasswordDto requestDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var result = await _emailAuthService.LoginWithEmail(requestDto);
        return Ok(new ImsHttpMessage<UserInfoDto>
        {
            Data = result,
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = (int)HttpStatusCode.OK
        });
    }

    [HttpPost("register")]
    public async Task<ActionResult<ImsHttpMessage<RegisterSuccessDto>>> RegisterWithEmailAndPassword(RegisterUserWithEmailAndPasswordDto requestDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var result = await _emailAuthService.RegisterWithEmail(requestDto);
        return Created("",new ImsHttpMessage<RegisterSuccessDto>
        {
            Data = result,
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = (int)HttpStatusCode.Created
        });
    }
    
    // public async Task<ActionResult> LoginWithGoogle()
    // {
    //     return Ok();
    // }
    //
    // public async Task<ActionResult> LoginWithFacebook()
    // {
    //     return Ok();
    // }
    
    // [Authorize]
    // [HttpDelete("logout")]
    // public async Task<ActionResult> Logout()
    // {
    //     return Ok();
    // }
}