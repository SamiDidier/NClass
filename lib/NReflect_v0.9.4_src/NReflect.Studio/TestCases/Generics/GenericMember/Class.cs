using System;
using System.Windows.Forms;

namespace NReflectTest.Tests.Generics.Member
{
  public class GenericMemberTests<T> where T : class 
  {
    private T field;

    public T Property { get; set; }

    public GenericMemberTests(T param)
    {
      
    }

    public T Method(T param)
    {
      return null;
    }

    public static bool operator ==(GenericMemberTests<T> param1, T param2)
    {
      return false;
    }

    public static bool operator !=(GenericMemberTests<T> param1, T param2)
    {
      return !(param1 == param2);
    }

    public static GenericMemberTests<T> operator + (GenericMemberTests<T> i)
    {
      return null;
    }

    public static implicit operator T(GenericMemberTests<T> i)
    {
      return null;
    }

    public delegate T GenericDelegate(T param);

    public event GenericDelegate Event; 
  }
}
