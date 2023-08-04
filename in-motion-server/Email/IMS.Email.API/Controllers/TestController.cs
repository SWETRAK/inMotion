using IMS.Email.IBLL.Services;
using IMS.Shared.Messaging.Messages.Email;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Email.API.Controllers;


[ApiController]
[Route("test")]
public class TestController: ControllerBase
{
    private readonly IEmailSenderService _emailSenderService;

    public TestController(IEmailSenderService emailSenderService)
    {
        _emailSenderService = emailSenderService;
    }

    [HttpGet("send")]
    public ActionResult SendEmail()
    {
        _emailSenderService.SendUserLoggedInWithEmail(new UserLoggedInEmailMessage
        {
            Email = "kamilpietrak123@gmail.com",
            LoggedDate = DateTime.UtcNow
        });

        return Ok();
    }
}