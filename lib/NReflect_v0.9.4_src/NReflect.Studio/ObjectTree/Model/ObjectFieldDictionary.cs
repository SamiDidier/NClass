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
using System.Collections;
using System.Collections.Generic;

namespace NReflect.Studio.ObjectTree.Model
{
  /// <summary>
  /// An <see cref="ObjectField"/> which is a dictionary.
  /// </summary>
  public class ObjectFieldDictionary : ObjectFieldCollection
  {
    // ========================================================================
    // Fields

    #region === Fields

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectFieldDictionary"/>.
    /// </summary>
    public ObjectFieldDictionary()
    {
      Items = new Dictionary<object, ObjectField>();
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectFieldDictionary"/> with the
    /// values of the given <see cref="ObjectFieldDictionary"/>.
    /// </summary>
    /// <param name="source">The <see cref="ObjectFieldDictionary"/> to take the values from.</param>
    private ObjectFieldDictionary(ObjectFieldDictionary source)
      : base(source)
    {
      Items = new Dictionary<object, ObjectField>();
      foreach(object key in source.Items.Keys)
      {
        Items.Add(key, source.Items[key].Clone() as ObjectField);
      }
      KeyType = source.KeyType;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the type of the keys of the dictionary.
    /// </summary>
    public Type KeyType { get; set; }

    /// <summary>
    /// Gets a dictionary containing the items.
    /// </summary>
    public Dictionary<object, ObjectField> Items { get; private set; }

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
      get { return Items.Values; }
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
      return new ObjectFieldDictionary(this);
    }

    /// <summary>
    /// Adds a new item to the dictionary.
    /// </summary>
    /// <param name="item">The item itself.</param>
    public override void Add(ObjectField item)
    {
      IDictionary dictionary = Value as IDictionary;
      string key = item.NameTag as string;
      if (dictionary != null && key != null)
      {
        dictionary.Add(key, item.Value);
        Items.Add(key, item);
        Count++;
        OnItemAdded(item);
      }
    }

    /// <summary>
    /// Removes an item from the dictionary.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    public override void Remove(ObjectField item)
    {
      IDictionary dictionary = Value as IDictionary;
      string key = item.NameTag as string;
      if (dictionary != null && key != null)
      {
        dictionary.Remove(key);
        Items.Remove(key);
        Count--;
        OnItemRemoved(item);
      }
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}