using IMS.Auth.IBLL.Services;
using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;
using IMS.Shared.Utils.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Auth.App.Controllers;

[ApiController]
[Route("api/facebook")]
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
        var serverRequestTime = DateTime.UtcNow;
        var result = await _facebookAuthService.SignIn(authenticateWithFacebookProviderDto);

        return Ok(new ImsHttpMessage<UserInfoDto>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Data = result,
            Status = StatusCodes.Status204NoContent
        });
    }
    
    [Authorize]
    [HttpPost("add")]
    public async Task<ActionResult<ImsHttpMessage<bool>>> AddGoogleProvider(
        [FromBody] AuthenticateWithFacebookProviderDto authenticateWithFacebookProviderDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var result = await _facebookAuthService.AddFacebookProvider(authenticateWithFacebookProviderDto, userIdString);
        return Ok(new ImsHttpMessage<bool>
        {
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Data = result,
            Status = StatusCodes.Status204NoContent
        });
    }
}