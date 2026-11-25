using System;

namespace ECommerce.Application.Notifications;

public interface INotificationChannel
{
     string Name {get;}
     Task SendAsync(string to, string message);
}


// Liskov's Substution Prensibini ihlal etme senaryosu: 
// eğer exception fırlıyorsa, kendisini implemente eden interface'in yerine kullanılamaz.
// public class FaxNotification : INotificationChannel
// {
//     public string Name => throw new NotImplementedException();

//     public Task SendAsync(string to, string message)
//     {
      
//     }
// }