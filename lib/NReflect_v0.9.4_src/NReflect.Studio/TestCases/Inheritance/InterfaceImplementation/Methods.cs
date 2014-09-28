namespace NReflectTest.Tests
{
  abstract class MethodRealizationTests : IInterfaceMethods
  {
    public void Method()
    {
    }

    public virtual void MethodVirtual()
    {
    }
    
    public abstract void MethodAbstract();
    
    void IInterfaceMethods.MethodExplicit()
    {
    }
  }
  
  public interface IInterfaceMethods
  {
    void Method();
    
    void MethodVirtual();
    
    void MethodAbstract();
    
    void MethodExplicit();
  }

}
