namespace NReflectTest.Tests
{
  public class MultipleNestingTop
  {
    MultipleNestingTop nestingTop;
    
    MultipleNested1 nesting1;
    MultipleNested1.MultipleNested2 nesting2;
    MultipleNested1.MultipleNested2.MultipleNested3 nesting3;
    
    public class MultipleNested1
    {
      MultipleNestingTop nestingTop;
    
      MultipleNested1 nesting1;
      MultipleNested2 nesting2;
      MultipleNested2.MultipleNested3 nesting3;
      
      public class MultipleNested2
      {
        MultipleNestingTop nestingTop;
      
        MultipleNested1 nesting1;
        MultipleNested2 nesting2;
        MultipleNested3 nesting3;
        public class MultipleNested3
        {
          MultipleNestingTop nestingTop;
        
          MultipleNested1 nesting1;
          MultipleNested2 nesting2;
          MultipleNested3 nesting3;
        }
      }
    }
  }
  
  public class UsingNestedTypesUser
  {
    MultipleNestingTop nestingTop;
    
    MultipleNestingTop.MultipleNested1 nesting1;
    MultipleNestingTop.MultipleNested1.MultipleNested2 nesting2;
    MultipleNestingTop.MultipleNested1.MultipleNested2.MultipleNested3 nesting3;
  }
}