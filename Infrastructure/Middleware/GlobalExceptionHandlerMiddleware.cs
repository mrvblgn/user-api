using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Senswise.UserService.Core.Common;
using System.Net;

namespace Senswise.UserService.Infrastructure.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = exception switch
        {
            ValidationException validationException => new
            {
                statusCode = (int)HttpStatusCode.BadRequest,
                response = ApiResponse<object>.ErrorResponse(
                    "Validation failed",
                    validationException.Errors.Select(e => e.ErrorMessage).ToList()
                )
            },
            KeyNotFoundException notFoundException => new
            {
                statusCode = (int)HttpStatusCode.NotFound,
                response = ApiResponse<object>.ErrorResponse(notFoundException.Message)
            },
            ArgumentException argumentException => new
            {
                statusCode = (int)HttpStatusCode.BadRequest,
                response = ApiResponse<object>.ErrorResponse(argumentException.Message)
            },
            _ => new
            {
                statusCode = (int)HttpStatusCode.InternalServerError,
                response = ApiResponse<object>.ErrorResponse(
                    "An unexpected error occurred. Please try again later.",
                    new List<string> { exception.Message }
                )
            }
        };

        context.Response.StatusCode = response.statusCode;
        await context.Response.WriteAsJsonAsync(response.response);
    }
}
