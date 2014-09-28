using System.Collections.Generic;

namespace NReflectTest.Tests
{
  class ParameterTests
  {
    public void M1(int iIn, out int iOut, ref int iRef)
    {
      iOut = 0;
    }

    public void M2(params int[] i)
    { 
    }

    public void M3(int i = 0, bool b = true, string s = "foo", char c = 'c')
    {
    }

    public void M4(ref List<int> refListOfInt)
    {}

    public delegate void TestDelegate(ref string s);
    public delegate void TestDelegate2(out string s);
    public delegate void TestDelegate3(params int[] s);
  }
}
