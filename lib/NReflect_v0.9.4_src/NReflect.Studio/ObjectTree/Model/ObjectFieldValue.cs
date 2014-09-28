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

using System.Collections.Generic;

namespace NReflect.Studio.ObjectTree.Model
{
  /// <summary>
  /// An <see cref="ObjectField"/> which is a simple value.
  /// </summary>
  public class ObjectFieldValue : ObjectField
  {
    // ========================================================================
    // Fields

    #region === Fields

     

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectFieldValue"/>.
    /// </summary>
    public ObjectFieldValue()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectFieldValue"/> with
    /// the values of the given <see cref="ObjectFieldValue"/>.
    /// </summary>
    /// <param name="source">The <see cref="ObjectFieldValue"/> to take the values from.</param>
    public ObjectFieldValue(ObjectFieldValue source) : base(source)
    {
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets an enumerator which can be used to go through all child elements of this <see cref="ObjectField"/>.
    /// </summary>
    public override IEnumerable<ObjectField> Childs
    {
      get { return new List<ObjectField>(); }
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>A new object that is a copy of this instance.</returns>
    public override object Clone()
    {
      return new ObjectFieldValue(this);
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}