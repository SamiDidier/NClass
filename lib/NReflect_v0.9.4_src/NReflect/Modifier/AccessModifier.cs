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
  /// This enumeration contains all possible access modifiers.
  /// </summary>
  public enum AccessModifier
  {
    /// <summary>
    /// The default access in the context of the element's scope.
    /// </summary>
    Default = 0,

    /// <summary>
    /// Access is not restricted.
    /// </summary>
    Public = 1,

    /// <summary>
    /// Access is limited to the current assembly or types 
    /// derived from the containing class.
    /// </summary>
    ProtectedInternal = 2,

    /// <summary>
    /// Access is limited to the current assembly.
    /// </summary>
    Internal = 3,

    /// <summary>
    /// Access is limited to the containing class or types 
    /// derived from the containing class.
    /// </summary>
    Protected = 4,

    /// <summary>
    /// Access is limited to the containing type.
    /// </summary>
    Private = 5
  }
}