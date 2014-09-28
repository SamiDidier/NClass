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
using System.IO;
using System.Linq;

namespace NReflect.Studio.Models
{
  public class TestCaseModel
  {
    // ========================================================================
    // Fields

    #region === Fields

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of this class.
    /// </summary>
    public TestCaseModel()
    {
      DirtyTestCases = new List<TestCase>();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the root node.
    /// </summary>
    public TestCaseGroup Root { get; set; }

    /// <summary>
    /// Gets the base directory for test cases.
    /// </summary>
    public string TestCaseDirectory
    {
      get { return Root == null ? null : Root.Directory; }
    }

    /// <summary>
    /// Gets a list of dirty test cases.
    /// </summary>
    public List<TestCase> DirtyTestCases { get; private set; }

    /// <summary>
    /// Gets the number of test cases.
    /// </summary>
    public int CountTestCases { get; private set; }

    /// <summary>
    /// Gets the number of test case groups.
    /// </summary>
    public int CountTestCaseGroups { get; private set; }

    /// <summary>
    /// Gets an enumerato which goes through all <see cref="TestCase"/>es.
    /// </summary>
    public IEnumerable<TestCase> TestCases
    {
      get
      {
        IEnumerable<TestCase> result = from testCase in Root.TestCases
                                       where testCase is TestCase
                                       select testCase as TestCase;

        result =
          Root.TestCases.OfType<TestCaseGroup>()
              .Aggregate(result, (current, testCaseBase) => current.Concat(GetChildTestCases(testCaseBase)));

        return result;
      }
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Gets an enumerator for all testcases within the given <see cref="TestCaseGroup"/>.
    /// </summary>
    /// <param name="testCaseGroup">The <see cref="TestCaseGroup"/> to search for <see cref="TestCase"/>es.</param>
    /// <returns>An enumerator.</returns>
    public IEnumerable<TestCase> GetChildTestCases(TestCaseGroup testCaseGroup)
    {
      IEnumerable<TestCase> result = from testCase in testCaseGroup.TestCases
                                     where testCase is TestCase
                                     select testCase as TestCase;
      result = testCaseGroup.TestCases.OfType<TestCaseGroup>()
                            .Aggregate(result,
                                       (current, testCaseBase) => current.Concat(GetChildTestCases(testCaseBase)));

      return result;
    }

    /// <summary>
    /// Loads the test cases within the specified directory and its subdirectories.
    /// </summary>
    /// <param name="testCaseDirectory">The directory to search for test cases.</param>
    public void LoadTestCases(string testCaseDirectory)
    {
      CoreData.Instance.Messages.Add(new Message(MessageSeverity.Info, "Loading test cases..."));
      try
      {
        if(!Directory.Exists(testCaseDirectory))
        {
          throw new FileNotFoundException("Directory '" + testCaseDirectory + "' does not exist.");
        }

        Root = new TestCaseGroup(null, testCaseDirectory);
        CountTestCases = 0;
        CountTestCaseGroups = 0;
        LoadTestCases(testCaseDirectory, Root);

        OnTestCasesLoaded();
        CoreData.Instance.Messages.Add(new Message(MessageSeverity.Info, "Loading test cases successful. Loaded " + CountTestCases + " test cases in " + CountTestCaseGroups + " groups."));
      }
      catch(Exception ex)
      {
        CoreData.Instance.Messages.Add(new Message(MessageSeverity.Error, "Loading test cases failed: " + ex.Message));
      }
    }

    /// <summary>
    /// Loads the test cases within the specified directory and its subdirectories
    /// and places them into the parent group.
    /// </summary>
    /// <param name="path">The directory to search for test cases.</param>
    /// <param name="parent">A list to add the test cases to.</param>
    private void LoadTestCases(string path, TestCaseGroup parent)
    {
      // Get sub-dirs...
      string[] directories = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
      foreach(string directory in directories)
      {
        TestCaseGroup testCaseGroup = new TestCaseGroup(parent, Path.GetFileName(directory));

        LoadTestCases(directory, testCaseGroup);
        parent.TestCases.Add(testCaseGroup);
        CountTestCaseGroups++;
      }

      // Get files...
      List<string> files = new List<string>();
      files.AddRange(Directory.GetFiles(path, "*.cs", SearchOption.TopDirectoryOnly));
      files.AddRange(Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly));
      files.AddRange(Directory.GetFiles(path, "*.exe", SearchOption.TopDirectoryOnly));
      foreach(string file in files)
      {
        TestCase testCase;
        if(file.EndsWith(".cs"))
        {
          testCase = new TestCaseCSharp(parent, Path.GetFileName(file));
        }
        else
        {
          testCase = new TestCaseAssembly(parent, Path.GetFileName(file));
        }
        testCase.Load();

        parent.TestCases.Add(testCase);
        CountTestCases++;
      }
    }

    /// <summary>
    /// Saves a test case.
    /// </summary>
    /// <param name="testCase">The test case to save.</param>
    public void Save(TestCase testCase)
    {
      testCase.Save();
    }

    /// <summary>
    /// Adds a new test case to the model.
    /// </summary>
    /// <param name="parent">The parent to add the test case to.</param>
    /// <param name="name">The name of the new test case to add.</param>
    public TestCase AddTestCase(TestCaseGroup parent, string name)
    {
      File.Create(Path.Combine(parent.Directory, name)).Close();

      TestCase newTestCase = new TestCase(parent, name);
      parent.TestCases.Add(newTestCase);

      CountTestCases++;
      OnTestCaseAdded(parent, newTestCase);

      return newTestCase;
    }

    /// <summary>
    /// Adds a new test case group to the model.
    /// </summary>
    /// <param name="parent">The parent to add the test case group to.</param>
    /// <param name="name">The name of the new test case to add.</param>
    public TestCaseGroup AddTestCaseGroup(TestCaseGroup parent, string name)
    {
      Directory.CreateDirectory(Path.Combine(parent.Directory, name));

      TestCaseGroup newGroup = new TestCaseGroup(parent, name);
      parent.TestCases.Add(newGroup);

      CountTestCaseGroups++;
      OnTestCaseAdded(parent, newGroup);

      return newGroup;
    }

    /// <summary>
    /// Removes a given test case from the model.
    /// </summary>
    /// <param name="parent">The paret to the test case.</param>
    /// <param name="testCase">The test case to remove.</param>
    public bool RemoveTestCase(TestCaseGroup parent, TestCase testCase)
    {
      File.Delete(Path.Combine(testCase.Path, testCase.Name + ".cs"));
      CountTestCases--;

      return RemoveTestCaseBase(parent, testCase);
    }

    /// <summary>
    /// Removes a given test case group from the model.
    /// </summary>
    /// <param name="parent">The parent to the test case group.</param>
    /// <param name="testCase">The test case group to remove.</param>
    public bool RemoveTestCaseGroup(TestCaseGroup parent, TestCaseGroup testCase)
    {
      Directory.Delete(Path.Combine(testCase.Path, testCase.Name), true);
      CountTestCaseGroups--;

      return RemoveTestCaseBase(parent, testCase);
    }

    /// <summary>
    /// Removes a given test case base from the model.
    /// </summary>
    /// <param name="parent">The parent to the item to remove.</param>
    /// <param name="testCaseBase">The test case base to remove.</param>
    private bool RemoveTestCaseBase(TestCaseGroup parent, TestCaseBase testCaseBase)
    {
      bool result = parent.TestCases.Remove(testCaseBase);
      OnTestCaseRemoved(parent, testCaseBase);

      return result;
    }

    /// <summary>
    /// Moves a test case to a new group.
    /// </summary>
    /// <param name="testCase">The test case to move.</param>
    /// <param name="destGroup">The destination of the operation.</param>
    public void MoveTestCase(TestCase testCase, TestCaseGroup destGroup)
    {
      File.Move(testCase.FullPath, Path.Combine(destGroup.Directory, testCase.FileName));
      if(testCase.ExpectedObjectFile != null)
      {
        if(File.Exists(testCase.ExpectedObjectFile))
        {
          string fileName = Path.GetFileName(testCase.ExpectedObjectFile);
          if(fileName != null)
          {
            File.Move(testCase.ExpectedObjectFile, Path.Combine(destGroup.Directory, fileName));
          }
        }
      }
      testCase.Parent.TestCases.Remove(testCase);
      destGroup.TestCases.Add(testCase);
      testCase.Parent = destGroup;
    }

    /// <summary>
    /// Moves a test case group to a new group.
    /// </summary>
    /// <param name="testCaseGroup">The test case group to move.</param>
    /// <param name="destGroup">The destination of the operation.</param>
    public void MoveTestCaseGroup(TestCaseGroup testCaseGroup, TestCaseGroup destGroup)
    {
      Directory.Move(testCaseGroup.Directory, Path.Combine(destGroup.Directory, testCaseGroup.Name));
      testCaseGroup.Parent.TestCases.Remove(testCaseGroup);
      destGroup.TestCases.Add(testCaseGroup);
      testCaseGroup.Parent = destGroup;
    }

    #region --- OnXXX

    /// <summary>
    /// Fires the <see cref="TestCasesLoaded"/> event.
    /// </summary>
    protected void OnTestCasesLoaded()
    {
      if(TestCasesLoaded != null)
      {
        TestCasesLoaded(this, EventArgs.Empty);
      }
    }

    /// <summary>
    /// Fires the <see cref="TestCaseAdded"/> event.
    /// </summary>
    /// <param name="parent">The test case group the test case is located in.</param>
    /// <param name="testCaseBase">The added test case.</param>
    protected void OnTestCaseAdded(TestCaseGroup parent, TestCaseBase testCaseBase)
    {
      if(TestCaseAdded != null)
      {
        TestCaseAdded(this, new TestCaseEventArgs(parent, testCaseBase));
      }
    }

    /// <summary>
    /// Fires the <see cref="TestCaseRemoved"/> event.
    /// </summary>
    /// <param name="parent">The test case group the test case is located in.</param>
    /// <param name="testCaseBase">The removed test case.</param>
    protected void OnTestCaseRemoved(TestCaseGroup parent, TestCaseBase testCaseBase)
    {
      if(TestCaseRemoved != null)
      {
        TestCaseRemoved(this, new TestCaseEventArgs(parent, testCaseBase));
      }
    }

    #endregion

    #endregion

    // ========================================================================
    // Events

    #region === Events

    /// <summary>
    /// Occures after loading of the test cases.
    /// </summary>
    public event EventHandler TestCasesLoaded;

    /// <summary>
    /// Occures when a new test case was added.
    /// </summary>
    public event EventHandler<TestCaseEventArgs> TestCaseAdded;

    /// <summary>
    /// Occures when a test case was removed.
    /// </summary>
    public event EventHandler<TestCaseEventArgs> TestCaseRemoved;

    #endregion
  }
}