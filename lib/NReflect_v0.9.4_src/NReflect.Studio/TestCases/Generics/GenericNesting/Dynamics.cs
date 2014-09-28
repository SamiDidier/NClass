namespace NReflectTest.Tests.Generics.Nesting
{
  public struct GenericStruct<T0>
  {
    public struct NestedGenericStruct<T1,T2>
    {
       
    }

    public struct NestedSingleGenericStruct<T1>
    {
      public struct A<T3>{}
    }
  }

  public class UserClass
  {
    private GenericStruct<GenericStruct<dynamic>> genericGenericDynamicStruct;

    private GenericStruct<dynamic> genericDynamicStruct;
    
    // False True False
    private GenericStruct<dynamic>.NestedSingleGenericStruct<short> genericDynamicNestingShort;
    // False False True
    private GenericStruct<short>.NestedSingleGenericStruct<dynamic> genericShortNestingDynamic;
    
    private GenericStruct<dynamic>.NestedGenericStruct<string, short> genericDynamicNestedGenericStringShort;
    
    // False True False False
    private GenericStruct<dynamic>.NestedSingleGenericStruct<short>.A<bool> genericDynamicNestingShortABool;
    // False False True False True False False
    private GenericStruct<GenericStruct<dynamic>.NestedGenericStruct<int, dynamic>>.NestedSingleGenericStruct<short>.A<bool> genericNestedDynamicNestingShortABool;
    
    private GenericStruct<dynamic>.NestedGenericStruct<string, short?>? genericDynamicNestedGenericStringShortNullableNullable;
    
    private GenericStruct<int?>.NestedSingleGenericStruct<int?>? nestedSingleGenericStruct;
  }
}