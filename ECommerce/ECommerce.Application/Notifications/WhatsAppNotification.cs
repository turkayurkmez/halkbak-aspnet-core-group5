using Microsoft.Extensions.Logging;

namespace ECommerce.Application.Notifications;

public class WhatsAppNotification(ILogger<SMSNotification> logger) : INotificationChannel
{
    public string Name => "WhatsApp";

    public async Task SendAsync(string to, string message)
    {
        await Task.CompletedTask;
        logger.LogInformation("[{Channel}] {To}: {Message} gönderildi", Name, to, message);

    }
}

// public class FaxNotification : INotificationChannel
// {
//     public string Name => throw new NotImplementedException();

//     public Task SendAsync(string to, string message)
//     {
      
//     }
// }