using System;

namespace NReflect.Tests
{
  public enum EmptyEnumTest
  {
  }
  
  public enum EnumTest
  {
    A,
    B
  }

  public enum ShortEnum : short
  {
    A = 0,
    B = 1,
    C = 2
  }

  [Flags]
  internal enum FlagEnum {
    A = 0x01,
    B = 0x02,
    C = 0x04,
    Default = unchecked((int)0xFFFFFFFF),
  }
}
