using System.Net;
using IMS.Auth.IBLL.Services;
using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Auth.App.Controllers;

[ApiController]
[Route("email")]
public class EmailAuthController: ControllerBase
{
    private readonly IEmailAuthService _emailAuthService;

    public EmailAuthController(IEmailAuthService emailAuthService)
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
    public async Task<ActionResult<ImsHttpMessage<SuccessfulRegistrationResponseDto>>> RegisterWithEmailAndPassword(RegisterUserWithEmailAndPasswordDto requestDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var result = await _emailAuthService.RegisterWithEmail(requestDto);
        return Created("",new ImsHttpMessage<SuccessfulRegistrationResponseDto>
        {
            Data = result,
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = (int)HttpStatusCode.Created
        });
    }
}