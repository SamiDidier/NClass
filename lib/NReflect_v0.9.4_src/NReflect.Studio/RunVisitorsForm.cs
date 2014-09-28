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
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using NReflect.Studio.Models;
using System.Linq;
using NReflect.Studio.Properties;
using NReflect.Studio.Visitor;

namespace NReflect.Studio
{
  public partial class RunVisitorsForm : Form
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The currently selected testcases.
    /// </summary>
    private readonly IEnumerable<TestCase> selectedTestCases;

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="RunVisitorsForm"/>.
    /// </summary>
    /// <param name="selectedTestCases">The currently selected test cases.</param>
    public RunVisitorsForm(IEnumerable<TestCase> selectedTestCases)
    {
      this.selectedTestCases = selectedTestCases;
      InitializeComponent();

      ClientSize = Settings.Default.RunVisitorsClientSize;
      radioAll.Checked = Settings.Default.RunVisitorForAllTestCases;
      radioOnlySelected.Checked = !Settings.Default.RunVisitorForAllTestCases;

      foreach(string visitorName in VisitorManager.Instance.VisitorNames)
      {
        lstVisitors.Items.Add(visitorName);
        lstVisitors.SetItemChecked(lstVisitors.Items.Count - 1, true);
      }

      EnableControls(true);
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties


    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Enables the controls on the form regarding of the parameter.
    /// </summary>
    /// <param name="enabled">If set to <c>true</c>, the normal controls are enabled.</param>
    private void EnableControls(bool enabled)
    {
      cmdRun.Enabled = enabled;
      cmdClose.Enabled = enabled;
      cmdChoose.Enabled = enabled;
      cmdAll.Enabled = enabled;
      cmdNone.Enabled = enabled;
      chkRunTestCases.Enabled = enabled;
      txtSaveLocation.Enabled = enabled;
      lstVisitors.Enabled = enabled;

      if(!selectedTestCases.Any())
      {
        radioAll.Enabled = false;
        radioAll.Checked = true;
        radioOnlySelected.Enabled = false;
      }
      else
      {
        radioAll.Enabled = enabled;
        radioOnlySelected.Enabled = enabled;
      }
    }

    /// <summary>
    /// Saves th given visitor results.
    /// </summary>
    /// <param name="visitor">The name of the visitor to run.</param>
    /// <param name="testCase">The test case to run the visitor on.</param>
    /// <param name="basePath">The base path where to store the result.</param>
    private void RunAndSaveVisitor(string visitor, TestCase testCase, string basePath)
    {
      string result = VisitorManager.Instance.RunVisitor(testCase.CurrentResult.NRAssembly, visitor);

      string path = "";
      string testCaseDirectory = CoreData.Instance.TestCaseModel.TestCaseDirectory;
      if(testCase.Path.StartsWith(testCaseDirectory))
      {
        path = testCase.Path.Substring(testCaseDirectory.Length);
        // Remove a leading '\' otherwise Path.Combine doesn't work.
        if(path.StartsWith("\\"))
        {
          path = path.Substring(1);
        }
      }
      path = Path.Combine(basePath, path);
      Directory.CreateDirectory(path);
      File.WriteAllText(Path.Combine(path, testCase.Name + "." + visitor + ".nrv"), result);
    }
    #endregion

    // ========================================================================
    // Event-Handling

    #region === Event-Handling

    private void RunVisitorsForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      Settings.Default.RunVisitorsClientSize = ClientSize;
      Settings.Default.RunVisitorForAllTestCases = radioAll.Checked;
    }

    /// <summary>
    /// Gets called after the choose button is pressed and displays a directory chooser.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void cmdChoose_Click(object sender, EventArgs e)
    {
      folderBrowserDialog.SelectedPath = txtSaveLocation.Text;
      if(folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
      {
        txtSaveLocation.Text = folderBrowserDialog.SelectedPath;
      }
    }

    /// <summary>
    /// Gets called after the all button is pressed and selects all visitors.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void cmdAll_Click(object sender, EventArgs e)
    {
      for(int i = 0; i < lstVisitors.Items.Count; ++i )
      {
        lstVisitors.SetItemChecked(i, true);
      }
    }

    /// <summary>
    /// Gets called after the none button is pressed and deselects all visitors.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void cmdNone_Click(object sender, EventArgs e)
    {
      for(int i = 0; i < lstVisitors.Items.Count; ++i)
      {
        lstVisitors.SetItemChecked(i, false);
      }
    }

    /// <summary>
    /// Gets called after the run button is pressed and starts the process.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void cmdRun_Click(object sender, EventArgs e)
    {
      EnableControls(false);
      if(Settings.Default.OptionsClearMessagesOnRun)
      {
        CoreData.Instance.Messages.Clear();
      }
      VisitorWorkerArgs args = new VisitorWorkerArgs
                                 {
                                   RunTestCases = chkRunTestCases.Checked,
                                   DestinationPath = txtSaveLocation.Text
                                 };
      foreach(string item in lstVisitors.CheckedItems)
      {
        args.Visitors.Add(item);
      }
      args.TestCases.AddRange(radioAll.Checked ? CoreData.Instance.TestCaseModel.TestCases : selectedTestCases);
      CoreData.Instance.Messages.Add(new Message(MessageSeverity.Info, "Start to execute " + args.Visitors.Count + " visitors on " + args.TestCases.Count + " test cases..."));
      backgroundWorker.RunWorkerAsync(args);
    }

    /// <summary>
    /// Gets called after the progress of the background worker changed.
    /// Updates the progress bar.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      progressBar.Value = e.ProgressPercentage;
    }

    /// <summary>
    /// Gets called start the background working process.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      VisitorWorkerArgs args = e.Argument as VisitorWorkerArgs;
      if(args != null)
      {
        double steps = args.TestCases.Count*args.Visitors.Count;
        double step = 0;
        foreach(TestCase testCase in args.TestCases)
        {
          if(testCase.CurrentResult == null && args.RunTestCases)
          {
            // Run the test case.
            if(testCase is TestCaseCSharp)
            {
              TestCaseRunner.Instance.CompileAndReflect(testCase as TestCaseCSharp);
            }
            else
            {
              TestCaseRunner.Instance.Reflect(testCase as TestCaseAssembly);
            }
          }
          if(testCase.CurrentResult != null)
          {
            // Run the visitors and store the result.
            foreach(string visitor in args.Visitors)
            {
              RunAndSaveVisitor(visitor, testCase, args.DestinationPath);
              step++;
            }
          }
          else
          {
            step += args.Visitors.Count;
          }
          backgroundWorker.ReportProgress((int)((step / steps) * 100));
        }
      }
    }

    /// <summary>
    /// Gets called after the background worker has finished.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      CoreData.Instance.Messages.Add(new Message(MessageSeverity.Info, "Finished."));
      EnableControls(true);
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion

    // ========================================================================
    // Nested types

    #region === Nested types

    /// <summary>
    /// This class takes all arguments which have to be provided for the backgrond
    /// worker.
    /// </summary>
    private class VisitorWorkerArgs
    {
      /// <summary>
      /// Initializes a new instance of <see cref="VisitorWorkerArgs"/>.
      /// </summary>
      public VisitorWorkerArgs()
      {
        Visitors = new List<string>();
        TestCases = new List<TestCase>();
      }

      /// <summary>
      /// Gets a list of visitors to run.
      /// </summary>
      public List<string> Visitors { get; private set; }
      /// <summary>
      /// Gets a list of test cases to run teh visitors for.
      /// </summary>
      public List<TestCase> TestCases { get; private set; }
      /// <summary>
      /// Set to <c>true</c> to run the test cases before running the visitor.
      /// </summary>
      public bool RunTestCases { get; set; }
      /// <summary>
      /// Gets or sets the path where to store the visitor results.
      /// </summary>
      public string DestinationPath { get; set; }
    }

    #endregion
  }
}