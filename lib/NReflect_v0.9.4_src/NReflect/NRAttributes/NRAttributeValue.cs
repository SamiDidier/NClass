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

namespace NReflect.NRAttributes
{
  /// <summary>
  /// Represents a value used to initialize an attribute which is reflected by NReflect.
  /// </summary>
  [Serializable]
  public class NRAttributeValue
  {
    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the name of the type of the attribute value.
    /// </summary>
    /// <remarks>
    /// The name is prefixed by the namespace of the type.
    /// </remarks>
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the value of the attribute value.
    /// </summary>
    /// <remarks>
    /// If the type of the attribute value is a <see cref="System.Type"/>,
    /// this property will hold a <see cref="string"/> instead of the
    /// <see cref="System.Type"/>. The reason to do this is the fact that
    /// a <see cref="System.Type"/> is not serializable. All other types
    /// which can occur as an attribute value are serializable since they
    /// are of any number, string, boolean or enum type.
    /// </remarks>
    public object Value { get; set; }

    #endregion
  }
}