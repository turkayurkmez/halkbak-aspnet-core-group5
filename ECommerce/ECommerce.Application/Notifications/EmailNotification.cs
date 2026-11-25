using Microsoft.Extensions.Logging;

namespace ECommerce.Application.Notifications;

public class EmailNotification(ILogger<EmailNotification> logger) : INotificationChannel
{
    public string Name => "E-POSTA";

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