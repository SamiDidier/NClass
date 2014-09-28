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
using NReflect.Modifier;

namespace NReflect.NRMembers
{
  /// <summary>
  /// Represents a field of a type which is reflected by NReflect.
  /// </summary>
  [Serializable]
  public class NRField : NRMember
  {
    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the initial value of this field.
    /// </summary>
    public string InitialValue { get; set; }

    /// <summary>
    /// Gets or sets the field modifier for this field.
    /// </summary>
    public FieldModifier FieldModifier { get; set; }

    /// <summary>
    /// Gets or sets if the field is static.
    /// </summary>
    public bool IsStatic
    {
      get { return (FieldModifier & FieldModifier.Static) != 0; }
      set
      {
        if (value)
        {
          FieldModifier |= FieldModifier.Static;
        }
        else
        {
          FieldModifier &= ~FieldModifier.Static;
        }
      }
    }

    /// <summary>
    /// Gets or sets if the field is readonly.
    /// </summary>
    public bool IsReadonly
    {
      get { return (FieldModifier & FieldModifier.Readonly) != 0; }
      set
      {
        if (value)
        {
          FieldModifier |= FieldModifier.Readonly;
        }
        else
        {
          FieldModifier &= ~FieldModifier.Readonly;
        }
      }
    }

    /// <summary>
    /// Gets or sets if the field is a constant.
    /// </summary>
    public bool IsConstant
    {
      get { return (FieldModifier & FieldModifier.Constant) != 0; }
      set
      {
        if (value)
        {
          FieldModifier |= FieldModifier.Constant;
        }
        else
        {
          FieldModifier &= ~FieldModifier.Constant;
        }
      }
    }

    /// <summary>
    /// Gets or sets if the field is a hider.
    /// </summary>
    public bool IsHider
    {
      get { return (FieldModifier & FieldModifier.Hider) != 0; }
      set
      {
        if (value)
        {
          FieldModifier |= FieldModifier.Hider;
        }
        else
        {
          FieldModifier &= ~FieldModifier.Hider;
        }
      }
    }

    /// <summary>
    /// Gets or sets if the field is volatile.
    /// </summary>
    public bool IsVolatile
    {
      get { return (FieldModifier & FieldModifier.Volatile) != 0; }
      set
      {
        if (value)
        {
          FieldModifier |= FieldModifier.Volatile;
        }
        else
        {
          FieldModifier &= ~FieldModifier.Volatile;
        }
      }
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Accept an <see cref="IVisitor"/> instance on the implementing class and all its children.
    /// </summary>
    /// <param name="visitor">The <see cref="IVisitor"/> instance to accept.</param>
    public override void Accept(IVisitor visitor)
    {
      visitor.Visit(this);
    }

    #endregion
  }
}