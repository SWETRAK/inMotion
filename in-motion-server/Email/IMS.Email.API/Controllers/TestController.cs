using IMS.Email.IBLL.Services;
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
}