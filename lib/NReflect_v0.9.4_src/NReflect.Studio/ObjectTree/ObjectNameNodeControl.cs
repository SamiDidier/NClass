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

using System.Collections;
using System.Windows.Forms;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using NReflect.Studio.ObjectTree.Model;

namespace NReflect.Studio.ObjectTree
{
  public class ObjectNameNodeControl : NodeTextBox
  {
    // ========================================================================
    // Fields

    #region === Fields

     

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectNameNodeControl"/>.
    /// </summary>
    public ObjectNameNodeControl()
    {
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
    /// This method is called to determine if the node can be edited.
    /// </summary>
    /// <param name="node">The node in question.</param>
    /// <returns><c>True</c> if the node can be edited, <c>false</c> otherwise.</returns>
    protected override bool CanEdit(TreeNodeAdv node)
    {
      ObjectField field = node.Tag as ObjectField;
      return IsEditEnabled(node) && field != null && field.Parent is ObjectFieldDictionary && field.NameTag is string;
    }

    /// <summary>
    /// Creates an editor for a node.
    /// </summary>
    /// <param name="node">The node to create the editor for.</param>
    /// <returns>The editor.</returns>
    protected override Control CreateEditor(TreeNodeAdv node)
    {
      ObjectField field = node.Tag as ObjectField;

      Control control = base.CreateEditor(node);
      if(field != null)
      {
        control.Text = field.NameTag.ToString();
      }

      return control;
    }

    /// <summary>
    /// Applies the changes mede by the user to the model.
    /// </summary>
    /// <param name="node">The changed node.</param>
    /// <param name="editor">The editor used for changing.</param>
    protected override void DoApplyChanges(TreeNodeAdv node, Control editor)
    {
      ObjectField field = node.Tag as ObjectField;
      if(field != null && field.Parent is ObjectFieldDictionary && field.NameTag is string)
      {
        ObjectFieldDictionary dictionaryField = field.Parent as ObjectFieldDictionary;
        IDictionary dictionary = dictionaryField.Value as IDictionary;

        if(dictionary != null)
        {
          dictionaryField.Items.Remove(field.NameTag);
          dictionary.Remove(field.NameTag);

          field.NameTag = editor.Text;
          field.Name = "[" + editor.Text + "]";

          dictionaryField.Items.Add(field.NameTag, field);
          dictionary.Add(field.NameTag, field.Value);

          editor.Text = field.Name;
        }
      }
      base.DoApplyChanges(node, editor);
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}