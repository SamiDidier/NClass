namespace NReflectTest.Tests.Generics.Using
{
  public class GenericClassDynamic<T>
  {
  }
  
  public class TwoGenericClassDynamic<T1, T2>
  {
  }
  
  public class ThreeGenericClassDynamic<T1, T2, T3>
  {
  }
  
  public class UserDynamic
  {
    // False True
    private GenericClassDynamic<dynamic> genericClassOfDynamic;
    
    private TwoGenericClassDynamic<dynamic, int> twoGenericClassOfDynamicAndInt;
    
    private TwoGenericClassDynamic<int, dynamic> twoGenericClassOfIntAndDynamic;
    
    // False False True
    private GenericClassDynamic<GenericClassDynamic<dynamic>> genericClassOfgenericClassOfDynamic;  
    
    // False False True False
    private ThreeGenericClassDynamic<int, dynamic, string> threeGenericClassOfIntDynamicAndString;
    // False True False False
    private ThreeGenericClassDynamic<dynamic, int, string> threeGenericClassOfDynamicIntAndString;
    // False False False False True False
    private ThreeGenericClassDynamic<int, TwoGenericClassDynamic<int, dynamic>, string> threeGenericClassOfIntTwoGenericClassAndString;
  }
}