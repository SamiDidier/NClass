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

using NReflect.Studio.Models;

namespace NReflect.Studio.Panels
{
  public partial class CodePanel : BaseTestCasePanel
  {
    // ========================================================================
    // Fields

    #region === Fields

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="CodePanel"/>.
    /// </summary>
    public CodePanel()
    {
      InitializeComponent();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    #endregion

    // ========================================================================
    // Event-Handling

    #region === Event-Handling

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Gets called after the test case has changed.
    /// </summary>
    /// <param name="oldTestCase">The test case which was selected before the change.</param>
    /// <param name="newTestCase">The test case which is selected now.</param>
    protected override void OnTestCaseChanged(TestCase oldTestCase, TestCase newTestCase)
    {
      if(newTestCase is TestCaseCSharp)
      {
        testCaseBindingSource.DataSource = newTestCase;
      }
      else
      {
        testCaseBindingSource.DataSource = typeof(TestCaseCSharp);
      }
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}