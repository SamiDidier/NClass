namespace NReflectTest.Tests
{
  struct MultipleRealizationTestsStruct : IInterface1MultipleStruct
  {
    public void Method1()
    {
    }
  }
  
  struct MultipleRealizationTestsStruct2 : IInterface2MultipleStruct
  {
    public void Method2()
    {
    }
  }
  
  struct MultipleRealizationTestsStruct3 : IInterface1MultipleStruct, IInterface2MultipleStruct
  {
    public void Method1()
    {
    }

    public void Method2()
    {
    }
  }
  
  public interface IInterface1MultipleStruct
  {
    void Method1();
  }
  
  public interface IInterface2MultipleStruct
  {
    void Method2();
  }
}
