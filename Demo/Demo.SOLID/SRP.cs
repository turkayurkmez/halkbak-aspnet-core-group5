using System;

namespace Demo.SOLID;

public class CreateProductScreenBefore
{
   //Bir nesnenin sadece bir sorumluluğu olmalı.
   public void CreateProduct()
    {
        //db işlemlerini yaptım.
        //Insert sorgusu...
        //Gelen yanıtı kontrol ettim.

    }

    public void SendEmail(string boss)
    {
        //burada da mail atılıyor.
    }
    
    public void ChangeBackColor()
    {
        //ekran rengi değiştiriliyor.
    }



}

public class ProductService
{
    public void CreateProduct(string name, decimal price)
    {
        //db'ye ekleniyor....
    }

    //Update()
    //Delete()
    //Select()
}

public class MailSender
{
    public void SendEmail(string to)
    {
        
    }
}

public class CreateProductScreenAfter
{
    public void AddNewProduct(string name, decimal price)
    {
        ProductService productService = new ProductService();
        productService.CreateProduct(name,price);

        MailSender mailSender = new MailSender();
        mailSender.SendEmail("patron");
    }
}
