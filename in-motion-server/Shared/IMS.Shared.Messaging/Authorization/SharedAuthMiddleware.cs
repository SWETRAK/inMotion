using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.JWT;
using IMS.Shared.Models.Dto;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IMS.Shared.Messaging.Authorization;

public class SharedAuthMiddleware: IMiddleware
{
    private readonly ILogger<SharedAuthMiddleware> _logger;
    private readonly IRequestClient<ImsBaseMessage<RequestJwtValidationMessage>> _requestClient;

    public SharedAuthMiddleware(
        ILogger<SharedAuthMiddleware> logger, 
        IRequestClient<ImsBaseMessage<RequestJwtValidationMessage>> requestClient
    )
    {
        _logger = logger;
        _requestClient = requestClient;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            var message = new ImsBaseMessage<RequestJwtValidationMessage>();
            var userInfoMessageResponse = await _requestClient.GetResponse<ImsBaseMessage<ValidatedUserInfoMessage>>(message);
            
            // TODO: Implement logging user validation;
            
            await next.Invoke(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception , "{ExceptionName}, {Message}",
                nameof(exception), exception.Message);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await SendErrorResponse(context, StatusCodes.Status500InternalServerError, "InternalServerError", nameof(exception));
        }
    }
    
    private static async Task SendErrorResponse(HttpContext context, int statusCode, string message, string exceptionTypeName)
    {
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(new ImsHttpError
        {
            Status = statusCode,
            ErrorMessage = message,
            ErrorType = exceptionTypeName
        });
    }
}