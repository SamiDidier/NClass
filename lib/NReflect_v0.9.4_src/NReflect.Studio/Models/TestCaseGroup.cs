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
using System.Collections.ObjectModel;
using System.Linq;

namespace NReflect.Studio.Models
{
  public class TestCaseGroup : TestCaseBase
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The name of the test case group.
    /// </summary>
    private string name;

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="TestCaseGroup"/>.
    /// </summary>
    /// <param name="parent">The parent of this test case or group.</param>
    /// <param name="name">The name of the test case.</param>
    public TestCaseGroup(TestCaseGroup parent, string name)
      : base(parent)
    {
      TestCases = new ObservableCollection<TestCaseBase>();
      this.name = name;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the name of the test case group. Renames the directory
    /// if possible.
    /// </summary>
    public override string Name
    {
      get { return name; }
      set
      {
        if (value == name)
        {
          return;
        }
        if (!string.IsNullOrEmpty(Path) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
        {
          string sourceFileName = System.IO.Path.Combine(Path, name);
          string destFileName = System.IO.Path.Combine(Path, value);
          if (sourceFileName != destFileName)
          {
            System.IO.Directory.Move(sourceFileName, destFileName);
          }
        }
        name = value;
        OnPropertyChanged("Name");
        OnPropertyChanged("Directory");
      }
    }

    /// <summary>
    /// Gets or sets the percentage of success.
    /// </summary>
    public override double Percent
    {
      get
      {
        return TestCases.Count > 0 ? TestCases.Sum(testCaseBase => testCaseBase.Percent) / TestCases.Count : 0;
      }
      set
      {
        throw new InvalidOperationException("Set is not possible for the percentage of a test case group.");
      }
    }

    /// <summary>
    /// Gets or sets the state of the test case.
    /// </summary>
    public override TestCaseState State
    {
      get
      {
        if(TestCases.Any(testCase => testCase.State == TestCaseState.Fail))
        {
          return TestCaseState.Fail;
        }
        if(TestCases.Any(testCase => testCase.State == TestCaseState.CompilationFailed))
        {
          return TestCaseState.CompilationFailed;
        }
        if(TestCases.Any(testCase => testCase.State == TestCaseState.ReflectionFailed))
        {
          return TestCaseState.ReflectionFailed;
        }
        if(TestCases.Count > 0 && TestCases.All(testCase => testCase.State == TestCaseState.Success))
        {
          return TestCaseState.Success;
        }
        return TestCaseState.Unknown;
      }
      set
      {
        throw new InvalidOperationException("Set is not possible for the state of a test case group.");
      }
    }

    /// <summary>
    /// Gets or sets a list of testcases of this group.
    /// </summary>
    public ObservableCollection<TestCaseBase> TestCases { get; private set; }

    /// <summary>
    /// Gets the name and path of the directory containing the tests.
    /// </summary>
    public string Directory
    {
      get { return System.IO.Path.Combine(Path, Name); }
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}