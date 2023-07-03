using MassTransit;
using Newtonsoft.Json;

namespace IMS.Example1;

public class OrderConsumer: IConsumer<OrderDto>
{
    public async Task Consume(ConsumeContext<OrderDto> context)
    {
        var jsonMessage = JsonConvert.SerializeObject(context.Message);
        Console.WriteLine($"OrderCreated message: {jsonMessage}");
        await context.RespondAsync<OrderDtoResponse>(new
        {
            Id = context.Message.Id,
            Message = context.Message.Text + context.Message.Text
        });
    }
}

