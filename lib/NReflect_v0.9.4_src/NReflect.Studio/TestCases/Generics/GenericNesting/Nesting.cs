namespace NReflectTest.Tests.Generics.Nesting
{
  public class NestingParent
  {
    public class NestingClass
    {
    }
    
    public class GenericNestingClass<T>
    {
    }

    public class TwoGenericNestingClass<T1, T2>
    {
    }

    private NestingParent nestingParent;
    private NestingClass nestingClass;
    private GenericNestingClass<string> genericNestingClass;
    private TwoGenericNestingClass<string, int> twoGenericNestingClass;
    
    private GenericNestingClass<NestingClass> genericNestingOfNesting;
    private GenericNestingClass<GenericNestingClass<string>> genericNestingOfGenericNestingOfString;
  }
  
  public class NestingUser
  {
    private NestingParent nestingParent;
    private NestingParent.NestingClass nestingClass;
    private NestingParent.GenericNestingClass<string> genericNestingOfString;
    private NestingParent.TwoGenericNestingClass<string, int> twoGenericNestingOfStringAndInt;
    
    private NestingParent.GenericNestingClass<NestingParent.NestingClass> genericNestingOfNesting;
    private NestingParent.GenericNestingClass<NestingParent.GenericNestingClass<string>> genericNestingOfGenericNestingOfString;
  }
}