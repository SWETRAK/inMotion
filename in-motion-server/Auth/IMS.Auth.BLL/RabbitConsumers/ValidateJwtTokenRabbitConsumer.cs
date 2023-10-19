using AutoMapper;
using IMS.Auth.IBLL.Services;
using IMS.Auth.Models.Exceptions;
using IMS.Shared.Messaging;
using IMS.Shared.Messaging.Consumers;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.JWT;
using IMS.Shared.Messaging.Models;
using Microsoft.Extensions.Logging;

namespace IMS.Auth.BLL.RabbitConsumers;

public class ValidateJwtTokenRabbitConsumer: SimpleConsumerWithResponse<RequestJwtValidationMessage, ImsBaseMessage<ValidatedUserInfoMessage>>
{
    private readonly ILogger<ValidatedUserInfoMessage> _logger;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;

    public ValidateJwtTokenRabbitConsumer(
        ILogger<ValidatedUserInfoMessage> logger, 
        IJwtService jwtService, 
        IMapper mapper) : base(logger)
    {
        _logger = logger;
        _jwtService = jwtService;
        _mapper = mapper;
    }
    
    protected override QueueConfiguration ConfigureQueue()
    {
        return new QueueConfiguration(
            EventsBusNames.CustomRabbitConfigurationNames.ValidateJwtQueueName, 
            EventsBusNames.CustomRabbitConfigurationNames.ValidateJwtExchangeName,
            EventsBusNames.CustomRabbitConfigurationNames.ValidateJwtRoutingKeyName);
    }

    protected override async Task<ImsBaseMessage<ValidatedUserInfoMessage>> ExecuteTask(RequestJwtValidationMessage message) {
        var responseMessage = new ImsBaseMessage<ValidatedUserInfoMessage>();
        try
        {
            var validationResult = await _jwtService.ValidateToken(message.JwtToken);
            var validationResultMessage = _mapper.Map<ValidatedUserInfoMessage>(validationResult);
            responseMessage.Data = validationResultMessage;
        }
        catch (MissingTokenException missingTokenException)
        {
            _logger.LogWarning("Token is null or empty, {Exception}", missingTokenException);
            responseMessage.Error = true;
            responseMessage.ErrorMessage = "Token is null or empty";
        }
        catch (WrongTokenException wrongTokenException)
        {
            _logger.LogWarning("This token isn\'t valid JWT Token, {Exception}", wrongTokenException);
            responseMessage.Error = true;
            responseMessage.ErrorMessage = "This token isn\'t valid JWT Token";
        }
        catch (IncorrectTokenUserIdException incorrectTokenUserIdException)
        {
            _logger.LogWarning("User id is unknown, {Exception}", incorrectTokenUserIdException);
            responseMessage.Error = true;
            responseMessage.ErrorMessage = "User id is unknown";
        }
        catch (Exception exception)
        {
            _logger.LogWarning("Unknown error, {Exception}", exception);
            responseMessage.Error = true;
            responseMessage.ErrorMessage = "Unknown error";
        }
        return responseMessage;
    }
}