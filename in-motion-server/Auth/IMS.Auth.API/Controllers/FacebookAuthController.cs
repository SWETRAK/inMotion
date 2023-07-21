using IMS.Auth.IBLL.Services;
using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Auth.App.Controllers;

[ApiController]
[Route("facebook")]
public class FacebookAuthController: ControllerBase
{
    private readonly ILogger<FacebookAuthController> _logger;
    private readonly IFacebookAuthService _facebookAuthService;

    public FacebookAuthController(
        ILogger<FacebookAuthController> logger, 
        IFacebookAuthService facebookAuthService
    )
    {
        _logger = logger;
        _facebookAuthService = facebookAuthService;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<ImsHttpMessage<UserInfoDto>>> SignIn(
        [FromBody] AuthenticateWithFacebookProviderDto authenticateWithFacebookProviderDto)
    {
        var result = await _facebookAuthService.SignIn(authenticateWithFacebookProviderDto);
        _logger.LogInformation("User logged in successfully with Facebook provider");
        return Ok(result);
    }
}