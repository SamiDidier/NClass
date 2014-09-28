namespace NReflectTest.Tests.Generics.Nesting
{
  public class GenericParent<T0>
  {
    public class FirstNestingGenericClass<T1>
    {
      public class SecondNestingGenericClass<T2>
      {
        public class ThirdNestingClass
        {

        }
      }
      public class SecondNestingClass
      {

      }

      private GenericParent<T0> genericParentOfT0;
      private FirstNestingClass firstNesting;
      private FirstNestingGenericClass<T1> firstNestingOfInt;
      private SecondNestingClass firstNestingOfIntSecondNesting;
      private SecondNestingGenericClass<short> firstNestingOfIntSecondNestingOfShort;
      private SecondNestingGenericClass<short>.ThirdNestingClass firstNestingOfIntSecondNestingOfShortThirdNesting;

      private GenericParent<string> genericParentOfString;
      private GenericParent<string>.FirstNestingClass genericParentOfStringFirstNesting;
      private GenericParent<string>.FirstNestingGenericClass<int> genericParentOfStringFirstNestingOfInt;
      private GenericParent<string>.FirstNestingGenericClass<int>.SecondNestingClass genericParentOfStringFirstNestingOfIntSecondNesting;
      private GenericParent<string>.FirstNestingGenericClass<int>.SecondNestingGenericClass<short> genericParentOfStringFirstNestingOfIntSecondNestingOfShort;
      private GenericParent<string>.FirstNestingGenericClass<int>.SecondNestingGenericClass<short>.ThirdNestingClass genericParentOfStringFirstNestingOfIntSecondNestingOfShortThirdNesting;
    }
  
    public class FirstNestingClass
    {

    }

    private GenericParent<T0> genericParentOfT0;
    private FirstNestingClass firstNesting;
    private FirstNestingGenericClass<int> firstNestingOfInt;
    private FirstNestingGenericClass<int>.SecondNestingClass firstNestingOfIntSecondNesting;
    private FirstNestingGenericClass<int>.SecondNestingGenericClass<short> firstNestingOfIntSecondNestingOfShort;
    private FirstNestingGenericClass<int>.SecondNestingGenericClass<short>.ThirdNestingClass firstNestingOfIntSecondNestingOfShortThirdNesting;

    private GenericParent<string> genericParentOfString;
    private GenericParent<string>.FirstNestingClass genericParentOfStringFirstNesting;
    private GenericParent<string>.FirstNestingGenericClass<int> genericParentOfStringFirstNestingOfInt;
    private GenericParent<string>.FirstNestingGenericClass<int>.SecondNestingClass genericParentOfStringFirstNestingOfIntSecondNesting;
    private GenericParent<string>.FirstNestingGenericClass<int>.SecondNestingGenericClass<short> genericParentOfStringFirstNestingOfIntSecondNestingOfShort;
    private GenericParent<string>.FirstNestingGenericClass<int>.SecondNestingGenericClass<short>.ThirdNestingClass genericParentOfStringFirstNestingOfIntSecondNestingOfShortThirdNesting;
  }

  public class GenericUser
  {
    private GenericParent<string> genericParentOfString;
    private GenericParent<string>.FirstNestingClass genericParentOfStringFirstNesting;
    private GenericParent<string>.FirstNestingGenericClass<int> genericParentOfStringFirstNestingOfInt;
    private GenericParent<string>.FirstNestingGenericClass<int>.SecondNestingClass genericParentOfStringFirstNestingOfIntSecondNesting;
    private GenericParent<string>.FirstNestingGenericClass<int>.SecondNestingGenericClass<short> genericParentOfStringFirstNestingOfIntSecondNestingOfShort;
    private GenericParent<string>.FirstNestingGenericClass<int>.SecondNestingGenericClass<short>.ThirdNestingClass genericParentOfStringFirstNestingOfIntSecondNestingOfShortThirdNesting;
  }
}