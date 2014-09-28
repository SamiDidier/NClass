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

using System.Linq;
using NReflect.Studio.Models;
using NReflect.Studio.ObjectTree.Model;
using NReflect.Studio.Properties;

namespace NReflect.Studio.Panels
{
  public partial class ObjectPanel : BaseTestCasePanel
  {
    // ========================================================================
    // Fields

    #region === Fields

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectPanel"/>.
    /// </summary>
    public ObjectPanel()
    {
      InitializeComponent();

      toolStripButtonAutoResizeColumns.Checked = Settings.Default.ObjectPanelAutoResizeColumns;
      toolStripButtonTypeVisible.Checked = Settings.Default.ObjectPanelTypeVisible;
      toolStripButtonAutoResizeColumns_Click(null, null);
      toolStripButtonTypeVisible_Click(null, null);
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    #endregion

    // ========================================================================
    // Event-Handling

    #region === Event-Handling

    private void objectTree_Expanded(object sender, ObjectTree.ObjectFieldEventArgs e)
    {
      ObjectFieldCollection collection = e.ObjectField as ObjectFieldCollection;
      if (collection != null && collection.Count == 1)
      {
        objectTree.Expand(collection.Childs.First());
      }
    }

    private void objectTree_SelectionChanged(object sender, ObjectTree.ObjectFieldEventArgs e)
    {
      toolStripButtonUseAsRoot.Enabled = e.ObjectField != null || toolStripButtonUseAsRoot.Checked;
    }

    private void toolStripButtonExpandAll_Click(object sender, System.EventArgs e)
    {
      objectTree.ExpandAll();
    }

    private void toolStripButtonCollapseAll_Click(object sender, System.EventArgs e)
    {
      objectTree.CollapseAll();
    }

    private void toolStripButtonAutoResizeColumns_Click(object sender, System.EventArgs e)
    {
      objectTree.AutoResizeColumns = toolStripButtonAutoResizeColumns.Checked;
      Settings.Default.ObjectPanelAutoResizeColumns = toolStripButtonAutoResizeColumns.Checked;
    }

    private void toolStripButtonTypeVisible_Click(object sender, System.EventArgs e)
    {
      objectTree.TypeColumnVisible = toolStripButtonTypeVisible.Checked;
      Settings.Default.ObjectPanelTypeVisible = toolStripButtonTypeVisible.Checked;
    }

    private void toolStripButtonUseAsRoot_Click(object sender, System.EventArgs e)
    {
      objectTree.RootField = toolStripButtonUseAsRoot.Checked ? objectTree.SelectedField : null;
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
      objectTree.Object = TestCase != null ? TestCase.CurrentResult : null;
    }

    /// <summary>
    /// Gets called after a property of the current test case changed.
    /// </summary>
    /// <param name="propertyName">The name of the changed property.</param>
    protected override void OnTestCasePropertyChanged(string propertyName)
    {
      if(propertyName == "CurrentResult")
      {
        objectTree.Object = TestCase != null ? TestCase.CurrentResult : null;
      }
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}