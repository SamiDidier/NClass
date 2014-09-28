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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NReflect
{
  /// <summary>
  /// This class contains some extension methods.
  /// </summary>
  public static class Extensions
  {
    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Gets the methods with the specified <paramref name="name"/> and
    /// <paramref name="parameterTypes"/> of the <see cref="Type"/> <paramref name="type"/>
    /// regardless if the methods are generic or not.
    /// </summary>
    /// <param name="type">The type to get the methods from.</param>
    /// <param name="name">The name of the methods to get.</param>
    /// <param name="parameterTypes">The parameters of the method.</param>
    /// <returns>The <see cref="MethodInfo"/>s of the found methods.</returns>
    public static IEnumerable<MethodInfo> GetMethods(this Type type, string name, params Type[] parameterTypes)
    {
      return (from method in type.GetMethods()
              where method.Name == name
              where parameterTypes.SequenceEqual(method.GetParameters().Select(p => p.ParameterType))
              select method);
    }

    #endregion
  }
}