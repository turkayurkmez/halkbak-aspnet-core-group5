using System;

namespace Demo.SOLID;

public abstract class CardTypes
{
    public abstract decimal GetDiscountedPrice(decimal price);
   
}

public class Standard : CardTypes
{
    public override decimal GetDiscountedPrice(decimal price)
    {
        return price * .95m;
    }
}

public class Silver : CardTypes
{
    public override decimal GetDiscountedPrice(decimal price)
    {
        return price * .9m;
    }
}

public class Gold : CardTypes
{
    public override decimal GetDiscountedPrice(decimal price)
    {
        return price * .85m;
    }
}

public class Premium : CardTypes
{
    public override decimal GetDiscountedPrice(decimal price)
    {
       return price * .80m;
    }
}

public class Customer
{
    public CardTypes CardType { get; set; }
}
// public class PaymentSystemBefore
// {
//     public Customer Customer { get; set; }
//     public decimal DiscountedPrice(decimal price)
//     {
//         switch (Customer.CardType)
//         {
//             case CardTypes.Standard:

//                 return price * .95m;
//             case CardTypes.Silver:
//                 return price * .9m;

//             case CardTypes.Gold:
//                 return price * .85m;

//             default:
//                 return price;

//         }
//     }
// }

public class PaymentSystemAfter
{
    public Customer Customer { get; set; }
    //Bir nesne
    //gelişime AÇIK
    //değişime KAPALI
    public decimal DiscountedPrice(decimal price)
    {
        return Customer.CardType.GetDiscountedPrice(price);
    }
}
