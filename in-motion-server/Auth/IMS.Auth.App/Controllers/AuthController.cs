using System.Runtime.InteropServices;
using IMS.Auth.IBLL.Services;
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

    
    public async Task<ActionResult> LoginWithEmailAndPassword()
    {
        // var result = await _emailAuthService.LoginWithEmail();
        return Ok();
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