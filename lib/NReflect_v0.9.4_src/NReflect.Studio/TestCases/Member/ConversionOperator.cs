namespace NReflectTest.Tests
{
  class ConversionOperatorTests
  {
    public static explicit operator int(ConversionOperatorTests i) { return 0; }
    public static implicit operator long(ConversionOperatorTests i) { return 0; }
  }
}
