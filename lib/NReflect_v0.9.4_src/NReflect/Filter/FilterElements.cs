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

namespace NReflect.Filter
{
  /// <summary>
  /// A collection of possible elements.
  /// </summary>
  public enum FilterElements
  {
    /// <summary>
    /// Means "all ellements"
    /// </summary>
    AllElements,
    /// <summary>
    /// Type class
    /// </summary>
    Class,
    /// <summary>
    /// Type struct
    /// </summary>
    Struct,
    /// <summary>
    /// Type interface
    /// </summary>
    Interface,
    /// <summary>
    /// Type enum
    /// </summary>
    Enum,
    /// <summary>
    /// Member enum value
    /// </summary>
    EnumValue,
    /// <summary>
    /// Type delegate
    /// </summary>
    Delegate,
    /// <summary>
    /// Member field
    /// </summary>
    Field,
    /// <summary>
    /// Member constant
    /// </summary>
    Constant,
    /// <summary>
    /// Member property
    /// </summary>
    Property,
    /// <summary>
    /// Member constructor
    /// </summary>
    Constructor,
    /// <summary>
    /// Member method
    /// </summary>
    Method,
    /// <summary>
    /// Member operator
    /// </summary>
    Operator,
    /// <summary>
    /// Member event
    /// </summary>
    Event,
    /// <summary>
    /// Attribute
    /// </summary>
    Attribute,
    /// <summary>
    /// Module
    /// </summary>
    Module
  }
}