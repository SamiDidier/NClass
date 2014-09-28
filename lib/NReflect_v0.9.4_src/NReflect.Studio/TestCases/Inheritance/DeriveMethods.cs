using System;
using System.Collections.Generic;
using System.Text;

namespace NReflectTest.Tests
{
  public class DeriveMethodTests
  {
    public virtual void Virtual1()
    {
    }
    public virtual void Virtual2()
    {
    }
    public virtual void Virtual3()
    {
    }
    public virtual void Virtual4()
    {
    }
    public void NoVirtual1()
    {
    }
    public void NoVirtual2()
    {
    }
    public void NoVirtual3()
    {
    }
  }

  public class DerivedMethods : DeriveMethodTests
  {
    public new virtual void Virtual1()
    {
    }

    public override void Virtual2()
    {
    }

    public sealed override void Virtual3()
    {
    }
    
    public void Virtual4()
    {
    }

    public new void NoVirtual1()
    {
    }
    
    public void NoVirtual2()
    {
    }
    
    public new virtual void NoVirtual3()
    {
    }
  }
}
