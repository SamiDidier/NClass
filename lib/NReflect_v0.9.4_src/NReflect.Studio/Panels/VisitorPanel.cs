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
using System.ComponentModel;
using FastColoredTextBoxNS;
using NReflect.Studio.Models;
using NReflect.Studio.Properties;
using NReflect.Studio.Visitor;

namespace NReflect.Studio.Panels
{
  public partial class VisitorPanel : BaseTestCasePanel
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The configuration of the currently selected visitor.
    /// </summary>
    private VisitorConfig visitorConfig;

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="VisitorPanel"/>.
    /// </summary>
    public VisitorPanel()
    {
      InitializeComponent();

      LoadVisitors();

      if(cboVisitor.Items.Contains(Settings.Default.VisitorPanelVisitor))
      {
        cboVisitor.SelectedItem = Settings.Default.VisitorPanelVisitor;
      }
      else if(cboVisitor.Items.Count > 0)
      {
        cboVisitor.SelectedIndex = 0;
      }

    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets the name of the currently selected visitor.
    /// </summary>
    private string SelectedVisitorName
    {
      get { return cboVisitor.SelectedItem.ToString(); }
    }

    #endregion

    // ========================================================================
    // Event-Handling

    #region === Event-Handling

    private void cboVisitor_SelectedIndexChanged(object sender, EventArgs e)
    {
      Settings.Default.VisitorPanelVisitor = SelectedVisitorName;

      if(visitorConfig != null)
      {
        visitorConfig.PropertyChanged -= visitorConfig_PropertyChanged;
      }
      visitorConfig = VisitorManager.Instance.GetVisitorConfig(SelectedVisitorName);
      if(visitorConfig != null)
      {
        txtVisitorResult.Language = visitorConfig.ViewHighlighting;
        visitorConfig.PropertyChanged += visitorConfig_PropertyChanged;
      }
      else
      {
        txtVisitorResult.Language = Language.Custom;
      }

      RunVisitor(SelectedVisitorName);
    }

    /// <summary>
    /// Gets called after the configuration of the visitor has changed.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    void visitorConfig_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      RunVisitor(SelectedVisitorName);
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Load all visitors.
    /// </summary>
    private void LoadVisitors()
    {
      cboVisitor.Items.Clear();
      foreach(string visitor in VisitorManager.Instance.VisitorNames)
      {
        cboVisitor.Items.Add(visitor);
      }
    }

    /// <summary>
    /// Runs the currently selected visitor.
    /// </summary>
    /// <param name="visitorName">The name of the visitor to run.</param>
    private void RunVisitor(string visitorName)
    {
      if(TestCase != null && TestCase.CurrentResult != null)
      {
        try
        {
          txtVisitorResult.Text = VisitorManager.Instance.RunVisitor(TestCase.CurrentResult.NRAssembly, visitorName);
        }
        catch(Exception e)
        {
          txtVisitorResult.Text = "An error occured while running the visitor: \n" + e.Message;
        }
      }
      else
      {
        txtVisitorResult.Text = "";
      }
    }

    /// <summary>
    /// Gets called after the test case has changed.
    /// </summary>
    /// <param name="oldTestCase">The test case which was selected before the change.</param>
    /// <param name="newTestCase">The test case which is selected now.</param>
    protected override void OnTestCaseChanged(TestCase oldTestCase, TestCase newTestCase)
    {
      RunVisitor(cboVisitor.SelectedItem.ToString());
    }

    /// <summary>
    /// Gets called after a property of the current test case changed.
    /// </summary>
    /// <param name="propertyName">The name of the changed property.</param>
    protected override void OnTestCasePropertyChanged(string propertyName)
    {
      RunVisitor(cboVisitor.SelectedItem.ToString());
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}