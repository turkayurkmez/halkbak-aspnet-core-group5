using System;

namespace Demo.SOLID;

public interface ISender
{
    void Send();
}


public class ReportGenerator
{
    /*
       prensip: Dependency Inversion
       pattern: Dependency Injection
    */
   private ISender sender;
   public ReportGenerator(ISender sender)
   {
    this.sender = sender;
   }

   public void Send()
    {
         // EpostaSender epostaSender = new EpostaSender();
          sender.Send();
    }

}

public class EpostaSender : ISender
{
     public void Send(){}  
}
