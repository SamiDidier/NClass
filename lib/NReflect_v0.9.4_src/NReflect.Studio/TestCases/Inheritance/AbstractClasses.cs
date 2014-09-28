using System;

namespace NReflectTest
{
  public abstract class Base
  {
    public abstract void Method();
  }

  public abstract class Derived1 : Base
  {
    private int NonAbstractMethod()
    {
      return 1;
    }
  }

  public class Derived2 : Derived1
  {
    public override void Method()
    {
      
    }
  }
}