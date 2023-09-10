using IMS.Auth.Models.Exceptions;
using IMS.Shared.Models.Dto;
using IMS.Shared.Models.Exceptions;
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
        catch (IncorrectProviderTokenException exception)
        {
            _logger.LogWarning("User try to authenticate with incorrect google token, {Exception}, {Message}", exception, exception.Message);
            await SendErrorResponse(context, StatusCodes.Status401Unauthorized, "User try to authenticate with incorrect google token", nameof(exception));
        }
        catch (UserWithEmailAlreadyExistsException exception)
        {
            _logger.LogWarning("User with {Email} try login with provider, {Exception}, {Message}", exception.Email, nameof(exception), exception.Message);
            await SendErrorResponse(context, StatusCodes.Status401Unauthorized, "User try login with provider not related to account", nameof(exception));
        }
        catch (InvalidGuidStringException exception)
        {
            _logger.LogWarning("User Guid cant be Parsed, {Exception}, {Message}", exception, exception.Message);
            await SendErrorResponse(context, StatusCodes.Status401Unauthorized, "User try to authenticate with incorrect google token", nameof(exception));
        }
        catch (UserGuidStringEmptyException exception)
        {
            _logger.LogWarning("User Guid string is empty, {Exception}, {Message}", exception, exception.Message);
            await SendErrorResponse(context, StatusCodes.Status401Unauthorized, "User Guid string is empty", nameof(exception));
        }
        catch (UserWithThisProviderExists exception)
        {
            _logger.LogWarning("User with this provider exists, {Exception}, {Message}", exception, exception.Message);
            await SendErrorResponse(context, StatusCodes.Status409Conflict, "User with this provider exists", nameof(exception));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "{ExceptionName}, {Message}",
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