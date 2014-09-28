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

namespace NReflect.Studio.ObjectTree.Comparer
{
  /// <summary>
  /// Represents a comparation result of one field.
  /// </summary>
  public class CompareResult
  {
    // ========================================================================
    // Fields

    #region === Fields

     

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="CompareResult"/>.
    /// </summary>
    /// <param name="state">The result state.</param>
    /// <param name="percent">The percentage of equal fields.</param>
    public CompareResult(ResultState state, double percent)
    {
      State = state;
      Percent = percent;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets the result state.
    /// </summary>
    public ResultState State { get; private set; }
    /// <summary>
    /// Gets the percentage of equal fields.
    /// </summary>
    public double Percent { get; private set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods


    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}