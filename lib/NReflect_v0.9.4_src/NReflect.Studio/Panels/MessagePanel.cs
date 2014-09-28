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
using System.Collections.Specialized;
using System.Windows.Forms;
using NReflect.Studio.Properties;
using System.Linq;

namespace NReflect.Studio.Panels
{
  public partial class MessagePanel : BasePanel
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
    public MessagePanel()
    {
      InitializeComponent();

      toolStripButtonErrors.CheckState = Settings.Default.MessagesPanelShowErrors;
      toolStripButtonWarnings.CheckState = Settings.Default.MessagesPanelShowWarnings;
      toolStripButtonMessages.CheckState = Settings.Default.MessagesPanelShowInfos;

      CoreData.Instance.Messages.CollectionChanged += Messages_CollectionChanged;
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

    void Messages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if(InvokeRequired)
      {
        BeginInvoke(new NotifyCollectionChangedEventHandler(Messages_CollectionChanged), sender, e);
      }
      else
      {
        if(e.Action == NotifyCollectionChangedAction.Add)
        {
          foreach(Message message in e.NewItems.Cast<Message>())
          {
            DisplayMessage(message);
          }
        }
        else if(e.Action == NotifyCollectionChangedAction.Reset)
        {
          dataGridView.Rows.Clear();
        }
        UpdateButtonText();
      }
    }

    /// <summary>
    /// Displays a new message at the grid.
    /// </summary>
    /// <param name="message">The message to display.</param>
    private void DisplayMessage(Message message)
    {
      DataGridViewRow newRow = new DataGridViewRow {Tag = message};
      newRow.Cells.Add(new DataGridViewImageCell {Value = imageList.Images[message.Severity.ToString()]});
      newRow.Cells.Add(new DataGridViewTextBoxCell {Value = message.MessageText});
      newRow.Cells.Add(new DataGridViewTextBoxCell { Value = message.File });
      newRow.Cells.Add(new DataGridViewTextBoxCell { Value = message.Line });
      newRow.Cells.Add(new DataGridViewTextBoxCell { Value = message.Column });
      UpdateRowVisibility(newRow);

      dataGridView.Rows.Add(newRow);

      UpdateButtonText();
    }

    private void UpdateButtonText()
    {
      int infos = 0;
      int warnings = 0;
      int errors = 0;

      for(int i = 0; i < CoreData.Instance.Messages.Count; ++i)
      {
        switch(CoreData.Instance.Messages[i].Severity)
        {
          case MessageSeverity.Info:
            infos++;
            break;
          case MessageSeverity.Warning:
            warnings++;
            break;
          case MessageSeverity.Error:
            errors++;
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }

      toolStripButtonMessages.Text = infos + " Messages";
      toolStripButtonWarnings.Text = warnings + " Warnings";
      toolStripButtonErrors.Text = errors + " Errors";
    }

    private void UpdateFilter()
    {
      foreach(DataGridViewRow row in dataGridView.Rows)
      {
        UpdateRowVisibility(row);
      }
    }

    private void UpdateRowVisibility(DataGridViewRow row)
    {
      Message message = row.Tag as Message;
      if(message != null)
      {
        if(message.Severity == MessageSeverity.Info)
        {
          row.Visible = toolStripButtonMessages.Checked;
        }
        else if(message.Severity == MessageSeverity.Warning)
        {
          row.Visible = toolStripButtonWarnings.Checked;
        }
        else if(message.Severity == MessageSeverity.Error)
        {
          row.Visible = toolStripButtonErrors.Checked;
        }
        else
        {
          row.Visible = true;
        }
      }
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events

    #endregion

    // ========================================================================
    // Event handling

    #region === Event handling

    private void toolStripFilterButton_Click(object sender, EventArgs e)
    {
      Settings.Default.MessagesPanelShowErrors = toolStripButtonErrors.CheckState;
      Settings.Default.MessagesPanelShowWarnings = toolStripButtonWarnings.CheckState;
      Settings.Default.MessagesPanelShowInfos = toolStripButtonMessages.CheckState;

      UpdateFilter();
    }

    private void toolStripButtonClear_Click(object sender, EventArgs e)
    {
      CoreData.Instance.Messages.Clear();
    }

    #endregion
  }
}