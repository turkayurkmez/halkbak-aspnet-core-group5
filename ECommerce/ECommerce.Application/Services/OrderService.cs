using System;
using ECommerce.Application.Notifications;

namespace ECommerce.Application.Services;

public class OrderService(NotificationService notificationService)
{
   public async Task CreateOrder()
    {
        //db'ye eklendiğini varsayın....
        // TODO 1: Notification Service burada bildirim gönderecek... 
        await notificationService.NotifyAsync("Customer....","Sipariniz ulaştı");
    }
}
