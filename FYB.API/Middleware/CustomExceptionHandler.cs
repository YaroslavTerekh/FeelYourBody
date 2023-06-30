using FluentValidation;
using FYB.BL.Exceptions;
using FYB.Data.Constants;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace FYB.API.Middleware;

public class CustomExceptionHandler
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandler(RequestDelegate next) =>
        _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        object result = null;

        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = validationException.Errors.Select(e => e.ErrorMessage).ToList();
                break;

            case RegisterException registerException:
                code = registerException.Code;
                result = registerException.Errors.Select(e => e.Description).ToList();
                break;

            case NotFoundException notFoundException:
                code = HttpStatusCode.NotFound;
                result = notFoundException.Description;
                break;

            case DbUpdateException dbUpdateException:
                code = HttpStatusCode.InternalServerError;
                result = ErrorMessages.UnknownError;
                break;

            case Exception defaultException:
                code = HttpStatusCode.BadRequest;
                result = defaultException.Message;
                break;

            default:
                code = HttpStatusCode.BadRequest;
                result = "На сервері сталась невідома помилка";
                break;
        }
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == null)
        {
            result = exception.Message;
            code = HttpStatusCode.InternalServerError;
        }

        return context.Response.WriteAsync(JsonSerializer.Serialize(new { error = result, errorCode = code }));
    }
}
