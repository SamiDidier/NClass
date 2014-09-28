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
  /// Represents an realization relationship between two types.
  /// </summary>
  [Serializable]
  public class NRRealization
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="NRRealization"/>.
    /// </summary>
    public NRRealization()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="NRRealization"/>.
    /// </summary>
    /// <param name="baseType">The base type of the generalization.</param>
    /// <param name="implementingType">The implementing type of the realization.</param>
    public NRRealization(NRInterface baseType, NRSingleInheritanceType implementingType)
    {
      BaseType = baseType;
      ImplementingType = implementingType;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the base type of the generalization.
    /// </summary>
    public NRInterface BaseType { get; set; }

    /// <summary>
    /// Gets or sets the derived type of the generalization.
    /// </summary>
    public NRSingleInheritanceType ImplementingType { get; set; }

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
      return BaseType.Name + " < - " + ImplementingType.Name;
    }

    #endregion
  }
}