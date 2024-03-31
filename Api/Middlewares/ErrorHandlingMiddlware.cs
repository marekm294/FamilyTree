using Domain.Exceptions.Abstraction;
using Shared.Models.Outputs;
using System.Net;

namespace Api.Middlewares;

public class ErrorHandlingMiddlware
{
    private readonly RequestDelegate _requestDelegate;

    public ErrorHandlingMiddlware(RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _requestDelegate(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        if (exception is ICustomException customException)
        {
            context.Response.StatusCode = (int)customException.StatusCode;
            return context.Response.WriteAsync(customException.ErrorOutput.ToString());
        }

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(
            new ErrorOutput("An internal server error has occurred. Please contact an administrator or try again.").ToString());
    }
}