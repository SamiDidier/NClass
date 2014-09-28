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

namespace NReflect
{
  /// <summary>
  /// Contains a reflected typeUsage.
  /// </summary>
  [Serializable]
  public class NRTypeUsage : IVisitable
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="NRTypeUsage"/>.
    /// </summary>
    public NRTypeUsage()
    {
      GenericParameters = new List<NRTypeUsage>();
      ArrayRanks = new List<int>();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the name of the type.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Gets or sets the namespace of the used type.
    /// </summary>
    public string Namespace { get; set; }
    /// <summary>
    /// Gets or sets the full name of the used type.
    /// </summary>
    public string FullName { get; set; }
    /// <summary>
    /// Gets or sets the type which declares this type.
    /// </summary>
    public NRTypeUsage DeclaringType { get; set; }
    /// <summary>
    /// Gets a list of generic parameters of the type.
    /// </summary>
    public List<NRTypeUsage> GenericParameters { get; private set; }
    /// <summary>
    /// Gets a value indicating if the type is an array.
    /// </summary>
    public bool IsArray
    {
      get { return ArrayRanks.Count > 0; }
    }
    /// <summary>
    /// Gets a list of array ranks of the type.
    /// </summary>
    public List<int> ArrayRanks { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether the type is dynamic.
    /// </summary>
    public bool IsDynamic { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the type is nullable.
    /// </summary>
    public bool IsNullable { get; set; }
    /// <summary>
    /// Gets a value indicating if the type is a generic type.
    /// </summary>
    public bool IsGeneric
    {
      get { return GenericParameters.Count > 0; }
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