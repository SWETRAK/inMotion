using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.JWT;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Example1.Controllers;

[ApiController]
[Route("auth")]
public class AuthTestController: ControllerBase
{
    private readonly IRequestClient<ImsBaseMessage<RequestJwtValidationMessage>> _requestClient;

    public AuthTestController(IRequestClient<ImsBaseMessage<RequestJwtValidationMessage>> requestClient)
    {
        _requestClient = requestClient;
    }

    [HttpGet("{token}")]
    public async Task<ImsBaseMessage<ValidatedUserInfoMessage>> CheckValidation([FromRoute] string token)
    {
        var request = new ImsBaseMessage<RequestJwtValidationMessage>
        {
            Data = new RequestJwtValidationMessage
            {
                JwtToken = token,
            }
        };
        var responseFromBus = await _requestClient.GetResponse<ImsBaseMessage<ValidatedUserInfoMessage>>(request);

        return responseFromBus.Message;
    }
}