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

using System.Diagnostics;
using NReflect.Studio.ObjectTree.Model;

namespace NReflect.Studio.ObjectTree
{
  public class ValueChangedEventArgs : ObjectFieldEventArgs
  {
    // ========================================================================
    // Fields

    #region === Fields

     

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="ValueChangedEventArgs"/>.
    /// </summary>
    /// <param name="oldValue">The old value of the field.</param>
    /// <param name="newValue">The new value of the field.</param>
    /// <param name="field">The changed field.</param>
    [DebuggerStepThrough]
    public ValueChangedEventArgs(object oldValue, object newValue, ObjectField field)
      : base(field)
    {
      OldValue = oldValue;
      NewValue = newValue;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets the old value of the field.
    /// </summary>
    public object OldValue { get; private set; }
    /// <summary>
    /// Gets the new value of the field.
    /// </summary>
    public object NewValue { get; private set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods


    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}