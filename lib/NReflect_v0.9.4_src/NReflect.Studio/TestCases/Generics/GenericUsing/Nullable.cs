using System;

namespace NReflectTest.Tests.Generics.Using
{
  public struct GenericStructNullable<T>
  {
  }
  
  public class GenericClassNullable<T>
  {
    private GenericClassNullable<int?> genericClassNullableInt;

    private ArraySegment<int>? genericStructIntNullable;

    private ArraySegment<int?>? genericStructNullableIntNullable;
    
    private GenericStructNullable<int?[]> genericStructNullableIntArray;
    
    private ArraySegment<int?[]>? genericStructNullableIntArrayNullable;
    
    private ArraySegment<int?[]>?[] genericStructNullableIntArrayNullableArray;
  }
}