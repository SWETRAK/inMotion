using System.Net;
using System.Runtime.InteropServices;
using IMS.Auth.IBLL.Services;
using IMS.Auth.Models.Dto;
using IMS.Shared.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

    public async Task<ActionResult> RegisterWithEmailAndPassword()
    {
        // var result = await _emailAuthService.RegisterWithEmail();
        return Ok();
    }

    
    public async Task<ActionResult> LoginWithGoogle()
    {
        return Ok();
    }
    
    public async Task<ActionResult> LoginWithFacebook()
    {
        return Ok();
    }
    
    [Authorize]
    public async Task<ActionResult> Logout()
    {
        return Ok();
    }
}