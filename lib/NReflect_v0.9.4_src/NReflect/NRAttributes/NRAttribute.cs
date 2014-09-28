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

namespace NReflect.NRAttributes
{
  /// <summary>
  /// Represents an attribute which is reflected by NReflect.
  /// </summary>
  [Serializable]
  public class NRAttribute : IVisitable
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="NRAttribute"/>.
    /// </summary>
    public NRAttribute()
    {
      Values = new List<NRAttributeValue>();
      NamedValues = new Dictionary<string, NRAttributeValue>();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the name of the attribute.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the namespace of the attribute.
    /// </summary>
    public string Namespace { get; set; }

    /// <summary>
    /// Gets a list containing all positional values used to create the attribute.
    /// </summary>
    public List<NRAttributeValue> Values { get; private set; }

    /// <summary>
    /// Gets a list containing all named values used to create the attribute.
    /// </summary>
    public Dictionary<string, NRAttributeValue> NamedValues { get; private set; }

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