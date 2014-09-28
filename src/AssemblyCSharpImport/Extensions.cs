using NClass.Core;

namespace NClass.AssemblyCSharpImport
{
  public static class Extensions
  {
    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Converts the access modifier of the NReflect library into the corresponding
    /// NClass access modifier.
    /// </summary>
    /// <param name="accessModifier">The access modifier of the NReflect library to convert.</param>
    /// <returns>The converted NClass access modifier.</returns>
    public static AccessModifier ToNClass(this NReflect.Modifier.AccessModifier accessModifier)
    {
      switch (accessModifier)
      {
        case NReflect.Modifier.AccessModifier.Default:
          return AccessModifier.Default;
        case NReflect.Modifier.AccessModifier.Public:
          return AccessModifier.Public;
        case NReflect.Modifier.AccessModifier.ProtectedInternal:
          return AccessModifier.ProtectedInternal;
        case NReflect.Modifier.AccessModifier.Internal:
          return AccessModifier.Internal;
        case NReflect.Modifier.AccessModifier.Protected:
          return AccessModifier.Protected;
        case NReflect.Modifier.AccessModifier.Private:
          return AccessModifier.Private;
        default:
          return AccessModifier.Default;
      }
    }

    /// <summary>
    /// Converts the class modifier of the NReflect library into the corresponding
    /// NClass class modifier.
    /// </summary>
    /// <param name="classModifier">The class modifier of the NReflect library to convert.</param>
    /// <returns>The converted NClass class modifier.</returns>
    public static ClassModifier ToNClass(this NReflect.Modifier.ClassModifier classModifier)
    {
      switch (classModifier)
      {
        case NReflect.Modifier.ClassModifier.None:
          return ClassModifier.None;
        case NReflect.Modifier.ClassModifier.Abstract:
          return ClassModifier.Abstract;
        case NReflect.Modifier.ClassModifier.Sealed:
          return ClassModifier.Sealed;
        case NReflect.Modifier.ClassModifier.Static:
          return ClassModifier.Static;
        default:
          return ClassModifier.None;
      }
    }


    /// <summary>
    /// Converts the access modifier of the NRefactory library into the corresponding
    /// NClass access modifier.
    /// </summary>
    /// <param name="accessModifier">The access modifier of the NRefactory library to convert.</param>
    /// <returns>The converted NClass access modifier.</returns>
    public static AccessModifier ToNClass(this ICSharpCode.NRefactory.CSharp.Modifiers accessModifier)
    {
        switch (accessModifier)
        {
            /*
		
      = 0x0010,
    Virtual   = 0x0020,
    Sealed    = 0x0040,
    Static    = 0x0080,
    Override  = 0x0100,
    Readonly  = 0x0200,
    Const     = 0x0400,
    New       = 0x0800,
    Partial   = 0x1000,
		
    Extern    = 0x2000,
    Volatile  = 0x4000,
    Unsafe    = 0x8000,
    Async     = 0x10000,
		
    VisibilityMask = Private | Internal | Protected | Public,
            */

            case ICSharpCode.NRefactory.CSharp.Modifiers.None:
                return AccessModifier.Default;
            case ICSharpCode.NRefactory.CSharp.Modifiers.Private:
                return AccessModifier.Private;
            case ICSharpCode.NRefactory.CSharp.Modifiers.Internal:
                return AccessModifier.Internal;
            case ICSharpCode.NRefactory.CSharp.Modifiers.Protected:
                return AccessModifier.Protected;
            case ICSharpCode.NRefactory.CSharp.Modifiers.Public:
                return AccessModifier.Public;
            // case ICSharpCode.NRefactory.CSharp.Modifiers.Abstract :
                // return AccessModifier.;
            // case ICSharpCode.NRefactory.CSharp.Modifiers.Virtual :
            // return AccessModifier.;
            // case ICSharpCode.NRefactory.CSharp.Modifiers.Sealed :
            // return AccessModifier.;
            // case ICSharpCode.NRefactory.CSharp.Modifiers.Static :
            // return AccessModifier.;
            // case ICSharpCode.NRefactory.CSharp.Modifiers.Override :
            // return AccessModifier.;
            // case ICSharpCode.NRefactory.CSharp.Modifiers.Readonly :
            // return AccessModifier.;
            // case ICSharpCode.NRefactory.CSharp.Modifiers.Const :
            // return AccessModifier.;
            // case ICSharpCode.NRefactory.CSharp.Modifiers.New :
            // return AccessModifier.;
            // case ICSharpCode.NRefactory.CSharp.Modifiers.Partial :
            // return AccessModifier.;
            // case ICSharpCode.NRefactory.CSharp.Modifiers.Extern :
            // return AccessModifier.;
            // case ICSharpCode.NRefactory.CSharp.Modifiers.Volatile :
            // return AccessModifier.;
            // case ICSharpCode.NRefactory.CSharp.Modifiers.Unsafe :
            // return AccessModifier.;
            // case ICSharpCode.NRefactory.CSharp.Modifiers.Async :
            // return AccessModifier.;

            //case ICSharpCode.NRefactory.CSharp.Modifiers.ProtectedInternal:
                //return AccessModifier.ProtectedInternal;
            default:
                return AccessModifier.Default;
        }
    }

    /// <summary>
    /// Converts the class modifier of the NRefactory library into the corresponding
    /// NClass class modifier.
    /// </summary>
    /// <param name="classModifier">The class modifier of the NRefactory library to convert.</param>
    /// <returns>The converted NClass class modifier.</returns>
    public static ClassModifier ToNClassFromClass(this ICSharpCode.NRefactory.CSharp.Modifiers classModifier)
    {
        switch (classModifier)
        {
            case ICSharpCode.NRefactory.CSharp.Modifiers.None:
                return ClassModifier.None;
            case ICSharpCode.NRefactory.CSharp.Modifiers.Abstract:
                return ClassModifier.Abstract;
            case ICSharpCode.NRefactory.CSharp.Modifiers.Sealed:
                return ClassModifier.Sealed;
            case ICSharpCode.NRefactory.CSharp.Modifiers.Static:
                return ClassModifier.Static;
            default:
                return ClassModifier.None;
        }
    }

    /// <summary>
    /// Converts the parameter modifier of the NRefactory library into the corresponding
    /// NClass parameter modifier.
    /// </summary>
    /// <param name="parameterModifier">The parameter modifier of the NRefactory library to convert.</param>
    /// <returns>The converted NClass class modifier.</returns>
    public static ParameterModifier ToNClass(this ICSharpCode.NRefactory.CSharp.ParameterModifier parameterModifier)
    {
        switch (parameterModifier)
        {
            case ICSharpCode.NRefactory.CSharp.ParameterModifier.None:
                return ParameterModifier.In;
            case ICSharpCode.NRefactory.CSharp.ParameterModifier.Ref:
                return ParameterModifier.Inout;
            case ICSharpCode.NRefactory.CSharp.ParameterModifier.Out:
                return ParameterModifier.Out;
            case ICSharpCode.NRefactory.CSharp.ParameterModifier.Params:
                return ParameterModifier.Params;
            /*
            // To DO :
            case ICSharpCode.NRefactory.CSharp.ParameterModifier.This:
                return ParameterModifier.this;
            */
            default:
               return ParameterModifier.None;
        }
    }
    #endregion
  }
}
