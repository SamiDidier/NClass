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
using NReflect.NRMembers;

namespace NReflect.NREntities
{
  /// <summary>
  /// Represents a type which can contain fields and methods which is reflected by NReflect.
  /// </summary>
  [Serializable]
  public abstract class NRCompositeType : NRGenericType, IMethodContainer
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="NRCompositeType"/>.
    /// </summary>
    protected NRCompositeType()
    {
      ImplementedInterfaces = new List<NRTypeUsage>();
      Properties = new List<NRProperty>();
      Events = new List<NREvent>();
      Methods = new List<NRMethod>();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets a list which contains the full names of all implemented interfaces.
    /// </summary>
    public List<NRTypeUsage> ImplementedInterfaces { get; private set; }

    /// <summary>
    /// Gets a list of properties of this type.
    /// </summary>
    public List<NRProperty> Properties { get; private set; }

    /// <summary>
    /// Gets a list of events of this type.
    /// </summary>
    public List<NREvent> Events { get; private set; }

    /// <summary>
    /// Gets a list of methods of this type.
    /// </summary>
    public List<NRMethod> Methods { get; private set; }

    #endregion
  }
}