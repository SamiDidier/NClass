using System.Collections.Generic;

namespace NReflectTest.Tests.Generics.Using
{
  public class GenericClassUsing<T>
  {
  }
  
  public class TwoGenericClass<T1, T2>
  {
  }
  
  public class User
  {
    private GenericClassUsing<string> genericClassOfString;
    private TwoGenericClass<string, int> twoGenericClassOfStringAndInt;
    
    private TwoGenericClass<List<string>, int> twoGenericClassOfGenericClassOfStringAndInt;
  }
}