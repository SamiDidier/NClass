namespace NReflectTest.Tests
{
  public static class ExtensionMethodTests
  {
    public static string GetFoo(this string s)
    {
      return "foo";
    }

    public static void DoSomething(this object o)
    {
      
    }
  }
}
