using IMS.Auth.BLL.Utils;
using IMS.Auth.IBLL.Services;
using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Auth.App.Controllers;

[ApiController]
[Route("google")]
public class GoogleAuthController: ControllerBase
{
    private readonly IGoogleAuthService _googleAuthService;
    private readonly ILogger<GoogleAuthController> _logger;
    
    public GoogleAuthController(IGoogleAuthService googleAuthService, ILogger<GoogleAuthController> logger)
    {
        _googleAuthService = googleAuthService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<ActionResult<ImsHttpMessage<UserInfoDto>>> SignIn(
        [FromBody] AuthenticateWithGoogleProviderDto authenticateWithGoogleProviderDto)
    {
        var result = await _googleAuthService.SignIn(authenticateWithGoogleProviderDto);
        _logger.LogInformation("User logged in successfully with Google provider");
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost("add")]
    public async Task<ActionResult<ImsHttpMessage<bool>>> AddGoogleProvider(
        [FromBody] AuthenticateWithGoogleProviderDto authenticateWithGoogleProviderDto)
    {
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var result = await _googleAuthService.AddGoogleProvider(authenticateWithGoogleProviderDto, userIdString);
        return Ok(result);
    }
}