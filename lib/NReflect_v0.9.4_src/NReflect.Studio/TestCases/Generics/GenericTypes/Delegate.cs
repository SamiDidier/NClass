namespace NReflect.Tests.Generics.Types
{
  public delegate T GenericDelegate<T>(T para);

  public delegate T TwoGenericDelegate<T, S>(T paraT, S paraS);

  public delegate T TwoGenericDelegateNested<T, S>(T paraT, GenericDelegate<S> paraS);
}