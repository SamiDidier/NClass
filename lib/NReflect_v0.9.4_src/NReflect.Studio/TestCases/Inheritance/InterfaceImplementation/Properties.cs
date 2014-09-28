namespace NReflectTest.Tests
{
  abstract class PropertyRealizationTests : IInterfaceProperties
  {
    public int Property { get; set; }

    public virtual int PropertyVirtual { get; set; }

    public abstract int PropertyAbstract { get; set; }

    int IInterfaceProperties.PropertyExplicit { get; set; }
    
    public int OnlyGetProperty
    {
      get
      {
        return 0;
      }
    }
    
    public int OnlySetProperty
    {
      set
      {
      }
    }
    
    public int OnlyGetPropertyWithImplementedSet { get; set;}
    
    public int OnlySetPropertyWithImplementedGet { get; set; }
    
    public int OnlyGetPropertyWithImplementedPrivateSet { get; private set; }
    
    public int OnlySetPropertyWithImplementedPrivateGet { private get; set; }
  }
  
  public interface IInterfaceProperties
  {
    int Property { get; set; }

    int PropertyVirtual { get; set; }

    int PropertyAbstract { get; set; }

    int PropertyExplicit { get; set; }
    
    int OnlyGetProperty { get; }
    
    int OnlySetProperty { set; }
    
    int OnlyGetPropertyWithImplementedSet { get; }
    
    int OnlySetPropertyWithImplementedGet { set; }
    
    int OnlyGetPropertyWithImplementedPrivateSet { get; }
    
    int OnlySetPropertyWithImplementedPrivateGet { set; }
  }

}
