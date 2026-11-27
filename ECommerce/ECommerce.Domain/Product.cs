using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain;


/*
ECommerce uygulamasında ne (hangi varlıklar) var?

Müşteri sepete ürün ekler ve sipariş verir.
*/
public class Product
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Ürün Adı boş olamaz.")]
    [MaxLength(200, ErrorMessage ="Ürün adı en fazla 200 karakter olabilir.")]
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }

    //Navigation Property:
    public virtual Category? Category { get; set; }


}
