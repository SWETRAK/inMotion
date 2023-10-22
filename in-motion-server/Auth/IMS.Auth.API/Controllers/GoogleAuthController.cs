using IMS.Auth.IBLL.Services;
using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;
using IMS.Shared.Utils.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Auth.App.Controllers;

[ApiController]
[Route("api/google")]
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
        var serverRequestTime = DateTime.UtcNow;
        var result = await _googleAuthService.SignIn(authenticateWithGoogleProviderDto);
        _logger.LogInformation("User logged in successfully with Google provider");
        return Ok(new ImsHttpMessage<UserInfoDto>
        {
            Data = result,
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK
        });
    }
    
    [Authorize]
    [HttpPost("add")]
    public async Task<ActionResult<ImsHttpMessage<bool>>> AddGoogleProvider(
        [FromBody] AuthenticateWithGoogleProviderDto authenticateWithGoogleProviderDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var result = await _googleAuthService.AddGoogleProvider(authenticateWithGoogleProviderDto, userIdString);
        return Ok(new ImsHttpMessage<bool>
        {
            Data = result,
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status204NoContent
        });
    }
}