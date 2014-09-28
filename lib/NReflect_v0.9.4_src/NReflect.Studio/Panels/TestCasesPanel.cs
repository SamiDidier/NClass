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
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using NReflect.Studio.Models;
using NReflect.Studio.Properties;
using System.Linq;

namespace NReflect.Studio.Panels
{
  public partial class TestCasesPanel : BasePanel
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The testmanager which is used to manage the tests.
    /// </summary>
    private TestCaseTreeModel treeModel;

    /// <summary>
    /// The state column
    /// </summary>
    private readonly TreeColumn colState;

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="TestCasesPanel"/>.
    /// </summary>
    public TestCasesPanel()
    {
      InitializeComponent();

      colState = new TreeColumn("Percent", 50);
      TestCaseStateNodeControl nodeControlStatus = new TestCaseStateNodeControl
                                                     {
                                                       VirtualMode = true,
                                                       ParentColumn = colState
                                                     };
      nodeControlStatus.ValueNeeded += nodeControlStatus_ValueNeeded;
      trvTestCases.Columns.Insert(3, colState);
      trvTestCases.NodeControls.Add(nodeControlStatus);

      nodeFileIcon.ValueNeeded += nodeFileIcon_ValueNeeded;
      nodeIconState.ValueNeeded += nodeIconState_ValueNeeded;
      nodeIconMessages.ValueNeeded += nodeIconMessages_ValueNeeded;
      nodeIconMessages.ToolTipProvider = new MessageTootlTipProvider();
      nodeTextBoxName.DrawText += nodeTextBoxName_DrawText;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets the currently selected test case or null if none is selected.
    /// </summary>
    public TestCase SelectedTestCase
    {
      get
      {
        if(trvTestCases.SelectedNode != null)
        {
          return trvTestCases.SelectedNode.Tag as TestCase;
        }
        return null;
      }
    }

    /// <summary>
    /// Gets a list of all selected testcases including those which are
    /// nested within selected folders.
    /// </summary>
    public IEnumerable<TestCase> SelectedTestCases
    {
      get
      {
        // Get selecte test cases.
        IEnumerable<TestCase> result = from node in trvTestCases.SelectedNodes
                                       where node.Tag is TestCase
                                       select node.Tag as TestCase;
        // Get all test cases of selected test case groups.
        result =
          trvTestCases.SelectedNodes.Where(node => node.Tag is TestCaseGroup)
                      .Select(node => node.Tag as TestCaseGroup)
                      .Aggregate(result,
                                 (current, testCaseBase) =>
                                 current.Concat(CoreData.Instance.TestCaseModel.GetChildTestCases(testCaseBase)));
        // Maybe we have duplicates. Eliminate them.
        result = result.Distinct();

        return result;
      }
    }

    #endregion

    // ========================================================================
    // Event-Handling

    #region === Event-Handling

    private void TestCasesPanel_Load(object sender, EventArgs e)
    {
      treeModel = new TestCaseTreeModel(trvTestCases);
      trvTestCases.Model = new SortedTreeModel(treeModel);
    }

    private void trvTestCases_SelectionChanged(object sender, EventArgs e)
    {
      OnSelectedTestCaseChanged();
    }

    private void trvTestCases_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode != Keys.Delete)
      {
        return;
      }

      treeModel.RemoveTestCase(trvTestCases.SelectedNode);
    }

    /// <summary>
    /// Gets called when a drag'n'drop operation is about to start.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void trvTestCases_ItemDrag(object sender, ItemDragEventArgs e)
    {
      trvTestCases.DoDragDropSelectedNodes(DragDropEffects.Move);
    }

    /// <summary>
    /// Gets called when an item is draged over the treeview.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void trvTestCases_DragOver(object sender, DragEventArgs e)
    {
      if(e.Data.GetDataPresent(typeof(TreeNodeAdv[])) && trvTestCases.DropPosition.Node != null)
      {
        TreeNodeAdv[] nodes = e.Data.GetData(typeof(TreeNodeAdv[])) as TreeNodeAdv[];
        TreeNodeAdv parent = trvTestCases.DropPosition.Node;
        if(trvTestCases.DropPosition.Position == NodePosition.Inside && !(parent.Tag is TestCaseGroup))
        {
          e.Effect = DragDropEffects.None;
          return;
        }
        if(trvTestCases.DropPosition.Position != NodePosition.Inside)
        {
          parent = parent.Parent;
        }

        if(nodes == null || nodes.All(adv => adv.Parent == parent))
        {
          e.Effect = DragDropEffects.None;
          return;
        }

        e.Effect = e.AllowedEffect;
      }
    }

    /// <summary>
    /// Gets called when an item is dropped over the treeview.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void trvTestCases_DragDrop(object sender, DragEventArgs e)
    {
      trvTestCases.BeginUpdate();

      TreeNodeAdv[] nodes = (TreeNodeAdv[]) e.Data.GetData(typeof(TreeNodeAdv[]));
      TreeNodeAdv dropNode = trvTestCases.DropPosition.Node;

      if(trvTestCases.DropPosition.Position != NodePosition.Inside)
      {
        dropNode = dropNode.Parent;
      }

      foreach(TreeNodeAdv node in nodes)
      {
        treeModel.MoveTestCase(node, dropNode);
      }

      trvTestCases.EndUpdate();

      dropNode.IsExpanded = true;
    }

    private void trvTestCases_SizeChanged(object sender, EventArgs e)
    {
      colTestCase.Width = trvTestCases.ClientSize.Width - colTestCaseMessages.Width - colState.Width - colTestCaseState.Width - SystemInformation.VerticalScrollBarWidth;
    }

    /// <summary>
    /// Gets called when the text of a node is drawn.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the ebent.</param>
    private void nodeTextBoxName_DrawText(object sender, DrawEventArgs e)
    {
      if(e.Node.Tag is TestCase)
      {
        TestCase testCase = e.Node.Tag as TestCase;
        if(testCase.Dirty)
        {
          e.Font = new Font(trvTestCases.Font, FontStyle.Italic);
        }
      }
    }

    /// <summary>
    /// Gets called when the icon column needs an image.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the ebent.</param>
    private void nodeFileIcon_ValueNeeded(object sender, NodeControlValueEventArgs e)
    {
      e.Value = e.Node.Tag is TestCaseCSharp ? Resources.CSharpFile : (e.Node.Tag is TestCaseAssembly ? Resources.AssemblyFile : Resources.Folder);
    }

    /// <summary>
    /// Gets called when a value for the message column is needed.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    void nodeIconMessages_ValueNeeded(object sender, NodeControlValueEventArgs e)
    {
      TestCase testCase = e.Node.Tag as TestCase;
      if(testCase != null)
      {
        if(testCase.Messages.Any(m => m.Severity == MessageSeverity.Error))
        {
          e.Value = Resources.Error;
        }
        else if(testCase.Messages.Any(m => m.Severity == MessageSeverity.Warning))
        {
          e.Value = Resources.Warning;
        }
        else if(testCase.Messages.Any(m => m.Severity == MessageSeverity.Info))
        {
          e.Value = Resources.Info;
        }
      }
    }

    /// <summary>
    /// Gets called when a value for the state column is needed.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    void nodeIconState_ValueNeeded(object sender, NodeControlValueEventArgs e)
    {
      TestCaseBase testCase = e.Node.Tag as TestCaseBase;
      if(testCase != null)
      {
        switch(testCase.State)
        {
          case TestCaseState.Unknown:
            e.Value = Resources.Unknown;
            break;
          case TestCaseState.Success:
            e.Value = Resources.OK;
            break;
          case TestCaseState.Fail:
            e.Value = Resources.Error;
            break;
          case TestCaseState.CompilationFailed:
            e.Value = Resources.Error;
            break;
          case TestCaseState.ReflectionFailed:
            e.Value = Resources.Error;
            break;
        }
      }
    }

    /// <summary>
    /// Gets called when a value for the state column is needed.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void nodeControlStatus_ValueNeeded(object sender, NodeControlValueEventArgs e)
    {
      e.Value = e.Node.Tag as TestCaseBase;
    }

    private void addTestCaseToolStripMenuItem_Click(object sender, EventArgs e)
    {
      TreeNodeAdv newNode = treeModel.AddNewTestCase(trvTestCases.SelectedNode);
      trvTestCases.SelectedNode = newNode;
      nodeTextBoxName.BeginEdit();
    }

    private void addGroupToolStripMenuItem_Click(object sender, EventArgs e)
    {
      TreeNodeAdv newNode = treeModel.AddNewTestCaseGroup(trvTestCases.SelectedNode);
      trvTestCases.SelectedNode = newNode;
      nodeTextBoxName.BeginEdit();
    }

    private void toolStripButtonExpandAll_Click(object sender, EventArgs e)
    {
      trvTestCases.ExpandAll();
    }

    private void toolStripButtonCollapseAll_Click(object sender, EventArgs e)
    {
      trvTestCases.CollapseAll();
    }

    private void toolStripButtonReload_Click(object sender, EventArgs e)
    {
      CoreData.Instance.LoadTestCases();
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    protected void OnSelectedTestCaseChanged()
    {
      if(SelectedTestCaseChanged != null)
      {
        SelectedTestCaseChanged(this, EventArgs.Empty);
      }
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events

    /// <summary>
    /// Occures after the selected test case changed.
    /// </summary>
    public event EventHandler SelectedTestCaseChanged;

    #endregion

    // ========================================================================
    // Nested types

    #region === Nested types
    
    /// <summary>
    /// This class implements the <see cref="IToolTipProvider"/> interface
    /// to provide tool tips for the message column.
    /// </summary>
    private class MessageTootlTipProvider : IToolTipProvider
    {
      /// <summary>
      /// This method is called when a tool tip is needed.
      /// </summary>
      /// <param name="node">The node the tooltip is needed for.</param>
      /// <param name="nodeControl">The current control.</param>
      /// <returns>The text of the tool tip.</returns>
      public string GetToolTip(TreeNodeAdv node, NodeControl nodeControl)
      {
        TestCase testCase = node.Tag as TestCase;
        if(testCase != null && testCase.Messages.Count > 0)
        {
          StringBuilder sb = new StringBuilder();
          foreach(Message message in testCase.Messages)
          {
            sb.Append(message.MessageText);
          }
          return sb.ToString();
        }
        return null;
      }
    }

    #endregion
  }
}