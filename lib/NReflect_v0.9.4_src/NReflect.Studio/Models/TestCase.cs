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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NReflect.Studio.Models
{
  /// <summary>
  /// Represents a single test case.
  /// </summary>
  public class TestCase : TestCaseBase
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The name of the test case.
    /// </summary>
    private string name;

    /// <summary>
    /// Is set to true if the test case is dirty.
    /// </summary>
    private bool dirty;

    private TestCaseState state;
    private double percent;
    private NReflectResult currentResult;
    private NReflectResult expectedResult;

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TestCase"/> class.
    /// </summary>
    /// <param name="parent">The parent of this test case or group.</param>
    /// <param name="name">The name of the test case.</param>
    public TestCase(TestCaseGroup parent, string name): base(parent)
    {
      State = TestCaseState.Unknown;
      Dirty = false;
      this.name = name;
      Messages = new ObservableCollection<Message>();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the name of the test case.
    /// </summary>
    public override string Name
    {
      get
      {
        if (name.EndsWith(".cs"))
        {
          return name.Substring(0, name.Length - 3);
        }
        return name;
      }
      set
      {
        if (value == name)
        {
          return;
        }
        if (!value.EndsWith(".cs"))
        {
          value = value + ".cs";
        }
        if (!string.IsNullOrEmpty(Path) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
        {
          string sourceFileName = System.IO.Path.Combine(Path, name);
          string destFileName = System.IO.Path.Combine(Path, value);
          if (sourceFileName != destFileName)
          {
            File.Move(sourceFileName, destFileName);
            if(!string.IsNullOrEmpty(ExpectedObjectFile) && File.Exists(ExpectedObjectFile))
            {
              // Rename the expected object file, too.
              destFileName = System.IO.Path.Combine(Path, value.Substring(0, value.Length - 3) + ".ExpectedObject.nro");
              File.Move(ExpectedObjectFile, destFileName);
            }
          }
        }
        name = value;
        OnPropertyChanged("Name");
        OnPropertyChanged("FileName");
        OnPropertyChanged("ExpectedObjectFile");
        OnPropertyChanged("FullPath");
      }
    }

    /// <summary>
    /// Gets the file name of the underlying code file.
    /// </summary>
    public string FileName
    {
      get { return Name + ".cs"; }
    }

    /// <summary>
    /// Gets or sets the state of the test case.
    /// </summary>
    public override sealed TestCaseState State
    {
      get { return state; }
      set
      {
        if (value == state)
        {
          return;
        }
        state = value;
        OnPropertyChanged("State");
      }
    }

    /// <summary>
    /// Gets or sets the percentage of equality.
    /// </summary>
    public override double Percent
    {
      get { return percent; }
      set
      {
        if (value.Equals(percent))
        {
          return;
        }
        percent = value;
        OnPropertyChanged("Percent");
      }
    }

    /// <summary>
    /// The expected resulting assembly.
    /// </summary>
    public NReflectResult ExpectedResult
    {
      get { return expectedResult; }
      set
      {
        if (Equals(value, expectedResult))
        {
          return;
        }
        expectedResult = value;
        OnPropertyChanged("ExpectedResult");
      }
    }

    /// <summary>
    /// The current resulting assembly.
    /// </summary>
    public NReflectResult CurrentResult
    {
      get { return currentResult; }
      set
      {
        if (Equals(value, currentResult))
        {
          return;
        }
        currentResult = value;
        OnPropertyChanged("CurrentResult");
      }
    }

    /// <summary>
    /// Indicates wether this test case is edited but not saved.
    /// </summary>
    public bool Dirty
    {
      get { return dirty; }
      set
      {
        if(dirty != value)
        {
          dirty = value;
          OnPropertyChanged("Dirty");
        }
      }
    }

    /// <summary>
    /// Gets the full path and file name of the test case.
    /// </summary>
    public string FullPath
    {
      get { return System.IO.Path.Combine(Path, name); }
    }

    /// <summary>
    /// Gets the name of the expected object file.
    /// </summary>
    public string ExpectedObjectFile
    {
      get { return System.IO.Path.Combine(Path, Name + "ExpectedObject.nro"); }
    }

    /// <summary>
    /// Gets a collection of messages of this test case.
    /// </summary>
    public ObservableCollection<Message> Messages { get; private set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Loades the expected results from files.
    /// </summary>
    public virtual void Load()
    {
      ExpectedResult = null;

      if (File.Exists(ExpectedObjectFile))
      {
        try
        {
          BinaryFormatter formatter = new BinaryFormatter();
          Stream stream = new FileStream(ExpectedObjectFile, FileMode.Open, FileAccess.Read);
          ExpectedResult = formatter.Deserialize(stream) as NReflectResult;
          stream.Close();
        }
        catch(Exception e)
        {
          Messages.Add(new Message { File = FullPath, MessageText = "Loading the expected result failed: " + e.Message, Severity = MessageSeverity.Error });
        }
      }
    }

    /// <summary>
    /// Saves the expected results into the files.
    /// </summary>
    public virtual void Save()
    {
      if(ExpectedResult != null)
      {
        BinaryFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(ExpectedObjectFile, FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, ExpectedResult);
        stream.Close();
      }
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}