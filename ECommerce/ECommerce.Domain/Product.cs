namespace ECommerce.Domain;


/*
ECommerce uygulamasında ne (hangi varlıklar) var?

Müşteri sepete ürün ekler ve sipariş verir.
*/
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }

    //Navigation Property:
    public virtual Category? Category { get; set; }


}
