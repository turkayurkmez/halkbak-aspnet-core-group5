using System;

namespace ECommerce.Application.Notifications;

public class NotificationService(IEnumerable<INotificationChannel> channels)
{
    public async Task NotifyAsync(string to, string message)
    {
        foreach (var channel in channels)
        {
            await channel.SendAsync(to,message);
        }
    }
}
