using System;
using Microsoft.Extensions.Logging;

namespace ECommerce.Application.Notifications;

public interface INotificationChannel
{
     string Name {get;}
     Task SendAsync(string to, string message);
}

public class EmailNotification(ILogger<EmailNotification> logger) : INotificationChannel
{
    public string Name => "E-POSTA";

    public async Task SendAsync(string to, string message)
    {
        await Task.CompletedTask;
        logger.LogInformation("[{Channel}] {To}: {Message} gönderildi", Name, to, message);
    }
}

public class SMSNotification(ILogger<SMSNotification> logger) : INotificationChannel
{
    public string Name => "SMS";

    public async Task SendAsync(string to, string message)
    {
        await Task.CompletedTask;
        logger.LogInformation("[{Channel}] {To}: {Message} gönderildi", Name, to, message);

    }
}

public class WhatsAppNotification(ILogger<SMSNotification> logger) : INotificationChannel
{
    public string Name => "WhatsApp";

    public async Task SendAsync(string to, string message)
    {
        await Task.CompletedTask;
        logger.LogInformation("[{Channel}] {To}: {Message} gönderildi", Name, to, message);

    }
}