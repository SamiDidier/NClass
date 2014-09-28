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
  /// This enumeration contains all possible class modifiers.
  /// </summary>
  public enum ClassModifier
  {
    /// <summary>
    /// None of the other modifiers is set.
    /// </summary>
    None,

    /// <summary>
    /// Indicates that a class is intended only to be a base class of other classes.
    /// </summary>
    Abstract,

    /// <summary>
    /// Specifies that a class cannot be inherited.
    /// </summary>
    Sealed,

    /// <summary>
    /// Indicates that a class contains only static members.
    /// </summary>
    Static
  }
}