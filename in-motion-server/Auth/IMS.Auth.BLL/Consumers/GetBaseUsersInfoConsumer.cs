using AutoMapper;
using IMS.Auth.IBLL.Services;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.JWT;
using IMS.Shared.Models.Exceptions;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMS.Auth.BLL.Consumers;

public class GetBaseUsersInfoConsumer: IConsumer<ImsBaseMessage<GetBaseUsersInfoMessage>>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ILogger<GetBaseUsersInfoConsumer> _logger;

    public GetBaseUsersInfoConsumer(IMapper mapper, IUserService userService, ILogger<GetBaseUsersInfoConsumer> logger)
    {
        _mapper = mapper;
        _userService = userService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ImsBaseMessage<GetBaseUsersInfoMessage>> context)
    {
        var message = context.Message;
        var responseMessage = new ImsBaseMessage<GetBaseUsersInfoResponseMessage>
        {
            Error = false,
            ErrorMessage = null
        };
        try
        {
            var userResponse = await _userService.GetUsersInfo(message.Data.UsersId);
            responseMessage.Data = new GetBaseUsersInfoResponseMessage
            {
                UsersInfo = _mapper.Map<IEnumerable<GetBaseUserInfoResponseMessage>>(userResponse)
            };
        }
        catch (InvalidGuidStringException exception)
        {
            _logger.LogWarning(exception, "User guid is incorrect");
            responseMessage.Error = true;
            responseMessage.ErrorMessage = "User guid can not be able to parse";
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception,"Something went wrong");
            responseMessage.Error = true;
            responseMessage.ErrorMessage = "Something went wrong";
        }
        finally
        {
            await context.RespondAsync<ImsBaseMessage<GetBaseUsersInfoResponseMessage>>(responseMessage);
        }
    }
}