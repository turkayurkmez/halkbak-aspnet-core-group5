using System.Diagnostics;
using ECommerce.API;
using ECommerce.API.Middlewares;
using ECommerce.API.Options;
using ECommerce.Application;
using ECommerce.Application.Notifications;
using ECommerce.Application.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Bir httpRequest'in gerek duyduğu tüm nesneleri burada kofigüre eder ve "çantaya eklersiniz."
// Yani, teknik olarak HttpContext nesnesinin erişmesi gereken nesneleri burada ekiyorsunuz
builder.Services.AddControllers();
//appSettings.json içindeki Shop bölümünün değerlerini oku!!!
builder.Services.Configure<ShopOptions>(builder.Configuration.GetSection(ShopOptions.SectionName));
builder.Services.AddOpenApi();

builder.Services.AddSingleton<ProductService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddSingleton<INotificationChannel, SMSNotification>();
builder.Services.AddSingleton<INotificationChannel, EmailNotification>();
builder.Services.AddSingleton<INotificationChannel, WhatsAppNotification>();

var app = builder.Build(); //bu satır çantanın fermuarını çektiğiniz satırdır.


var currency = builder.Configuration["Shop:Currency"];
app.Logger.LogInformation("!!!!BİLGİ!!!! Para birimi: {currency} ",currency);

// app.Use(async (context, next) =>
// {
//     //next çağrılmadan önce; bu middleware'in çalıştığını söyle:
//     app.Logger.LogInformation("Logging için middleware'e istek geldi...");
//     await next(context);
//     //next çağrıldıktan sonra: response oluştu demektir.
//     app.Logger.LogInformation("Gelen istek: {Method} {Path} -> dönen response: {StatusCode}",
//                             context.Request.Method, context.Request.Path, context.Response.StatusCode);

// });
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<RequestTimingMiddleware>();

// app.Use(async (context, next) =>
// {
//     //next çağrılmadan önce: timer'ı başlat!
//     var stopWatch = Stopwatch.StartNew();
//     app.Logger.LogInformation("İstek şimdi zaman ölçme middleware'inde ve kronometre başladı!");

//     await next(context);


//     stopWatch.Stop();
//     //next çağrıldıktan sonra: response oluştu demektir.
//     app.Logger.LogInformation("Yanıt, {ms} milisaniyede oluştu.",stopWatch.ElapsedMilliseconds);

// });




// Configure the HTTP request pipeline.
app.MapGet("/ping", () => Results.Ok(new { durum = "Her şey yolunda" }));
app.MapGet("/error", () => { throw new NotImplementedException(); });

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.Run();
