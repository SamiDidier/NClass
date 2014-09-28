using System;
using System.Windows.Forms;

namespace NReflectTest.Tests.Generics.Constraints
{
  public class GenericClassDelegate<T>
  {
  }

  public delegate void GenericDelegate<T>(T para);
  public delegate void GenericDelegateTIn<in T>(T para);
  public delegate T GenericDelegateTOut<out T>();

  public delegate T GenericDelegateWhereTButton<T>(T para) where T : Button;

  public delegate T GenericDelegateWhereTGenericT<T>(T para) where T : GenericClassDelegate<T>;

  public delegate T GenericDelegateWhereTGenericString<T>(T para) where T : GenericClassDelegate<string>;

  public delegate T GenericDelegateWhereTNew<T>(T para) where T : new();

  public delegate T GenericDelegateWhereTStruct<T>(T para) where T : struct;

  public delegate T GenericDelegateWhereTClass<T>(T para) where T : class;

  public delegate T GenericDelegateWhereTIComparable<T>(T para) where T : IComparable;

  public delegate T GenericDelegateWhereTMultiple<T>(T para) where T : class, IComparable, IConvertible, new();

  public delegate T GenericDelegateWhereTMultipleSMultipleClass<T, S>(T paraT, S paraS) where T : class, IComparable, IConvertible, new()
                                                                                        where S : class, new();
}
