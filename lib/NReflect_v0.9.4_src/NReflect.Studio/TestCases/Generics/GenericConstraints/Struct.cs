using System;
using System.Windows.Forms;

namespace NReflectTest.Tests.Generics.Constraints
{
  public class GenericClassStruct<T>
  {
  }

  public struct GenericStruct<T>
  {
  }

  public struct GenericWhereTButtonStruct<T> where T : Button
  {
  }

  public struct GenericWhereTGenericTStruct<T> where T : GenericClassStruct<T>
  {
  }

  public struct GenericWhereTGenericStringStruct<T> where T : GenericClassStruct<string>
  {
  }

  public struct GenericWhereTNewStruct<T> where T : new()
  {
  }

  public struct GenericWhereTStructStruct<T> where T : struct
  {
  }

  public struct GenericWhereTClassStruct<T> where T : class
  {
  }

  public struct GenericWhereTIComparableStruct<T> where T : IComparable
  {
  }

  public struct GenericWhereTMultipleStruct<T> where T : class, IComparable, IConvertible, new()
  {
  }

  public struct GenericWhereTMultipleSMultipleStruct<T, S> where T : class, IComparable, IConvertible, new()
                                                           where S : class, new()
  {
  }
}
