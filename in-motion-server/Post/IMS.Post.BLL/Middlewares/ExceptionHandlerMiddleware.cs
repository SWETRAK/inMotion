using IMS.Post.Models.Exceptions;
using IMS.Shared.Models.Dto;
using IMS.Shared.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IMS.Post.BLL.Middlewares;

public class ExceptionHandlerMiddleware : IMiddleware
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
                "User try to authenticate with incorrect google token", exception.GetType().ToString());
        }
        catch (PostNotFoundException exception)
        {
            _logger.LogError(exception, "Post not found, {ExceptionName}, {Message}",
                exception.GetType().ToString(), exception.Message);
            await SendErrorResponse(context, StatusCodes.Status404NotFound, "Post not found",
                exception.GetType().ToString());
        }
        catch (PostCommentNotFoundException exception)
        {
            _logger.LogError(exception, "Post comment not found, {ExceptionName}, {Message}",
                exception.GetType().ToString(), exception.Message);
            await SendErrorResponse(context, StatusCodes.Status404NotFound, "Post comment not found",
                exception.GetType().ToString());
        }
        catch (PostAlreadyUploadedInCurrentIterationException exception)
        {
            _logger.LogWarning(exception, "Post already uploaded in current iteration, {ExceptionName}, {Message}",
                exception.GetType().ToString(), exception.Message);
            await SendErrorResponse(context, StatusCodes.Status400BadRequest, "Post comment not found",
                exception.GetType().ToString());
        }
        catch (PostCommentReactionNotFoundException exception)
        {
            _logger.LogError(exception, "Post comment reaction not found, {ExceptionName}, {Message}",
                exception.GetType().ToString(), exception.Message);
            await SendErrorResponse(context, StatusCodes.Status404NotFound, "Post comment reaction not found",
                exception.GetType().ToString());
        }
        catch (PostReactionNotFoundException exception)
        {
            _logger.LogError(exception, "Post reaction not found, {ExceptionName}, {Message}",
                exception.GetType().ToString(), exception.Message);
            await SendErrorResponse(context, StatusCodes.Status404NotFound, "Post reaction not found",
                exception.GetType().ToString());
        }
        catch (PostIterationNotFoundException exception)
        {
            _logger.LogError(exception, "Post iteration not found, {ExceptionName}, {Message}",
                exception.GetType().ToString(), exception.Message);
            await SendErrorResponse(context, StatusCodes.Status404NotFound, "Post iteration not found",
                exception.GetType().ToString());
        }
        catch (PostReactionAlreadyExistsException exception)
        {
            _logger.LogError(exception, "Post reaction already exists, {ExceptionName}, {Message}",
                exception.GetType().ToString(), exception.Message);
            await SendErrorResponse(context, StatusCodes.Status409Conflict, "Post reaction already exists",
                exception.GetType().ToString());
        }
        catch (PostCommentReactionAlreadyExistsException exception)
        {
            _logger.LogError(exception, "Post comment reaction already exists, {ExceptionName}, {Message}",
                exception.GetType().ToString(), exception.Message);
            await SendErrorResponse(context, StatusCodes.Status409Conflict, "Post comment reaction already exists",
                exception.GetType().ToString());
        }
        catch (PostVideoTypeEnumParseException exception)
        {
            _logger.LogError(exception, "Post comment reaction already exists, {ExceptionName}, {Message}",
                exception.GetType().ToString(), exception.Message);
            await SendErrorResponse(context, StatusCodes.Status400BadRequest, "Post comment reaction already exists",
                exception.GetType().ToString());
        }
        catch (RabbitMqException exception)
        {
            _logger.LogWarning(exception, "RabbitMq request failure, {Message}", exception.Message);
            await SendErrorResponse(context, StatusCodes.Status500InternalServerError, "User with this provider exists",
                exception.GetType().ToString());
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "{ExceptionName}, {Message}",
                exception.GetType().ToString(), exception.Message);
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