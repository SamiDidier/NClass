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
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NReflect.Studio.ObjectTree.Model
{
  /// <summary>
  /// A base class for the object tree model.
  /// </summary>
  public abstract class ObjectField : ICloneable
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The value of the field.
    /// </summary>
    private object value;
    /// <summary>
    /// A string representation of the value.
    /// </summary>
    protected string valueText;

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectField"/>.
    /// </summary>
    protected ObjectField()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectField"/> with the
    /// values of the given <see cref="ObjectField"/>.
    /// </summary>
    /// <param name="source">The <see cref="ObjectField"/> to take the values from.</param>
    protected ObjectField(ObjectField source)
    {
      Type = source.Type;
      IsNullable = source.IsNullable;
      PropertyInfo = source.PropertyInfo;
      Parent = source.Parent;
      Name = source.Name;
      NameTag = source.NameTag;
      value = source.value;
      valueText = source.valueText;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the type of the field.
    /// </summary>
    public Type Type { get; set; }
    /// <summary>
    /// Gets a readable name of the type.
    /// </summary>
    public string TypeName
    { 
      get { return GetTypeName(Type); }
    }
    /// <summary>
    /// Gets or sets a value indicating if this field can be null.
    /// </summary>
    public bool IsNullable { get; set; }

    /// <summary>
    /// Gets or sets the name of the field.
    /// </summary>
    public virtual string Name { get; set; }
    /// <summary>
    /// Gets or sers a tag for the name of the field.
    /// </summary>
    public virtual object NameTag { get; set; }

    /// <summary>
    /// Gets or sets the value of the field.
    /// </summary>
    public virtual object Value
    {
      get { return value; }
      set
      {
        if(this.value != value)
        {
          object oldValue = this.value;
          this.value = value;
          OnValueChanged(oldValue, value);
        }
      }
    }

    /// <summary>
    /// Gets or sets a string representation of the value.
    /// </summary>
    public virtual string ValueText
    {
      get { return valueText ?? (value != null ? value.ToString() : null); }
      set { valueText = value; }
    }

    /// <summary>
    /// Gets or sets the <see cref="System.Reflection.PropertyInfo"/> for the property.
    /// </summary>
    public PropertyInfo PropertyInfo { get; set; }

    /// <summary>
    /// Gets a value indicating if this property can be set.
    /// </summary>
    public virtual bool IsSetable
    {
      get
      {
        if(PropertyInfo != null)
        {
          return PropertyInfo.GetSetMethod(true) != null;
        }
        return false;
      }
    }

    /// <summary>
    /// Gets the parent of this field.
    /// </summary>
    public ObjectField Parent { get; set; }

    /// <summary>
    /// Gets an enumerator which can be used to go through all child elements of this <see cref="ObjectField"/>.
    /// </summary>
    public abstract IEnumerable<ObjectField> Childs { get; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>A new object that is a copy of this instance.</returns>
    public abstract object Clone();

    /// <summary>
    /// Gets a C# description of a type.
    /// </summary>
    /// <param name="type">The type to create the name for.</param>
    /// <returns>The name of the type.</returns>
    private string GetTypeName(Type type)
    {
      if(!type.IsGenericType)
      {
        return type.FullName;
      }
      StringBuilder name = new StringBuilder();
      name.Append(type.FullName.Substring(0, type.FullName.IndexOf("`", StringComparison.Ordinal)));
      name.Append("<");
      foreach(Type genericArgument in type.GetGenericArguments())
      {
        name.Append(GetTypeName(genericArgument));
        name.Append(", ");
      }
      if(type.GetGenericArguments().Length > 0)
      {
        name.Length -= 2;
      }
      name.Append(">");

      return name.ToString();
    }

    #region --- OnXXX

    /// <summary>
    /// Fires the <see cref="ValueChanged"/> event.
    /// </summary>
    /// <param name="oldValue">The old value of the field.</param>
    /// <param name="newValue">The new value of the field.</param>
    protected void OnValueChanged(object oldValue, object newValue)
    {
      if (ValueChanged != null)
      {
        ValueChanged(this, new ValueChangedEventArgs(oldValue, newValue, this));
      }
    }

    #endregion

    #endregion

    // ========================================================================
    // Events

    #region === Events

    /// <summary>
    /// Occures when the value of the field changed.
    /// </summary>
    public event EventHandler<ValueChangedEventArgs> ValueChanged;

    #endregion
  }
}