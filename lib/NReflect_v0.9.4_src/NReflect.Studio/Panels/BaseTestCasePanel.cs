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
using NReflect.Studio.Models;

namespace NReflect.Studio.Panels
{
  /// <summary>
  /// A base class for all panels displaying test case data.
  /// </summary>
  public partial class BaseTestCasePanel : BasePanel
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    ///  The current test case.
    /// </summary>
    private TestCase testCase;

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="BasePanel"/>.
    /// </summary>
    protected BaseTestCasePanel()
    {
      InitializeComponent();

      CoreView.Instance.SelectedTestCaseChanged += selectedTestCaseChanged;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the test case.
    /// </summary>
    protected TestCase TestCase
    {
      get { return CoreView.Instance.SelectedTestCase; }
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Gets called after the selected test case changed.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    void selectedTestCaseChanged(object sender, EventArgs e)
    {
      TestCase oldTestCase = testCase;
      TestCase newTestCase = TestCase;

      if(oldTestCase != newTestCase)
      {
        if(oldTestCase != null)
        {
          oldTestCase.PropertyChanged -= testCase_PropertyChanged;
        }

        OnTestCaseChanged(oldTestCase, newTestCase);

        if(newTestCase != null)
        {
          newTestCase.PropertyChanged += testCase_PropertyChanged;
        }

        testCase = newTestCase;
      }
    }

    /// <summary>
    /// Gets called after a property of the selected test case changed.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    void testCase_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if(InvokeRequired)
      {
        BeginInvoke(new Action<string>(OnTestCasePropertyChanged), e.PropertyName);
      }
      else
      {
        OnTestCasePropertyChanged(e.PropertyName);
      }
    }

    /// <summary>
    /// Gets called after the test case has changed.
    /// </summary>
    /// <param name="oldTestCase">The test case which was selected before the change.</param>
    /// <param name="newTestCase">The test case which is selected now.</param>
    protected virtual void OnTestCaseChanged(TestCase oldTestCase, TestCase newTestCase)
    {
      
    }

    /// <summary>
    /// Gets called after a property of the current test case changed.
    /// </summary>
    /// <param name="propertyName">The name of the changed property.</param>
    protected virtual void OnTestCasePropertyChanged(string propertyName)
    {
      
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}