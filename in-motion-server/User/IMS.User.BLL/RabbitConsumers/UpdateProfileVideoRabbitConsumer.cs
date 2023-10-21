using AutoMapper;
using IMS.Shared.Messaging;
using IMS.Shared.Messaging.Consumers;
using IMS.Shared.Messaging.Messages.Users;
using IMS.Shared.Messaging.Models;
using IMS.User.Domain;
using IMS.User.Domain.Entities;
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
            
            var userIdGuid = Guid.Parse(message.UserId);

            var userMetas = await dbContext.UserMetas
                .Include(u => u.ProfileVideo)
                .FirstOrDefaultAsync(x => x.UserExternalId.Equals(message.UserId));
            
            if (userMetas is null)
            {
                userMetas = new UserMetas
                {
                    UserExternalId = userIdGuid,
                    ProfileVideo = new UserProfileVideo
                    {
                        AuthorExternalId = userIdGuid,
                        Filename = requestData.Filename,
                        BucketName = requestData.BucketName,
                        BucketLocation = requestData.BucketLocation,
                        ContentType = requestData.ContentType,
                    },
                };
                await dbContext.AddAsync(userMetas);
            } 
            else if (userMetas.ProfileVideo is null)
            {
                userMetas.ProfileVideo = new UserProfileVideo
                {
                    AuthorExternalId = userIdGuid,
                    Filename = requestData.Filename,
                    BucketName = requestData.BucketName,
                    BucketLocation = requestData.BucketLocation,
                    ContentType = requestData.ContentType,
                };
                dbContext.Update(userMetas);
            }
            else
            {
                userMetas.ProfileVideo.Filename = userMetas.ProfileVideo.Filename;
                userMetas.ProfileVideo.BucketName = userMetas.ProfileVideo.BucketName;
                userMetas.ProfileVideo.BucketLocation = userMetas.ProfileVideo.BucketLocation;
                userMetas.ProfileVideo.ContentType = userMetas.ProfileVideo.ContentType;
                userMetas.ProfileVideo.LastEditionDate = DateTime.UtcNow;
                dbContext.Update(userMetas);
            }
            
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user profile video");
        }
    }
}