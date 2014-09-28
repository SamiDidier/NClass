namespace NReflectTest.Tests
{
  class UnsafePointer
  {
    unsafe private int* intPointer;

    unsafe public int* IntPointer { get; set; }

    private static unsafe int* ip;

    unsafe delegate int* IntPointerDelegate(int* pointer);

    unsafe event IntPointerDelegate IntPointerEvent;

    unsafe int* IntPointerMethod(int* pointer)
    {
      return pointer;
    }

    public static unsafe int* operator +(UnsafePointer up)
    {
      return ip;
    }

    public static unsafe UnsafePointer operator +(UnsafePointer up, int* pointer)
    {
      return ip;
    }

    public static unsafe implicit operator int*(UnsafePointer up)
    {
      return ip;
    }

    public static unsafe implicit operator UnsafePointer(int* i)
    {
      return new UnsafePointer();
    }
  }
}
