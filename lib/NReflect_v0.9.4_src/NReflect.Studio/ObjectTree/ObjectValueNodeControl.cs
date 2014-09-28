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
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using NReflect.Studio.ObjectTree.Model;
using NReflect.Studio.Properties;

namespace NReflect.Studio.ObjectTree
{
  /// <summary>
  /// A control which can be used to display and edit multiple data types.
  /// </summary>
  public class ObjectValueNodeControl : BaseTextControl
  {
    // ========================================================================
    // Fields

    #region === Fields

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectValueNodeControl"/>.
    /// </summary>
    public ObjectValueNodeControl()
    {
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the model creator.
    /// </summary>
    public ObjectTreeModelCreator ModelCreator { get; set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Draws the node using the base class and adds an add and delete icon
    /// if necesarry.
    /// </summary>
    /// <param name="node">The node to draw.</param>
    /// <param name="context">A draw context.</param>
    public override void Draw(TreeNodeAdv node, DrawContext context)
    {
      base.Draw(node, context);
      if(node == null || node.Tag == null || !(node.Tag is ObjectField) || !IsEditEnabled(node))
      {
        return;
      }
      ObjectField field = node.Tag as ObjectField;

      if(field is ObjectFieldCollection)
      {
        DrawButton(context, Resources.Add);
      }
      else if (field.Parent is ObjectFieldCollection)
      {
        DrawButton(context, Resources.Delete);
      }
      else if(field.IsNullable)
      {
        DrawButton(context, Resources.Null, field.Value == null ? new SolidBrush(SystemColors.InactiveCaption) : null);
      }
    }

    /// <summary>
    /// Determines if the <see cref="DrawText"/>-event has to be fired.
    /// </summary>
    /// <param name="node">The node currently drawn.</param>
    /// <returns>Always <c>true</c>.</returns>
    protected override bool DrawTextMustBeFired(TreeNodeAdv node)
    {
      return true;
    }

    /// <summary>
    /// This method is called to determine if the node can be edited.
    /// </summary>
    /// <param name="node">The node in question.</param>
    /// <returns><c>True</c> if the node can be edited, <c>false</c> otherwise.</returns>
    protected override bool CanEdit(TreeNodeAdv node)
    {
      return IsEditEnabled(node) && node != null && node.Tag is ObjectField && !(node.Tag is ObjectFieldCollection) && !(node.Tag is ObjectFieldClass) &&
             (node.Tag as ObjectField).IsSetable && (node.Tag as ObjectField).Value != null;
    }

    /// <summary>
    /// Returns the size of an editor.
    /// </summary>
    /// <param name="context">The context of the editor.</param>
    /// <returns>The size of the editor.</returns>
    protected override Size CalculateEditorSize(EditorContext context)
    {
      return context.Bounds.Size;
    }

    /// <summary>
    /// Applies the changes mede by the user to the model.
    /// </summary>
    /// <param name="node">The changed node.</param>
    /// <param name="editor">The editor used for changing.</param>
    protected override void DoApplyChanges(TreeNodeAdv node, Control editor)
    {
      if (node == null || node.Tag == null || !(node.Tag is ObjectField))
      {
        return;
      }
      ObjectField field = node.Tag as ObjectField;
      // We use a panel to place the editor itself. Get the real editor first.
      editor = editor.Controls[0];

      object newValue = field.Value;
      if (field.Type == typeof(string))
      {
        newValue = editor.Text;
      }
      else if (field.Type == typeof(int) || field.Value is int)
      {
        newValue = (int)((NumericUpDown)editor).Value;
      }
      if (field.Type.IsEnum || field.Type == typeof(bool))
      {
        newValue = ((ComboBox) editor).SelectedItem;
      }

      field.PropertyInfo.SetValue(field.Parent.Value, newValue, null);
      field.Value = newValue;
    }

    /// <summary>
    /// Gets called when a mousebutton is pressed inside the cell.
    /// </summary>
    /// <param name="args">Arguments for the click.</param>
    public override void MouseDown(TreeNodeAdvMouseEventArgs args)
    {
      if(args.Button == MouseButtons.Left && args.ModifierKeys == Keys.None && IsEditEnabled(args.Node))
      {
        ObjectField field = args.Node.Tag as ObjectField;
        if (field is ObjectFieldCollection &&
           GetIconRect(args.ControlBounds, Resources.Add).Contains(args.ViewLocation))
        {
          // Add a new item
          AddNewEntry(field);
          args.Handled = true;
        }
        else if (field != null && field.Parent is ObjectFieldCollection &&
                GetIconRect(args.ControlBounds, Resources.Delete).Contains(args.ViewLocation))
        {
          // Remove item
          RemoveEntry(field);
          args.Handled = true;
        }
        else if (field != null && field.Parent != null && field.IsNullable &&
           GetIconRect(args.ControlBounds, Resources.Null).Contains(args.ViewLocation))
        {
          // Set / Unset to null
          object newValue = null;
          if(field.Value == null)
          {
            newValue = GetNewValue(field);
          }
          else
          {
            args.Handled = true;
          }
          field.PropertyInfo.SetValue(field.Parent.Value, newValue, null);
          field.Value = newValue;
        }
      }
      if (!args.Handled)
      {
        base.MouseDown(args);
      }
    }

    /// <summary>
    /// Adds a field to the parent <see cref="ObjectFieldCollection"/>.
    /// </summary>
    /// <param name="field">The field to add.</param>
    private void AddNewEntry(ObjectField field)
    {
      object newValue = GetNewValue(field);

      ObjectFieldDictionary parentDictionary = field as ObjectFieldDictionary;
      if(parentDictionary != null)
      {
        // Find a new key value
        string key;
        for (int i = 1; ; ++i)
        {
          key = "Key" + i;
          if (!parentDictionary.Items.ContainsKey(key))
          {
            break;
          }
        }

        ObjectField newField = ModelCreator.CreateObjectField("[" + key + "]", newValue, parentDictionary);
        newField.NameTag = key;
        parentDictionary.Add(newField);
        return;
      }

      ObjectFieldList parentList = field as ObjectFieldList;
      if(parentList != null)
      {
        ObjectField newField = ModelCreator.CreateObjectField("[" + (parentList.Items.Count + 1) + "]", newValue, parentList);
        parentList.Add(newField);
      }
    }

    /// <summary>
    /// Removes an entry from the parent <see cref="ObjectFieldCollection"/>.
    /// </summary>
    /// <param name="field">The field to remove.</param>
    private void RemoveEntry(ObjectField field)
    {
      ObjectFieldDictionary parentDictionary = field.Parent as ObjectFieldDictionary;
      if (parentDictionary != null)
      {
        parentDictionary.Remove(field);

        return;
      }

      ObjectFieldList parentList = field.Parent as ObjectFieldList;
      if (parentList != null)
      {
        parentList.Remove(field);
      }
    }

    /// <summary>
    /// Creates an editor for a node.
    /// </summary>
    /// <param name="node">The node to create the editor for.</param>
    /// <returns>The editor.</returns>
    protected override Control CreateEditor(TreeNodeAdv node)
    {
      if(node == null || node.Tag == null || !(node.Tag is ObjectField))
      {
        return null;
      }
      ObjectField field = node.Tag as ObjectField;

      if(field.Type == typeof(string))
      {
        TextBox textBox = CreateTextBox(node);
        textBox.Text = field.Value as string;
        textBox.KeyDown += editorControl_KeyDown;

        return GetEditControlPanel(textBox);
      }
      if(field.Type == typeof(int) || (field.Type == typeof(object) && field.Value is int))
      {
        NumericUpDown numericUpDown = CreateNumericUpDown(node);
        numericUpDown.Value = (int) field.Value;
        numericUpDown.KeyDown += editorControl_KeyDown;

        return GetEditControlPanel(numericUpDown);
      }
      if(field.Type.IsEnum)
      {
        ComboBox comboBox = CreateComboBox(node);
        foreach(object enumValue in field.Value.GetType().GetEnumValues())
        {
          comboBox.Items.Add(enumValue);
        }
        comboBox.SelectedItem = field.Value;
        comboBox.KeyDown += editorControl_KeyDown;

        return GetEditControlPanel(comboBox);
      }
      if (field.Type == typeof(bool) || (field.Type == typeof(object) && field.Value is bool))
      {
        ComboBox comboBox = CreateComboBox(node);
        comboBox.Items.Add(true);
        comboBox.Items.Add(false);
        comboBox.SelectedItem = field.Value;
        comboBox.KeyDown += editorControl_KeyDown;

        return GetEditControlPanel(comboBox);
      }
      Label label = new Label {Text = "No editor available for " + field.TypeName};
      label.Height = label.Font.Height;
      Panel panel = GetEditControlPanel(label);
      panel.BackColor = Color.Red;
      return panel;
    }

    /// <summary>
    /// Cleans up.
    /// </summary>
    /// <param name="editor">The editor to dispose.</param>
    protected override void DisposeEditor(Control editor)
    {
      editor.KeyDown -= editorControl_KeyDown;
    }

    /// <summary>
    /// Gets called when the text of a node is drawn.
    /// </summary>
    /// <param name="args">More information about the ebent.</param>
    protected override void OnDrawText(DrawEventArgs args)
    {
      base.OnDrawText(args);
      if(args.Node != null && args.Node.Tag is ObjectField && !((ObjectField)args.Node.Tag).IsSetable)
      {
        args.TextColor = SystemColors.GrayText;
      }
    }

    /// <summary>
    /// Gets called when a key is pressed while the text box editor is displayed.
    /// </summary>
    /// <param name="sender">The issuer of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void editorControl_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        EndEdit(false);
      }
      else if (e.KeyCode == Keys.Enter)
      {
        EndEdit(true);
      }
    }

    /// <summary>
    /// Gets a new value for a field.
    /// </summary>
    /// <param name="field">The field to get the new value for.</param>
    /// <returns>The new value.</returns>
    private object GetNewValue(ObjectField field)
    {
      object newValue = OnNewValueNeeded(field);
      if(newValue != null)
      {
        return newValue;
      }
      Type type = field.Type;
      if(field is ObjectFieldCollection)
      {
        if(field.Value is IDictionary)
        {
          type = field.Type.GetProperty("Item").PropertyType;
        }
        else if (field.Value is IList)
        {
          type = field.Type.GetProperty("Item").PropertyType;
        }
      }
      if(type == typeof(string))
      {
        return "";
      }
      if(type == typeof(int))
      {
        return 0;
      }
      if(type == typeof(bool))
      {
        return false;
      }
      return Activator.CreateInstance(type);
    }

    /// <summary>
    /// Calculates a rectangle which displays the icon to add or remove
    /// an entry.
    /// </summary>
    /// <param name="bounds">The bounds of the cell.</param>
    /// <param name="bitmap">The bitmap to place inside the cell.</param>
    /// <returns>A rectangle containing the location and size of the icon.</returns>
    private Rectangle GetIconRect(Rectangle bounds, Bitmap bitmap)
    {
      int x = bounds.Right - bitmap.Width - LeftMargin - 4;
      int y = bounds.Y + (bounds.Height - bitmap.Height)/2;
      return new Rectangle(x, y, bitmap.Width, bitmap.Height);
    }

    /// <summary>
    /// Draws a button with a border arround it.
    /// </summary>
    /// <param name="context">The draw context to use.</param>
    /// <param name="bitmap">The bitmap of the button.</param>
    /// <param name="bgBrush">A brush used for filling the background if set.</param>
    private void DrawButton(DrawContext context, Bitmap bitmap, Brush bgBrush = null)
    {
      Rectangle iconRect = GetIconRect(context.Bounds, bitmap);
      Rectangle borderRect = new Rectangle(iconRect.X - 1, iconRect.Y - 1, iconRect.Width + 2, iconRect.Height +2);
      context.Graphics.FillRectangle(bgBrush ?? new SolidBrush(SystemColors.Window), borderRect);
      context.Graphics.DrawRectangle(new Pen(SystemColors.ButtonShadow), borderRect);
      context.Graphics.DrawImageUnscaled(bitmap, iconRect);
    }

    /// <summary>
    /// Creates a new combo box which can be used as an editor control.
    /// </summary>
    /// <param name="node">The node to create the editor for.</param>
    /// <returns>The new combo box.</returns>
    private ComboBox CreateComboBox(TreeNodeAdv node)
    {
      ComboBox comboBox = new ComboBox {DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Flat};
      SetEditControlProperties(comboBox, node);
      return comboBox;
    }

    /// <summary>
    /// Creates a new numeric up down control which can be used as an editor control.
    /// </summary>
    /// <param name="node">The node to create the editor for.</param>
    /// <returns>The new numeric up down control.</returns>
    private NumericUpDown CreateNumericUpDown(TreeNodeAdv node)
    {
      NumericUpDown numericUpDown = new NumericUpDown {TextAlign = TextAlign, BorderStyle = BorderStyle.None};
      SetEditControlProperties(numericUpDown, node);
      return numericUpDown;
    }

    /// <summary>
    /// Creates a new text box which can be used as an editor control.
    /// </summary>
    /// <param name="node">The node to create the editor for.</param>
    /// <returns>The new text box.</returns>
    private TextBox CreateTextBox(TreeNodeAdv node)
    {
      TextBox textBox = new TextBox {TextAlign = TextAlign, BorderStyle = BorderStyle.None, Font = Font};
      SetEditControlProperties(textBox, node);
      return textBox;
    }

    /// <summary>
    /// Creates a panel and puts the given control into it such
    /// the control is always vertically centered.
    /// </summary>
    /// <param name="control">The control to put on the panel.</param>
    /// <returns>The panel.</returns>
    private static Panel GetEditControlPanel(Control control)
    {
      Panel panel = new Panel {BorderStyle = BorderStyle.FixedSingle};
      panel.Resize += (sender, args) =>
                        {
                          control.Width = panel.Width;
                          control.Location = new Point(1, (panel.Height - control.Height - 1)/2 - 1);
                        };
      panel.GotFocus += (sender, args) => control.Focus();
      panel.Controls.Add(control);
      return panel;
    }

    #region --- OnXXX

    /// <summary>
    /// Fires the <see cref="NewValueNeeded"/> event.
    /// </summary>
    /// <param name="field">The <see cref="ObjectField"/> the new value is needed for.</param>
    /// <returns>The new value.</returns>
    protected object OnNewValueNeeded(ObjectField field)
    {
      if (NewValueNeeded != null)
      {
        NewValueEventArgs eventArgs = new NewValueEventArgs(field);
        NewValueNeeded(this, eventArgs);
        return eventArgs.Value;
      }
      return null;
    }

    #endregion

    #endregion

    // ========================================================================
    // Events

    #region === Events

    /// <summary>
    /// Occures when a new value is needed.
    /// </summary>
    public event EventHandler<NewValueEventArgs> NewValueNeeded;

    #endregion
  }
}