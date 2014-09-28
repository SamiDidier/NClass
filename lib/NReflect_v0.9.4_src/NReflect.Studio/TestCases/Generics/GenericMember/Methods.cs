using System;
using System.Windows.Forms;

namespace NReflectTest.Tests.Generics.Member
{
  public class GenericMethods<T>
  {
    public event GenericDelegate<T> Event; 

    public delegate X GenericDelegate<X>(X param);

    public void GenericMethod<X>(X param)
    {

    }

    public void GenericMethodWhereXClass<X>(X param) where X : class
    {

    }

    public void GenericMethodWhereXStruct<X>(X param) where X : struct 
    {

    }

    public void GenericMethodWhereXNew<X>(X param) where X : new()
    {

    }

    public void GenericMethodWhereXButton<X>(X param) where X : Button
    {

    }

    public void GenericMethodWhereXMultiple<X>(X param) where X : class, IConvertible, IComparable, new()
    {

    }

    public void GenericMethodWhereXMultipleAndYMultiple<X, Y>(X param) where X : class, IConvertible, IComparable, new()
      where Y : class, IConvertible, IComparable, new()
    {

    }
    
    public X GenericMethodWithReturn<X, Y>(int i, Y param, dynamic d)
    {
      return default(X);
    }
  }
}
