using System;
using System.Collections.Generic;
using System.Text;

namespace NReflectTest
{
  abstract class PropertiesTests
  {
    public int PublicPublicGet { get { return 2; } }
    public int PublicPublicSet { set { } }
    public int PublicPublicPublic { get; set; }
    public int PublicPublicPrivate { get; private set; }
    public int PublicPrivatePublic { private get; set; }

    protected int ProtectedProtectedGet { get { return 2; } }
    protected int ProtectedProtectedSet { set { } }
    protected int ProtectedProtectedProtected { get; set; }
    protected int ProtectedProtectedPrivate { get; private set; }
    protected int ProtectedPrivateProtected { private get; set; }

    internal int InternalInternalGet { get { return 2; } }
    internal int InternalInternalSet { set { } }
    internal int InternalInternalInternal { get; set; }
    internal int InternalInternalPrivate { get; private set; }
    internal int InternalPrivateInternal { private get; set; }

    protected internal int ProtectedInternalProtectedInternalGet { get { return 2; } }
    protected internal int ProtectedInternalProtectedInternalSet { set { } }
    protected internal int ProtectedInternalProtectedInternalProtectedInternal { get; set; }
    protected internal int ProtectedInternalProtectedInternalPrivate { get; private set; }
    protected internal int ProtectedInternalPrivateProtectedInternal { private get; set; }

    private int PrivatePrivateGet { get { return 2; } }
    private int PrivatePrivateSet { set { } }
    private int PrivatePrivatePrivate { get; set; }

    public int this[int i] { get { return 42; } set { } }
    public int this[long i] { private get { return 42; } set { } }
    public int this[bool i] { get { return 42; } private set { } }

    public static int Static { get; set; }
    public abstract int Abstract { get; set; }
    public virtual int Virtual { get; set; }

    public Dictionary<String, String>.KeyCollection Keys { get; set; }
  }
}
