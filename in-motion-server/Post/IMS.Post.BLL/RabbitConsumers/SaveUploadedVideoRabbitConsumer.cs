using AutoMapper;
using FluentValidation;
using IMS.Post.BLL.Services;
using IMS.Post.DAL.Repositories.Post;
using IMS.Post.Domain;
using IMS.Post.Models.Dto.Incoming;
using IMS.Shared.Messaging;
using IMS.Shared.Messaging.Consumers;
using IMS.Shared.Messaging.Messages.PostVideos;
using IMS.Shared.Messaging.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IMS.Post.BLL.RabbitConsumers;

public class SaveUploadedVideoRabbitConsumer: SimpleConsumer<UpdatePostVideoMetadataMessage>
{
    private readonly ILogger<SaveUploadedVideoRabbitConsumer> _logger;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IDbContextFactory<ImsPostDbContext> _dbContextFactory;
    private readonly IValidator<UploadVideosMetaDataDto> _uploadVideosMetaDataValidator;
    
    public SaveUploadedVideoRabbitConsumer(ILogger<SaveUploadedVideoRabbitConsumer> logger,
        IMapper mapper,
        
        IValidator<UploadVideosMetaDataDto> uploadVideosMetaDataValidator, IServiceScopeFactory serviceScopeFactory, IDbContextFactory<ImsPostDbContext> dbContextFactory) : base(logger)
    {
        _logger = logger;
        _mapper = mapper;
        _uploadVideosMetaDataValidator = uploadVideosMetaDataValidator;
        _serviceScopeFactory = serviceScopeFactory;
        _dbContextFactory = dbContextFactory;
    }

    protected override QueueConfiguration ConfigureQueue()
    {
        return new QueueConfiguration(
            EventsBusNames.CustomRabbitConfigurationNames.UpdatePostVideoQueueName,
            EventsBusNames.CustomRabbitConfigurationNames.UpdatePostVideoExchangeName,
            EventsBusNames.CustomRabbitConfigurationNames.UpdatePostVideoRoutingKeyName);
    }

    protected override async Task ExecuteTask(UpdatePostVideoMetadataMessage message)
    {
        var requestData = _mapper.Map<UploadVideosMetaDataDto>(message);
        
        var validationResult = await _uploadVideosMetaDataValidator.ValidateAsync(requestData);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning(JsonConvert.SerializeObject(validationResult.Errors));
            return;
        }

        try
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            var postVideoRepository = new PostVideoRepository(dbContext);
            var postRepository = new PostRepository(dbContext);
            var postVideoService = new PostVideoService(postVideoRepository, postRepository);
            await postVideoService.SaveUploadedVideos(requestData);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error while uploading post video");
        }
    }
}