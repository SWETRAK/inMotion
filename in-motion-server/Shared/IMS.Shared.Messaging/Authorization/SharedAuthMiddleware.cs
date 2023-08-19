using System.Security.Claims;
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

    //TODO: Clean this up
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            var jwtAuthString = context.Request.Headers.Authorization.FirstOrDefault();
            if (jwtAuthString is null) throw new Exception();
            
            var jwtTokenArray = jwtAuthString.Split(" ");
            if (jwtTokenArray.Length != 2 && jwtTokenArray[0].Equals("Bearer")) throw new Exception();
            
            var jwtToken = jwtTokenArray[1];
            var message = new ImsBaseMessage<RequestJwtValidationMessage>
            {
                Data = new RequestJwtValidationMessage
                {
                    JwtToken = jwtToken
                }
            };

            var userInfoMessageResponse = await _requestClient.GetResponse<ImsBaseMessage<ValidatedUserInfoMessage>>(message);
            
            if (userInfoMessageResponse.Message.Data is null) throw new Exception("Invalid token");
         
            var responseData = userInfoMessageResponse.Message.Data;
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                    new(ClaimTypes.NameIdentifier, responseData.Id),
                    new(ClaimTypes.Email, responseData.Email),
                    new(ClaimTypes.Name, responseData.Nickname),
                    new(ClaimTypes.Role, responseData.Role)
                },
                "token",
                ClaimTypes.Email,
                ClaimTypes.Role
            ));

            context.User = claimsPrincipal;
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