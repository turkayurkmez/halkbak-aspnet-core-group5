// See https://aka.ms/new-console-template for more information
using Demo.SOLID;

Console.WriteLine("Hello, World!");

Customer customer =new Customer();
customer.CardType = new Gold();
PaymentSystemAfter paymentSystemAfter = new PaymentSystemAfter();
paymentSystemAfter.Customer = customer;
System.Console.WriteLine(paymentSystemAfter.DiscountedPrice(100));

var rect = Geometry.GetRectangle(3,5);

System.Console.WriteLine(rect.GetArea());