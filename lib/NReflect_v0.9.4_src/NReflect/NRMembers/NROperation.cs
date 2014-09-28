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
using NReflect.Modifier;
using NReflect.NREntities;
using NReflect.NRParameters;

namespace NReflect.NRMembers
{
  /// <summary>
  /// Represents an operation of a type which is reflected by NReflect.
  /// </summary>
  [Serializable]
  public abstract class NROperation : NRMember
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="NROperation"/>.
    /// </summary>
    protected NROperation()
    {
      Parameters = new List<NRParameter>();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the operation modifier for this operation.
    /// </summary>
    public OperationModifier OperationModifier { get; set; }

    /// <summary>
    /// Gets or sets if the operation is static.
    /// </summary>
    public bool IsStatic
    {
      get { return (OperationModifier & OperationModifier.Static) != 0; }
      set
      {
        if (value)
        {
          OperationModifier |= OperationModifier.Static;
        }
        else
        {
          OperationModifier &= ~OperationModifier.Static;
        }
      }
    }

    /// <summary>
    /// Gets or sets if the operation is abstract.
    /// </summary>
    public bool IsAbstract
    {
      get { return (OperationModifier & OperationModifier.Abstract) != 0; }
      set
      {
        if (value)
        {
          OperationModifier |= OperationModifier.Abstract;
        }
        else
        {
          OperationModifier &= ~OperationModifier.Abstract;
        }
      }
    }

    /// <summary>
    /// Gets or sets if the operation is virtual.
    /// </summary>
    public bool IsVirtual
    {
      get { return (OperationModifier & OperationModifier.Virtual) != 0; }
      set
      {
        if (value)
        {
          OperationModifier |= OperationModifier.Virtual;
        }
        else
        {
          OperationModifier &= ~OperationModifier.Virtual;
        }
      }
    }

    /// <summary>
    /// Gets or sets if the operation is overriding another operation.
    /// </summary>
    public bool IsOverride
    {
      get { return (OperationModifier & OperationModifier.Override) != 0; }
      set
      {
        if (value)
        {
          OperationModifier |= OperationModifier.Override;
        }
        else
        {
          OperationModifier &= ~OperationModifier.Override;
        }
      }
    }

    /// <summary>
    /// Gets or sets if the operation is hiding another operation.
    /// </summary>
    public bool IsHider
    {
      get { return (OperationModifier & OperationModifier.Hider) != 0; }
      set
      {
        if (value)
        {
          OperationModifier |= OperationModifier.Hider;
        }
        else
        {
          OperationModifier &= ~OperationModifier.Hider;
        }
      }
    }

    /// <summary>
    /// Gets or sets if the operation is sealed.
    /// </summary>
    public bool IsSealed
    {
      get { return (OperationModifier & OperationModifier.Sealed) != 0; }
      set
      {
        if (value)
        {
          OperationModifier |= OperationModifier.Sealed;
        }
        else
        {
          OperationModifier &= ~OperationModifier.Sealed;
        }
      }
    }

    /// <summary>
    /// Gets a list of parameters of this operation.
    /// </summary>
    public List<NRParameter> Parameters { get; private set; }

    #endregion
  }
}