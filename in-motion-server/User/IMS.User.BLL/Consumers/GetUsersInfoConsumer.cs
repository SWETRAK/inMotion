using MassTransit;

namespace IMS.User.BLL.Consumers;

public class GetUsersInfoConsumer : IConsumer<GetUsersInfoConsumer>
{
    public Task Consume(ConsumeContext<GetUsersInfoConsumer> context)
    {
        throw new NotImplementedException();
    }
}