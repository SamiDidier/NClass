namespace NReflectTest.Tests
{
  class DeriveFieldTests
  {
    public int iPublic;

    protected int iProtected;

    protected internal int iProtectedInternal;

    internal int iInternal;
  }
  
  internal class DerivedFieldTests : DeriveFieldTests
  {
    public new int iPublic;

    protected new int iProtected;

    protected internal new int iProtectedInternal;

    internal new int iInternal;
  }
}
