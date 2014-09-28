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
using System.Reflection;
using NReflect.Studio.ObjectTree.Model;

namespace NReflect.Studio.ObjectTree
{
  /// <summary>
  /// This class provides methods to create an <see cref="ObjectTreeModel"/>.
  /// </summary>
  public class ObjectTreeModelCreator
  {
    // ========================================================================
    // Fields

    #region === Fields

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectTreeModelCreator"/>.
    /// </summary>
    public ObjectTreeModelCreator()
    {
      StopTypes = new List<Type> { typeof(string), typeof(object) };
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets a list conaining the value types where recursion will stop.
    /// </summary>
    public List<Type> StopTypes { get; private set; }
    /// <summary>
    /// Gets or sets a method which will be registered to the ValueChanged-event of the fields.
    /// </summary>
    public EventHandler<ValueChangedEventArgs> ValueChangedMethod { get; set; }
    /// <summary>
    /// Gets or sets a method which will be registered to the ItemAdded-event of the fields.
    /// </summary>
    public EventHandler<ObjectFieldEventArgs> ItemAddedMethod { get; set; }
    /// <summary>
    /// Gets or sets a method which will be registered to the ItemAdded-event of the fields.
    /// </summary>
    public EventHandler<ObjectFieldEventArgs> ItemRemovedMethod { get; set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Creates a complete model for the given object.
    /// </summary>
    /// <param name="o">The model will represent this object.</param>
    /// <returns>The model.</returns>
    public ObjectField CreateModel(object o)
    {
      if(o == null)
      {
        return null;
      }

      return CreateObjectField("Root", o.GetType(), o, null);
    }

    /// <summary>
    /// Creates an <see cref="ObjectField"/> of a given object and all it's children.
    /// </summary>
    /// <param name="name">The name of the field.</param>
    /// <param name="o">The value to create the field of.</param>
    /// <param name="parent">The parent of this field.</param>
    /// <returns>The newly created <see cref="ObjectField"/>.</returns>
    public ObjectField CreateObjectField(string name, object o, ObjectField parent)
    {
      if (o == null)
      {
        return null;
      }

      ObjectField field = CreateObjectField(name, o.GetType(), o, null);
      field.Parent = parent;

      return field;
    }

    /// <summary>
    /// Refreshes the contents of the <see cref="ObjectField"/>.
    /// </summary>
    /// <param name="field">The field to update.</param>
    /// <param name="o">The new object belonging to the field.</param>
    public void RefreshObjectField(ObjectFieldClass field, object o)
    {
      if(o == null)
      {
        field.ChildFields.Clear();
      }
      else
      {
        foreach (PropertyInfo innerProperty in o.GetType().GetProperties())
        {
          ObjectField innerField = CreateObjectField(innerProperty.Name, innerProperty.PropertyType, innerProperty.GetValue(o, null), innerProperty);
          if (innerField == null)
          {
            continue;
          }
          innerField.Parent = field;
          field.ChildFields.Add(innerField);
        }
      }
      field.Value = o;
    }

    private ObjectField CreateObjectField(string name, Type type, object o, PropertyInfo property)
    {
      if (o is IEnumerator)
      {
        // IEnumerators can't be displayed.
        return null;
      }

      if (type.IsClass && !StopTypes.Contains(type))
      {
        // The type of the current field is a class type => recursion
        if (o is IDictionary)
        {
          return CreateObjectFieldDictionary(name, type, property, o as IDictionary);
        }
        if (o is IEnumerable)
        {
          return CreateObjectFieldList(name, type, property, o as IList);
        }
        return CreateObjectFieldClass(name, type, property, o);
      }
      return CreateObjectFieldValue(name, type, property, o);
    }

    private ObjectFieldDictionary CreateObjectFieldDictionary(string name, Type type, PropertyInfo property, IDictionary dictionary)
    {
      ObjectFieldDictionary field = new ObjectFieldDictionary
      {
        Name = name,
        IsNullable = true,
        PropertyInfo = property,
        Type = type,
        Value = dictionary,
        Count = dictionary != null ? dictionary.Count : 0
      };
      field.ItemType = field.Type.GetProperty("Item").PropertyType;

      if(dictionary != null)
      {
        foreach(object key in dictionary.Keys)
        {
          ObjectField itemField = CreateObjectField("[" + key + "]", field.ItemType, dictionary[key], null);
          if (itemField == null)
          {
            continue;
          }
          itemField.Parent = field;
          itemField.NameTag = key;
          field.Items.Add(key, itemField);
        }
      }
      field.ItemAdded += ItemAddedMethod;
      field.ItemRemoved += ItemRemovedMethod;

      return field;
    }

    private ObjectFieldList CreateObjectFieldList(string name, Type type, PropertyInfo property, IList list)
    {
      ObjectFieldList field = new ObjectFieldList
      {
        Name = name,
        IsNullable = true,
        PropertyInfo = property,
        Type = type,
        Value = list,
        Count = list != null ? list.Count : 0
      };
      field.ItemType = field.Type.GetProperty("Item").PropertyType;

      int count = 0;
      if (list != null)
      {
        foreach (object item in list)
        {
          count++;
          ObjectField itemField = CreateObjectField("[" + count + "]", field.ItemType, item, null);
          if (itemField == null)
          {
            continue;
          }
          itemField.Parent = field;
          field.Items.Add(itemField);
        }
      }

      field.ItemAdded += ItemAddedMethod;
      field.ItemRemoved += ItemRemovedMethod;

      return field;
    }

    private ObjectFieldClass CreateObjectFieldClass(string name, Type type, PropertyInfo property, object o)
    {
      ObjectFieldClass field = new ObjectFieldClass
                                 {
                                   Name = name,
                                   IsNullable = true,
                                   PropertyInfo = property,
                                   Type = type
                                 };

      RefreshObjectField(field, o);

      field.ValueChanged += ValueChangedMethod;

      return field;
    }

    private ObjectFieldValue CreateObjectFieldValue(string name, Type type, PropertyInfo property, object o)
    {
      ObjectFieldValue field = new ObjectFieldValue
                                 {
                                   Name = name,
                                   IsNullable = !type.IsValueType,
                                   PropertyInfo = property,
                                   Type = type,
                                   Value = o
                                 };

      field.ValueChanged += ValueChangedMethod;

      return field;
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}