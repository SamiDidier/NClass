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
  /// Represents an association relationship between two types.
  /// </summary>
  [Serializable]
  public class NRAssociation
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="NRAssociation"/>.
    /// </summary>
    public NRAssociation()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="NRAssociation"/>.
    /// </summary>
    /// <param name="startType">The start type of the assotiation.</param>
    /// <param name="endType">The end type of the assotiation.</param>
    public NRAssociation(NRTypeBase startType, NRTypeBase endType)
    {
      StartType = startType;
      EndType = endType;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the start type of the assotiation.
    /// </summary>
    public NRTypeBase StartType { get; set; }

    /// <summary>
    /// Gets or sets the end type of the assotiation.
    /// </summary>
    public NRTypeBase EndType { get; set; }

    /// <summary>
    /// Gets or sets the start role.
    /// </summary>
    public string StartRole { get; set; }

    /// <summary>
    /// Gets or sets the end role.
    /// </summary>
    public string EndRole { get; set; }

    /// <summary>
    /// Gets or sets the start multiplicity.
    /// </summary>
    public string StartMultiplicity { get; set; }

    /// <summary>
    /// Gets or sets the end multiplicity.
    /// </summary>
    public string EndMultiplicity { get; set; }

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
      string startRole = "";
      string endRole = "";
      if (!String.IsNullOrWhiteSpace(StartRole))
      {
        startRole = " (" + StartRole + ")";
      }
      if (!String.IsNullOrWhiteSpace(EndRole))
      {
        endRole = " (" + EndRole + ")";
      }
      string startMultiplicity = " ";
      string endMultiplicity = " ";
      if (!String.IsNullOrWhiteSpace(StartMultiplicity))
      {
        startMultiplicity = " " + StartMultiplicity;
      }
      if (!String.IsNullOrWhiteSpace(EndMultiplicity))
      {
        endMultiplicity = EndMultiplicity + " ";
      }
      return StartType.Name + startRole + startMultiplicity + "---" + endMultiplicity + endRole + EndType.Name;
    }

    #endregion
  }
}