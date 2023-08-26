using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.Friendship;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMS.Friends.BLL.Consumers;

public class CheckFriendshipStatusConsumer: IConsumer<ImsBaseMessage<FriendshipStatusMessage>>
{
    private readonly ILogger<CheckFriendshipStatusConsumer> _logger;

    public CheckFriendshipStatusConsumer(ILogger<CheckFriendshipStatusConsumer> logger)
    {
        _logger = logger;
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


        }
        catch (Exception exception)
        {

        }
        finally
        {
            await context.RespondAsync(response);
        }
    }
}