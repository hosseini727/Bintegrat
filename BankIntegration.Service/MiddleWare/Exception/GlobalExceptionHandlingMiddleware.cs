using System.Net;
using System.Text.Json;

namespace BankIntegration.Service.MiddleWare.Exception;

using Microsoft.AspNetCore.Http;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (System.Exception exception)
        {
            await HandleExceptionAsync(exception, httpContext);
        }
    }

    private static Task HandleExceptionAsync(System.Exception exception, HttpContext context)
    {
        HttpStatusCode status;
        string stackTrace = exception.StackTrace ?? string.Empty;
        string message;
        string exceptionResult;
        var exceptionType = exception.GetType();
        var errorDetails = new validationErrorDetail();


        if (exceptionType == typeof(BadRequestException))
        {
            message = exception.Message;
            status = HttpStatusCode.BadRequest;
        }
        else if (exceptionType == typeof(NotFoundException))
        {
            message = exception.Message;
            status = HttpStatusCode.NotFound;
        }
        else if (exceptionType == typeof(NotImplementedException))
        {
            message = exception.Message;
            status = HttpStatusCode.NotImplemented;
        }
        else if (exceptionType == typeof(KeyNotFoundException))
        {
            message = exception.Message;
            status = HttpStatusCode.NotFound;
        }
        else if (exceptionType == typeof(UnUthorizedException))
        {
            message = exception.Message;
            status = HttpStatusCode.Unauthorized;
        }
        else if (exceptionType == typeof(FluentValidation.ValidationException))
        {
            message = exception.Message;
            status = HttpStatusCode.BadRequest;
        }
        else
        {
            message = exception.Message;
            status = HttpStatusCode.InternalServerError;
            stackTrace = exception.StackTrace;
        }

        errorDetails.ErrorMessage = message;
        errorDetails.StackTrace = stackTrace;
        errorDetails.Errors = GetAdditionalDetails(exception);

        exceptionResult = JsonSerializer.Serialize(errorDetails);

        //add log 
        //logger.LogError(exceptionResult);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        return context.Response.WriteAsync(exceptionResult);
    }

    private static List<KeyValuePair<string, string>> GetAdditionalDetails(System.Exception exception)
    {
        var additionalDetails = new List<KeyValuePair<string, string>>();
        if (exception is FluentValidation.ValidationException validationException)
        {
            foreach (var error in validationException.Errors)
            {
                additionalDetails.Add(
                    new KeyValuePair<string, string>($"{error.PropertyName}", $"{error.ErrorMessage}"));
            }
        }

        return additionalDetails;
    }
}