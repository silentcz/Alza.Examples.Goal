using System.Net;
using System.Text.Json;

namespace Goal.API.Middlewares;
public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // pipeline continue
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception: {ExMessage}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    /*
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(new
        {
            StatusCode = context.Response.StatusCode,
            Message = "An internal server error occurred."
        }.ToString() ?? string.Empty);
    }
     * old: - zpracovava vsechny vyjimky stejne -> return status 500 (InternalServerError)
     *      - v pripade vyjimky KeyNotFoundException by bole vhodne vracet status 404 (NotFound)
     */

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = exception switch
        {
            EntityNotFoundException => HttpStatusCode.NotFound,
            KeyNotFoundException => HttpStatusCode.NotFound,
            InvalidOperationAppException => HttpStatusCode.BadRequest,
            ArgumentNullException => HttpStatusCode.BadRequest,
            ArgumentException => HttpStatusCode.BadRequest,
            // other custom exception
            _ => HttpStatusCode.InternalServerError
        };

        context.Response.StatusCode = (int)statusCode;

        var message = exception switch
        {
            // default message
            KeyNotFoundException => exception.Message,
            EntityNotFoundException => exception.Message,
            InvalidOperationAppException => exception.Message,
            ArgumentNullException => exception.Message,
            ArgumentException => exception.Message,
            // message for other custom exception
            _ => "An internal server error occurred."
        };

        return context.Response.WriteAsync(
            JsonSerializer.Serialize(new
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            })
        );
    }
    /* zachyceni nativnich vyjimek
     * pridani vlastnich vyjimek
     * serializace do Json kvuli context.Response.ContentType = "application/json"
     */
}