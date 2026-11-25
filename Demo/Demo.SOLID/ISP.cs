using System;

namespace Demo.SOLID;

public interface IMath
{

    double Add(double a, double b);
    double Subtract(double a, double b);
    double Multiply(double a, double b);


}

public interface IAdvancedMath
{
    double Tan(double angle);
    double Cos(double angle);
    double Sqrt(double value);
}

public class SimpleMath : IMath
{
    public double Add(double a, double b)
    {
        return a + b;
    }


    public double Multiply(double a, double b)
    {
        return a * b;
    }



    public double Subtract(double a, double b)
    {
        return a - b;
    }


}

public class ComplexMath : IAdvancedMath, IMath
{
    public double Add(double a, double b)
    {
        throw new NotImplementedException();
    }

    public double Cos(double angle)
    {
        throw new NotImplementedException();
    }

    public double Multiply(double a, double b)
    {
        throw new NotImplementedException();
    }

    public double Sqrt(double value)
    {
        throw new NotImplementedException();
    }

    public double Subtract(double a, double b)
    {
        throw new NotImplementedException();
    }

    public double Tan(double angle)
    {
        throw new NotImplementedException();
    }
}