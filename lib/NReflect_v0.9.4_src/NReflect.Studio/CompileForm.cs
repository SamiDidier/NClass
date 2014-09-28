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
using System.Linq;
using System.Windows.Forms;
using NReflect.Studio.Models;
using NReflect.Studio.Properties;

namespace NReflect.Studio
{
  /// <summary>
  /// A form to batch compile all test cases into dll file(s).
  /// </summary>
  public partial class CompileForm : Form
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
    /// Initializes a new instance of <see cref="CompileForm"/>.
    /// </summary>
    /// <param name="selectedTestCases">The currently selected test cases.</param>
    public CompileForm(IEnumerable<TestCase> selectedTestCases)
    {
      this.selectedTestCases = selectedTestCases;
      InitializeComponent();

      ClientSize = Settings.Default.CompileFormClientSize;
      radioAll.Checked = Settings.Default.CompileFormAllTestCases;
      radioOnlySelected.Checked = !Settings.Default.CompileFormAllTestCases;
      radioSingleDll.Checked = Settings.Default.CompileFormSingleDll;
      radioMultipleDlls.Checked = !Settings.Default.CompileFormSingleDll;

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
      cmdChooseDir.Enabled = enabled && radioMultipleDlls.Checked;
      txtSaveDir.Enabled = enabled && radioMultipleDlls.Checked;
      cmdChooseDllName.Enabled = enabled && radioSingleDll.Checked;
      txtDllName.Enabled = enabled && radioSingleDll.Checked;

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

    #endregion

    // ========================================================================
    // Event-Handling

    #region === Event-Handling

    /// <summary>
    /// Gets called before the form is closed. Used to save some settings.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void CompileForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      Settings.Default.CompileFormClientSize = ClientSize;
      Settings.Default.CompileFormAllTestCases = radioAll.Checked;
      Settings.Default.CompileFormSingleDll = radioSingleDll.Checked;
    }

    /// <summary>
    /// Gets called after the run button was clicked.
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
      CompileWorkerArgs args = new CompileWorkerArgs
                                 {
                                   CompileToSingleDll = radioSingleDll.Checked,
                                   DestinationPath = radioSingleDll.Checked ? txtDllName.Text : txtSaveDir.Text
                                 };
      args.TestCases.AddRange(radioAll.Checked ? CoreData.Instance.TestCaseModel.TestCases.OfType<TestCaseCSharp>() : selectedTestCases.OfType<TestCaseCSharp>());
      CoreData.Instance.Messages.Add(new Message(MessageSeverity.Info, "Start to compile " + args.TestCases.Count + " test cases..."));
      backgroundWorker.RunWorkerAsync(args);
    }

    /// <summary>
    /// Gets called after the choose button is pressed and displays a directory chooser.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void cmdChooseDir_Click(object sender, EventArgs e)
    {
      folderBrowserDialog.SelectedPath = txtSaveDir.Text;
      if(folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
      {
        txtSaveDir.Text = folderBrowserDialog.SelectedPath;
      }
    }

    /// <summary>
    /// Gets called after the choose button is pressed and displays a file chooser.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void cmdChooseDllName_Click(object sender, EventArgs e)
    {
      saveFileDialog.FileName = txtDllName.Text;
      if(saveFileDialog.ShowDialog(this) == DialogResult.OK)
      {
        txtDllName.Text = saveFileDialog.FileName;
      }
    }

    /// <summary>
    /// Gets called after the checked state of the compile type radio button
    /// changed.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void radioCompileType_CheckedChanged(object sender, EventArgs e)
    {
      EnableControls(true);
    }

    /// <summary>
    /// Gets called start the background working process.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      CompileWorkerArgs args = e.Argument as CompileWorkerArgs;
      if(args != null)
      {
        if(args.CompileToSingleDll)
        {
          backgroundWorker.ReportProgress(0);
          TestCaseRunner.Instance.Compile(args.TestCases, args.DestinationPath);
          backgroundWorker.ReportProgress(100);
        }
        else
        {
          string testCaseDirectory = CoreData.Instance.TestCaseModel.TestCaseDirectory;
          int step = 0;
          foreach(TestCaseCSharp testCase in args.TestCases)
          {
            string path = "";
            if(testCase.Path.StartsWith(testCaseDirectory))
            {
              path = testCase.Path.Substring(testCaseDirectory.Length);
              // Remove a leading '\' otherwise Path.Combine doesn't work.
              if(path.StartsWith("\\"))
              {
                path = path.Substring(1);
              }
            }
            path = Path.Combine(args.DestinationPath, path);
            Directory.CreateDirectory(path);
            TestCaseRunner.Instance.Compile(new[]{testCase}, Path.Combine(path, testCase.Name + ".dll"));
            step++;
            backgroundWorker.ReportProgress((int)(((double)step / args.TestCases.Count) * 100));
          }
        }
      }
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
    private class CompileWorkerArgs
    {
      /// <summary>
      /// Initializes a new instance of <see cref="CompileWorkerArgs"/>.
      /// </summary>
      public CompileWorkerArgs()
      {
        TestCases = new List<TestCaseCSharp>();
      }

      /// <summary>
      /// Gets a list of test cases to compile.
      /// </summary>
      public List<TestCaseCSharp> TestCases { get; private set; }
      /// <summary>
      /// Set to <c>true</c> to compile all test cases into a single dll.
      /// </summary>
      public bool CompileToSingleDll { get; set; }
      /// <summary>
      /// Gets or sets the path where to store the compile results.
      /// </summary>
      public string DestinationPath { get; set; }
    }

    #endregion
  }
}