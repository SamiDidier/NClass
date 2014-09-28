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
  /// The result of the comparison.
  /// </summary>
  public enum ResultState
  {
    /// <summary>
    /// The compared fields are equal.
    /// </summary>
    Equal,
    /// <summary>
    /// The compared fields do not match.
    /// </summary>
    NotEqual,
    /// <summary>
    /// The field is missing.
    /// </summary>
    Missing,
    /// <summary>
    /// The field is new.
    /// </summary>
    New,
    /// <summary>
    /// The field is not known.
    /// </summary>
    Unknown,
    /// <summary>
    /// The field was ignored.
    /// </summary>
    Ignored
  }
}