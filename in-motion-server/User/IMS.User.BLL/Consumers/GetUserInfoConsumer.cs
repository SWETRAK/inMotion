using IMS.Shared.Messaging.Messages.Users;
using MassTransit;

namespace IMS.User.BLL.Consumers;

public class GetUserInfoConsumer : IConsumer<GetUserInfoMessage>
{
    public Task Consume(ConsumeContext<GetUserInfoMessage> context)
    {
        throw new NotImplementedException();
    }
}