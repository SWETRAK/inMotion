using AutoMapper;
using IMS.Email.IBLL.Services;
using IMS.Email.Models.Models;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.Email.Auth;
using MassTransit;

namespace IMS.Email.BLL.Consumers;

public class SendFailureLoginAttemptEmailConsumer : IConsumer<ImsBaseMessage<FailureLoginAttemptEmailMessage>>
{
    private readonly IMapper _mapper;
    private readonly IEmailSenderService _emailSenderService;

    public SendFailureLoginAttemptEmailConsumer(
        IMapper mapper,
        IEmailSenderService emailSenderService
    )
    {
        _mapper = mapper;
        _emailSenderService = emailSenderService;
    }

    public async Task Consume(ConsumeContext<ImsBaseMessage<FailureLoginAttemptEmailMessage>> context)
    {
        var message = context.Message;
        var sendFailedLoginAttempt = _mapper.Map<SendFailedLoginAttempt>(message.Data);

        await _emailSenderService.SendFailedLoginAttempt(sendFailedLoginAttempt);
    }
}