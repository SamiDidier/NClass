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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using NReflect.Studio.Models;
using NReflect.Studio.Properties;

namespace NReflect.Studio
{
  /// <summary>
  /// This class holds the main data.
  /// </summary>
  public class CoreData
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The only instance of this class.
    /// </summary>
    private static CoreData instance;

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="CoreData"/>.
    /// </summary>
    private CoreData()
    {
      Messages = new ObservableCollection<Message>();
      TestCaseModel = new TestCaseModel();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets the only instance of this class.
    /// </summary>
    public static CoreData Instance
    {
      get { return instance ?? (instance = new CoreData()); }
    }

    /// <summary>
    /// Gets the test case model.
    /// </summary>
    public TestCaseModel TestCaseModel { get; private set; }
    /// <summary>
    /// Gets a list of displayed messages.
    /// </summary>
    public ObservableCollection<Message> Messages { get; private set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Loads the test cases at the specified directory.
    /// </summary>
    public void LoadTestCases()
    {
      TestCaseModel.LoadTestCases(Settings.Default.TestCaseDirectory);
    }

    /// <summary>
    /// Clears the results of the given test cases.
    /// </summary>
    /// <param name="testCases">The results of this test cases are reset.</param>
    public void ClearResults(IEnumerable<TestCase> testCases)
    {
      foreach(TestCase testCase in testCases)
      {
        testCase.CurrentResult = null;
        testCase.Percent = 0;
        testCase.State = TestCaseState.Unknown;
      }
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}