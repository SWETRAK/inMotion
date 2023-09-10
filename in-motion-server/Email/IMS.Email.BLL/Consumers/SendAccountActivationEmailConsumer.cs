using AutoMapper;
using IMS.Email.IBLL.Services;
using IMS.Email.Models.Models;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.Email.Auth;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMS.Email.BLL.Consumers;

public class SendAccountActivationEmailConsumer : IConsumer<ImsBaseMessage<ActivateAccountEmailMessage>>
{
    private readonly IMapper _mapper;
    private readonly IEmailSenderService _emailSenderService;

    public SendAccountActivationEmailConsumer(
        IMapper mapper,
        IEmailSenderService emailSenderService
    )
    {
        _mapper = mapper;
        _emailSenderService = emailSenderService;
    }

    public async Task Consume(ConsumeContext<ImsBaseMessage<ActivateAccountEmailMessage>> context)
    {
        var message = context.Message;
        var sendAccountActivation = _mapper.Map<SendAccountActivation>(message.Data);

        await _emailSenderService.SendAccountActivation(sendAccountActivation);
    }
}