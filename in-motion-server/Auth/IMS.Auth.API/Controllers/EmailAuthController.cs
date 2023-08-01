using IMS.Auth.BLL.Utils;
using IMS.Auth.IBLL.Services;
using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;
using Microsoft.AspNetCore.Authorization;
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
            Status = StatusCodes.Status200OK
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
            Status = StatusCodes.Status201Created
        });
    }

    [Authorize]
    [HttpPut("password/update")]
    public async Task<ActionResult<ImsHttpMessage<bool>>> UpdatePassword(UpdatePasswordDto updatePasswordDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var result = await _emailAuthService.UpdatePassword(updatePasswordDto, userIdString);

        return Ok(new ImsHttpMessage<bool>
        {
            Data = result,
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status204NoContent
        });
    }

    [HttpPut("password/add")]
    public async Task<ActionResult<ImsHttpMessage<bool>>> SetPasswordForExistingAccount(AddPasswordDto addPasswordDto)
    {
        var serverRequestTime = DateTime.UtcNow;
        var userIdString = AuthenticationUtil.GetUserId(HttpContext.User);
        var result = await _emailAuthService.AddPasswordToExistingAccount(addPasswordDto, userIdString);

        return Ok(new ImsHttpMessage<bool>
        {
            Data = result,
            ServerRequestTime = serverRequestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status204NoContent
        });
    }
}