using IMS.Auth.Models.Exceptions;
using IMS.Shared.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IMS.Auth.BLL.Middlewares;

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
        catch (IncorrectLoginDataException exception)
        {
            _logger.LogWarning("User with {RequestDataEmail} try to login but not found, {Exception}, {Message}", 
                exception.Email, nameof(exception), exception.Message);
            await SendErrorResponse(context, StatusCodes.Status404NotFound, "User with this email not found", nameof(exception));
        }
        catch (UserNotFoundException exception)
        {
            _logger.LogWarning("User with {Email} try to activate account, {Exception}, {Message}",exception.Email, nameof(exception), exception.Message);
            await SendErrorResponse(context, StatusCodes.Status403Forbidden, "User not found or incorrect activation token", nameof(exception));
        }
        // catch (Exception exception)
        // {
        //     _logger.LogWarning("{Exception}, {Message}", 
        //         nameof(exception), exception.Message);
        //     context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        //     await SendErrorResponse(context, StatusCodes.Status500InternalServerError, "InternalServerError", nameof(exception));
        // }
    }

    private async Task SendErrorResponse(HttpContext context, int statusCode, string message, string exceptionTypeName)
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