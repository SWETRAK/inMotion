using System.Net;
using IMS.Auth.IBLL.Services;
using IMS.Auth.Models.Dto;
using IMS.Shared.Models.Dto;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult> RegisterWithEmailAndPassword()
    {
        // var result = await _emailAuthService.RegisterWithEmail();
        return Ok();
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
    
    [Authorize]
    [HttpDelete("logout")]
    public async Task<ActionResult> Logout()
    {
        return Ok();
    }
}