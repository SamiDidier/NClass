using System;
using System.Collections.Generic;
using System.Text;

namespace NReflectTest.Tests
{
  class FieldTests<TKey, TValue>
  {
    public int iPublic;

    private int iPrivate;

    protected int iProtected;

    protected internal int iProtectedInternal;

    int iDefault;

    internal int iInternal;

    // Since this is not a constant field, the initial value can't
    // be reflected.
    public int iInitial = 42;

    public volatile int iVolatile;

    public static int iStatic;

    // Since this is not a constant field, the initial value can't
    // be reflected.
    public readonly int iReadonly = 5;

    public const int iConst = 5;

    // Since this is not a constant field, the initial value can't
    // be reflected.
    public object nullObject = null;

    public const object constNullObject = null;

    public const string constNullString = null;

    private const string constSpaceString = " ";

    private const string constEscapedString = "\n\\\"\tnr\n\n\'";

    private const char constChar = 'a';

    private const char constEscapedChar = '\'';

    private Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>> dic;
  }
}