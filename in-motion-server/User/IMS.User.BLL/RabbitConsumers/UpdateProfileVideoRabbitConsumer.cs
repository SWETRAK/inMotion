using AutoMapper;
using IMS.Shared.Messaging;
using IMS.Shared.Messaging.Consumers;
using IMS.Shared.Messaging.Messages.Users;
using IMS.Shared.Messaging.Models;
using IMS.User.BLL.Services;
using IMS.User.DAL.Repositories;
using IMS.User.Domain;
using IMS.User.Models.Dto.Incoming;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IMS.User.BLL.RabbitConsumers;

public class UpdateProfileVideoRabbitConsumer: SimpleConsumer<UpdateUserProfileVideoMessage>
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<ImsUserDbContext> _dbContextFactory;
    private readonly ILogger<UpdateProfileVideoRabbitConsumer> _logger;
    
    public UpdateProfileVideoRabbitConsumer(
        IMapper mapper,
        ILogger<UpdateProfileVideoRabbitConsumer> logger, 
        IDbContextFactory<ImsUserDbContext> dbContextFactory) : base(logger)
    {
        _mapper = mapper;
        _logger = logger;
        _dbContextFactory = dbContextFactory;
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
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            var userMetasRepository = new UserMetasRepository(dbContext);
            var userVideoPartService = new UserVideoPartService(_mapper, userMetasRepository);
            await userVideoPartService.UpdateUserProfileVideo(message.UserId, requestData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user profile video");
        }
    }
}