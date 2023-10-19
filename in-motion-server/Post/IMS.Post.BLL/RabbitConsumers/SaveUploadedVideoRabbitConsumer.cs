using AutoMapper;
using FluentValidation;
using IMS.Post.IBLL.Services;
using IMS.Post.Models.Dto.Incoming;
using IMS.Shared.Messaging;
using IMS.Shared.Messaging.Consumers;
using IMS.Shared.Messaging.Messages.PostVideos;
using IMS.Shared.Messaging.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IMS.Post.BLL.RabbitConsumers;

public class SaveUploadedVideoRabbitConsumer: SimpleConsumer<UpdatePostVideoMetadataMessage>
{
    private readonly ILogger<SaveUploadedVideoRabbitConsumer> _logger;
    private readonly IMapper _mapper;
    private readonly IPostVideoService _postVideoService;
    private readonly IValidator<UploadVideosMetaDataDto> _uploadVideosMetaDataValidator;
    
    public SaveUploadedVideoRabbitConsumer(ILogger<SaveUploadedVideoRabbitConsumer> logger,
        IMapper mapper,
        IPostVideoService postVideoService,
        IValidator<UploadVideosMetaDataDto> uploadVideosMetaDataValidator) : base(logger)
    {
        _logger = logger;
        _mapper = mapper;
        _postVideoService = postVideoService;
        _uploadVideosMetaDataValidator = uploadVideosMetaDataValidator;
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

        await _postVideoService.SaveUploadedVideos(requestData);
    }
}