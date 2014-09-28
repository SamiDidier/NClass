namespace NReflect.Tests
{
  public class ParentClass
  {
    public class NestedClass
    {
    }
    
    public class DerivedWithinParentFromNested : NestedClass
    {
    }
  }
  
  public class DerivedClass : ParentClass
  {
  }
  
  public class SecondDerivedClass : DerivedClass
  {
  }
  
  public class DerivedFromNested : ParentClass.NestedClass
  {
  }
}
