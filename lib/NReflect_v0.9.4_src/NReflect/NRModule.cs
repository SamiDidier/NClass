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
using NReflect.NRMembers;

namespace NReflect
{
  /// <summary>
  /// Contains the reflection results of a module.
  /// </summary>
  [Serializable]
  public class NRModule : IVisitable, IEntityContainer, IAttributable, IFieldContainer, IMethodContainer
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="NRModule"/>.
    /// </summary>
    public NRModule()
    {
      Attributes = new List<NRAttribute>();
      Classes = new List<NRClass>();
      Interfaces = new List<NRInterface>();
      Structs = new List<NRStruct>();
      Delegates = new List<NRDelegate>();
      Enums = new List<NREnum>();
      Fields = new List<NRField>();
      Methods = new List<NRMethod>();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the name of the module.
    /// </summary>
    public string Name { get; set; }

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
    /// Gets a list of fields of this type.
    /// </summary>
    public List<NRField> Fields { get; private set; }

    /// <summary>
    /// Gets a list of methods of this type.
    /// </summary>
    public List<NRMethod> Methods { get; private set; }

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