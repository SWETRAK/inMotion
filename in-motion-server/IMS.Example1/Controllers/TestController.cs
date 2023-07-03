using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Example1.Controllers;

[ApiController]
[Route("test")]
public class TestController: ControllerBase
{
    private readonly IRequestClient<OrderDto> _requestClient; // do odpowiedzi 
    private readonly IPublishEndpoint _publishEndpoint; // bez odpowiedzi

    public TestController(IRequestClient<OrderDto> requestClient, IPublishEndpoint publishEndpoint)
    {
        _requestClient = requestClient;
        _publishEndpoint = publishEndpoint;
    }
    
    [HttpPost("withoutResponse")]
    public async Task<IActionResult> CreateOrderWithout(OrderDto orderDto)
    {
        // Bez odpowiedzi
        await _publishEndpoint.Publish<OrderDto>(orderDto);
        return Ok();
    }
    
    [HttpPost("withResponse")]
    public async Task<IActionResult> CreateOrder(OrderDto orderDto)
    {
        // Z odpowiedzią
        var result = await _requestClient.GetResponse<OrderDtoResponse>(orderDto);
        return Ok(result);
    }
}