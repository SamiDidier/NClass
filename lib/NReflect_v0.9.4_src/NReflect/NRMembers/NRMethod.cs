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
using NReflect.NRParameters;

namespace NReflect.NRMembers
{
  /// <summary>
  /// Represents a method of a type which is reflected by NReflect.
  /// </summary>
  [Serializable]
  public class NRMethod : NRReturnValueOperation, IGeneric
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="NRMethod"/>.
    /// </summary>
    public NRMethod()
    {
      GenericTypes = new List<NRTypeParameter>();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets a value indicating if the method is a 
    /// </summary>
    public bool IsExtensionMethod { get; set; }

    /// <summary>
    /// Gets a list containing all type parameters of a type.
    /// </summary>
    public List<NRTypeParameter> GenericTypes { get; private set; }

    /// <summary>
    /// Gets a value indicating wether this type is a generic.
    /// </summary>
    public bool IsGeneric
    {
      get { return GenericTypes.Count > 0; }
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Accept an <see cref="IVisitor"/> instance on the implementing class and all its children.
    /// </summary>
    /// <param name="visitor">The <see cref="IVisitor"/> instance to accept.</param>
    public override void Accept(IVisitor visitor)
    {
      visitor.Visit(this);
    }

    #endregion
  }
}