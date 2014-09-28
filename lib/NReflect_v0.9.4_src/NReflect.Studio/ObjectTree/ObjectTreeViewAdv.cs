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

using System.Reflection;
using System.Windows.Forms;
using Aga.Controls.Tree;

namespace NReflect.Studio.ObjectTree
{
  /// <summary>
  /// This tree view improves the abilities of the original <see cref="TreeViewAdv"/>.
  /// </summary>
  public class ObjectTreeViewAdv : TreeViewAdv
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The vertical scrollbar of the tree view.
    /// </summary>
    private VScrollBar vScrollBar;

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectTreeViewAdv"/>.
    /// </summary>
    public ObjectTreeViewAdv()
    {
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets the vertical scrollbar of the treeView.
    /// </summary>
    public VScrollBar VerticalScrollBar
    {
      get
      {
        if(vScrollBar == null)
        {
          FieldInfo vScrollBarField = typeof(TreeViewAdv).GetField("_vScrollBar", BindingFlags.NonPublic | BindingFlags.Instance);
          if (vScrollBarField != null)
          {
            vScrollBar = (VScrollBar)vScrollBarField.GetValue(this);
          }
        }
        return vScrollBar;
      }
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    #endregion

    // ========================================================================
    // Events

    #region === Events

    #endregion
  }
}