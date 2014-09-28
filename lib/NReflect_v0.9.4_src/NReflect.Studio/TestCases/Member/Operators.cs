namespace NReflectTest.Tests
{
  class OperatorTests
  {
    public static int operator +(OperatorTests i, int y) { return y; }
    public static int operator -(OperatorTests i, int y) { return y; }
    public static int operator *(OperatorTests i, int y) { return y; }
    public static int operator /(OperatorTests i, int y) { return y; }
    public static int operator %(OperatorTests i, int y) { return y; }
    public static int operator &(OperatorTests i, int y) { return y; }
    public static int operator |(OperatorTests i, int y) { return y; }
    public static int operator ^(OperatorTests i, int y) { return y; }
    public static int operator <<(OperatorTests i, int y) { return y; }
    public static int operator >>(OperatorTests i, int y) { return y; }
    public static int operator ==(OperatorTests i, int y) { return y; }
    public static int operator !=(OperatorTests i, int y) { return y; }
    public static int operator <(OperatorTests i, int y) { return y; }
    public static int operator >(OperatorTests i, int y) { return y; }
    public static int operator <=(OperatorTests i, int y) { return y; }
    public static int operator >=(OperatorTests i, int y) { return y; }
    public static bool operator +(OperatorTests i) { return false; }
    public static bool operator -(OperatorTests i) { return false; }
    public static bool operator !(OperatorTests i) { return false; }
    public static bool operator ~(OperatorTests i) { return false; }
    public static OperatorTests operator ++(OperatorTests i) { return i; }
    public static OperatorTests operator --(OperatorTests i) { return i; }
    public static bool operator true(OperatorTests i) { return false; }
    public static bool operator false(OperatorTests i) { return false; }

    public bool Equals(OperatorTests other)
    {
      return !ReferenceEquals(null, other);
    }

    public override bool Equals(object obj)
    {
      if(ReferenceEquals(null, obj))
      {
        return false;
      }
      if(ReferenceEquals(this, obj))
      {
        return true;
      }
      if(obj.GetType() != typeof(OperatorTests))
      {
        return false;
      }
      return Equals((OperatorTests)obj);
    }

    public override int GetHashCode()
    {
      return 0;
    }
  }
}
