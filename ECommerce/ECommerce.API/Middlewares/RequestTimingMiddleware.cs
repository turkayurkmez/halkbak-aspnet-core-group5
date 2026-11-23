using System;
using System.Diagnostics;

namespace ECommerce.API.Middlewares;

public class RequestTimingMiddleware
{

    private readonly RequestDelegate _next;
    public RequestTimingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<RequestTimingMiddleware> logger)
    {
        //next çağrılmadan önce: timer'ı başlat!
        var stopWatch = Stopwatch.StartNew();
        logger.LogInformation("İstek şimdi zaman ölçme middleware'inde ve kronometre başladı!");


        throw new NullReferenceException("Bu hatayı bilerek fırlattık!!");
        await _next(context);


        stopWatch.Stop();
        //next çağrıldıktan sonra: response oluştu demektir.
        logger.LogInformation("Yanıt, {ms} milisaniyede oluştu.", stopWatch.ElapsedMilliseconds);

    }

}
