using IMS.Friends.IBLL.Services;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.Friendship;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMS.Friends.BLL.Consumers;

public class GetUserFriendsConsumer: IConsumer<ImsBaseMessage<GetUserFriendsMessage>>
{
    private readonly IFriendsListsService _friendsListsService;
    private readonly ILogger<GetUserFriendsConsumer> _logger;

    public GetUserFriendsConsumer(IFriendsListsService friendsListsService, ILogger<GetUserFriendsConsumer> logger)
    {
        _friendsListsService = friendsListsService;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<ImsBaseMessage<GetUserFriendsMessage>> context)
    {
        var response = new ImsBaseMessage<GetUserFriendsResponseMessage>();
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

            var friendshipResponse = await _friendsListsService.GetFriendsIdsAsync(requestData.UserId);

            response.Data = new GetUserFriendsResponseMessage
            {
                FriendsIds  = friendshipResponse.Select(s => s.ToString())
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