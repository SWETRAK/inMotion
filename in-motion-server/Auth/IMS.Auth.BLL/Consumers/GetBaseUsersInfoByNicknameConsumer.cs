using AutoMapper;
using IMS.Auth.IBLL.Services;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.JWT;
using IMS.Shared.Models.Exceptions;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMS.Auth.BLL.Consumers;

public class GetBaseUsersInfoByNicknameConsumer: IConsumer<ImsBaseMessage<GetBaseUserInfoByNicknameMessage>>
{
    private readonly IUserService _userService;
    private readonly ILogger<GetBaseUsersInfoByNicknameConsumer> _logger;
    private readonly IMapper _mapper;

    public GetBaseUsersInfoByNicknameConsumer(
        IUserService userService, 
        ILogger<GetBaseUsersInfoByNicknameConsumer> logger, 
        IMapper mapper)
    {
        _userService = userService;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task Consume(ConsumeContext<ImsBaseMessage<GetBaseUserInfoByNicknameMessage>> context)
    {
        var message = context.Message;
        var responseMessage = new ImsBaseMessage<GetBaseUserInfoByNicknameResponseMessage>
        {
            Error = false,
            ErrorMessage = null
        };
        try
        {
            var userResponse = await _userService.GetUsersByNickname(message.Data.Username);
            responseMessage.Data = new GetBaseUserInfoByNicknameResponseMessage
            {
                Users = _mapper.Map<IEnumerable<GetBaseUserInfoResponseMessage>>(userResponse)
            };
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception,"Something went wrong");
            responseMessage.Error = true;
            responseMessage.ErrorMessage = "Something went wrong";
        }
        finally
        {
            await context.RespondAsync<ImsBaseMessage<GetBaseUserInfoByNicknameResponseMessage>>(responseMessage);
        }
    }
}