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

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace NReflect.Studio.ObjectTree.Model
{
  /// <summary>
  /// An <see cref="ObjectField"/> which is a list.
  /// </summary>
  public class ObjectFieldList : ObjectFieldCollection
  {
    // ========================================================================
    // Fields

    #region === Fields

     

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectFieldList"/>.
    /// </summary>
    [DebuggerStepThrough]
    public ObjectFieldList()
    {
      Items = new List<ObjectField>();
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectFieldList"/> with the
    /// values of the given <see cref="ObjectFieldList"/>.
    /// </summary>
    /// <param name="source">The <see cref="ObjectFieldList"/> to take the values from.</param>
    public ObjectFieldList(ObjectFieldList source)
      : base(source)
    {
      Items = new List<ObjectField>();
      foreach(ObjectField item in source.Items)
      {
        Items.Add(item.Clone() as ObjectField);
      }
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets a list containing the items of the list.
    /// </summary>
    public List<ObjectField> Items { get; private set; }

    /// <summary>
    /// Gets a value indicating that this property can't be set.
    /// </summary>
    public override bool IsSetable
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// Gets an enumerator which can be used to go through all child elements of this <see cref="ObjectField"/>.
    /// </summary>
    public override IEnumerable<ObjectField> Childs
    {
      get { return Items; }
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
      return new ObjectFieldList(this);
    }

    /// <summary>
    /// Adds a new item to the list.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public override void Add(ObjectField item)
    {
      IList list = Value as IList;
      if(list != null)
      {
        list.Add(item.Value);
        Items.Add(item);
        Count++;
        RefreshItemNames();
        OnItemAdded(item);
      }
    }

    /// <summary>
    /// Removes an item from the list.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    public override void Remove(ObjectField item)
    {
      IList list = Value as IList;
      if (list != null && Items.Contains(item))
      {
        list.Remove(item.Value);
        Items.Remove(item);
        RefreshItemNames();
        Count--;
        OnItemRemoved(item);
      }
    }

    /// <summary>
    /// Inserts a new item at the given index to the list.
    /// </summary>
    /// <param name="index">The index to place the new item.</param>
    /// <param name="item">The item to add.</param>
    public void Insert(int index, ObjectField item)
    {
      IList list = Value as IList;
      if (list != null)
      {
        list.Insert(index, item.Value);
        Items.Insert(index, item);
        Count++;
        RefreshItemNames();
        OnItemAdded(item);
      }
    }

    /// <summary>
    /// Moves an item of the list to another position.
    /// </summary>
    /// <param name="item">The item to move.</param>
    /// <param name="newIndex">The new position inside the list of items.</param>
    public void Move(ObjectField item, int newIndex)
    {
      int oldIndex = Items.IndexOf(item);
      IList list = Value as IList;
      if(list != null && oldIndex >= 0)
      {
        Remove(item);
        if (oldIndex < newIndex)
        {
          list.Insert(newIndex - 1, item.Value);
          Items.Insert(newIndex - 1, item);
        }
        else
        {
          list.Insert(newIndex, item.Value);
          Items.Insert(newIndex, item);
        }
        Count++;  // We removed the old item, so Count was decreased before.
        RefreshItemNames();
        OnItemAdded(item);
      }
    }

    /// <summary>
    /// Updates the names of the items (the numbers).
    /// </summary>
    private void RefreshItemNames()
    {
      for(int i = 1; i <= Items.Count; i++)
      {
        Items[i - 1].Name = "[" + i + "]";
      }
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}