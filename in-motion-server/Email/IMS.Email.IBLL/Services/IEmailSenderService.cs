using IMS.Shared.Messaging.Messages.Email;

namespace IMS.Email.IBLL.Services;

public interface IEmailSenderService
{
    public void SendUserLoggedInWithEmail(UserLoggedInEmailMessage userLoggedInEmailMessage);
}