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
using Aga.Controls.Tree;
using NReflect.Studio.ObjectTree.Model;

namespace NReflect.Studio.ObjectTree
{
  public class ObjectTreeModel : TreeModelBase
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The tree view this model belongs to.
    /// </summary>
    private readonly TreeViewAdv treeView;

    /// <summary>
    /// A value indicating the dirty status of the model.
    /// </summary>
    private bool dirty;

    /// <summary>
    /// Holds the displayed root node.
    /// </summary>
    private ObjectField displayRoot;

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectTreeModel"/>.
    /// </summary>
    /// <param name="treeView">The tree view this model belongs to.</param>
    /// <param name="o">The object to display.</param>
    public ObjectTreeModel(TreeViewAdv treeView, object o)
    {
      this.treeView = treeView;

      ModelCreator = new ObjectTreeModelCreator
                       {
                         ValueChangedMethod = field_ValueChanged,
                         ItemAddedMethod = field_ItemAdded,
                         ItemRemovedMethod = field_ItemRemoved
                       };

      Root = ModelCreator.CreateModel(o);
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the root of the tree.
    /// </summary>
    public ObjectField Root { get; private set; }
    /// <summary>
    /// Gets or sets the node which is displaed as the root.
    /// </summary>
    public ObjectField DisplayRoot
    {
      get { return displayRoot ?? Root; }
      set 
      {
        displayRoot = value;
        OnStructureChanged(new TreePathEventArgs(TreePath.Empty));
      }
    }

    /// <summary>
    /// Gets or sets a value indicating the dirty status of the model.
    /// </summary>
    public bool Dirty
    {
      get { return dirty; }
      set
      {
        if(dirty != value)
        {
          dirty = value;
          OnDirtyChanged();
        }
      }
    }

    /// <summary>
    /// Gets the <see cref="ObjectTreeModelCreator"/> used to create the model.
    /// </summary>
    public ObjectTreeModelCreator ModelCreator { get; private set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Returns the children of a node at a specified path.
    /// </summary>
    /// <param name="treePath">The path.</param>
    /// <returns>The children.</returns>
    public override IEnumerable GetChildren(TreePath treePath)
    {
      ObjectField field;
      if(treePath.IsEmpty())
      {
        if(DisplayRoot == null)
        {
          return null;
        }
        field = DisplayRoot;
      }
      else
      {
        field = treePath.LastNode as ObjectField;
      }

      ObjectFieldClass objectFieldClass = field as ObjectFieldClass;
      if(objectFieldClass != null)
      {
        return objectFieldClass.ChildFields;
      }
      ObjectFieldList objectFieldList = field as ObjectFieldList;
      if(objectFieldList != null)
      {
        return objectFieldList.Items;
      }
      ObjectFieldDictionary objectFieldDictionary = field as ObjectFieldDictionary;
      if(objectFieldDictionary != null)
      {
        return objectFieldDictionary.Items.Values;
      }
      return null;
    }

    /// <summary>
    /// Determines if a <see cref="TreePath"/> describes a leaf node.
    /// </summary>
    /// <param name="treePath">The path.</param>
    /// <returns><c>True</c> if the paht points to a leaf node.</returns>
    public override bool IsLeaf(TreePath treePath)
    {
      return treePath.LastNode is ObjectFieldValue;
    }

    /// <summary>
    /// Gets the <see cref="TreePath"/> of the specified <see cref="ObjectField"/>.
    /// </summary>
    /// <param name="field">The <see cref="TreePath"/> of this <see cref="ObjectField"/> is returned.</param>
    /// <returns>The <see cref="TreePath"/> of the <see cref="ObjectField"/> or <c>null</c> if no <see cref="TreePath"/> could be found.</returns>
    private TreePath GetPath(ObjectField field)
    {
      return treeView.GetPath(treeView.FindNodeByTag(field));
    }

    /// <summary>
    /// Gets called after a new item was added to a collection.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="args">Informations about the event.</param>
    private void field_ItemAdded(object sender, ObjectFieldEventArgs args)
    {
      ObjectFieldCollection collection = args.ObjectField.Parent as ObjectFieldCollection;
      if(collection != null)
      {
        Dirty = true;
        TreePath parentPath = GetPath(collection);
        ObjectFieldList listField = collection as ObjectFieldList;
        int index = collection.Count;
        if(listField != null)
        {
          index = listField.Items.IndexOf(args.ObjectField);
        }
        OnNodesInserted(new TreeModelEventArgs(parentPath, new[] { index }, new object[] { args.ObjectField }));
        treeView.FindNode(parentPath).Expand();
        OnItemAdded(args);
      }
    }

    /// <summary>
    /// Gets called after an item was removed from a collection.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="args">Informations about the event.</param>
    private void field_ItemRemoved(object sender, ObjectFieldEventArgs args)
    {
      Dirty = true;
      OnNodesRemoved(new TreeModelEventArgs(GetPath(args.ObjectField.Parent), new object[] { args.ObjectField }));
      OnItemRemoved(args);
    }

    /// <summary>
    /// Gets called after a value of a field has changed.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Informations about the event.</param>
    private void field_ValueChanged(object sender, ValueChangedEventArgs e)
    {
      ObjectFieldClass fieldClass = e.ObjectField as ObjectFieldClass;
      if(fieldClass != null)
      {
        // The changed field is a class.
        ModelCreator.RefreshObjectField(fieldClass, e.NewValue);
        TreePath treePath = GetPath(e.ObjectField);
        OnStructureChanged(new TreePathEventArgs(treePath));
        treeView.FindNode(treePath).Expand();
      }
      Dirty = true;
      OnValueChanged(e);
    }

    #region --- OnXXX

    /// <summary>
    /// Fires the <see cref="DirtyChanged"/> event.
    /// </summary>
    protected void OnDirtyChanged()
    {
      if (DirtyChanged != null)
      {
        DirtyChanged(this, EventArgs.Empty);
      }
    }

    /// <summary>
    /// Fires the <see cref="ValueChanged"/> event.
    /// </summary>
    /// <param name="args">The args of the event.</param>
    protected void OnValueChanged(ValueChangedEventArgs args)
    {
      if (ValueChanged != null)
      {
        ValueChanged(this, args);
      }
    }

    /// <summary>
    /// Fires the <see cref="ItemAdded"/> event.
    /// </summary>
    /// <param name="args">More information about the event.</param>
    protected void OnItemAdded(ObjectFieldEventArgs args)
    {
      if (ItemAdded != null)
      {
        ItemAdded(this, args);
      }
    }

    /// <summary>
    /// Fires the <see cref="ItemRemoved"/> event.
    /// </summary>
    /// <param name="args">More information about the event.</param>
    protected void OnItemRemoved(ObjectFieldEventArgs args)
    {
      if (ItemRemoved != null)
      {
        ItemRemoved(this, args);
      }
    }

    #endregion

    #endregion

    // ========================================================================
    // Events

    #region === Events

    /// <summary>
    /// Occures when the dirty status of the model changed.
    /// </summary>
    public event EventHandler<EventArgs> DirtyChanged;

    /// <summary>
    /// Occures when the value of a field changed.
    /// </summary>
    public event EventHandler<ValueChangedEventArgs> ValueChanged;
    /// <summary>
    /// Occures after a new item was added.
    /// </summary>
    public event EventHandler<ObjectFieldEventArgs> ItemAdded;
    /// <summary>
    /// Occures after an item was removed.
    /// </summary>
    public event EventHandler<ObjectFieldEventArgs> ItemRemoved;

    #endregion
  }
}