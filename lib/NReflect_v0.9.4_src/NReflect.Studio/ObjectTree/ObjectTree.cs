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
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using NReflect.Studio.ObjectTree.Model;
using System.Linq;

namespace NReflect.Studio.ObjectTree
{
  public partial class ObjectTree : UserControl
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The control for the name column.
    /// </summary>
    private readonly ObjectNameNodeControl objectNameNodeControl;

    /// <summary>
    /// The control for the value column.
    /// </summary>
    private readonly ObjectValueNodeControl objectValueNodeControl;

    /// <summary>
    /// A value indicating if the tree should be editable.
    /// </summary>
    private bool editable = true;
    /// <summary>
    /// A value indicating if the columns of the tree should be resized automatically.
    /// </summary>
    private bool autoResizeColumns;

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectTree"/>.
    /// </summary>
    public ObjectTree()
    {
      InitializeComponent();

      objectNameNodeControl = new ObjectNameNodeControl
                                {
                                  DataPropertyName = "Name",
                                  LeftMargin = 3,
                                  ParentColumn = colMember,
                                  EditEnabled = true,
                                  EditOnClick = true,
                                  Font = trvTree.Font
                                };
      trvTree.NodeControls.Insert(0, objectNameNodeControl);

      objectValueNodeControl = new ObjectValueNodeControl
                                 {
                                   DataPropertyName = "ValueText",
                                   LeftMargin = 0,
                                   ParentColumn = colValue,
                                   EditEnabled = true,
                                   EditOnClick = true,
                                   Font = trvTree.Font
                                 };
      objectValueNodeControl.NewValueNeeded += objectValueNodeControl_NewValueNeeded;
      trvTree.NodeControls.Insert(1, objectValueNodeControl);
      trvTree.MouseWheel += trvTree_MouseWheel;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the root of the tree.
    /// </summary>
    public object Object
    {
      get { return TreeModel == null ? null : (TreeModel.Root == null ? null : TreeModel.Root.Value); }
      set
      {
        TreeModel = new ObjectTreeModel(trvTree, value);
        TreeModel.DirtyChanged += treeModel_DirtyChanged;
        TreeModel.ValueChanged += treeModel_ValueChanged;
        TreeModel.ItemAdded += treeModel_ItemAdded;
        TreeModel.ItemRemoved += treeModel_ItemRemoved;
        objectValueNodeControl.ModelCreator = TreeModel.ModelCreator;
        trvTree.Model = TreeModel;
        ResizeColumns();
      }
    }

    /// <summary>
    /// The tree model for the tree view.
    /// </summary>
    public ObjectTreeModel TreeModel { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating if the tree should be editable.
    /// </summary>
    public bool Editable
    {
      get { return editable; }
      set
      {
        if(editable != value)
        {
          editable = value;
          objectValueNodeControl.EditEnabled = value;
          objectNameNodeControl.EditEnabled = value;
          Invalidate();
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating if the columns should be resized automatically.
    /// </summary>
    public bool AutoResizeColumns
    {
      get { return autoResizeColumns; }
      set
      {
        autoResizeColumns = value;
        if(value)
        {
          ResizeColumns();
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating the dirty status of the model.
    /// </summary>
    public bool Dirty
    {
      get
      {
        if(TreeModel == null)
        {
          return false;
        }
        return TreeModel.Dirty;
      }
      set
      {
        if(TreeModel != null)
        {
          TreeModel.Dirty = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets the currently selected <see cref="ObjectField"/>.
    /// </summary>
    public ObjectField SelectedField
    {
      get { return trvTree.SelectedNode != null ? trvTree.SelectedNode.Tag as ObjectField : null; }
      set { trvTree.SelectedNode = trvTree.FindNodeByTag(value); }
    }

    /// <summary>
    /// Gets the first node which is visible to the user.
    /// </summary>
    public ObjectField FirstVisibleField
    {
      get
      {
        TreeNodeAdv node = trvTree.GetNodeAt(new Point(1, trvTree.RowHeight));
        return node != null ? node.Tag as ObjectField : null;
      }
      set
      {
        TreeNodeAdv node = trvTree.FindNodeByTag(value);
        if(node != null)
        {
          trvTree.BeginUpdate();
          trvTree.ScrollTo(trvTree.AllNodes.LastOrDefault());
          trvTree.ScrollTo(node);
          trvTree.EndUpdate();
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating if the type column is visible.
    /// </summary>
    public bool TypeColumnVisible
    {
      get { return colType.IsVisible; }
      set
      {
        colType.IsVisible = value;
        ResizeColumns();
      }
    }

    /// <summary>
    /// Gets or sets the field which is displayed as root.
    /// </summary>
    public ObjectField RootField
    {
      get { return TreeModel == null ? null : TreeModel.DisplayRoot; }
      set
      {
        if(TreeModel != null)
        {
          TreeModel.DisplayRoot = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating if a node can be draged.
    /// </summary>
    public bool StartDrag { get; set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Resizes the columns so they fit best.
    /// </summary>
    private void ResizeColumns()
    {
      if(AutoResizeColumns)
      {
        trvTree.AutoSizeColumn(colValue);
        trvTree.AutoSizeColumn(colType);

        int currentWidth = trvTree.Columns.Where(column => column.IsVisible).Sum(column => column.Width);
        int delta = trvTree.ClientSize.Width - currentWidth - (trvTree.VerticalScrollBar != null ? (trvTree.VerticalScrollBar.Visible ? trvTree.VerticalScrollBar.Width : 0) : 0);
        colValue.Width += delta;
        colValue.Width = Math.Max(colValue.Width, 50);
      }
    }

    /// <summary>
    /// Expands the node for the given <see cref="ObjectField"/>.
    /// </summary>
    /// <param name="otherField">The node of this <see cref="ObjectField"/> will be expanded if found.</param>
    public void Expand(ObjectField otherField)
    {
      TreeNodeAdv node = trvTree.FindNodeByTag(otherField);
      if(node != null)
      {
        node.Expand();
      }
    }

    /// <summary>
    /// Collaps the node for the given <see cref="ObjectField"/>.
    /// </summary>
    /// <param name="otherField">The node of this <see cref="ObjectField"/> will be collapsed if found.</param>
    public void Collapse(ObjectField otherField)
    {
      TreeNodeAdv node = trvTree.FindNodeByTag(otherField);
      if(node != null)
      {
        node.Collapse();
      }
    }

    /// <summary>
    /// Expands all nodes of the tree.
    /// </summary>
    public void ExpandAll()
    {
      trvTree.ExpandAll();
    }

    /// <summary>
    /// Collapses all nodes of the tree.
    /// </summary>
    public void CollapseAll()
    {
      trvTree.CollapseAll();
    }

    /// <summary>
    /// Synchronizes the state of this tree view with the given one. So nodes
    /// which are expanded on the source tree view will expanded here and so on.
    /// </summary>
    /// <param name="otherTree">The source tree view.</param>
    public void SynchronizeTreeView(ObjectTree otherTree)
    {
      SynchronizeTreeView(otherTree.TreeModel.Root, otherTree);
    }

    /// <summary>
    /// Synchronizes the state of a node and its child nodes.
    /// </summary>
    /// <param name="otherField">The current field from the other tree view.</param>
    /// <param name="otherTree">The source tree view.</param>
    private void SynchronizeTreeView(ObjectField otherField, ObjectTree otherTree)
    {
      if(otherField == null)
      {
        return;
      }
      TreeNodeAdv otherNode = otherTree.trvTree.FindNodeByTag(otherField);
      TreeNodeAdv equivalentNode = trvTree.FindNodeByTag(GetEquivalentField(otherField));
      if(otherNode != null && equivalentNode != null)
      {
        equivalentNode.IsExpanded = otherNode.IsExpanded;
        equivalentNode.IsSelected = otherNode.IsSelected;
      }
      foreach(ObjectField childField in otherField.Childs)
      {
        SynchronizeTreeView(childField, otherTree);
      }
    }

    /// <summary>
    /// Gets the <see cref="ObjectField"/> which is eqivalent to the given field
    /// within this tree.
    /// </summary>
    /// <param name="field">The field to search the equivalent for.</param>
    /// <returns>The found field or null.</returns>
    public ObjectField GetEquivalentField(ObjectField field)
    {
      if(field == null || field.Parent == null)
      {
        return TreeModel.Root;
      }
      ObjectField equivalentParentField = GetEquivalentField(field.Parent);
      ObjectField equivalentField = null;

      ObjectFieldClass equivalentParentFieldClass = equivalentParentField as ObjectFieldClass;
      if(equivalentParentFieldClass != null)
      {
        equivalentField =
          equivalentParentFieldClass.ChildFields.FirstOrDefault(childField => childField.Name == field.Name);
      }
      ObjectFieldList equivalentParentFieldList = equivalentParentField as ObjectFieldList;
      if(equivalentParentFieldList != null)
      {
        equivalentField = equivalentParentFieldList.Items.FirstOrDefault(item => item.Name == field.Name);
      }
      ObjectFieldDictionary equivalentParentFieldDictionary = equivalentParentField as ObjectFieldDictionary;
      if(equivalentParentFieldDictionary != null)
      {
        equivalentField = equivalentParentFieldDictionary.Items.Values.FirstOrDefault(item => item.Name == field.Name);
      }

      return equivalentField;
    }

    /// <summary>
    /// Adds an extra column to the displayed columns.
    /// </summary>
    /// <param name="index">The index to insert the column to.</param>
    /// <param name="column">The column to add.</param>
    /// <param name="nodeControl">The node control used to display the values.</param>
    public void AddExtraColumn(int index, TreeColumn column, NodeControl nodeControl)
    {
      if (index <= 0)
      {
        throw new ArgumentException("index must be greater than zero.");
      }
      trvTree.Columns.Insert(index, column);
      trvTree.NodeControls.Add(nodeControl);
    }

    /// <summary>
    /// Adds an extra column to the end of the displayed columns.
    /// </summary>
    /// <param name="column">The column to add.</param>
    /// <param name="nodeControl">The node control used to display the values.</param>
    public void AddExtraColumn(TreeColumn column, NodeControl nodeControl)
    {
      AddExtraColumn(trvTree.Columns.Count, column, nodeControl);
    }

    /// <summary>
    /// Gets called when a value of the model changed.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Information about the event.</param>
    private void treeModel_ValueChanged(object sender, ValueChangedEventArgs e)
    {
      Invalidate(true);
      OnValueChanged(e);
    }

    /// <summary>
    /// Gets called after the dirty status of the model has changed.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Information about the event.</param>
    private void treeModel_DirtyChanged(object sender, EventArgs e)
    {
      OnDirtyChanged();
    }

    /// <summary>
    /// Gets called after a new field was added to a collection field.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Information about the event.</param>
    void treeModel_ItemAdded(object sender, ObjectFieldEventArgs e)
    {
      OnValueChanged(new ValueChangedEventArgs(null, e.ObjectField, e.ObjectField));
    }

    /// <summary>
    /// Gets called after a field was removed from a collection field.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Information about the event.</param>
    void treeModel_ItemRemoved(object sender, ObjectFieldEventArgs e)
    {
      OnValueChanged(new ValueChangedEventArgs(e.ObjectField, null, e.ObjectField));
    }

    /// <summary>
    /// Gets called when a new value for the tree is needed by the model.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Information about the event.</param>
    private void objectValueNodeControl_NewValueNeeded(object sender, NewValueEventArgs e)
    {
      OnNewValueNeeded(e);
    }

    /// <summary>
    /// Gets called when a drag'n'drop operation is about to start.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void trvTree_ItemDrag(object sender, ItemDragEventArgs e)
    {
      TreeNodeAdv[] nodes = e.Item as TreeNodeAdv[];
      if(nodes != null && nodes.Length == 1)
      {
        ObjectField field = nodes[0].Tag as ObjectField;
        if(field != null && field.Parent is ObjectFieldList)
        {
          trvTree.DoDragDropSelectedNodes(DragDropEffects.Move | DragDropEffects.Copy);
        }
      }
    }

    /// <summary>
    /// Gets called when an item is draged over the treeview.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void trvTree_DragOver(object sender, DragEventArgs e)
    {
      if(e.Data.GetDataPresent(typeof(TreeNodeAdv[])) && trvTree.DropPosition.Node != null)
      {
        TreeNodeAdv[] nodes = e.Data.GetData(typeof(TreeNodeAdv[])) as TreeNodeAdv[];
        TreeNodeAdv parent = trvTree.DropPosition.Node;
        if(nodes != null && nodes.Length == 1 && parent != null && Editable)
        {
          ObjectField draggedField = nodes[0].Tag as ObjectField;
          ObjectField destField = parent.Tag as ObjectField;
          if(draggedField != null && destField != null && trvTree.DropPosition.Position != NodePosition.Inside)
          {
            bool ownNode = trvTree.FindNodeByTag(draggedField) != null;
            if(ownNode && draggedField.Parent == destField.Parent)
            {
              e.Effect = DragDropEffects.Move;
              return;
            }
            if(!ownNode)
            {
              ObjectFieldCollection parentDraggedCollection = draggedField.Parent as ObjectFieldCollection;
              ObjectFieldCollection parentDestCollection = destField.Parent as ObjectFieldCollection;
              if(parentDraggedCollection != null && parentDestCollection != null && parentDraggedCollection.ItemType == parentDestCollection.ItemType)
              {
                e.Effect = DragDropEffects.Copy;
                return;
              }
            }
          }
          e.Effect = DragDropEffects.None;
        }
      }
    }

    /// <summary>
    /// Gets called when an item is dropped over the treeview.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void trvTree_DragDrop(object sender, DragEventArgs e)
    {
      if(e.Data.GetDataPresent(typeof(TreeNodeAdv[])) && trvTree.DropPosition.Node != null)
      {
        TreeNodeAdv[] nodes = e.Data.GetData(typeof(TreeNodeAdv[])) as TreeNodeAdv[];
        TreeNodeAdv parent = trvTree.DropPosition.Node;
        if(nodes != null && nodes.Length == 1 && parent != null)
        {
          ObjectField draggedField = nodes[0].Tag as ObjectField;
          ObjectField destField = parent.Tag as ObjectField;
          if(draggedField != null && destField != null)
          {
            bool ownNode = trvTree.FindNodeByTag(draggedField) != null;
            if(ownNode)
            {
              ObjectFieldList parentList = destField.Parent as ObjectFieldList;
              if(parentList != null)
              {
                int index = parentList.Items.IndexOf(destField);
                index = trvTree.DropPosition.Position == NodePosition.After ? index + 1 : index;
                bool expanded = nodes[0].IsExpanded;
                parentList.Move(draggedField, index);
                trvTree.FindNodeByTag(draggedField).IsExpanded = expanded;
              }
            }
            else
            {
              ObjectFieldDictionary parentDestDict = destField.Parent as ObjectFieldDictionary;
              ObjectFieldList parentDestList = destField.Parent as ObjectFieldList;
              ObjectField clonedField = draggedField.Clone() as ObjectField;
              if(clonedField != null)
              {
                clonedField.Parent = destField.Parent;
                //TODO: The cloned field still wraps the uncloned value. Cloning this is not easy since
                //      we can't use Clone() on it since it could not be implemented.
                if (parentDestDict != null)
                {
                  parentDestDict.Add(clonedField);
                }
                else if(parentDestList != null)
                {
                  int index = parentDestList.Items.IndexOf(destField);
                  index += trvTree.DropPosition.Position == NodePosition.After ? 1 : 0;
                  parentDestList.Insert(index, clonedField);
                }
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// Gets called after a node was expanded.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void trvTree_Expanded(object sender, TreeViewAdvEventArgs e)
    {
      OnExpanded(new ObjectFieldEventArgs(e.Node.Tag as ObjectField));
      ResizeColumns();
    }

    /// <summary>
    /// Gets called after a node was collapsed.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void trvTree_Collapsed(object sender, TreeViewAdvEventArgs e)
    {
      OnCollapsed(new ObjectFieldEventArgs(e.Node.Tag as ObjectField));
      ResizeColumns();
    }

    /// <summary>
    /// Gets called after a new node was selected.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void trvTree_SelectionChanged(object sender, EventArgs e)
    {
      OnSelectionChanged(
        new ObjectFieldEventArgs(trvTree.SelectedNode != null ? trvTree.SelectedNode.Tag as ObjectField : null));
    }

    /// <summary>
    /// Gets called after the mouse wheel was used.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void trvTree_MouseWheel(object sender, MouseEventArgs e)
    {
      OnScroll(new ScrollEventArgs(e.Delta > 0 ? ScrollEventType.SmallIncrement : ScrollEventType.SmallDecrement, -1));
    }

    /// <summary>
    /// Gets called after the tree view was scrolled.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void trvTree_Scroll(object sender, ScrollEventArgs e)
    {
      if(e.ScrollOrientation == ScrollOrientation.VerticalScroll)
      {
        OnScroll(e);
      }
    }

    /// <summary>
    /// Gets called when a key was pressed while the tree view has the focus.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">More information about the event.</param>
    private void trvTree_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Enter)
      {
        if(trvTree.SelectedNode != null && trvTree.SelectedNode.Tag is ObjectFieldValue)
        {
          ObjectFieldValue valueField = trvTree.SelectedNode.Tag as ObjectFieldValue;
          if(valueField.Type == typeof(bool))
          {
            valueField.Value = !((bool) valueField.Value);
            return;
          }
        }
        objectValueNodeControl.BeginEdit();
      }
    }

    #region --- OnXXX

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      ResizeColumns();
    }

    /// <summary>
    /// Fires the <see cref="ValueChanged"/> event.
    /// </summary>
    /// <param name="args">Information about the event.</param>
    protected void OnValueChanged(ValueChangedEventArgs args)
    {
      if (ValueChanged != null)
      {
        ValueChanged(this, args);
      }
    }

    /// <summary>
    /// Fires the <see cref="DirtyChanged"/> event.
    /// </summary>
    protected void OnDirtyChanged()
    {
      if(DirtyChanged != null)
      {
        DirtyChanged(this, EventArgs.Empty);
      }
    }

    /// <summary>
    /// Fires the <see cref="NewValueNeeded"/> event.
    /// </summary>
    /// <param name="args">Information about the event.</param>
    protected void OnNewValueNeeded(NewValueEventArgs args)
    {
      if(NewValueNeeded != null)
      {
        NewValueNeeded(this, args);
      }
    }

    /// <summary>
    /// Fires the <see cref="Expanded"/> event.
    /// </summary>
    /// <param name="args">Information about the event.</param>
    protected void OnExpanded(ObjectFieldEventArgs args)
    {
      if(Expanded != null)
      {
        Expanded(this, args);
      }
    }

    /// <summary>
    /// Fires the <see cref="Collapsed"/> event.
    /// </summary>
    /// <param name="args">Information about the event.</param>
    protected void OnCollapsed(ObjectFieldEventArgs args)
    {
      if(Collapsed != null)
      {
        Collapsed(this, args);
      }
    }

    /// <summary>
    /// Fires the <see cref="SelectionChanged"/> event.
    /// </summary>
    /// <param name="args">Information about the event.</param>
    protected void OnSelectionChanged(ObjectFieldEventArgs args)
    {
      if(SelectionChanged != null)
      {
        SelectionChanged(this, args);
      }
    }

    #endregion

    #endregion

    // ========================================================================
    // Events

    #region === Events

    /// <summary>
    /// Occures when a value of an <see cref="ObjectField"/> of the model changed.
    /// </summary>
    public event EventHandler<ValueChangedEventArgs> ValueChanged;

    /// <summary>
    /// Occures when the dirty status of the model changed.
    /// </summary>
    public event EventHandler<EventArgs> DirtyChanged;

    /// <summary>
    /// Occures when a new value is needed.
    /// </summary>
    public event EventHandler<NewValueEventArgs> NewValueNeeded;

    /// <summary>
    /// Occures when a node was expanded.
    /// </summary>
    public event EventHandler<ObjectFieldEventArgs> Expanded;

    /// <summary>
    /// Occures when a node was collapsed.
    /// </summary>
    public event EventHandler<ObjectFieldEventArgs> Collapsed;

    /// <summary>
    /// Occures when a new node was selected.
    /// </summary>
    public event EventHandler<ObjectFieldEventArgs> SelectionChanged;

    #endregion
  }
}