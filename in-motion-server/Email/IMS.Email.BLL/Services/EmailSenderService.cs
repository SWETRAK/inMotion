using System.Net;
using System.Net.Mail;
using IMS.Email.BLL.Configurations;
using IMS.Email.BLL.Utils;
using IMS.Email.IBLL.Services;
using IMS.Email.Models.Models;
using IMS.Shared.Messaging.Messages.Email;
using Microsoft.Extensions.Logging;

namespace IMS.Email.BLL.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly ILogger<EmailSenderService> _logger;
    private readonly SmtpClient _smtpClient;
    private readonly EmailConfiguration _emailConfiguration;

    public EmailSenderService(
        ILogger<EmailSenderService> logger, 
        EmailConfiguration emailConfiguration
    )
    {
        _logger = logger;
        _emailConfiguration = emailConfiguration;

        _smtpClient = new SmtpClient(_emailConfiguration.Host)
        {
            Port = _emailConfiguration.Port,
            Credentials = new NetworkCredential(_emailConfiguration.UserName, _emailConfiguration.Password),
            EnableSsl = true,
        };
    }

    public void SendUserLoggedInWithEmail(UserLoggedInEmailMessage userLoggedInEmailMessage)
    {
        var emailBody = EmailBodyUtil.GetSuccessLoginBody();
        emailBody = emailBody
            .Replace("{loginDate}", userLoggedInEmailMessage.LoggedDate.ToString("MM/dd/yyyy"))
            .Replace("{loginTime}", userLoggedInEmailMessage.LoggedDate.ToString("HH:mm:ss zz"));
        var emailSubject = $"You just logged in {userLoggedInEmailMessage.LoggedDate:MM/dd/yyyy}";
        
        SendEmail(userLoggedInEmailMessage.Email, emailSubject, emailBody);   
    }

    public void SendFailedLoginAttempt(SendFailedLoginAttempt sendFailedLoginAttempt)
    {
        var emailBody = EmailBodyUtil.GetFailedLogginAttemptsBody();
        emailBody = emailBody
            .Replace("{loginDate}", sendFailedLoginAttempt.AttemptDateTime.ToString("MM/dd/yyyy"))
            .Replace("{loginTime}", sendFailedLoginAttempt.AttemptDateTime.ToString("HH:mm:ss zz"));

        var emailSubject = $"Failed login attempt";

        SendEmail(sendFailedLoginAttempt.Email, emailSubject, emailBody);
    }

    public void SendAccountActivation(SendAccountActivation sendAccountActivation)
    {
        // var emailBody = EmailBodyUtil.GetFailedLogginAttemptsBody();
        // emailBody = emailBody
        //     .Replace("{loginDate}", sendFailedLoginAttempt.AttemptDateTime.ToString("MM/dd/yyyy"))
        //     .Replace("{loginTime}", sendFailedLoginAttempt.AttemptDateTime.ToString("HH:mm:ss zz"));
        //
        // var emailSubject = $"Failed login attempt";
        //
        // SendEmail(sendFailedLoginAttempt.Email, emailSubject, emailBody);
    }

    private void SendEmail(string email, string subject, string body)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailConfiguration.Email),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
            To = { email }
        };
        
        mailMessage.To.Add(email);
        _smtpClient.Send(mailMessage);
        _logger.LogInformation("Email to user {Email} with topic \"{Subject}\"", email, subject);
    }
}