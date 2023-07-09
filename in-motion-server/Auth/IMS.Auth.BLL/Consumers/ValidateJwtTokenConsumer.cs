using AutoMapper;
using IMS.Auth.IBLL.Services;
using IMS.Auth.Models.Exceptions;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.JWT;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMS.Auth.BLL.Consumers;

public class ValidateJwtTokenConsumer: IConsumer<ImsBaseMessage<RequestJwtValidationMessage>>
{
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;
    private readonly ILogger<ValidateJwtTokenConsumer> _logger;

    public ValidateJwtTokenConsumer(IJwtService jwtService, IMapper mapper)
    {
        _jwtService = jwtService;
        _mapper = mapper;
    }
    
    public async Task Consume(ConsumeContext<ImsBaseMessage<RequestJwtValidationMessage>> context)
    {
        var message = context.Message;
        var responseMessage = new ImsBaseMessage<ValidatedUserInfoMessage>();
        try
        {
            var validationResult = _jwtService.ValidateToken(message.Data.JwtToken);
            var validationResultMessage = _mapper.Map<ValidatedUserInfoMessage>(validationResult);
            responseMessage.Data = validationResultMessage;
        }
        catch (MissingTokenException missingTokenException)
        {
            responseMessage.Error = true;
            responseMessage.ErrorMessage = "Token is null or empty";
        }
        catch (WrongTokenException wrongTokenException)
        {
            responseMessage.Error = true;
            responseMessage.ErrorMessage = "This token isn\'t valid JWT Token";
        }
        catch (IncorrectTokenUserIdException incorrectTokenUserIdException)
        {
            responseMessage.Error = true;
            responseMessage.ErrorMessage = "User id is unknown";
        }
        catch (Exception exception)
        {
            responseMessage.Error = true;
            responseMessage.ErrorMessage = "Unknown error";
        }
        finally
        {
            await context.RespondAsync<ImsBaseMessage<ValidatedUserInfoMessage>>(responseMessage);
        }
    }
}