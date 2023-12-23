using System.Net;
using System.Net.Mail;
using IMS.Email.BLL.Configurations;
using IMS.Email.BLL.Utils;
using IMS.Email.IBLL.Services;
using IMS.Email.Models.Models;
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

        _smtpClient = new SmtpClient
        {
            Host = _emailConfiguration.Host,
            Port = _emailConfiguration.Port,
            Credentials = new NetworkCredential(_emailConfiguration.UserName, _emailConfiguration.Password),
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false
        };
    }

    public async Task SendUserLoggedInWithEmail(SendUserLoggedInEmail sendUserLoggedInEmail)
    {
        var emailBody = EmailBodyUtil.GetSuccessLoginBody(_logger);
        emailBody = emailBody
            .Replace("{loginDate}", sendUserLoggedInEmail.DateTime.ToString("MM/dd/yyyy"))
            .Replace("{loginTime}", sendUserLoggedInEmail.DateTime.ToString("HH:mm:ss zz"));
        var emailSubject = $"You just logged in {sendUserLoggedInEmail.DateTime:MM/dd/yyyy}";
        
        await SendEmail(sendUserLoggedInEmail.Email, emailSubject, emailBody);   
    }

    public async Task SendFailedLoginAttempt(SendFailedLoginAttempt sendFailedLoginAttempt)
    {
        var emailBody = EmailBodyUtil.GetFailedLogInAttemptsBody(_logger);
        emailBody = emailBody
            .Replace("{loginDate}", sendFailedLoginAttempt.AttemptDateTime.ToString("MM/dd/yyyy"))
            .Replace("{loginTime}", sendFailedLoginAttempt.AttemptDateTime.ToString("HH:mm:ss zz"));

        var emailSubject = $"Failed login attempt";

        await SendEmail(sendFailedLoginAttempt.Email, emailSubject, emailBody);
    }
    
    public async Task SendAccountActivation(SendAccountActivation sendAccountActivation)
    {
        var emailBody = EmailBodyUtil.GetAccountActivationBody(_logger);
        emailBody = emailBody
            .Replace("{registerDate}", sendAccountActivation.RegisterTime.ToString("MM/dd/yyyy"))
            .Replace("{registerTime}", sendAccountActivation.RegisterTime.ToString("HH:mm:ss zz"))
            .Replace("{activationCode}", sendAccountActivation.ActivationCode);

        var emailSubject = $"Welcome to InMotion, activate your new account";
        
        await SendEmail(sendAccountActivation.Email, emailSubject, emailBody);
    }

    private async Task SendEmail(string email, string subject, string body)
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
        try
        {
            await _smtpClient.SendMailAsync(mailMessage);
            _logger.LogInformation("Email to user {Email} with topic \"{Subject}\"", email, subject);
        }
        catch (Exception exception)
        {
            _logger.LogWarning(
                exception,
                "Email to user {Email} not send, Exception thrown with message {Message} ",
                email,                 
                exception.Message);
        }
    }
}