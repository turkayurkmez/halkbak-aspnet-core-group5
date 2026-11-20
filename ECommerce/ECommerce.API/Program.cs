var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Bir httpRequest'in gerek duyduğu tüm nesneleri burada kofigüre eder ve "çantaya eklersiniz."

var app = builder.Build(); //bu satır çantanın fermuarını çektiğiniz satırdır.

// Configure the HTTP request pipeline.
app.Run();
