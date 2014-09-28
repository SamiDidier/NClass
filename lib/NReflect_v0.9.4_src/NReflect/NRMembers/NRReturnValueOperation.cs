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

using System;
using System.Collections.Generic;
using NReflect.NRAttributes;

namespace NReflect.NRMembers
{
  /// <summary>
  /// Represents an operation which has a return value of a type which is reflected by NReflect.
  /// </summary>
  [Serializable]
  public abstract class NRReturnValueOperation : NROperation
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="NRReturnValueOperation"/>.
    /// </summary>
    protected NRReturnValueOperation()
    {
      ReturnValueAttributes = new List<NRAttribute>();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets a list of attributes bound to the return value of the method.
    /// </summary>
    public List<NRAttribute> ReturnValueAttributes { get; private set; } 

    #endregion
  }
}