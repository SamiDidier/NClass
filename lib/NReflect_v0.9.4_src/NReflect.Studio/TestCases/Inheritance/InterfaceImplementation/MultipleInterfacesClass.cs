namespace NReflectTest.Tests
{
  class MultipleRealizationTestsClass : IInterface1MultipleClass
  {
    public void Method1()
    {
    }
  }
  
  class MultipleRealizationTestsClass2 : IInterface2MultipleClass
  {
    public void Method2()
    {
    }
  }
  
  class MultipleRealizationTestsClass3 : IInterface1MultipleClass, IInterface2MultipleClass
  {
    public void Method1()
    {
    }

    public void Method2()
    {
    }
  }
  
  class DerivedClass : MultipleRealizationTestsClass3
  {

  }
  
  public interface IInterface1MultipleClass
  {
    void Method1();
  }
  
  public interface IInterface2MultipleClass
  {
    void Method2();
  }
}
