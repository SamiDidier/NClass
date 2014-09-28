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
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace NReflect.Studio.Panels
{
  /// <summary>
  /// A base class for all panels.
  /// </summary>
  public partial class BasePanel : DockContent
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The image of the panel.
    /// </summary>
    private Image image;

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="BasePanel"/>.
    /// </summary>
    protected BasePanel()
    {
      ToolStripMenuItem = new ToolStripMenuItem();
      ToolStripButton = new ToolStripButton();

      InitializeComponent();

      ToolStripButton.CheckOnClick = true;
      ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      ToolStripButton.ImageTransparentColor = Color.Magenta;
      ToolStripButton.Size = new Size(23, 22);

      ToolStripMenuItem.CheckOnClick = true;
      ToolStripMenuItem.Size = new Size(164, 22);

      ToolStripButton.CheckedChanged += toolStripButton_CheckedChanged;
      ToolStripMenuItem.CheckedChanged += toolStripMenuItem_CheckedChanged;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the image of the panel.
    /// </summary>
    public Image Image
    {
      get { return image; }
      set
      {
        image = value;
        ToolStripButton.Image = image;
        ToolStripMenuItem.Image = image;
        Icon = Icon.FromHandle(new Bitmap(image).GetHicon());
      }
    }

    /// <summary>
    /// Gets the menu item which controls the visibility of the panel.
    /// </summary>
    public ToolStripMenuItem ToolStripMenuItem { get; private set; }

    /// <summary>
    /// Gets the tool strip button which controls the visibility of the panel.
    /// </summary>
    public ToolStripButton ToolStripButton { get; private set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.TextChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
    protected override void OnTextChanged(EventArgs e)
    {
      base.OnTextChanged(e);
      ToolStripButton.Text = Text;
      ToolStripMenuItem.Text = Text;
    }

    /// <summary>
    /// Raises the <see cref="DockContent.DockStateChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
    protected override void OnDockStateChanged(EventArgs e)
    {
      base.OnDockStateChanged(e);
      ToolStripButton.Checked = DockState != DockState.Hidden;
      ToolStripMenuItem.Checked = DockState != DockState.Hidden;
    }

    /// <summary>
    /// Gets called after the checked state of the menu item changed.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    void toolStripMenuItem_CheckedChanged(object sender, EventArgs e)
    {
      if(ToolStripMenuItem.Checked)
      {
        Show();
      }
      else
      {
        Hide();
      }
    }

    /// <summary>
    /// Gets called after the checked state of the tool strip button changed.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    void toolStripButton_CheckedChanged(object sender, EventArgs e)
    {
      if(ToolStripButton.Checked)
      {
        Show();
      }
      else
      {
        Hide();
      }
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}