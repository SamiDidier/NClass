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
  public class NewValueEventArgs : ObjectFieldEventArgs
  {
    // ========================================================================
    // Fields

    #region === Fields

     

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="NewValueEventArgs"/>.
    /// </summary>
    /// <param name="field">The object field.</param>
    [DebuggerStepThrough]
    public NewValueEventArgs(ObjectField field)
      : base(field)
    {
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the value of the field.
    /// </summary>
    public object Value { get; set; }

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