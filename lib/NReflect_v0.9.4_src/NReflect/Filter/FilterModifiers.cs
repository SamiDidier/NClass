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
  /// A collection of possible modifiers.
  /// </summary>
  [Flags]
  public enum FilterModifiers
  {
    /// <summary>
    /// Means "all modifiers"
    /// </summary>
    AllModifiers = 0,
    /// <summary>
    /// Modifier private
    /// </summary>
    Private = 1,
    /// <summary>
    /// Modifier public
    /// </summary>
    Public = 2,
    /// <summary>
    /// Modifier protected
    /// </summary>
    Protected = 4,
    /// <summary>
    /// Modifier internal
    /// </summary>
    Internal = 8,
    /// <summary>
    /// Modifier protected internal
    /// </summary>
    ProtectedInternal = 16,
    /// <summary>
    /// static elements
    /// </summary>
    Static = 32,
    /// <summary>
    /// elements which are not static
    /// </summary>
    Instance = 64,
    /// <summary>
    /// The default access modifier.
    /// </summary>
    Default =  128
  }
}