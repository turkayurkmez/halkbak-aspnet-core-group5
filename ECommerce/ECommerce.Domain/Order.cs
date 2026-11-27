using System;

namespace ECommerce.Domain;

public class Order
{
   public int Id { get; set; }
   public int CustomerId { get; set; }

   public DateTime CreatedAt { get; set; }
   
   public virtual List<OrderItem> Items { get; set; } = [];

   public virtual Customer? Customer { get; set; }
}
