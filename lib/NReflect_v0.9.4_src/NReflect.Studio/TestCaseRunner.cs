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
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.CSharp;
using NReflect.Filter;
using NReflect.NRMembers;
using NReflect.NRParameters;
using NReflect.Studio.Models;
using NReflect.Studio.ObjectTree;
using NReflect.Studio.ObjectTree.Comparer;
using NReflect.Studio.ObjectTree.Model;

namespace NReflect.Studio
{
  /// <summary>
  /// This class is able to run the testcases.
  /// </summary>
  public class TestCaseRunner
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The only instance of the <see cref="TestCaseRunner"/>.
    /// </summary>
    private static TestCaseRunner instance;

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="TestCaseRunner"/>.
    /// </summary>
    private TestCaseRunner()
    {
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets the only instance of the <see cref="TestCaseRunner"/>.
    /// </summary>
    public static TestCaseRunner Instance
    {
      get { return instance ?? (instance = new TestCaseRunner()); }  
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    public void Compile(IEnumerable<TestCaseCSharp> testCases, string assemblyName)
    {
      CompilerResults compilerResults = Compile(testCases.Select(testCase => testCase.Code).ToArray(), false, assemblyName);
      ToMessages(compilerResults.Errors, "").ForEach(CoreData.Instance.Messages.Add);
    }

    private CompilerResults Compile(string[] code, bool generateInMemory = true, string assemblyName = null)
    {
      CSharpCodeProvider codeProvider = new CSharpCodeProvider();
      CompilerParameters compilerParams = new CompilerParameters
      {
        GenerateExecutable = false,
        GenerateInMemory = generateInMemory,
        IncludeDebugInformation = false,
        TreatWarningsAsErrors = false,
        CompilerOptions = "/unsafe",
        OutputAssembly = assemblyName
      };
      compilerParams.ReferencedAssemblies.Add("System.dll");
      compilerParams.ReferencedAssemblies.Add("System.Windows.Forms.dll");
      compilerParams.ReferencedAssemblies.Add("System.Core.dll");

      return codeProvider.CompileAssemblyFromSource(compilerParams, code);
    }

    public void Reflect(TestCaseAssembly testCase)
    {
      ObservableCollection<Message> messages = CoreData.Instance.Messages;

      // Do the reflection
      NRAssembly nrAssembly;
      IFilter filter = new StatisticFilter(new ReflectAllFilter());
      try
      {
        Reflector reflector = new Reflector();
        nrAssembly = reflector.Reflect(testCase.FullPath, ref filter);
      }
      catch(Exception ex)
      {
        messages.Add(new Message { MessageText = "Reflection failed with the following exception: " + ex.Message, Severity = MessageSeverity.Error });
        testCase.State = TestCaseState.ReflectionFailed;
        return;
      }
      messages.Add(new Message { MessageText = "Reflection successful.", Severity = MessageSeverity.Info });

      // Create relationships
      RelationshipCreator relationshipCreator = new RelationshipCreator();
      NRRelationships nrRelationships = relationshipCreator.CreateRelationships(nrAssembly);

      // Save the results
      testCase.CurrentResult = new NReflectResult(nrAssembly, nrRelationships);
    }

    /// <summary>
    /// Compiles and reflects a single <see cref="TestCase"/>.
    /// </summary>
    /// <param name="testCase">The <see cref="TestCase"/> to run.</param>
    public void CompileAndReflect(TestCaseCSharp testCase)
    {
      ObservableCollection<Message> messages = CoreData.Instance.Messages;
      messages.Add(new Message { MessageText = "Compilation of test case '" + testCase.Name + "' started.", Severity = MessageSeverity.Info });

      // Compile the code
      CompilerResults compilerResults = Compile(new[] {testCase.Code});

      List<Message> compileMessages = ToMessages(compilerResults.Errors, testCase.Name);
      compileMessages.ForEach(messages.Add);

      if (compileMessages.Count > 0)
      {
        messages.Add(new Message { MessageText = "Compilation failed. Aboarding.", Severity = MessageSeverity.Error });
        testCase.State = TestCaseState.CompilationFailed;
        return;
      }
      messages.Add(new Message { MessageText = "Compilation successful.", Severity = MessageSeverity.Info });

      // Do the reflection
      NRAssembly nrAssembly;
      IFilter filter = new StatisticFilter(new ReflectAllFilter());
      try
      {
        Reflector reflector = new Reflector();
        nrAssembly = reflector.Reflect(compilerResults.CompiledAssembly, ref filter);
      }
      catch (Exception ex)
      {
        messages.Add(new Message { MessageText = "Reflection failed with the following exception: " + ex.Message, Severity = MessageSeverity.Error });
        testCase.State = TestCaseState.ReflectionFailed;
        return;
      }
      messages.Add(new Message { MessageText = "Reflection successful.", Severity = MessageSeverity.Info });

      // Create relationships
      RelationshipCreator relationshipCreator = new RelationshipCreator();
      NRRelationships nrRelationships = relationshipCreator.CreateRelationships(nrAssembly);

      // Save the results
      testCase.CurrentResult = new NReflectResult(nrAssembly, nrRelationships);
    }

    /// <summary>
    /// Compares the current result of the <see cref="TestCase"/> with the
    /// expected result.
    /// </summary>
    /// <param name="testCase">The <see cref="TestCase"/> to compare.</param>
    public void Compare(TestCase testCase)
    {
      // Compare the result
      if(testCase.ExpectedResult != null)
      {
        ObjectTreeModelCreator creator = new ObjectTreeModelCreator();
        ObjectField currentTreeRoot = creator.CreateModel(testCase.CurrentResult);
        ObjectField expectedTreeRoot = creator.CreateModel(testCase.ExpectedResult);
        ObjectTreeModelComparer comparer = new ObjectTreeModelComparer();
        comparer.CompareFields += Comparer_CompareFields;
        comparer.Compare(expectedTreeRoot, currentTreeRoot);
        testCase.Percent = comparer.GetResult(expectedTreeRoot).Percent;
        testCase.State = testCase.Percent >= 1.0 ? TestCaseState.Success : TestCaseState.Fail;
      }
    }

    /// <summary>
    /// This method is called when two fields are compared.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    public void Comparer_CompareFields(object sender, CompareFieldsEventArgs e)
    {
      if(e.ExpectedField != null)
      {
        if(e.ExpectedField.Name == "FullName" && (e.ExpectedField.Parent.Value is NRAssembly || e.ExpectedField.Parent.Value is NRTypeUsage))
        {
          e.CompareResult = new CompareResult(ResultState.Ignored, 0);
        }
        else if(e.ExpectedField.Name == "TypeFullName" && (e.ExpectedField.Parent.Value is NRField || e.ExpectedField.Parent.Value is NRParameter || e.ExpectedField.Parent.Value is NRMethod))
        {
          e.CompareResult = new CompareResult(ResultState.Ignored, 0);
        }
      }
    }

    /// <summary>
    /// Converts the contents of a <see cref="CompilerErrorCollection"/>
    /// into a list of <see cref="Message"/>s.
    /// </summary>
    /// <param name="errors">The source collection for the conversion.</param>
    /// <param name="name">The name of the current test case.</param>
    /// <returns>A list containing the converted messages.</returns>
    private List<Message> ToMessages(CompilerErrorCollection errors, string name)
    {
      List<Message> result = new List<Message>(errors.Count);
      result.AddRange(from CompilerError error in errors
                      select new Message
                      {
                        MessageText = error.ErrorText,
                        Severity = error.IsWarning ? MessageSeverity.Warning : MessageSeverity.Error,
                        File = name,
                        Line = error.Line,
                        Column = error.Column
                      });

      return result;
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events

    #endregion
  }
}