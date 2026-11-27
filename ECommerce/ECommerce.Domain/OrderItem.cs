using System;

namespace ECommerce.Domain;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }

    public int Quantit { get; set; }
    public decimal UnitPrice { get; set; }

    //OrderItem.Order olmasını bilerek istemiyoruz. 
    //Hem gereksiz bir bağ hem de API JSON döndüreceği için ekstra döngü olmasın.
    //public Order Order { get; set; }
    public virtual Product? Product { get; set; }

}
