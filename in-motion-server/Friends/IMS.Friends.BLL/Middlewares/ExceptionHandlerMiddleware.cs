using IMS.Friends.Models.Exceptions;
using IMS.Shared.Models.Dto;
using IMS.Shared.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IMS.Friends.BLL.Middlewares;

public class ExceptionHandlerMiddleware: IMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (InvalidGuidStringException exception)
        {
            _logger.LogWarning(exception, "User Guid cant be Parsed, {Message}", exception.Message);
            await SendErrorResponse(context, StatusCodes.Status401Unauthorized,
                "User try to authenticate with incorrect google token", nameof(exception));
        }
        catch (InvalidFriendshipActionException exception)
        {
            _logger.LogWarning(exception, "Can't create friendship with message, {Message}", exception.Message);
            await SendErrorResponse(context, StatusCodes.Status400BadRequest, "User Guid string is empty",
                nameof(exception));
        }
        catch (UsersNotFoundException exception)
        {
            _logger.LogWarning(exception, exception.Message);
            await SendErrorResponse(context, StatusCodes.Status404NotFound, exception.Message, nameof(exception));
        }
        catch (RabbitMqException exception)
        {
            _logger.LogWarning(exception, "RabbitMq request failure, {Message}", exception.Message);
            await SendErrorResponse(context, StatusCodes.Status500InternalServerError, "Internal RabbitMq exception",
                nameof(exception));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "{ExceptionName}, {Message}",
                nameof(exception), exception.Message);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await SendErrorResponse(context, StatusCodes.Status500InternalServerError, "InternalServerError",
                nameof(exception));
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