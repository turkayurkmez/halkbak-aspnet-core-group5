using System;

namespace ECommerce.API.Options;

public class ShopOptions
{
    /*
    "Shop":{
       "Currency":"TRY",
       "MaxOrderItems":20,
       "StoreName":"ECommerce"
     }
    */
    public const string SectionName = "Shop";

    public string Currency { get; set; } = string.Empty;
    public int MaxOrderItems { get; set; }
    public string StoreName { get; set; } = string.Empty;

}
