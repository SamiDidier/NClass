using System;
using System.Windows.Forms;

namespace NReflect.Tests.Generics.Constraints
{
  public class GenericClass<T>
  {
  }

  public class GenericWhereTButtonClass<T> where T : Button
  {
  }

  public class GenericWhereTGenericTClass<T> where T : GenericClass<T>
  {
  }

  public class GenericWhereTGenericStringClass<T> where T : GenericClass<string>
  {
  }

  public class GenericWhereTNewClass<T> where T : new()
  {
  }

  public class GenericWhereTStructClass<T> where T : struct
  {
  }

  public class GenericWhereTClassClass<T> where T : class
  {
  }

  public class GenericWhereTIComparableClass<T> where T : IComparable
  {
  }

  public class GenericWhereTMultipleClass<T> where T : class, IComparable, IConvertible, new()
  {
  }

  public class GenericWhereTMultipleSMultipleClass<T, S> where T : class, IComparable, IConvertible, new()
                                                         where S : class, new()
  {
  }
}
