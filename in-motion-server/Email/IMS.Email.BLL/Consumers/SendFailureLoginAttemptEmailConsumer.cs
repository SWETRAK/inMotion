using AutoMapper;
using IMS.Email.IBLL.Services;
using IMS.Email.Models.Models;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.Email;
using IMS.Shared.Messaging.Messages.Email.Auth;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMS.Email.BLL.Consumers;

public class SendFailureLoginAttemptEmailConsumer: IConsumer<ImsBaseMessage<FailureLoginAttemptEmailMessage>>
{
    private readonly IMapper _mapper;
    private readonly ILogger<SendFailureLoginAttemptEmailConsumer> _logger;
    private readonly IEmailSenderService _emailSenderService;

    public SendFailureLoginAttemptEmailConsumer(
        IMapper mapper, 
        ILogger<SendFailureLoginAttemptEmailConsumer> logger, 
        IEmailSenderService emailSenderService
        )
    {
        _mapper = mapper;
        _logger = logger;
        _emailSenderService = emailSenderService;
    }

    public async Task Consume(ConsumeContext<ImsBaseMessage<FailureLoginAttemptEmailMessage>> context)
    {
        var message = context.Message;
        var sendFailedLoginAttempt = _mapper.Map<SendFailedLoginAttempt>(message.Data);
        await _emailSenderService.SendFailedLoginAttempt(sendFailedLoginAttempt);
    }
}