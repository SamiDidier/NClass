namespace NReflectTest.Tests.Generics.Using
{
  public struct GenericStructNullableDynamic<T0>
  {
  }

  public class UserClass
  {
    // Nullable and dynamic
    private GenericStructNullableDynamic<dynamic>? genericDynamicNullable;
  }
}
