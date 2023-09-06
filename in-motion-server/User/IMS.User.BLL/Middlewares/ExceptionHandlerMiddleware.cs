using IMS.Shared.Models.Dto;
using IMS.Shared.Models.Exceptions;
using IMS.User.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IMS.User.BLL.Middlewares;

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
                "User try to authenticate with incorrect google token", typeof(InvalidGuidStringException).ToString());
        }
        catch (NestedRabbitMqRequestException exception)
        {
            _logger.LogWarning(exception, "Nested RabbitMQ Request Exception, {Message}", exception.Message);
            await SendErrorResponse(context, StatusCodes.Status500InternalServerError,
                "Nested RabbitMQ Request Exception", typeof(NestedRabbitMqRequestException).ToString());
        }
        catch (UserNotFoundException exception)
        {
            _logger.LogWarning(exception, "User not found, {Message}", exception.Message);
            await SendErrorResponse(context, StatusCodes.Status404NotFound,
                "User not found", typeof(UserNotFoundException).ToString());
        }
        catch (NotFoundVideoException exception)
        {
            _logger.LogWarning(exception, "Video not found, {Message}", exception.Message);
            await SendErrorResponse(context, StatusCodes.Status404NotFound,
                "Video not found", typeof(NotFoundVideoException).ToString());
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "{ExceptionName}, {Message}",
                nameof(exception), exception.Message);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await SendErrorResponse(context, StatusCodes.Status500InternalServerError, "InternalServerError",
                exception.GetType().ToString());
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