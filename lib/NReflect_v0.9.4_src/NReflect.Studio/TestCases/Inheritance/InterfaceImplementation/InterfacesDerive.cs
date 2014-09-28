namespace NReflectTest.Tests
{
  public interface IInterface1
  {
    void Method1();
  }
  
  public interface IInterface2
  {
    void Method2();
  }
  
  public interface IDerivedInterface : IInterface1, IInterface2
  {
    void Method3();
  }
  
  public interface ISecondDerivedInterface : IDerivedInterface
  {
    void Method4();
  }
}
