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

using System.ComponentModel;
using NReflect.Studio.Annotations;

namespace NReflect.Studio.Models
{
  /// <summary>
  /// A base for all test case relating classes.
  /// </summary>
  public abstract class TestCaseBase : INotifyPropertyChanged
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The parent of this <see cref="TestCaseBase"/>.
    /// </summary>
    private TestCaseGroup parent;
    
    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TestCaseBase"/> class.
    /// </summary>
    /// <param name="parent">The parent of this test case or group.</param>
    protected TestCaseBase(TestCaseGroup parent)
    {
      Parent = parent;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the name of the test case or group.
    /// </summary>
    public abstract string Name { get; set; }

    /// <summary>
    /// Gets or sets the percentage of success.
    /// </summary>
    public abstract double Percent { get; set; }

    /// <summary>
    /// Gets or sets the state of the test case.
    /// </summary>
    public abstract TestCaseState State { get; set; }

    /// <summary>
    /// Gets or sets the path of the test case or group.
    /// </summary>
    public string Path
    {
      get { return Parent != null ? System.IO.Path.Combine(Parent.Path, Parent.Name) : Name; }
    }

    /// <summary>
    /// Gets or sets the parent of this test case or group.
    /// </summary>
    public TestCaseGroup Parent
    {
        get { return parent; }
        set
        {
          if (Equals(value, parent))
          {
            return;
          }
          parent = value;
          OnPropertyChanged("Parent");
          OnPropertyChanged("Path");
        }
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Fires the <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <param name="propertyName">The name of the changed property.</param>
    [NotifyPropertyChangedInvocator]
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
    /// Occures after a property changed.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}