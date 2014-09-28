namespace NReflect.Studio.Panels
{
  partial class TestCasesPanel
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestCasesPanel));
      this.trvTestCases = new Aga.Controls.Tree.TreeViewAdv();
      this.colTestCase = new Aga.Controls.Tree.TreeColumn();
      this.colTestCaseMessages = new Aga.Controls.Tree.TreeColumn();
      this.colTestCaseState = new Aga.Controls.Tree.TreeColumn();
      this.contextMenuNode = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.addTestCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.addGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.nodeFileIcon = new Aga.Controls.Tree.NodeControls.NodeIcon();
      this.nodeTextBoxName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
      this.nodeIconState = new Aga.Controls.Tree.NodeControls.NodeIcon();
      this.nodeIconMessages = new Aga.Controls.Tree.NodeControls.NodeIcon();
      this.toolStripTop = new System.Windows.Forms.ToolStrip();
      this.toolStripButtonExpandAll = new System.Windows.Forms.ToolStripButton();
      this.toolStripButtonCollapseAll = new System.Windows.Forms.ToolStripButton();
      this.toolStripButtonAddTestCase = new System.Windows.Forms.ToolStripButton();
      this.toolStripButtonAddGroup = new System.Windows.Forms.ToolStripButton();
      this.toolStripButtonReload = new System.Windows.Forms.ToolStripButton();
      this.contextMenuNode.SuspendLayout();
      this.toolStripTop.SuspendLayout();
      this.SuspendLayout();
      // 
      // trvTestCases
      // 
      this.trvTestCases.AllowDrop = true;
      this.trvTestCases.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.trvTestCases.BackColor = System.Drawing.SystemColors.Window;
      this.trvTestCases.Columns.Add(this.colTestCase);
      this.trvTestCases.Columns.Add(this.colTestCaseMessages);
      this.trvTestCases.Columns.Add(this.colTestCaseState);
      this.trvTestCases.ContextMenuStrip = this.contextMenuNode;
      this.trvTestCases.DefaultToolTipProvider = null;
      this.trvTestCases.DisplayDraggingNodes = true;
      this.trvTestCases.DragDropMarkColor = System.Drawing.Color.Black;
      this.trvTestCases.DragDropMarkWidth = 1F;
      this.trvTestCases.FullRowSelect = true;
      this.trvTestCases.GridLineStyle = ((Aga.Controls.Tree.GridLineStyle)((Aga.Controls.Tree.GridLineStyle.Horizontal | Aga.Controls.Tree.GridLineStyle.Vertical)));
      this.trvTestCases.LineColor = System.Drawing.SystemColors.ControlDark;
      this.trvTestCases.Location = new System.Drawing.Point(0, 28);
      this.trvTestCases.Model = null;
      this.trvTestCases.Name = "trvTestCases";
      this.trvTestCases.NodeControls.Add(this.nodeFileIcon);
      this.trvTestCases.NodeControls.Add(this.nodeTextBoxName);
      this.trvTestCases.NodeControls.Add(this.nodeIconState);
      this.trvTestCases.NodeControls.Add(this.nodeIconMessages);
      this.trvTestCases.SelectedNode = null;
      this.trvTestCases.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.Multi;
      this.trvTestCases.ShowNodeToolTips = true;
      this.trvTestCases.Size = new System.Drawing.Size(360, 333);
      this.trvTestCases.TabIndex = 0;
      this.trvTestCases.Text = "treeViewAdv1";
      this.trvTestCases.UseColumns = true;
      this.trvTestCases.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.trvTestCases_ItemDrag);
      this.trvTestCases.SelectionChanged += new System.EventHandler(this.trvTestCases_SelectionChanged);
      this.trvTestCases.SizeChanged += new System.EventHandler(this.trvTestCases_SizeChanged);
      this.trvTestCases.DragDrop += new System.Windows.Forms.DragEventHandler(this.trvTestCases_DragDrop);
      this.trvTestCases.DragOver += new System.Windows.Forms.DragEventHandler(this.trvTestCases_DragOver);
      this.trvTestCases.KeyDown += new System.Windows.Forms.KeyEventHandler(this.trvTestCases_KeyDown);
      // 
      // colTestCase
      // 
      this.colTestCase.Header = "Testcase";
      this.colTestCase.SortOrder = System.Windows.Forms.SortOrder.None;
      this.colTestCase.TooltipText = null;
      this.colTestCase.Width = 150;
      // 
      // colTestCaseMessages
      // 
      this.colTestCaseMessages.Header = "Messages";
      this.colTestCaseMessages.SortOrder = System.Windows.Forms.SortOrder.None;
      this.colTestCaseMessages.TooltipText = null;
      this.colTestCaseMessages.Width = 20;
      // 
      // colTestCaseState
      // 
      this.colTestCaseState.Header = "State";
      this.colTestCaseState.SortOrder = System.Windows.Forms.SortOrder.None;
      this.colTestCaseState.TooltipText = null;
      this.colTestCaseState.Width = 20;
      // 
      // contextMenuNode
      // 
      this.contextMenuNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTestCaseToolStripMenuItem,
            this.addGroupToolStripMenuItem});
      this.contextMenuNode.Name = "contextMenuNode";
      this.contextMenuNode.Size = new System.Drawing.Size(150, 48);
      // 
      // addTestCaseToolStripMenuItem
      // 
      this.addTestCaseToolStripMenuItem.Image = global::NReflect.Studio.Properties.Resources.NewCSharpFile;
      this.addTestCaseToolStripMenuItem.Name = "addTestCaseToolStripMenuItem";
      this.addTestCaseToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
      this.addTestCaseToolStripMenuItem.Text = "Add Test Case";
      this.addTestCaseToolStripMenuItem.Click += new System.EventHandler(this.addTestCaseToolStripMenuItem_Click);
      // 
      // addGroupToolStripMenuItem
      // 
      this.addGroupToolStripMenuItem.Image = global::NReflect.Studio.Properties.Resources.NewFolder;
      this.addGroupToolStripMenuItem.Name = "addGroupToolStripMenuItem";
      this.addGroupToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
      this.addGroupToolStripMenuItem.Text = "Add Group";
      this.addGroupToolStripMenuItem.Click += new System.EventHandler(this.addGroupToolStripMenuItem_Click);
      // 
      // nodeFileIcon
      // 
      this.nodeFileIcon.LeftMargin = 1;
      this.nodeFileIcon.ParentColumn = this.colTestCase;
      this.nodeFileIcon.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Clip;
      this.nodeFileIcon.VirtualMode = true;
      // 
      // nodeTextBoxName
      // 
      this.nodeTextBoxName.DataPropertyName = "Name";
      this.nodeTextBoxName.EditEnabled = true;
      this.nodeTextBoxName.IncrementalSearchEnabled = true;
      this.nodeTextBoxName.LeftMargin = 3;
      this.nodeTextBoxName.ParentColumn = this.colTestCase;
      this.nodeTextBoxName.Trimming = System.Drawing.StringTrimming.EllipsisCharacter;
      // 
      // nodeIconState
      // 
      this.nodeIconState.LeftMargin = 1;
      this.nodeIconState.ParentColumn = this.colTestCaseState;
      this.nodeIconState.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Clip;
      this.nodeIconState.VirtualMode = true;
      // 
      // nodeIconMessages
      // 
      this.nodeIconMessages.LeftMargin = 1;
      this.nodeIconMessages.ParentColumn = this.colTestCaseMessages;
      this.nodeIconMessages.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Clip;
      this.nodeIconMessages.VirtualMode = true;
      // 
      // toolStripTop
      // 
      this.toolStripTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExpandAll,
            this.toolStripButtonCollapseAll,
            this.toolStripButtonAddTestCase,
            this.toolStripButtonAddGroup,
            this.toolStripButtonReload});
      this.toolStripTop.Location = new System.Drawing.Point(0, 0);
      this.toolStripTop.Name = "toolStripTop";
      this.toolStripTop.Padding = new System.Windows.Forms.Padding(0, 3, 1, 0);
      this.toolStripTop.Size = new System.Drawing.Size(360, 26);
      this.toolStripTop.TabIndex = 1;
      this.toolStripTop.Text = "toolStrip1";
      // 
      // toolStripButtonExpandAll
      // 
      this.toolStripButtonExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonExpandAll.Image = global::NReflect.Studio.Properties.Resources.Plus;
      this.toolStripButtonExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonExpandAll.Name = "toolStripButtonExpandAll";
      this.toolStripButtonExpandAll.Size = new System.Drawing.Size(23, 20);
      this.toolStripButtonExpandAll.Text = "Expand all";
      this.toolStripButtonExpandAll.Click += new System.EventHandler(this.toolStripButtonExpandAll_Click);
      // 
      // toolStripButtonCollapseAll
      // 
      this.toolStripButtonCollapseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonCollapseAll.Image = global::NReflect.Studio.Properties.Resources.Minus;
      this.toolStripButtonCollapseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonCollapseAll.Name = "toolStripButtonCollapseAll";
      this.toolStripButtonCollapseAll.Size = new System.Drawing.Size(23, 20);
      this.toolStripButtonCollapseAll.Text = "Collapse all";
      this.toolStripButtonCollapseAll.Click += new System.EventHandler(this.toolStripButtonCollapseAll_Click);
      // 
      // toolStripButtonAddTestCase
      // 
      this.toolStripButtonAddTestCase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonAddTestCase.Image = global::NReflect.Studio.Properties.Resources.NewCSharpFile;
      this.toolStripButtonAddTestCase.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonAddTestCase.Name = "toolStripButtonAddTestCase";
      this.toolStripButtonAddTestCase.Size = new System.Drawing.Size(23, 20);
      this.toolStripButtonAddTestCase.Text = "Add Test Case";
      this.toolStripButtonAddTestCase.Click += new System.EventHandler(this.addTestCaseToolStripMenuItem_Click);
      // 
      // toolStripButtonAddGroup
      // 
      this.toolStripButtonAddGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonAddGroup.Image = global::NReflect.Studio.Properties.Resources.NewFolder;
      this.toolStripButtonAddGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonAddGroup.Name = "toolStripButtonAddGroup";
      this.toolStripButtonAddGroup.Size = new System.Drawing.Size(23, 20);
      this.toolStripButtonAddGroup.Text = "Add Group";
      this.toolStripButtonAddGroup.Click += new System.EventHandler(this.addGroupToolStripMenuItem_Click);
      // 
      // toolStripButtonReload
      // 
      this.toolStripButtonReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonReload.Image = global::NReflect.Studio.Properties.Resources.Refresh;
      this.toolStripButtonReload.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonReload.Name = "toolStripButtonReload";
      this.toolStripButtonReload.Size = new System.Drawing.Size(23, 20);
      this.toolStripButtonReload.Text = "Reload test cases";
      this.toolStripButtonReload.Click += new System.EventHandler(this.toolStripButtonReload_Click);
      // 
      // TestCasesPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(360, 361);
      this.Controls.Add(this.toolStripTop);
      this.Controls.Add(this.trvTestCases);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.HideOnClose = true;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Image = global::NReflect.Studio.Properties.Resources.Explorer;
      this.Name = "TestCasesPanel";
      this.Text = "TestCases";
      this.Load += new System.EventHandler(this.TestCasesPanel_Load);
      this.contextMenuNode.ResumeLayout(false);
      this.toolStripTop.ResumeLayout(false);
      this.toolStripTop.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Aga.Controls.Tree.TreeViewAdv trvTestCases;
    private Aga.Controls.Tree.TreeColumn colTestCase;
    private Aga.Controls.Tree.NodeControls.NodeIcon nodeFileIcon;
    private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxName;
    private System.Windows.Forms.ContextMenuStrip contextMenuNode;
    private System.Windows.Forms.ToolStripMenuItem addTestCaseToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem addGroupToolStripMenuItem;
    private Aga.Controls.Tree.TreeColumn colTestCaseState;
    private Aga.Controls.Tree.NodeControls.NodeIcon nodeIconState;
    private System.Windows.Forms.ToolStrip toolStripTop;
    private System.Windows.Forms.ToolStripButton toolStripButtonExpandAll;
    private System.Windows.Forms.ToolStripButton toolStripButtonCollapseAll;
    private Aga.Controls.Tree.TreeColumn colTestCaseMessages;
    private Aga.Controls.Tree.NodeControls.NodeIcon nodeIconMessages;
    private System.Windows.Forms.ToolStripButton toolStripButtonAddTestCase;
    private System.Windows.Forms.ToolStripButton toolStripButtonAddGroup;
    private System.Windows.Forms.ToolStripButton toolStripButtonReload;

  }
}