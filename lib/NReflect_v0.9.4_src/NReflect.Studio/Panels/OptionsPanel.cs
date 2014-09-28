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
using System.Reflection;
using System.Windows.Forms;
using NReflect.Studio.Properties;
using NReflect.Studio.Visitor;
using System.Linq;

namespace NReflect.Studio.Panels
{
  public partial class OptionsPanel : BasePanel
  {
    // ========================================================================
    // Fields

    #region === Fields

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="OptionsPanel"/>.
    /// </summary>
    public OptionsPanel()
    {
      InitializeComponent();

      LoadVisitorConfigPanels();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties


    #endregion

    // ========================================================================
    // Event-Handling

    #region === Event-Handling


    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Loads the panels for the visitor configs.
    /// </summary>
    private void LoadVisitorConfigPanels()
    {
      Assembly assembly = typeof(VisitorConfigPanel<>).Assembly;

      foreach(Type visitor in VisitorManager.Instance.VisitorTypes)
      {
        Type panelType = typeof(VisitorConfigPanel<>).MakeGenericType(visitor);
        tabMain.TabPages.AddRange(
          assembly.GetTypes().Where(t => t.IsSubclassOf(panelType))
                  .Select(Activator.CreateInstance)
                  .Cast<UserControl>()
                  .Select(panel =>
                            {
                              TabPage page = new TabPage(panel.Text);
                              page.Controls.Add(panel);
                              panel.Dock = DockStyle.Fill;
                              return page;
                            })
                  .ToArray());
      }
    }

    /// <summary>
    /// Gets called after the choose button is pushed.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void cmdChooseTestCaseDirectory_Click(object sender, EventArgs e)
    {
      folderBrowserDialog.SelectedPath = txtTestCaseDirectory.Text;
      if(folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
      {
        Settings.Default.TestCaseDirectory = folderBrowserDialog.SelectedPath;
        CoreData.Instance.LoadTestCases();
      }
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}