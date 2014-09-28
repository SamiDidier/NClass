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

namespace NReflect.Filter
{
  /// <summary>
  /// A combination of a modifier and an element.
  /// </summary>
  [Serializable]
  public struct FilterRule
  {
    #region === Construction

    /// <summary>
    /// Creates a new FilterRule with the given values.
    /// </summary>
    /// <param name="modifier">The Modifier for this rule.</param>
    /// <param name="element">The element for this rule</param>
    public FilterRule(FilterModifiers modifier, FilterElements element)
      : this()
    {
      Element = element;
      Modifier = modifier;
    }

    #endregion

    #region === Properties

    /// <summary>
    /// Gets or sets the modifier for this rule.
    /// </summary>
    public FilterModifiers Modifier { get; set; }

    /// <summary>
    /// Gets or sets the element for this rule.
    /// </summary>
    public FilterElements Element { get; set; }

    #endregion
  }
}