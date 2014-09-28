// NReflect - Easy assembly reflection
// Copyright (C) 2010-2011 Malte Ried
//
// This file is part of NReflect.
//
// NReflect is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// NReflect is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with NReflect. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using NReflect.NRAttributes;
using NReflect.NREntities;

namespace NReflect
{
  /// <summary>
  /// Contains the reflection results of an assembly.
  /// </summary>
  [Serializable]
  public class NRAssembly : IVisitable, IAttributable, IEntityContainer
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="NRAssembly"/>.
    /// </summary>
    public NRAssembly()
    {
      Classes = new List<NRClass>();
      Interfaces = new List<NRInterface>();
      Structs = new List<NRStruct>();
      Enums = new List<NREnum>();
      Delegates = new List<NRDelegate>();
      Attributes = new List<NRAttribute>();
      Modules = new List<NRModule>();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// The full name of the assembly.
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// The source of the assembly. Possibly a file name.
    /// </summary>
    public string Source { get; set; }

    /// <summary>
    /// Gets a list of reflected modules.
    /// </summary>
    public List<NRModule> Modules { get; private set; }

    /// <summary>
    /// Gets a list of attributes.
    /// </summary>
    public List<NRAttribute> Attributes { get; private set; }

    /// <summary>
    /// Gets a list of reflected classes.
    /// </summary>
    public List<NRClass> Classes { get; private set; }

    /// <summary>
    /// Gets a list of reflected interfaces.
    /// </summary>
    public List<NRInterface> Interfaces { get; private set; }

    /// <summary>
    /// Gets a list of reflected structs.
    /// </summary>
    public List<NRStruct> Structs { get; private set; }

    /// <summary>
    /// Gets a list of reflected enums.
    /// </summary>
    public List<NREnum> Enums { get; private set; }

    /// <summary>
    /// Gets a list of reflected delegates.
    /// </summary>
    public List<NRDelegate> Delegates { get; private set; }

    /// <summary>
    /// Gets an enumerator to access all types of the assembly in the following order:
    /// interfaces, classes, structs, enums and delegates.
    /// </summary>
    /// <returns>An enumerator to go through all entities.</returns>
    public IEnumerable<NRTypeBase> Types
    {
      get
      {
        foreach (NRCompositeType nrCompositeType in CompositTypes)
        {
          yield return nrCompositeType;
        }
        foreach (NREnum nrEnum in Enums)
        {
          yield return nrEnum;
        }
        foreach (NRDelegate nrDelegate in Delegates)
        {
          yield return nrDelegate;
        }
      }
    }

    /// <summary>
    /// Gets an enumerator to access all composit types of the assembly in the following order:
    /// interfaces, classes, structs.
    /// </summary>
    /// <returns>An enumerator to go through all entities.</returns>
    public IEnumerable<NRCompositeType> CompositTypes
    {
      get
      {
        foreach (NRInterface nrInterface in Interfaces)
        {
          yield return nrInterface;
        }
        foreach (NRSingleInheritanceType nrSingleInheritanceType in SingleInheritanceTypes)
        {
          yield return nrSingleInheritanceType;
        }
      }
    }

    /// <summary>
    /// Gets an enumerator to access all single inheritance types of the assembly in the following order:
    /// classes, structs.
    /// </summary>
    /// <returns>An enumerator to go through all entities.</returns>
    public IEnumerable<NRSingleInheritanceType> SingleInheritanceTypes
    {
      get
      {
        foreach (NRClass nrClass in Classes)
        {
          yield return nrClass;
        }
        foreach (NRStruct nrStruct in Structs)
        {
          yield return nrStruct;
        }
      }
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Accept an <see cref="IVisitor"/> instance on the implementing class and all its children.
    /// </summary>
    /// <param name="visitor">The <see cref="IVisitor"/> instance to accept.</param>
    public void Accept(IVisitor visitor)
    {
      visitor.Visit(this);
    }

    #endregion
  }
}