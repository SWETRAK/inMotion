using System.Security.Claims;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.JWT;
using IMS.Shared.Models.Dto;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

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
            var jwtAuthString = !context.Request.Headers.Authorization.IsNullOrEmpty() ? 
                                    context.Request.Headers.Authorization.FirstOrDefault() :
                                    default;
            
            if (jwtAuthString is not null && !jwtAuthString.Equals(string.Empty))
            {

                var jwtTokenArray = jwtAuthString.Split(" ");
                if (jwtTokenArray.Length == 2 && jwtTokenArray[0].Equals("Bearer"))
                {
                    var message = new ImsBaseMessage<RequestJwtValidationMessage>
                    {
                        Data = new RequestJwtValidationMessage
                        {
                            JwtToken = jwtTokenArray[1]
                        }
                    };

                    var userInfoMessageResponse = await _requestClient.GetResponse<ImsBaseMessage<ValidatedUserInfoMessage>>(message);

                    if (userInfoMessageResponse.Message.Data is not null) 
                    {
                        context.User = new ClaimsPrincipal(new ClaimsIdentity(
                            new Claim[]
                            {
                                new(ClaimTypes.NameIdentifier, userInfoMessageResponse.Message.Data.Id),
                                new(ClaimTypes.Email, userInfoMessageResponse.Message.Data.Email),
                                new(ClaimTypes.Name, userInfoMessageResponse.Message.Data.Nickname),
                                new(ClaimTypes.Role, userInfoMessageResponse.Message.Data.Role)
                            },
                            "Token"
                        ));
                    }
                }
            }
            
            await next.Invoke(context);
        }
        catch (InvalidOperationException exception)
        {
            _logger.LogWarning(exception, "Authorization fail");
            await SendErrorResponse(context, StatusCodes.Status403Forbidden, null, exception.GetType().FullName);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "{ExceptionName}, {Message}",
                nameof(exception), exception.Message);
            await SendErrorResponse(context, StatusCodes.Status500InternalServerError, "InternalServerError", exception.GetType().FullName);
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