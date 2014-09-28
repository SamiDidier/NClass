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

namespace NReflect.Studio.Models
{
  /// <summary>
  /// Envent args containing a test case base.
  /// </summary>
  public class TestCaseEventArgs : EventArgs
  {
    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TestCaseEventArgs"/> class.
    /// </summary>
    /// <param name="parent">The test case group the test case is located in.</param>
    /// <param name="testCaseBase">The test case base in question.</param>
    public TestCaseEventArgs(TestCaseGroup parent, TestCaseBase testCaseBase)
    {
      TestCaseBase = testCaseBase;
      Parent = parent;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets the test case base in question.
    /// </summary>
    public TestCaseBase TestCaseBase { get; private set; }
    /// <summary>
    /// Gets the test case group the test case is located in.
    /// </summary>
    public TestCaseGroup Parent { get; private set; }

    #endregion
  }
}