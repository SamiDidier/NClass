namespace NReflectTest.Tests
{
  abstract class MethodTests
  {
    public void VoidMethod()
    {
    }
    
    public int IntMethod()
    {
      return 0;
    }
    
    public void VoidMethodIntParam(int i)
    {
    }
    
    public void PublicMethod()
    {
    }

    internal void InternalMethod()
    {
    }

    private void PrivateMethod()
    {
    }
    
    protected void ProtectedMethod()
    {
    }
    
    protected internal void ProtectedInternalMethod()
    {
    }
    
    void DefaultMethod()
    {
    }
    
    public abstract void AbstractMethod();
  }
}
