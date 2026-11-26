using System;

namespace ECommerce.API.Middlewares;

//Tüm middleware'ler singleton'dur.
public class ExceptionHandlingMiddleware
{

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    //private readonly DemoService _demoService;
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
      
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            //Sonrasında çalışacak tüm middleware ve endpoint'leri try'a aldık:
            
            await _next(context);
        }
        catch (System.Exception ex)
        {
            //http isteğinin yaşamı boyunca ve elbette yanıt istemciye gönderilmeden önce her yerdeki hatalar buraya düşecek:
            _logger.LogError(ex, "İşlenmeyen hata: {Method} {Path}",context.Request.Method, context.Request.Path);
            
            if (context.Response.HasStarted)
            {
                throw;
            }  

            //istemciye gönderilen yanıt: 500 olsun
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //mesajı da şöyle olsun:
            await context.Response.WriteAsJsonAsync(new {error="Beklenmeyen bir hata meydana geldi"});

            
        }
      
    }
}
