using AutoMapper;
using IMS.Email.IBLL.Services;
using IMS.Email.Models.Models;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.Email;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMS.Email.BLL.Consumers;

public class SendUserLoggedInEmailConsumer : IConsumer<ImsBaseMessage<UserLoggedInEmailMessage>>
{
    private readonly IMapper _mapper;
    private readonly ILogger<SendUserLoggedInEmailConsumer> _logger;
    private readonly IEmailSenderService _emailSenderService;

    public SendUserLoggedInEmailConsumer(
        IMapper mapper,
        ILogger<SendUserLoggedInEmailConsumer> logger, 
        IEmailSenderService emailSenderService)
    {
        _mapper = mapper;
        _logger = logger;
        _emailSenderService = emailSenderService;
    }

    public async Task Consume(ConsumeContext<ImsBaseMessage<UserLoggedInEmailMessage>> context)
    {
        var message = context.Message;
        var sendUserLoggedInEmail = _mapper.Map<SendUserLoggedInEmail>(message.Data);
        
        await _emailSenderService.SendUserLoggedInWithEmail(sendUserLoggedInEmail);
    }
}