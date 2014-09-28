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
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using NReflect.Studio.Models;
using NReflect.Studio.Panels;
using NReflect.Studio.Properties;
using NReflect.Studio.Visitor;
using WeifenLuo.WinFormsUI.Docking;
using System.Linq;

namespace NReflect.Studio
{
  /// <summary>
  /// The main form of the NReflect Studio.
  /// </summary>
  public partial class MainForm : Form
  {
    // ========================================================================
    // Fields

    #region === Fields

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="MainForm"/>.
    /// </summary>
    public MainForm()
    {
      InitializeComponent();
    }

    #endregion

    // ========================================================================
    // Event-Handling

    #region === Event-Handling

    private void MainForm_Load(object sender, EventArgs e)
    {
      WindowState = Settings.Default.MainFormWindowSate;

      AddPanel(CoreView.Instance.CodePanel);
      AddPanel(CoreView.Instance.VisitorPanel);
      AddPanel(CoreView.Instance.ObjectPanel);
      AddPanel(CoreView.Instance.ObjectComparePanel);

      windowToolBar.Items.Add(new ToolStripSeparator());
      windowMenuItem.DropDownItems.Add(new ToolStripSeparator());

      AddPanel(CoreView.Instance.TestCasesPanel);
      AddPanel(CoreView.Instance.MessagePanel);
      AddPanel(CoreView.Instance.OptionsPanel);

      string configFile = Path.Combine(Application.LocalUserAppDataPath, "NReflectStudio.config");

      if (File.Exists(configFile))
      {
        dockPanel.LoadFromXml(configFile, GetContentFromPersistString);
      }
      else
      {
        CoreView.Instance.CodePanel.Show(dockPanel, DockState.Document);
        CoreView.Instance.TestCasesPanel.Show(dockPanel, DockState.DockLeft);
        CoreView.Instance.ObjectPanel.Show(dockPanel, DockState.Document);
        CoreView.Instance.ObjectComparePanel.Show(dockPanel, DockState.Document);
        CoreView.Instance.VisitorPanel.Show(dockPanel, DockState.Document);
        CoreView.Instance.MessagePanel.Show(dockPanel, DockState.DockBottom);
        CoreView.Instance.OptionsPanel.Show(dockPanel, DockState.Document);
      }

      CoreData.Instance.Messages.CollectionChanged += CoreDataMessages_CollectionChanged;
      CoreData.Instance.LoadTestCases();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      // Save the docking states
      string configFile = Path.Combine(Application.LocalUserAppDataPath, "NReflectStudio.config");
      dockPanel.SaveAsXml(configFile);

      // Save the visitor configs
      VisitorManager.Instance.StoreConfigs();

      // Save the application settings.
      Settings.Default.MainFormWindowSate = WindowState;
      Settings.Default.Save();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void toolStripButtonRunSelectedTests_Click(object sender, EventArgs e)
    {
      RunTestCases(CoreView.Instance.SelectedTestCases);
    }

    private void toolStripButtonRunAllTests_Click(object sender, EventArgs e)
    {
      RunTestCases(CoreData.Instance.TestCaseModel.TestCases);
    }

    private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      IEnumerable<TestCase> testCases = e.Argument as IEnumerable<TestCase>;
      if(testCases != null)
      {
        foreach(TestCase testCase in testCases)
        {
          if(testCase is TestCaseCSharp)
          {
            TestCaseRunner.Instance.CompileAndReflect(testCase as TestCaseCSharp);
          }
          else
          {
            TestCaseRunner.Instance.Reflect(testCase as TestCaseAssembly);
          }
          TestCaseRunner.Instance.Compare(testCase);
        }
      }
    }

    private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      CoreData.Instance.Messages.Add(new Message(MessageSeverity.Info, "Finished."));
      EnableControls(true);
    }

    private void toolStripButtonClearResults_Click(object sender, EventArgs e)
    {
      CoreData.Instance.ClearResults(CoreData.Instance.TestCaseModel.TestCases);
    }

    /// <summary>
    /// Gets called after the message collection has changed.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    void CoreDataMessages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if(InvokeRequired)
      {
        BeginInvoke(new NotifyCollectionChangedEventHandler(CoreDataMessages_CollectionChanged), sender, e);
      }
      else
      {
        if(e.Action == NotifyCollectionChangedAction.Add)
        {
          // Display the last message at the status bar.
          Message message = e.NewItems.Cast<Message>().LastOrDefault();
          if(message != null)
          {
            statusLabel.Text = message.Severity + ": " + message.MessageText;
          }
        }
      }
    }

    private void saveAction_Click(object sender, EventArgs e)
    {
      if(CoreView.Instance.SelectedTestCase != null)
      {
        CoreData.Instance.TestCaseModel.Save(CoreView.Instance.SelectedTestCase);
      }
    }

    private void saveAllAction_Click(object sender, EventArgs e)
    {
      foreach(TestCase testCase in CoreData.Instance.TestCaseModel.TestCases)
      {
        CoreData.Instance.TestCaseModel.Save(testCase);
      }
    }

    private void acceptAllCurrentResultsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      foreach(TestCase testCase in CoreView.Instance.SelectedTestCases)
      {
        if(testCase.CurrentResult != null)
        {
          testCase.ExpectedResult = testCase.CurrentResult;
          testCase.Dirty = true;
          TestCaseRunner.Instance.Compare(testCase);
        }
      }
    }

    private void runVisitorsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      new RunVisitorsForm(CoreView.Instance.SelectedTestCases).ShowDialog(this);
    }

    private void compileTestCasesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      new CompileForm(CoreView.Instance.SelectedTestCases).ShowDialog(this);
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Adds the button and menu entry of the panel to the
    /// toolbar and menu.
    /// </summary>
    /// <param name="panel">The panel to add.</param>
    private void AddPanel(BasePanel panel)
    {
      windowToolBar.Items.Add(panel.ToolStripButton);
      windowMenuItem.DropDownItems.Add(panel.ToolStripMenuItem);
    }

    /// <summary>
    /// Runs the given test cases.
    /// </summary>
    /// <param name="testCases">The test cases to run.</param>
    private void RunTestCases(IEnumerable<TestCase> testCases)
    {
      EnableControls(false);
      if(Settings.Default.OptionsClearMessagesOnRun)
      {
        CoreData.Instance.Messages.Clear();
      }
      CoreData.Instance.Messages.Add(new Message(MessageSeverity.Info, "Start to execute " + testCases.Count() + " test cases..."));
      backgroundWorker.RunWorkerAsync(testCases);
    }

    /// <summary>
    /// Enables the controls on the form regarding of the parameter.
    /// </summary>
    /// <param name="enabled">If set to <c>true</c>, the normal controls are enabled.</param>
    private void EnableControls(bool enabled)
    {
      toolStripButtonCompileTestCases.Enabled = enabled;
      toolStripButtonRunAllTests.Enabled = enabled;
      toolStripButtonRunSelectedTests.Enabled = enabled;
      runAllTestsToolStripMenuItem.Enabled = enabled;
      compileTestCasesToolStripMenuItem.Enabled = enabled;
      runSelectedTestsToolStripMenuItem.Enabled = enabled;
      runVisitorsToolStripMenuItem.Enabled = enabled;
    }

    /// <summary>
    /// A method returning the correct panel for the given string. This
    /// is needed to load the panel layout.
    /// </summary>
    /// <param name="persistString">A string containing the typname of the panel.</param>
    /// <returns>The panel matching to the string or null if none matches.</returns>
    private IDockContent GetContentFromPersistString(string persistString)
    {
      if(persistString == typeof(TestCasesPanel).ToString())
        return CoreView.Instance.TestCasesPanel;
      if(persistString == typeof(CodePanel).ToString())
        return CoreView.Instance.CodePanel;
      if(persistString == typeof(VisitorPanel).ToString())
        return CoreView.Instance.VisitorPanel;
      if (persistString == typeof(ObjectPanel).ToString())
        return CoreView.Instance.ObjectPanel;
      if (persistString == typeof(ObjectComparePanel).ToString())
        return CoreView.Instance.ObjectComparePanel;
      if(persistString == typeof(MessagePanel).ToString())
        return CoreView.Instance.MessagePanel;
      if(persistString == typeof(OptionsPanel).ToString())
        return CoreView.Instance.OptionsPanel;

      return null;
    }

    #endregion
  }
}