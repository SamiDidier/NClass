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
using NReflect.NREntities;

namespace NReflect.NRRelationship
{
  /// <summary>
  /// Represents an generalization relationship between two types.
  /// </summary>
  [Serializable]
  public class NRGeneralization
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="NRGeneralization"/>.
    /// </summary>
    public NRGeneralization()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="NRGeneralization"/>.
    /// </summary>
    /// <param name="baseType">The base type of the generalization.</param>
    /// <param name="derivedType">The derived type of the generalization.</param>
    public NRGeneralization(NRCompositeType baseType, NRCompositeType derivedType)
    {
      BaseType = baseType;
      DerivedType = derivedType;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the base type of the generalization.
    /// </summary>
    public NRCompositeType BaseType { get; set; }

    /// <summary>
    /// Gets or sets the derived type of the generalization.
    /// </summary>
    public NRCompositeType DerivedType { get; set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
    /// </returns>
    /// <filterpriority>2</filterpriority>
    public override string ToString()
    {
      return BaseType.Name + " <|-- " + DerivedType.Name;
    }

    #endregion
  }
}