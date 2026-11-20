using System;

namespace ECommerce.API.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<RequestLoggingMiddleware> logger)
    {
        //next çağrılmadan önce; bu middleware'in çalıştığını söyle:
        logger.LogInformation("Logging için middleware'e istek geldi...");
        await _next(context);
        //next çağrıldıktan sonra: response oluştu demektir.
        logger.LogInformation("Gelen istek: {Method} {Path} -> dönen response: {StatusCode}",
                                context.Request.Method, context.Request.Path, context.Response.StatusCode);

    }
}
