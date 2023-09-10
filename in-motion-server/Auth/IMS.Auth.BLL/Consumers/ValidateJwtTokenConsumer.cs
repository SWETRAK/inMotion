using AutoMapper;
using IMS.Auth.IBLL.Services;
using IMS.Auth.Models.Exceptions;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.JWT;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IMS.Auth.BLL.Consumers;

public class ValidateJwtTokenConsumer: IConsumer<ImsBaseMessage<RequestJwtValidationMessage>>
{
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;
    private readonly ILogger<ValidateJwtTokenConsumer> _logger;
    
    public ValidateJwtTokenConsumer(IJwtService jwtService, IMapper mapper, ILogger<ValidateJwtTokenConsumer> logger)
    {
        _jwtService = jwtService;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<ImsBaseMessage<RequestJwtValidationMessage>> context)
    {
        var message = context.Message;
        _logger.LogInformation(JsonConvert.SerializeObject(context));
        var responseMessage = new ImsBaseMessage<ValidatedUserInfoMessage>();
        try
        {
            var validationResult = await _jwtService.ValidateToken(message.Data.JwtToken);
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
        finally
        {
            await context.RespondAsync<ImsBaseMessage<ValidatedUserInfoMessage>>(responseMessage);
        }
    }
}