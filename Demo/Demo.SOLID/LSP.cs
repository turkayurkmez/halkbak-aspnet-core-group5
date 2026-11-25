using System;
using System.Diagnostics;

namespace Demo.SOLID;

/*
Liskov diyor ki: Bir base class ile ondan türeyen bir derived class birbirlerinin yerine herhangi bir değişiklik olmadan
(exception, tipe göre karar vs) olmadan kullanılabilir olmalıdır.

Aksi halde bu nesneler arasında miras uygulanamaz.
*/
public class Geometry
{
    public static IArea GetRectangle(int unit1, int? unit2 = null)
    {
        if (unit2 is null)
        {
            return new Square(){ EdgeLength = unit1 };
        }

        return new Rectangle(){ Width = unit1, Height = unit2.Value};
    }

    // public static void Sample(Rectangle rectangle)
    // {

    // }
}


public interface IArea
{
    double GetArea();
}

public class Rectangle : IArea
{
   public virtual int Width { get; set; }
   public virtual int Height { get; set; }

   public double GetArea() => Width * Height;
}

public class Square : IArea //: Rectangle
{
    // public override int Width { get => base.Width; set {base.Width = value; base.Height = value;} }
    // public override int Height { get => base.Width; set {base.Width = value; base.Height = value;} }

    public int EdgeLength { get; set; }

    public double GetArea()
    {
       return Math.Pow(EdgeLength,2);
    }
}