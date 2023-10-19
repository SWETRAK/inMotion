using AutoMapper;
using IMS.Shared.Messaging;
using IMS.Shared.Messaging.Consumers;
using IMS.Shared.Messaging.Messages.Users;
using IMS.Shared.Messaging.Models;
using IMS.User.IBLL.Services;
using IMS.User.Models.Dto.Incoming;
using Microsoft.Extensions.Logging;

namespace IMS.User.BLL.RabbitConsumers;

public class UpdateProfileVideoRabbitConsumer: SimpleConsumer<UpdateUserProfileVideoMessage>
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly ILogger<UpdateProfileVideoRabbitConsumer> _logger;
    
    public UpdateProfileVideoRabbitConsumer(IMapper mapper, IUserService userService, ILogger<UpdateProfileVideoRabbitConsumer> logger) : base(logger)
    {
        _mapper = mapper;
        _userService = userService;
        _logger = logger;
    }

    protected override QueueConfiguration ConfigureQueue()
    {
        return new QueueConfiguration(
            EventsBusNames.CustomRabbitConfigurationNames.UpdateProfileVideoQueueName,
            EventsBusNames.CustomRabbitConfigurationNames.UpdateProfileVideoExchangeName,
            EventsBusNames.CustomRabbitConfigurationNames.UpdateProfileVideoRoutingKeyName);
    }
    
    protected override async Task ExecuteTask(UpdateUserProfileVideoMessage message)
    {
        var requestData = _mapper.Map<UpdateUserProfileVideoDto>(message);
        try
        {
            await _userService.UpdateUserProfileVideo(message.UserId, requestData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user profile video");
        }
    }
}