namespace NReflect.Studio.Panels
{
  partial class ObjectPanel
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectPanel));
      this.toolStripTop = new System.Windows.Forms.ToolStrip();
      this.toolStripButtonExpandAll = new System.Windows.Forms.ToolStripButton();
      this.toolStripButtonCollapseAll = new System.Windows.Forms.ToolStripButton();
      this.toolStripButtonAutoResizeColumns = new System.Windows.Forms.ToolStripButton();
      this.toolStripButtonUseAsRoot = new System.Windows.Forms.ToolStripButton();
      this.toolStripButtonTypeVisible = new System.Windows.Forms.ToolStripButton();
      this.objectTree = new NReflect.Studio.ObjectTree.ObjectTree();
      this.toolStripTop.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripTop
      // 
      this.toolStripTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExpandAll,
            this.toolStripButtonCollapseAll,
            this.toolStripButtonAutoResizeColumns,
            this.toolStripButtonUseAsRoot,
            this.toolStripButtonTypeVisible});
      this.toolStripTop.Location = new System.Drawing.Point(0, 0);
      this.toolStripTop.Name = "toolStripTop";
      this.toolStripTop.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
      this.toolStripTop.Size = new System.Drawing.Size(573, 33);
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
      // toolStripButtonTypeVisible
      // 
      this.toolStripButtonTypeVisible.Checked = true;
      this.toolStripButtonTypeVisible.CheckOnClick = true;
      this.toolStripButtonTypeVisible.CheckState = System.Windows.Forms.CheckState.Checked;
      this.toolStripButtonTypeVisible.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolStripButtonTypeVisible.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonTypeVisible.Image")));
      this.toolStripButtonTypeVisible.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonTypeVisible.Name = "toolStripButtonTypeVisible";
      this.toolStripButtonTypeVisible.Size = new System.Drawing.Size(37, 20);
      this.toolStripButtonTypeVisible.Text = "Type";
      this.toolStripButtonTypeVisible.Click += new System.EventHandler(this.toolStripButtonTypeVisible_Click);
      // 
      // objectTree
      // 
      this.objectTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.objectTree.AutoResizeColumns = true;
      this.objectTree.Dirty = false;
      this.objectTree.Editable = false;
      this.objectTree.FirstVisibleField = null;
      this.objectTree.Location = new System.Drawing.Point(0, 36);
      this.objectTree.Name = "objectTree";
      this.objectTree.Object = null;
      this.objectTree.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
      this.objectTree.RootField = null;
      this.objectTree.SelectedField = null;
      this.objectTree.Size = new System.Drawing.Size(573, 440);
      this.objectTree.StartDrag = false;
      this.objectTree.TabIndex = 0;
      this.objectTree.TypeColumnVisible = true;
      this.objectTree.Expanded += new System.EventHandler<NReflect.Studio.ObjectTree.ObjectFieldEventArgs>(this.objectTree_Expanded);
      this.objectTree.SelectionChanged += new System.EventHandler<NReflect.Studio.ObjectTree.ObjectFieldEventArgs>(this.objectTree_SelectionChanged);
      // 
      // ObjectPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(573, 476);
      this.Controls.Add(this.toolStripTop);
      this.Controls.Add(this.objectTree);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.HideOnClose = true;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Image = global::NReflect.Studio.Properties.Resources.Object;
      this.Name = "ObjectPanel";
      this.Text = "Object";
      this.toolStripTop.ResumeLayout(false);
      this.toolStripTop.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private ObjectTree.ObjectTree objectTree;
    private System.Windows.Forms.ToolStrip toolStripTop;
    private System.Windows.Forms.ToolStripButton toolStripButtonExpandAll;
    private System.Windows.Forms.ToolStripButton toolStripButtonCollapseAll;
    private System.Windows.Forms.ToolStripButton toolStripButtonAutoResizeColumns;
    private System.Windows.Forms.ToolStripButton toolStripButtonUseAsRoot;
    private System.Windows.Forms.ToolStripButton toolStripButtonTypeVisible;
  }
}