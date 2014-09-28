using System;
using System.Windows.Forms;

namespace NReflectTest.Tests.Generics.Constraints
{
  public class GenericClassInterface<T>
  {
  }

  public interface IGeneric<T>
  {
  }

  public interface IGenericIn<in T>
  {
  }

  public interface IGenericOut<out T>
  {
  }

  public interface IGenericWhereTButton<T> where T : Button
  {
  }

  public interface IGenericWhereTGenericT<T> where T : GenericClassInterface<T>
  {
  }

  public interface IGenericWhereTGenericString<T> where T : GenericClassInterface<string>
  {
  }

  public interface IGenericWhereTNew<T> where T : new()
  {
  }

  public interface IGenericWhereTStruct<T> where T : struct
  {
  }

  public interface IGenericWhereTClass<T> where T : class
  {
  }

  public interface IGenericWhereTIComparable<T> where T : IComparable
  {
  }

  public interface IGenericWhereTMultiple<T> where T : class, IComparable, IConvertible, new()
  {
  }

  public interface IGenericWhereTMultipleSMultiple<T, S> where T : class, IComparable, IConvertible, new()
                                                         where S : class, new()
  {
  }
}
