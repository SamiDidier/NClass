using NReflect.Studio.ObjectTree;

namespace NReflect.Studio.Panels
{
  partial class ObjectComparePanel
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectComparePanel));
      this.splitContainerObject = new System.Windows.Forms.SplitContainer();
      this.expectedObjectTree = new NReflect.Studio.ObjectTree.ObjectTree();
      this.lblExpectedObject = new System.Windows.Forms.Label();
      this.currentObjectTree = new NReflect.Studio.ObjectTree.ObjectTree();
      this.lblCurrentObject = new System.Windows.Forms.Label();
      this.toolStripTop = new System.Windows.Forms.ToolStrip();
      this.toolStripButtonExpandAll = new System.Windows.Forms.ToolStripButton();
      this.toolStripButtonCollapseAll = new System.Windows.Forms.ToolStripButton();
      this.toolStripButtonSynchronizeTrees = new System.Windows.Forms.ToolStripButton();
      this.toolStripButtonAutoResizeColumns = new System.Windows.Forms.ToolStripButton();
      this.toolStripButtonUseAsRoot = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButtonAcceptCurrentResult = new System.Windows.Forms.ToolStripButton();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainerObject)).BeginInit();
      this.splitContainerObject.Panel1.SuspendLayout();
      this.splitContainerObject.Panel2.SuspendLayout();
      this.splitContainerObject.SuspendLayout();
      this.toolStripTop.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainerObject
      // 
      this.splitContainerObject.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainerObject.Location = new System.Drawing.Point(0, 36);
      this.splitContainerObject.Name = "splitContainerObject";
      // 
      // splitContainerObject.Panel1
      // 
      this.splitContainerObject.Panel1.Controls.Add(this.expectedObjectTree);
      this.splitContainerObject.Panel1.Controls.Add(this.lblExpectedObject);
      // 
      // splitContainerObject.Panel2
      // 
      this.splitContainerObject.Panel2.Controls.Add(this.currentObjectTree);
      this.splitContainerObject.Panel2.Controls.Add(this.lblCurrentObject);
      this.splitContainerObject.Size = new System.Drawing.Size(781, 499);
      this.splitContainerObject.SplitterDistance = 370;
      this.splitContainerObject.TabIndex = 2;
      // 
      // expectedObjectTree
      // 
      this.expectedObjectTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.expectedObjectTree.AutoResizeColumns = false;
      this.expectedObjectTree.Dirty = false;
      this.expectedObjectTree.Editable = true;
      this.expectedObjectTree.FirstVisibleField = null;
      this.expectedObjectTree.Location = new System.Drawing.Point(6, 19);
      this.expectedObjectTree.Name = "expectedObjectTree";
      this.expectedObjectTree.Object = null;
      this.expectedObjectTree.RootField = null;
      this.expectedObjectTree.SelectedField = null;
      this.expectedObjectTree.Size = new System.Drawing.Size(363, 477);
      this.expectedObjectTree.StartDrag = false;
      this.expectedObjectTree.TabIndex = 4;
      this.expectedObjectTree.TypeColumnVisible = false;
      this.expectedObjectTree.ValueChanged += new System.EventHandler<NReflect.Studio.ObjectTree.ValueChangedEventArgs>(this.expectedObjectTree_ValueChanged);
      this.expectedObjectTree.DirtyChanged += new System.EventHandler<System.EventArgs>(this.expectedObjectTree_DirtyChanged);
      this.expectedObjectTree.NewValueNeeded += new System.EventHandler<NReflect.Studio.ObjectTree.NewValueEventArgs>(this.expectedObjectTree_NewValueNeeded);
      this.expectedObjectTree.Expanded += new System.EventHandler<NReflect.Studio.ObjectTree.ObjectFieldEventArgs>(this.expectedObjectTree_Expanded);
      this.expectedObjectTree.Collapsed += new System.EventHandler<NReflect.Studio.ObjectTree.ObjectFieldEventArgs>(this.expectedObjectTree_Collapsed);
      this.expectedObjectTree.SelectionChanged += new System.EventHandler<NReflect.Studio.ObjectTree.ObjectFieldEventArgs>(this.expectedObjectTree_SelectionChanged);
      this.expectedObjectTree.Scroll += new System.Windows.Forms.ScrollEventHandler(this.expectedObjectTree_Scroll);
      // 
      // lblExpectedObject
      // 
      this.lblExpectedObject.AutoSize = true;
      this.lblExpectedObject.Location = new System.Drawing.Point(3, 3);
      this.lblExpectedObject.Name = "lblExpectedObject";
      this.lblExpectedObject.Size = new System.Drawing.Size(52, 13);
      this.lblExpectedObject.TabIndex = 2;
      this.lblExpectedObject.Text = "Expected";
      // 
      // currentObjectTree
      // 
      this.currentObjectTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.currentObjectTree.AutoResizeColumns = false;
      this.currentObjectTree.Dirty = false;
      this.currentObjectTree.Editable = false;
      this.currentObjectTree.FirstVisibleField = null;
      this.currentObjectTree.Location = new System.Drawing.Point(1, 19);
      this.currentObjectTree.Name = "currentObjectTree";
      this.currentObjectTree.Object = null;
      this.currentObjectTree.RootField = null;
      this.currentObjectTree.SelectedField = null;
      this.currentObjectTree.Size = new System.Drawing.Size(400, 477);
      this.currentObjectTree.StartDrag = false;
      this.currentObjectTree.TabIndex = 4;
      this.currentObjectTree.TypeColumnVisible = true;
      this.currentObjectTree.Expanded += new System.EventHandler<NReflect.Studio.ObjectTree.ObjectFieldEventArgs>(this.currentObjectTree_Expanded);
      this.currentObjectTree.Collapsed += new System.EventHandler<NReflect.Studio.ObjectTree.ObjectFieldEventArgs>(this.currentObjectTree_Collapsed);
      this.currentObjectTree.SelectionChanged += new System.EventHandler<NReflect.Studio.ObjectTree.ObjectFieldEventArgs>(this.currentObjectTree_SelectionChanged);
      this.currentObjectTree.Scroll += new System.Windows.Forms.ScrollEventHandler(this.currentObjectTree_Scroll);
      // 
      // lblCurrentObject
      // 
      this.lblCurrentObject.AutoSize = true;
      this.lblCurrentObject.Location = new System.Drawing.Point(3, 3);
      this.lblCurrentObject.Name = "lblCurrentObject";
      this.lblCurrentObject.Size = new System.Drawing.Size(41, 13);
      this.lblCurrentObject.TabIndex = 3;
      this.lblCurrentObject.Text = "Current";
      // 
      // toolStripTop
      // 
      this.toolStripTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExpandAll,
            this.toolStripButtonCollapseAll,
            this.toolStripButtonSynchronizeTrees,
            this.toolStripButtonAutoResizeColumns,
            this.toolStripButtonUseAsRoot,
            this.toolStripSeparator1,
            this.toolStripButtonAcceptCurrentResult});
      this.toolStripTop.Location = new System.Drawing.Point(0, 0);
      this.toolStripTop.Name = "toolStripTop";
      this.toolStripTop.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
      this.toolStripTop.Size = new System.Drawing.Size(781, 33);
      this.toolStripTop.TabIndex = 3;
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
      // toolStripButtonSynchronizeTrees
      // 
      this.toolStripButtonSynchronizeTrees.Checked = true;
      this.toolStripButtonSynchronizeTrees.CheckOnClick = true;
      this.toolStripButtonSynchronizeTrees.CheckState = System.Windows.Forms.CheckState.Checked;
      this.toolStripButtonSynchronizeTrees.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonSynchronizeTrees.Image = global::NReflect.Studio.Properties.Resources.Sync;
      this.toolStripButtonSynchronizeTrees.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.toolStripButtonSynchronizeTrees.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.toolStripButtonSynchronizeTrees.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonSynchronizeTrees.Name = "toolStripButtonSynchronizeTrees";
      this.toolStripButtonSynchronizeTrees.Size = new System.Drawing.Size(23, 20);
      this.toolStripButtonSynchronizeTrees.Text = "Synchronize Trees";
      this.toolStripButtonSynchronizeTrees.Click += new System.EventHandler(this.toolStripButtonSynchronizeTrees_Click);
      // 
      // toolStripButtonAutoResizeColumns
      // 
      this.toolStripButtonAutoResizeColumns.Checked = true;
      this.toolStripButtonAutoResizeColumns.CheckOnClick = true;
      this.toolStripButtonAutoResizeColumns.CheckState = System.Windows.Forms.CheckState.Checked;
      this.toolStripButtonAutoResizeColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonAutoResizeColumns.Image = global::NReflect.Studio.Properties.Resources.Autoresize;
      this.toolStripButtonAutoResizeColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonAutoResizeColumns.Name = "toolStripButtonAutoResizeColumns";
      this.toolStripButtonAutoResizeColumns.Size = new System.Drawing.Size(23, 20);
      this.toolStripButtonAutoResizeColumns.Text = "Automatic column resize";
      this.toolStripButtonAutoResizeColumns.Click += new System.EventHandler(this.toolStripButtonAutoResizeColumns_Click);
      // 
      // toolStripButtonUseAsRoot
      // 
      this.toolStripButtonUseAsRoot.CheckOnClick = true;
      this.toolStripButtonUseAsRoot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonUseAsRoot.Enabled = false;
      this.toolStripButtonUseAsRoot.Image = global::NReflect.Studio.Properties.Resources.UseAsRoot;
      this.toolStripButtonUseAsRoot.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonUseAsRoot.Name = "toolStripButtonUseAsRoot";
      this.toolStripButtonUseAsRoot.Size = new System.Drawing.Size(23, 20);
      this.toolStripButtonUseAsRoot.Text = "Use as root";
      this.toolStripButtonUseAsRoot.Click += new System.EventHandler(this.toolStripButtonUseAsRoot_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
      // 
      // toolStripButtonAcceptCurrentResult
      // 
      this.toolStripButtonAcceptCurrentResult.Enabled = false;
      this.toolStripButtonAcceptCurrentResult.Image = global::NReflect.Studio.Properties.Resources.OK;
      this.toolStripButtonAcceptCurrentResult.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonAcceptCurrentResult.Name = "toolStripButtonAcceptCurrentResult";
      this.toolStripButtonAcceptCurrentResult.Size = new System.Drawing.Size(137, 20);
      this.toolStripButtonAcceptCurrentResult.Text = "Accept current result";
      this.toolStripButtonAcceptCurrentResult.Click += new System.EventHandler(this.toolStripButtonAcceptCurrentResult_Click);
      // 
      // ObjectComparePanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(781, 535);
      this.Controls.Add(this.toolStripTop);
      this.Controls.Add(this.splitContainerObject);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.HideOnClose = true;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Image = global::NReflect.Studio.Properties.Resources.ObjectCompare;
      this.Name = "ObjectComparePanel";
      this.Text = "Object Compare";
      this.splitContainerObject.Panel1.ResumeLayout(false);
      this.splitContainerObject.Panel1.PerformLayout();
      this.splitContainerObject.Panel2.ResumeLayout(false);
      this.splitContainerObject.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainerObject)).EndInit();
      this.splitContainerObject.ResumeLayout(false);
      this.toolStripTop.ResumeLayout(false);
      this.toolStripTop.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainerObject;
    private System.Windows.Forms.Label lblExpectedObject;
    private System.Windows.Forms.Label lblCurrentObject;
    private ObjectTree.ObjectTree currentObjectTree;
    private ObjectTree.ObjectTree expectedObjectTree;
    private System.Windows.Forms.ToolStrip toolStripTop;
    private System.Windows.Forms.ToolStripButton toolStripButtonExpandAll;
    private System.Windows.Forms.ToolStripButton toolStripButtonCollapseAll;
    private System.Windows.Forms.ToolStripButton toolStripButtonSynchronizeTrees;
    private System.Windows.Forms.ToolStripButton toolStripButtonAutoResizeColumns;
    private System.Windows.Forms.ToolStripButton toolStripButtonUseAsRoot;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton toolStripButtonAcceptCurrentResult;


  }
}