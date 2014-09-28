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
using NReflect.NRAttributes;

namespace NReflect.NRParameters
{
  /// <summary>
  /// Represents a type parameter which is reflected by NReflect.
  /// </summary>
  [Serializable]
  public class NRTypeParameter : IVisitable, IAttributable
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="NRTypeParameter"/>.
    /// </summary>
    public NRTypeParameter()
    {
      Attributes = new List<NRAttribute>();
      BaseTypes = new List<NRTypeUsage>();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the name of the type parameter.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets a list of types representing the constraints of the type parameter.
    /// </summary>
    public List<NRTypeUsage> BaseTypes { get; private set; }

    /// <summary>
    /// Gets or sets a value that indicates wether the generic parameter has a class constraint.
    /// </summary>
    public bool IsClass { get; set; }

    /// <summary>
    /// Gets or sets a value that indicates wether the generic parameter has a struct constraint.
    /// </summary>
    public bool IsStruct { get; set; }

    /// <summary>
    /// Gets or sets a value that indicates wether the generic parameter has a default constructor constraint.
    /// </summary>
    public bool IsConstructor { get; set; }

    /// <summary>
    /// Gets or sets a value that indicates wether the generic parameter has a contravariant constraint.
    /// </summary>
    public bool IsIn { get; set; }

    /// <summary>
    /// Gets or sets a value that indicates wether the generic parameter has a covariant constraint.
    /// </summary>
    public bool IsOut { get; set; }

    /// <summary>
    /// Gets a list of attributes of the type parameter.
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
    public void Accept(IVisitor visitor)
    {
      visitor.Visit(this);
    }

    #endregion
  }
}