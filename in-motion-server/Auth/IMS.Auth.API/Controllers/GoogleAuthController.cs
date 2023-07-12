using IMS.Auth.IBLL.Services;
using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;
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

    [HttpPost]
    public async Task<ActionResult<ImsHttpMessage<UserInfoDto>>> SignIn(AuthenticateWithGoogleProviderDto authenticateWithGoogleProviderDto)
    {
        var token = await _googleAuthService.SignIn(authenticateWithGoogleProviderDto);
        return Ok(token);
    }
}