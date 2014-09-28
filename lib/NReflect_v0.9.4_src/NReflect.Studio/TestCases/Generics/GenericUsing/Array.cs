namespace NReflectTest.Tests.Generics.Using
{
  public class GenericClassArray<T>
  {
    private GenericClassArray<int[]> genericClassOfIntArray;

    private GenericClassArray<int[][]> genericClassOfTwoIntArray;

    private GenericClassArray<int[,]> genericClassOfTwoDIntArray;
    
    private GenericClassArray<int[,][,,]> genericClassOfTwoMoreDIntArray;
    
    private GenericClassArray<int>[] genericClassIntArray;
    
    private GenericClassArray<int>[][] genericClassTwoIntArray;
    
    private GenericClassArray<int>[,] genericClassTwoDIntArray;
    
    private GenericClassArray<int>[,][,,] genericClassTwoMoreDIntArray;
    
    private GenericClassArray<int[,][,,]>[,][,,] genericClassOfTwoMoreDIntArrayTwoMoreDIntArray;
  }
}