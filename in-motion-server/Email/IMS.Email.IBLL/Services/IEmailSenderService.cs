using IMS.Email.Models.Models;
using Microsoft.Extensions.Logging;

namespace IMS.Email.IBLL.Services;

public interface IEmailSenderService
{
    public Task SendUserLoggedInWithEmail(SendUserLoggedInEmail sendUserLoggedInEmail);

    public Task SendFailedLoginAttempt(SendFailedLoginAttempt sendFailedLoginAttempt);

    public Task SendAccountActivation(SendAccountActivation sendAccountActivation);
}