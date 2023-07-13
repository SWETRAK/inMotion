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
            _logger.LogWarning("User with {RequestDataEmail} tried to login but wasn't found in database, {Exception}, {Message}",
                exception.Email, nameof(exception), exception.Message);
            await SendErrorResponse(context, StatusCodes.Status404NotFound, "User with this email not found", nameof(exception));
        }
        catch (UserNotFoundException exception)
        {
            _logger.LogWarning("User with {Email} try to activate account, {Exception}, {Message}", exception.Email, nameof(exception), exception.Message);
            await SendErrorResponse(context, StatusCodes.Status401Unauthorized, "User not found or incorrect activation token", nameof(exception));
        }
        catch (IncorrectGoogleTokenException exception)
        {
            _logger.LogWarning("User try to authenticate with incorrect google token, {Exception}, {Message}", exception, exception.Message);
            await SendErrorResponse(context, StatusCodes.Status401Unauthorized, "User try to authenticate with incorrect google token", nameof(exception));
        }
        catch (UserWithEmailAlreadyExistsException exception)
        {
            _logger.LogError("User with {Email} try login with provider, {Exception}, {Message}", exception.Email, nameof(exception), exception.Message);
            await SendErrorResponse(context, StatusCodes.Status401Unauthorized, "User try login with provider not related to account", nameof(exception));
        }
        catch (Exception exception)
        {
            _logger.LogWarning("{ExceptionName}, {Exception}, {Message}",
                nameof(exception), exception, exception.Message);
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