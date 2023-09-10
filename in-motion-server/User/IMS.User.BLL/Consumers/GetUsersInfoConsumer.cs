using AutoMapper;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.Users;
using IMS.Shared.Models.Exceptions;
using IMS.User.IBLL.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMS.User.BLL.Consumers;

public class GetUsersInfoConsumer : IConsumer<ImsBaseMessage<GetUsersInfoMessage>>
{
    private readonly IUserService _userService;
    private readonly ILogger<GetUsersInfoConsumer> _logger;
    private readonly IMapper _mapper;

    public GetUsersInfoConsumer(
        IUserService userService, 
        ILogger<GetUsersInfoConsumer> logger, 
        IMapper mapper)
    {
        _userService = userService;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<ImsBaseMessage<GetUsersInfoMessage>> context)
    {
        var message = context.Message;
        var responseMessage = ImsBaseMessage<GetUsersInfoResponseMessage>.CreateInstance(null);
        try
        {
            var userResponse = await _userService.GetFullUsersInfoAsync(message.Data.UserIds.ToList());
            responseMessage.Data = new GetUsersInfoResponseMessage
            {
                UsersInfo = _mapper.Map<IEnumerable<GetUserInfoResponseMessage>>(userResponse)
            }; 
        }
        catch (InvalidGuidStringException exception)
        {
            responseMessage.Error = true;
            responseMessage.ErrorMessage = "User id invalid format";
            _logger.LogWarning(exception, "User id invalid format");
        }
        catch (NestedRabbitMqRequestException exception)
        {
            responseMessage.Error = true;
            responseMessage.ErrorMessage = exception.Message;
            _logger.LogWarning(exception, "{ExceptionMessage}", exception.Message);
        }
        catch (Exception exception)
        {
            responseMessage.Error = true;
            responseMessage.ErrorMessage = "Unknown error occurs while executing GetUserInfoConsumer";
            _logger.LogError(exception, "Unknown error occurs while executing GetUserInfoConsumer");
        }
        finally
        {
            await context.RespondAsync<ImsBaseMessage<GetUserInfoResponseMessage>>(responseMessage);
        }
    }
}