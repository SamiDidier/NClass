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
using System.Windows.Forms;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using NReflect.NRAttributes;
using NReflect.Studio.Models;
using NReflect.Studio.ObjectTree;
using NReflect.Studio.ObjectTree.Comparer;
using NReflect.Studio.ObjectTree.Model;
using NReflect.Studio.Properties;
using System.Linq;

namespace NReflect.Studio.Panels
{
  public partial class ObjectComparePanel : BaseTestCasePanel
  {
    // ========================================================================
    // Fields

    #region === Fields

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectComparePanel"/>.
    /// </summary>
    public ObjectComparePanel()
    {
      InitializeComponent();

      toolStripButtonSynchronizeTrees.CheckState = Settings.Default.ObjectComparePanelSynchronizeTrees;
      toolStripButtonAutoResizeColumns.CheckState = Settings.Default.ObjectComparePanelAutoResizeColumnsCheckState;
      toolStripButtonAutoResizeColumns_Click(null, null);

      TreeColumn treeColumnResult = new TreeColumn("Result", Resources.Equal.Width);
      NodeIcon nodeControlResult = new NodeIcon { VirtualMode = true, ParentColumn = treeColumnResult, VerticalAlign = VerticalAlignment.Center };
      treeColumnResult.Width += 2 * nodeControlResult.LeftMargin;
      nodeControlResult.ValueNeeded += nodeControlResult_ValueNeeded;
      expectedObjectTree.AddExtraColumn(treeColumnResult, nodeControlResult);

      TreeColumn treeColumnPercent = new TreeColumn("Percent", 50);
      ComparePercentageNodeControl nodeControlPercent = new ComparePercentageNodeControl { VirtualMode = true, ParentColumn = treeColumnPercent };
      nodeControlPercent.ValueNeeded += nodeControlPercent_ValueNeeded;
      expectedObjectTree.AddExtraColumn(treeColumnPercent, nodeControlPercent);

      TreeColumn treeColumnResultCurrent = new TreeColumn("Result", Resources.Equal.Width);
      NodeIcon nodeControlResultCurrent = new NodeIcon { VirtualMode = true, ParentColumn = treeColumnResultCurrent, VerticalAlign = VerticalAlignment.Center };
      treeColumnResultCurrent.Width += 2 * nodeControlResult.LeftMargin;
      nodeControlResultCurrent.ValueNeeded += nodeControlResult_ValueNeeded;
      currentObjectTree.AddExtraColumn(2, treeColumnResultCurrent, nodeControlResultCurrent);

      TreeColumn treeColumnPercentCurrent = new TreeColumn("Percent", 50);
      ComparePercentageNodeControl nodeControlPercentCurrent = new ComparePercentageNodeControl { VirtualMode = true, ParentColumn = treeColumnPercentCurrent };
      nodeControlPercentCurrent.ValueNeeded += nodeControlPercent_ValueNeeded;
      currentObjectTree.AddExtraColumn(3, treeColumnPercentCurrent, nodeControlPercentCurrent);

      Comparer = new ObjectTreeModelComparer();
      Comparer.CompareFields += TestCaseRunner.Instance.Comparer_CompareFields;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// A camparer to compare the two models.
    /// </summary>
    public ObjectTreeModelComparer Comparer { get; private set; }

    #endregion

    // ========================================================================
    // Event-Handling

    #region === Event-Handling

    private void toolStripButtonAcceptCurrentResult_Click(object sender, EventArgs e)
    {
      if(TestCase != null && TestCase.ExpectedResult != TestCase.CurrentResult)
      {
        TestCase.ExpectedResult = TestCase.CurrentResult;
        TestCase.Dirty = true;
        TestCaseRunner.Instance.Compare(TestCase);
        expectedObjectTree.Object = TestCase.ExpectedResult;
      }
    }

    /// <summary>
    /// Gets called when the dirty status of the expected tree changed.
    /// </summary>
    /// <param name="sender">The issuer of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void expectedObjectTree_DirtyChanged(object sender, EventArgs e)
    {
      if(TestCase != null)
      {
        TestCase.Dirty = expectedObjectTree.Dirty;
      }
    }

    /// <summary>
    /// Gets called when a new value is needed by the model.
    /// </summary>
    /// <param name="sender">The issuer of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void expectedObjectTree_NewValueNeeded(object sender, NewValueEventArgs e)
    {
      if(e.ObjectField != null && e.ObjectField.Parent is ObjectFieldClass && e.ObjectField.Parent.Value is NRAttributeValue)
      {
        NRAttributeValue attribute = e.ObjectField.Parent.Value as NRAttributeValue;
        Type type = Type.GetType(attribute.Type);
        if(type != null)
        {
          e.Value = Activator.CreateInstance(type);
        }
      }
    }

    private void toolStripButtonExpandAll_Click(object sender, EventArgs e)
    {
      expectedObjectTree.ExpandAll();
      currentObjectTree.ExpandAll();
    }

    private void toolStripButtonCollapseAll_Click(object sender, EventArgs e)
    {
      expectedObjectTree.CollapseAll();
      currentObjectTree.CollapseAll();
    }

    private void toolStripButtonSynchronizeTrees_Click(object sender, EventArgs e)
    {
      if (toolStripButtonSynchronizeTrees.Checked)
      {
        currentObjectTree.SynchronizeTreeView(expectedObjectTree);
        currentObjectTree.FirstVisibleField = currentObjectTree.GetEquivalentField(expectedObjectTree.FirstVisibleField);
      }
      Settings.Default.ObjectComparePanelSynchronizeTrees = toolStripButtonSynchronizeTrees.CheckState;
    }

    private void expectedObjectTree_ValueChanged(object sender, ValueChangedEventArgs e)
    {
      Comparer.Compare(expectedObjectTree.TreeModel, currentObjectTree.TreeModel);
      TestCase.Percent = Comparer.GetResult(expectedObjectTree.TreeModel.Root).Percent;
      TestCase.State = TestCase.Percent >= 1.0 ? TestCaseState.Success : TestCaseState.Fail;

      currentObjectTree.Invalidate(true);
    }

    private void expectedObjectTree_Expanded(object sender, ObjectFieldEventArgs e)
    {
      ObjectField otherField = currentObjectTree.GetEquivalentField(e.ObjectField);
      if (toolStripButtonSynchronizeTrees.Checked)
      {
        // Synchronize with the other tree view
        currentObjectTree.Expand(otherField);
      }
      bool openOther = true;
      if(otherField != null)
      {
        ObjectFieldCollection otherCollection = otherField as ObjectFieldCollection;
        if(otherCollection != null && otherCollection.Count > 1)
        {
          openOther = false;
        }
      }
      ObjectFieldCollection collection = e.ObjectField as ObjectFieldCollection;
      if(collection != null && collection.Count == 1 && (openOther || !toolStripButtonSynchronizeTrees.Checked))
      {
        expectedObjectTree.Expand(collection.Childs.First());
      }
    }

    private void expectedObjectTree_Collapsed(object sender, ObjectFieldEventArgs e)
    {
      if (toolStripButtonSynchronizeTrees.Checked)
      {
        // Synchronize with the other tree view
        ObjectField otherField = currentObjectTree.GetEquivalentField(e.ObjectField);
        currentObjectTree.Collapse(otherField);
      }
    }

    private void currentObjectTree_Expanded(object sender, ObjectFieldEventArgs e)
    {
      if (toolStripButtonSynchronizeTrees.Checked)
      {
        // Synchronize with the other tree view
        ObjectField otherField = expectedObjectTree.GetEquivalentField(e.ObjectField);
        expectedObjectTree.Expand(otherField);
      }
    }

    private void currentObjectTree_Collapsed(object sender, ObjectFieldEventArgs e)
    {
      if (toolStripButtonSynchronizeTrees.Checked)
      {
        // Synchronize with the other tree view
        ObjectField otherField = expectedObjectTree.GetEquivalentField(e.ObjectField);
        expectedObjectTree.Collapse(otherField);
      }
    }

    private void expectedObjectTree_SelectionChanged(object sender, ObjectFieldEventArgs e)
    {
      if (toolStripButtonSynchronizeTrees.Checked && e.ObjectField != null)
      {
        // Synchronize with the other tree view
        ObjectField otherField = currentObjectTree.GetEquivalentField(e.ObjectField);
        currentObjectTree.SelectedField = otherField;
      }

      toolStripButtonUseAsRoot.Enabled = e.ObjectField != null || toolStripButtonUseAsRoot.Checked;
    }

    private void currentObjectTree_SelectionChanged(object sender, ObjectFieldEventArgs e)
    {
      if (toolStripButtonSynchronizeTrees.Checked && e.ObjectField != null)
      {
        // Synchronize with the other tree view
        ObjectField otherField = expectedObjectTree.GetEquivalentField(e.ObjectField);
        expectedObjectTree.SelectedField = otherField;
      }
    }

    private void expectedObjectTree_Scroll(object sender, ScrollEventArgs e)
    {
      if (toolStripButtonSynchronizeTrees.Checked)
      {
        // Synchronize with the other tree view
        currentObjectTree.FirstVisibleField = currentObjectTree.GetEquivalentField(expectedObjectTree.FirstVisibleField);
      }
    }

    private void currentObjectTree_Scroll(object sender, ScrollEventArgs e)
    {
      if (toolStripButtonSynchronizeTrees.Checked)
      {
        // Synchronize with the other tree view
        expectedObjectTree.FirstVisibleField = expectedObjectTree.GetEquivalentField(currentObjectTree.FirstVisibleField);
      }
    }

    private void toolStripButtonAutoResizeColumns_Click(object sender, EventArgs e)
    {
      expectedObjectTree.AutoResizeColumns = toolStripButtonAutoResizeColumns.Checked;
      currentObjectTree.AutoResizeColumns = toolStripButtonAutoResizeColumns.Checked;

      Settings.Default.ObjectComparePanelAutoResizeColumnsCheckState = toolStripButtonAutoResizeColumns.CheckState;
    }

    private void toolStripButtonUseAsRoot_Click(object sender, EventArgs e)
    {
      if(toolStripButtonUseAsRoot.Checked)
      {
        expectedObjectTree.RootField = expectedObjectTree.SelectedField;
        currentObjectTree.RootField = currentObjectTree.SelectedField;
      }
      else
      {
        expectedObjectTree.RootField = null;
        currentObjectTree.RootField = null;
        // The selection is gone now but the selection change even is not fired...
        toolStripButtonUseAsRoot.Enabled = false;
      }
    }

    /// <summary>
    /// This method is called when a new value for the result column is needed.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    void nodeControlResult_ValueNeeded(object sender, NodeControlValueEventArgs e)
    {
      ObjectField field = e.Node.Tag as ObjectField;
      ResultState resultState = Comparer.GetResult(field).State;
      switch(resultState)
      {
        case ResultState.Equal:
          e.Value = Resources.Equal;
          break;
        case ResultState.Unknown:
          e.Value = null;
          break;
        case ResultState.Missing:
          e.Value = Resources.Out;
          break;
        case ResultState.New:
          e.Value = Resources.In;
          break;
        case ResultState.NotEqual:
          e.Value = Resources.NotEqual;
          break;
        case ResultState.Ignored:
          e.Value = Resources.Ignore;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    /// <summary>
    /// This method is called when a new value for the result percentage column is needed.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    void nodeControlPercent_ValueNeeded(object sender, NodeControlValueEventArgs e)
    {
      ObjectField field = e.Node.Tag as ObjectField;
      e.Value = Comparer.GetResult(field);
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Gets called after the test case has changed.
    /// </summary>
    /// <param name="oldTestCase">The test case which was selected before the change.</param>
    /// <param name="newTestCase">The test case which is selected now.</param>
    protected override void OnTestCaseChanged(TestCase oldTestCase, TestCase newTestCase)
    {
      expectedObjectTree.Object = newTestCase != null ? newTestCase.ExpectedResult : null;
      currentObjectTree.Object = newTestCase != null ? newTestCase.CurrentResult : null;
      toolStripButtonAcceptCurrentResult.Enabled = newTestCase != null && newTestCase.CurrentResult != null;

      Comparer.Compare(expectedObjectTree.TreeModel, currentObjectTree.TreeModel);
    }

    /// <summary>
    /// Gets called after a property of the current test case changed.
    /// </summary>
    /// <param name="propertyName">The name of the changed property.</param>
    protected override void OnTestCasePropertyChanged(string propertyName)
    {
      if(propertyName == "CurrentResult")
      {
        if(InvokeRequired)
        {
          BeginInvoke(new Action<string>(OnTestCasePropertyChanged), propertyName);
        }
        else
        {
          expectedObjectTree.Object = TestCase != null ? TestCase.ExpectedResult : null;
          currentObjectTree.Object = TestCase != null ? TestCase.CurrentResult : null;
          toolStripButtonAcceptCurrentResult.Enabled = TestCase != null && TestCase.CurrentResult != null;

          Comparer.Compare(expectedObjectTree.TreeModel, currentObjectTree.TreeModel);
        }
      }
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}