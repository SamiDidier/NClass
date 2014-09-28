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
using System.Collections.Generic;
using System.ComponentModel;
using Aga.Controls.Tree;

namespace NReflect.Studio.Models
{
  public class TestCaseTreeModel : TreeModelBase
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The tree view this model belongs to.
    /// </summary>
    private readonly TreeViewAdv treeView;

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="TestCaseTreeModel"/>.
    /// </summary>
    /// <param name="treeView">The tree view this model belongs to.</param>
    public TestCaseTreeModel(TreeViewAdv treeView)
    {
      this.treeView = treeView;

      CoreData.Instance.TestCaseModel.TestCasesLoaded += TestCaseModel_TestCasesLoaded;
      CoreData.Instance.TestCaseModel.TestCaseAdded += TestCaseModel_TestCaseAdded;
      CoreData.Instance.TestCaseModel.TestCaseRemoved += TestCaseModel_TestCaseRemoved;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    public override IEnumerable GetChildren(TreePath treePath)
    {
      if(treePath.IsEmpty())
      {
        if(CoreData.Instance.TestCaseModel.Root == null)
        {
          return null;
        }
        return CoreData.Instance.TestCaseModel.Root.TestCases;
      }
      TestCaseGroup testCaseGroup = treePath.LastNode as TestCaseGroup;
      if(testCaseGroup != null)
      {
        return testCaseGroup.TestCases;
      }
      return new List<TestCaseBase>();
    }

    public override bool IsLeaf(TreePath treePath)
    {
      return treePath.LastNode is TestCase;
    }

    private TreePath GetPath(TestCaseBase testCaseBase)
    {
      List<TestCaseBase> list = new List<TestCaseBase>();
      TestCaseBase currentBase = testCaseBase;
      while(currentBase != null)
      {
        list.Add(currentBase);
        currentBase = currentBase.Parent;
      }
      list.Reverse();
      list.RemoveAt(0); // Remove the root group since it is only a dummy

      return new TreePath(list.ToArray());
    }

    public TreeNodeAdv AddNewTestCase(TreeNodeAdv parentNode)
    {
      TestCaseGroup parentGroup = null;
      if(parentNode != null)
      {
        parentGroup = parentNode.Tag as TestCaseGroup ?? parentNode.Parent.Tag as TestCaseGroup;
      }
      if(parentGroup == null)
      {
        parentGroup = CoreData.Instance.TestCaseModel.Root;
      }

      TestCase newTestCase = CoreData.Instance.TestCaseModel.AddTestCase(parentGroup, "NewTestCase.cs");

      return treeView.FindNodeByTag(newTestCase);
    }

    public TreeNodeAdv AddNewTestCaseGroup(TreeNodeAdv parentNode)
    {
      TestCaseGroup parentGroup = null;
      if (parentNode != null)
      {
        parentGroup = parentNode.Tag as TestCaseGroup ?? parentNode.Parent.Tag as TestCaseGroup;
      }
      if (parentGroup == null)
      {
        parentGroup = CoreData.Instance.TestCaseModel.Root;
      }

      TestCaseGroup newTestCaseGroup = CoreData.Instance.TestCaseModel.AddTestCaseGroup(parentGroup, "NewGroup");

      return treeView.FindNodeByTag(newTestCaseGroup);
    }

    public void RemoveTestCase(TreeNodeAdv node)
    {
      TestCaseGroup parentGroup = null;
      if (node != null)
      {
        parentGroup = node.Parent.Tag as TestCaseGroup;
      }
      if (parentGroup == null)
      {
        parentGroup = CoreData.Instance.TestCaseModel.Root;
      }

      if(node != null && node.Tag is TestCase)
      {
        CoreData.Instance.TestCaseModel.RemoveTestCase(parentGroup, node.Tag as TestCase);
      }
      else if(node != null && node.Tag is TestCaseGroup)
      {
        CoreData.Instance.TestCaseModel.RemoveTestCaseGroup(parentGroup, node.Tag as TestCaseGroup);
      }
    }

    /// <summary>
    /// Moves a test case or group to a new position.
    /// </summary>
    /// <param name="node">The moved node.</param>
    /// <param name="destNode">The destination.</param>
    public void MoveTestCase(TreeNodeAdv node, TreeNodeAdv destNode)
    {
      TestCaseGroup destGroup = destNode.Tag as TestCaseGroup;
      if (destNode.Tag == null)
      {
        // Move to root
        destGroup = CoreData.Instance.TestCaseModel.Root;
      }
      if(destGroup == null)
      {
        return;
      }
      TreePath oldPath = treeView.GetPath(node.Parent);
      if(node.Tag is TestCase)
      {
        CoreData.Instance.TestCaseModel.MoveTestCase(node.Tag as TestCase, destGroup);
      }
      else if(node.Tag is TestCaseGroup)
      {
        CoreData.Instance.TestCaseModel.MoveTestCaseGroup(node.Tag as TestCaseGroup, destGroup);
      }
      OnNodesRemoved(new TreeModelEventArgs(oldPath, new[] {node.Tag}));
      OnNodesInserted(new TreeModelEventArgs(treeView.GetPath(destNode), new[] {node.Tag}));
    }

    private void TestCaseModel_TestCaseRemoved(object sender, TestCaseEventArgs e)
    {
      OnNodesRemoved(new TreeModelEventArgs(GetPath(e.Parent), new object[] {e.TestCaseBase}));
      e.TestCaseBase.PropertyChanged -= testCase_PropertyChanged;
    }

    private void TestCaseModel_TestCaseAdded(object sender, TestCaseEventArgs e)
    {
      OnNodesInserted(new TreeModelEventArgs(GetPath(e.Parent), new object[] {e.TestCaseBase}));
      e.TestCaseBase.PropertyChanged += testCase_PropertyChanged;
    }

    private void TestCaseModel_TestCasesLoaded(object sender, EventArgs e)
    {
      OnNodesChanged(new TreeModelEventArgs(TreePath.Empty, new object[] {null}));

      foreach(TestCase testCase in CoreData.Instance.TestCaseModel.TestCases)
      {
        testCase.PropertyChanged += testCase_PropertyChanged;
      }
    }

    void testCase_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      treeView.Invalidate();
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events

    #endregion
  }
}