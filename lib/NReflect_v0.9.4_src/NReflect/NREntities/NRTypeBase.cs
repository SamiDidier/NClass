// NReflect - Easy assembly reflection
// Copyright (C) 2010-2013 Malte Ried
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
using NReflect.Modifier;
using NReflect.NRAttributes;

namespace NReflect.NREntities
{
  /// <summary>
  /// Represents the base of all types which are reflected by NReflect.
  /// </summary>
  [Serializable]
  public abstract class NRTypeBase : IVisitable, IAttributable
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="NRTypeBase"/>.
    /// </summary>
    protected NRTypeBase()
    {
      Attributes = new List<NRAttribute>();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the name of this type.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the namespace of this type.
    /// </summary>
    public string Namespace { get; set; }

    /// <summary>
    /// Gets or sets the full name of this type.
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Gets or sets the access modifier for this type.
    /// </summary>
    public AccessModifier AccessModifier { get; set; }

    /// <summary>
    /// Gets or sets the full full name of the type in which this type is nested.
    /// </summary>
    public string DeclaringTypeFullName { get; set; }

    /// <summary>
    /// Gets a list of attributes of the type.
    /// </summary>
    public List<NRAttribute> Attributes { get; private set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Accept an <see cref="IVisitor"/> instance on the implementing class and all its children.
    /// </summary>
    /// <param name="visitor">The <see cref="IVisitor"/> instance to accept.</param>
    public abstract void Accept(IVisitor visitor);

    #endregion
  }
}