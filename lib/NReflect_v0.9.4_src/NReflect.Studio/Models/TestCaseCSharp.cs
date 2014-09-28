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
using System.IO;

namespace NReflect.Studio.Models
{
  /// <summary>
  /// Represents a test case using C#-Code.
  /// </summary>
  public class TestCaseCSharp : TestCase
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The code of the test case.
    /// </summary>
    private string code;

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="TestCaseCSharp"/>.
    /// </summary>
    /// <param name="parent">The parent of this test case or group.</param>
    /// <param name="name">The name of the test case.</param>
    public TestCaseCSharp(TestCaseGroup parent, string name)
      : base(parent, name)
    {
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the code of the test case.
    /// </summary>
    public string Code
    {
      get { return code; }
      set
      {
        if (value == code)
        {
          return;
        }
        Dirty = true;
        code = value;
        OnPropertyChanged("Code");
      }
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Loades the code and expected results from files.
    /// </summary>
    public override void Load()
    {
      Code = "";

      if(File.Exists(FullPath))
      {
        try
        {
          Code = File.ReadAllText(FullPath);
        }
        catch(Exception e)
        {
          Messages.Add(new Message { File = FullPath, MessageText = "Loading the code failed: " + e.Message, Severity = MessageSeverity.Error });
        }
      }

      base.Load();

      Dirty = false;
    }

    /// <summary>
    /// Saves the code and corresponding expected results into the files.
    /// </summary>
    public override void Save()
    {
      File.WriteAllText(FullPath, Code);

      base.Save();

      Dirty = false;
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}