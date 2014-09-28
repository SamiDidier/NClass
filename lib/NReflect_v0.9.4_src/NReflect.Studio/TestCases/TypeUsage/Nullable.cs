namespace NReflectTest.Tests
{
  public class NullableTests
  {
    public NullableTests(int? nullInt)
    {
        Nullint = nullInt;
    }

    public NullableTests(ref int? nullRefInt)
    {
        Nullint = nullRefInt;
    }

    public int? Nullint { get; set; }

    public struct s
    {
      private char? c;
    }

    public s? NullMethod(s? foo)
    {
      return foo;
    }

    public int? NullRefMethod(ref int? refInt)
    {
      return refInt;
    }

    public delegate int? NullDelegate(int? n);

    public delegate int? NullRefDelegate(ref int? n);

    public event NullDelegate NullEvent;
  }
}