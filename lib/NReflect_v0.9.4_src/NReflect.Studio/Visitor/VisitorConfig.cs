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
using System.ComponentModel;
using FastColoredTextBoxNS;

namespace NReflect.Studio.Visitor
{
  /// <summary>
  /// A base class for all visitor configs.
  /// </summary>
  [Serializable]
  public abstract class VisitorConfig : INotifyPropertyChanged
  {
    // ========================================================================
    // Fields

    #region === Fields

     

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="VisitorConfig"/>.
    /// </summary>
    protected VisitorConfig()
    {
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets the name of the visitor this configuration is for.
    /// </summary>
    public abstract string VisitorName { get; }
    /// <summary>
    /// Gets or sets the language of the highlighting.
    /// </summary>
    public Language ViewHighlighting { get; set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Applies the config.
    /// </summary>
    /// <param name="visitor">The visitor to configure.</param>
    public abstract void Apply(IVisitor visitor);

    /// <summary>
    /// Fires the <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <param name="propertyName">The name of the changed property.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
      if(PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}