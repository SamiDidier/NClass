using System;

namespace NReflectTest.Tests
{
  [AttributeUsage(AttributeTargets.All)]
  public class DummyAttribute : Attribute
  {
    public int IntProperty { get; set; }

    public string StringProperty { get; set; }

    public DummyAttribute()
    {}

    public DummyAttribute(int i, string s, Type type)
    {}
  }
  [AttributeUsage(AttributeTargets.Parameter)]
  public class Dummy2Attribute : Attribute
  {
  }
  
  [Dummy]
  public class AttributedClass<[Dummy] T>
  {
    private const int CONST_INT = 0;

    [Dummy(42, "dummy", typeof(AttributedEnum), IntProperty = CONST_INT, StringProperty = "abc")]
    private int i;

    [Dummy]
    public string StringProperty { get; set; }

    [Dummy]
    public AttributedClass() { }

    [Dummy]
    [return: Dummy]
    public static bool operator <(AttributedClass<T> o1, AttributedClass<T> o2)
    {
      return false;
    }

    [Dummy]
    [return: Dummy]
    public static bool operator >(AttributedClass<T> o1, AttributedClass<T> o2)
    {
      throw new NotImplementedException();
    }

    [Dummy]
    [return: Dummy]
    public int Method<[Dummy]X>([Dummy][Dummy2]int i)
    {
      return 0;
    }

    [Dummy]
    public event AttributedDelgeate Event;
  }

  [Dummy]
  public struct AttributedStruct
  { }

  [Dummy]
  public interface AttributedInterface
  {    
    [Dummy]
    string StringProperty { get; set; }

    [Dummy]
    [return: Dummy]
    void Method();

    [Dummy]
    event AttributedDelgeate Event;
}

  [Dummy]
  public enum AttributedEnum
  {
    [Dummy]
    Simple = 0,
    [Dummy(42, "dummy", typeof(AttributedEnum), IntProperty = 1, StringProperty = "abc")]
    NotSimple = 1,
    Without = 2
  }

  [Dummy]
  public delegate void AttributedDelgeate();
}
