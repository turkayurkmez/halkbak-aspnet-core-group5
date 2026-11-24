// See https://aka.ms/new-console-template for more information
using Demo.SOLID;

Console.WriteLine("Hello, World!");

Customer customer =new Customer();
customer.CardType = new Premium();
PaymentSystemAfter paymentSystemAfter = new PaymentSystemAfter();
paymentSystemAfter.Customer = customer;
System.Console.WriteLine(paymentSystemAfter.DiscountedPrice(100));