using AutoMapper;
using IMS.Auth.IBLL.Services;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.JWT;
using IMS.Shared.Models.Exceptions;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMS.Auth.BLL.Consumers;

public class GetBaseUserInfoConsumer: IConsumer<ImsBaseMessage<GetBaseUserInfoMessage>>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ILogger<GetBaseUserInfoConsumer> _logger;

    public GetBaseUserInfoConsumer(
        IMapper mapper,
        ILogger<GetBaseUserInfoConsumer> logger, IUserService userService)
    {
        _mapper = mapper;
        _logger = logger;
        _userService = userService;
    }
    
    public async Task Consume(ConsumeContext<ImsBaseMessage<GetBaseUserInfoMessage>> context)
    {
        var message = context.Message;
        var responseMessage = new ImsBaseMessage<GetBaseUserInfoResponseMessage>
        {
            Error = false,
            ErrorMessage = null
        };
        try
        {
            var userResponse = await _userService.GetUserInfo(message.Data.UserId);
            responseMessage.Data = _mapper.Map<GetBaseUserInfoResponseMessage>(userResponse);
        }
        catch (InvalidUserGuidStringException exception)
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
            await context.RespondAsync<ImsBaseMessage<GetBaseUserInfoResponseMessage>>(responseMessage);
        }
    }
}