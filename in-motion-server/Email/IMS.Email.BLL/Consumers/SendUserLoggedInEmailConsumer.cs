using AutoMapper;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.Email;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMS.Email.BLL.Consumers;

public class SendUserLoggedInEmailConsumer : IConsumer<ImsBaseMessage<UserLoggedInEmailMessage>>
{
    private readonly IMapper _mapper;
    private readonly ILogger<SendUserLoggedInEmailConsumer> _logger;

    public SendUserLoggedInEmailConsumer(
        IMapper mapper,
        ILogger<SendUserLoggedInEmailConsumer> logger
    )
    {
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ImsBaseMessage<UserLoggedInEmailMessage>> context)
    {
        var message = context.Message;
        
        
    }
}