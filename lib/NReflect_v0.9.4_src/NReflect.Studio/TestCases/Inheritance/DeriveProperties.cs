using System;
using System.Collections.Generic;
using System.Text;

namespace NReflectTest.Tests
{
  public class DerivePropertiesTests
  {
    public virtual int Virtual1 { get; set; }
    public virtual int Virtual2 { get; set; }
    public virtual int Virtual3 { get; set; }
    public virtual int Virtual4 { get; set; }

    public int NoVirtual1 { get; set; }
    public int NoVirtual2 { get; set; }
    public int NoVirtual3 { get; set; }
  }

  public class DerivedProperties : DerivePropertiesTests
  {
    public new virtual int Virtual1 { get; set; }
    public override int Virtual2 { get; set; }
    public sealed override int Virtual3 { get; set; }
    public int Virtual4 { get; set; }

    public new int NoVirtual1 { get; set; }
    public int NoVirtual2 { get; set; }
    public new virtual int NoVirtual3 { get; set; }
  }
}
