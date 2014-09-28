using System;
using System.Text;

namespace NReflect.Tests
{
  delegate void DelgateTest();

  public delegate void VoidDelegate(int i);

  delegate int IntDelegate(ref int iRef, out double dOut, StringBuilder stringBuilder, params Object[] objectParams);
  
  delegate void ParamsDelegat(params int[] p);
}
