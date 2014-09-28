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
  /// An <see cref="ObjectField"/> which can contain children.
  /// </summary>
  public class ObjectFieldClass : ObjectField
  {
    // ========================================================================
    // Fields

    #region === Fields

     

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectFieldClass"/>.
    /// </summary>
    public ObjectFieldClass()
    {
      ChildFields = new List<ObjectField>();
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectFieldClass"/> with
    /// the values of the given <see cref="ObjectFieldClass"/>.
    /// </summary>
    /// <param name="source">The <see cref="ObjectFieldClass"/> to take the values from.</param>
    public ObjectFieldClass(ObjectFieldClass source)
      : base(source)
    {
      ChildFields = new List<ObjectField>();
      foreach(ObjectField childField in source.ChildFields)
      {
        ChildFields.Add(childField.Clone() as ObjectField);
      }
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets a list containing the child fields.
    /// </summary>
    public List<ObjectField> ChildFields { get; private set; }

    /// <summary>
    /// Gets an enumerator which can be used to go through all child elements of this <see cref="ObjectField"/>.
    /// </summary>
    public override IEnumerable<ObjectField> Childs
    {
      get { return ChildFields; }
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
      return new ObjectFieldClass(this);
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}