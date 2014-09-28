namespace NReflect.Studio.ObjectTree
{
  partial class ObjectTree
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.trvTree = new ObjectTreeViewAdv();
      this.colMember = new Aga.Controls.Tree.TreeColumn();
      this.colValue = new Aga.Controls.Tree.TreeColumn();
      this.colType = new Aga.Controls.Tree.TreeColumn();
      this.nodeTextBoxType = new Aga.Controls.Tree.NodeControls.NodeTextBox();
      this.SuspendLayout();
      // 
      // trvTree
      // 
      this.trvTree.AllowDrop = true;
      this.trvTree.BackColor = System.Drawing.SystemColors.Window;
      this.trvTree.Columns.Add(this.colMember);
      this.trvTree.Columns.Add(this.colValue);
      this.trvTree.Columns.Add(this.colType);
      this.trvTree.DefaultToolTipProvider = null;
      this.trvTree.Dock = System.Windows.Forms.DockStyle.Fill;
      this.trvTree.DragDropMarkColor = System.Drawing.Color.Black;
      this.trvTree.FullRowSelect = true;
      this.trvTree.GridLineStyle = ((Aga.Controls.Tree.GridLineStyle)((Aga.Controls.Tree.GridLineStyle.Horizontal | Aga.Controls.Tree.GridLineStyle.Vertical)));
      this.trvTree.LineColor = System.Drawing.SystemColors.ControlDark;
      this.trvTree.Location = new System.Drawing.Point(0, 0);
      this.trvTree.Model = null;
      this.trvTree.Name = "trvTree";
      this.trvTree.NodeControls.Add(this.nodeTextBoxType);
      this.trvTree.RowHeight = 21;
      this.trvTree.SelectedNode = null;
      this.trvTree.Size = new System.Drawing.Size(422, 423);
      this.trvTree.TabIndex = 0;
      this.trvTree.Text = "treeViewAdv1";
      this.trvTree.UseColumns = true;
      this.trvTree.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.trvTree_ItemDrag);
      this.trvTree.SelectionChanged += new System.EventHandler(this.trvTree_SelectionChanged);
      this.trvTree.Collapsed += new System.EventHandler<Aga.Controls.Tree.TreeViewAdvEventArgs>(this.trvTree_Collapsed);
      this.trvTree.Expanded += new System.EventHandler<Aga.Controls.Tree.TreeViewAdvEventArgs>(this.trvTree_Expanded);
      this.trvTree.Scroll += new System.Windows.Forms.ScrollEventHandler(this.trvTree_Scroll);
      this.trvTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.trvTree_DragDrop);
      this.trvTree.DragOver += new System.Windows.Forms.DragEventHandler(this.trvTree_DragOver);
      this.trvTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.trvTree_KeyDown);
      // 
      // colMember
      // 
      this.colMember.Header = "Member";
      this.colMember.SortOrder = System.Windows.Forms.SortOrder.None;
      this.colMember.TooltipText = null;
      this.colMember.Width = 150;
      // 
      // colValue
      // 
      this.colValue.Header = "Value";
      this.colValue.SortOrder = System.Windows.Forms.SortOrder.None;
      this.colValue.TooltipText = null;
      // 
      // colType
      // 
      this.colType.Header = "Type";
      this.colType.SortOrder = System.Windows.Forms.SortOrder.None;
      this.colType.TooltipText = null;
      // 
      // nodeTextBoxType
      // 
      this.nodeTextBoxType.DataPropertyName = "TypeName";
      this.nodeTextBoxType.IncrementalSearchEnabled = true;
      this.nodeTextBoxType.LeftMargin = 3;
      this.nodeTextBoxType.ParentColumn = this.colType;
      this.nodeTextBoxType.Trimming = System.Drawing.StringTrimming.EllipsisCharacter;
      // 
      // ObjectTree
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.trvTree);
      this.Name = "ObjectTree";
      this.Size = new System.Drawing.Size(422, 423);
      this.ResumeLayout(false);

    }

    #endregion

    private ObjectTreeViewAdv trvTree;
    private Aga.Controls.Tree.TreeColumn colMember;
    private Aga.Controls.Tree.TreeColumn colValue;
    private Aga.Controls.Tree.TreeColumn colType;
    private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxType;
  }
}
