using AutoMapper;
using FluentValidation;
using IMS.Post.IBLL.Services;
using IMS.Post.IBLL.SoapServices;
using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Models.Soap;
using Microsoft.Extensions.Logging;
using static Newtonsoft.Json.JsonConvert;

namespace IMS.Post.BLL.SoapServices;

public class PostVideoSoapService: IPostVideoSoapService
{
    private readonly IPostVideoService _postVideoService;
    private readonly IMapper _mapper;
    private readonly IValidator<UploadVideosMetaDataDto> _uploadVideosMetaDataValidator;
    private readonly ILogger<PostVideoSoapService> _logger;
    
    public PostVideoSoapService(IPostVideoService postVideoService,
        IMapper mapper,
        IValidator<UploadVideosMetaDataDto> uploadVideosMetaDataValidator,
        ILogger<PostVideoSoapService> logger)
    {
        _postVideoService = postVideoService;
        _mapper = mapper;
        _uploadVideosMetaDataValidator = uploadVideosMetaDataValidator;
        _logger = logger;
    }
    
    public async Task SaveUploadedVideo(UploadVideosMetaData uploadVideosMetaData)
    {
        try
        {
            var requestData = _mapper.Map<UploadVideosMetaDataDto>(uploadVideosMetaData);

            var validationResult = await _uploadVideosMetaDataValidator.ValidateAsync(requestData);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning(SerializeObject(validationResult.Errors));
                return;
            }

            await _postVideoService.SaveUploadedVideos(requestData);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while executing PostVideoSoapService");
        }
    }
}