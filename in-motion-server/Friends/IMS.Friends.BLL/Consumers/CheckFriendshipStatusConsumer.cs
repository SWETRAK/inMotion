using IMS.Friends.IBLL.Services;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.Friendship;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMS.Friends.BLL.Consumers;

public class CheckFriendshipStatusConsumer: IConsumer<ImsBaseMessage<FriendshipStatusMessage>>
{
    private readonly ILogger<CheckFriendshipStatusConsumer> _logger;
    private readonly IFriendsService _friendsService;

    public CheckFriendshipStatusConsumer(
        ILogger<CheckFriendshipStatusConsumer> logger, 
        IFriendsService friendsService
    )
    {
        _logger = logger;
        _friendsService = friendsService;
    }

    public async Task Consume(ConsumeContext<ImsBaseMessage<FriendshipStatusMessage>> context)
    {
        var response = new ImsBaseMessage<FriendshipStatusResponseMessage>();
        var message = context.Message;
        if (message.Data is null)
        {
            response.Error = true;
            response.ErrorMessage = "Message data is null";
            _logger.LogWarning("Message data is null in {ConsumerName}", nameof(CheckFriendshipStatusConsumer));
            await context.RespondAsync(response);
            return;
        }

        try
        {
            var requestData = message.Data;

            var friendshipResponse = await _friendsService.GetFriendshipStatus(
                requestData.FirstUserId, requestData.SecondUserId);

            response.Data = new FriendshipStatusResponseMessage
            {
                FirstUserId = requestData.FirstUserId,
                SecondUserId = requestData.SecondUserId,
                FriendshipStatus = friendshipResponse
            };
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception, "{ConsumerName} | Something went wrong", nameof(CheckFriendshipStatusConsumer));
            response.ErrorMessage = "Something went wrong";
            response.Error = true;
        }
        finally
        {
            await context.RespondAsync(response);
        }
    }
}