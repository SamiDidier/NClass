namespace NReflectTest.Tests
{
  class DynamicType
  {
    private dynamic foo;
  
    // False True
    private dynamic[] dynamicArray;
    // False False False True
    private dynamic[][,,][] dynamicDoubleArray;
    
    private const dynamic constDynamic = null;

    public DynamicType(dynamic a)
    {

    }

    private dynamic DynamicProperty { get; set; }

    public event DynamicDelegate DynamicEvent;

    dynamic DynamicMethod(dynamic a)
    {
      return 42;
    }

    public static dynamic operator +(DynamicType d1, dynamic d2)
    {
      return 42;
    }

    /********************** Nested **********************/
    public GenericClass<dynamic> dynamicGeneric;

    public class GenericClass<T>
    {
       
    }
    /********************** Nested **********************/

    public delegate dynamic DynamicDelegate(dynamic a);
  }
}
