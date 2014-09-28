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
using System.Globalization;

namespace NReflect.Studio.ObjectTree.Model
{
  /// <summary>
  /// An <see cref="ObjectField"/> which is a collection.
  /// </summary>
  public abstract class ObjectFieldCollection : ObjectField
  {
    // ========================================================================
    // Fields

    #region === Fields

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectFieldCollection"/>.
    /// </summary>
    protected ObjectFieldCollection()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectFieldCollection"/> with the
    /// values of the given <see cref="ObjectFieldCollection"/>.
    /// </summary>
    /// <param name="source">The <see cref="ObjectFieldCollection"/> to take the values from.</param>
    protected ObjectFieldCollection(ObjectFieldCollection source) : base(source)
    {
      Count = source.Count;
      ItemType = source.ItemType;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the count of elementes inside the collection.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Gets or sets the type of the items of the collection.
    /// </summary>
    public Type ItemType { get; set; }

    /// <summary>
    /// Gets or sets a string representation of the value.
    /// </summary>
    public override string ValueText
    {
      get
      {
        return valueText ?? Count.ToString(CultureInfo.InvariantCulture);
      }
      set
      {
        base.ValueText = value;
      }
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Adds a new item to the collection.
    /// </summary>
    /// <param name="item">The item itself.</param>
    public abstract void Add(ObjectField item);

    /// <summary>
    /// Removes an item from the collection.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    public abstract void Remove(ObjectField item);

    #region --- OnXXX

    /// <summary>
    /// Fires the <see cref="ItemAdded"/> event.
    /// </summary>
    /// <param name="item">The added item.</param>
    protected void OnItemAdded(ObjectField item)
    {
      if (ItemAdded != null)
      {
        ItemAdded(this, new ObjectFieldEventArgs(item));
      }
    }

    /// <summary>
    /// Fires the <see cref="ItemRemoved"/> event.
    /// </summary>
    /// <param name="item">The removed item.</param>
    protected void OnItemRemoved(ObjectField item)
    {
      if (ItemRemoved != null)
      {
        ItemRemoved(this, new ObjectFieldEventArgs(item));
      }
    }

    #endregion

    #endregion

    // ========================================================================
    // Events

    #region === Events

    /// <summary>
    /// Occures after a new item was added.
    /// </summary>
    public event EventHandler<ObjectFieldEventArgs> ItemAdded;
    /// <summary>
    /// Occures after an item was removed.
    /// </summary>
    public event EventHandler<ObjectFieldEventArgs> ItemRemoved;

    #endregion
  }
}