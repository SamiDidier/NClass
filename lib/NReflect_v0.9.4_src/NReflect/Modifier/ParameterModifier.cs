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

namespace NReflect.Modifier
{
  /// <summary>
  /// This enumeration contains all possible modifiers for a parameter.
  /// </summary>
  public enum ParameterModifier
  {
    /// <summary>
    /// The parameter is an in parameter.
    /// </summary>
    In,
    /// <summary>
    /// The parameter is an in/out parameter.
    /// </summary>
    InOut,
    /// <summary>
    /// The parameter is an out parameter.
    /// </summary>
    Out,
    /// <summary>
    /// The parameter is the parameter containing a params-array.
    /// </summary>
    Params,
    /// <summary>
    /// The parameter is optional.
    /// </summary>
    Optional
  }
}