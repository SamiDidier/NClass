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

namespace NReflect.NRMembers
{
  /// <summary>
  /// Enumerates the types of operators.
  /// </summary>
  public enum OperatorType
  {
    /// <summary>
    /// The unaryplus operator.
    /// </summary>
    UnaryPlus,
    /// <summary>
    /// The unary negation operator.
    /// </summary>
    UnaryNegation,
    /// <summary>
    /// The logical not operator.
    /// </summary>
    LogicalNot,
    /// <summary>
    /// The ones complement operator.
    /// </summary>
    OnesComplement,
    /// <summary>
    /// The increment operator.
    /// </summary>
    Increment,
    /// <summary>
    /// The decrement operator.
    /// </summary>
    Decrement,
    /// <summary>
    /// The true operator.
    /// </summary>
    True,
    /// <summary>
    /// The false operator.
    /// </summary>
    False,
    /// <summary>
    /// The addition operator.
    /// </summary>
    Addition,
    /// <summary>
    /// The subtraction operator.
    /// </summary>
    Subtraction,
    /// <summary>
    /// The multiply operator.
    /// </summary>
    Multiply,
    /// <summary>
    /// The division operator.
    /// </summary>
    Division,
    /// <summary>
    /// The modulus operator.
    /// </summary>
    Modulus,
    /// <summary>
    /// The bitwise and operator.
    /// </summary>
    BitwiseAnd,
    /// <summary>
    /// The bitwise or operator.
    /// </summary>
    BitwiseOr,
    /// <summary>
    /// The exclusive or operator.
    /// </summary>
    ExclusiveOr,
    /// <summary>
    /// The left shift operator.
    /// </summary>
    LeftShift,
    /// <summary>
    /// The right shift operator.
    /// </summary>
    RightShift,
    /// <summary>
    /// The equality operator.
    /// </summary>
    Equality,
    /// <summary>
    /// The inequality operator.
    /// </summary>
    Inequality,
    /// <summary>
    /// The less than operator.
    /// </summary>
    LessThan,
    /// <summary>
    /// The greater than operator.
    /// </summary>
    GreaterThan,
    /// <summary>
    /// The less than or equal operator.
    /// </summary>
    LessThanOrEqual,
    /// <summary>
    /// The greater than or equal operator.
    /// </summary>
    GreaterThanOrEqual,
    /// <summary>
    /// The implicit conversion operator.
    /// </summary>
    Implicit,
    /// <summary>
    /// The explicit conversion operator.
    /// </summary>
    Explicit,
    /// <summary>
    /// The type of the operator is unknown.
    /// </summary>
    Unknown = -1,
  }
}