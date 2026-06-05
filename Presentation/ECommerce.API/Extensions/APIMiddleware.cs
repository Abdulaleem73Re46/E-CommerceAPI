using System.Text.Json;
using Core.Shared.Helpers;
using Core.Entities.Exceptions;

namespace API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var traceId = context.TraceIdentifier;

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception | TraceId: {TraceId}", traceId);

            await HandleExceptionAsync(context, ex, traceId);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex, string traceId)
    {
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse
        {
            TraceId = traceId,
            Errors = new List<string>()
        };

        switch (ex)
        {
            case ValidationException validation:
                response.StatusCode = 400;
                response.Message = "Validation failed";
                response.Errors.Add(validation.Message);
                context.Response.StatusCode = 400;
                break;

            case NotFoundException notFound:
                response.StatusCode = 404;
                response.Message = "Resource not found";
                response.Errors.Add(notFound.Message);
                context.Response.StatusCode = 404;
                break;

            case UnauthorizedAccessException:
                response.StatusCode = 401;
                response.Message = "Unauthorized";
                response.Errors.Add("You are not authorized to access this resource");
                context.Response.StatusCode = 401;
                break;

            case ForbiddenException forbidden:
                response.StatusCode = 403;
                response.Message = "Forbidden";
                response.Errors.Add(forbidden.Message);
                context.Response.StatusCode = 403;
                break;

            case ConflictException conflict:
                response.StatusCode = 409;
                response.Message = "Conflict";
                response.Errors.Add(conflict.Message);
                context.Response.StatusCode = 409;
                break;

            default:
                response.StatusCode = 500;
                response.Message = "Something went wrong";
                response.Errors.Add("Unexpected error occurred. Please try again later.");
                context.Response.StatusCode = 500;
                break;
        }

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}