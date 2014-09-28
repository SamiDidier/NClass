namespace NReflectTest.Tests.Generics.Using
{
  public class GenericClassDerive<T>
  {
  }
  
  public class DerivedGenericClass<T1, T2> : GenericClassDerive<T1>
  {
  }
  
  public class SecondDerivedClass : DerivedGenericClass<string, int>
  {
  }
  
  public class DerivedGenericWithGenericClass : DerivedGenericClass<GenericClassDerive<string>, int>
  {
  }
}