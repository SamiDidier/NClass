namespace NReflect.Studio.Panels
{
  partial class MessagePanel
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessagePanel));
      this.imageList = new System.Windows.Forms.ImageList(this.components);
      this.dataGridView = new System.Windows.Forms.DataGridView();
      this.ColumnSeverity = new System.Windows.Forms.DataGridViewImageColumn();
      this.ColumnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.toolStripTop = new System.Windows.Forms.ToolStrip();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButtonWarnings = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButtonMessages = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButtonClear = new System.Windows.Forms.ToolStripButton();
      this.toolStripButtonErrors = new System.Windows.Forms.ToolStripButton();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
      this.toolStripTop.SuspendLayout();
      this.SuspendLayout();
      // 
      // imageList
      // 
      this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
      this.imageList.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList.Images.SetKeyName(0, "Info");
      this.imageList.Images.SetKeyName(1, "Warning");
      this.imageList.Images.SetKeyName(2, "Error");
      // 
      // dataGridView
      // 
      this.dataGridView.AllowUserToAddRows = false;
      this.dataGridView.AllowUserToDeleteRows = false;
      this.dataGridView.AllowUserToOrderColumns = true;
      this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
      this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSeverity,
            this.ColumnDescription,
            this.ColumnFile,
            this.ColumnLine,
            this.ColumnColumn});
      this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataGridView.Location = new System.Drawing.Point(0, 25);
      this.dataGridView.Name = "dataGridView";
      this.dataGridView.ReadOnly = true;
      this.dataGridView.RowHeadersVisible = false;
      this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dataGridView.ShowCellErrors = false;
      this.dataGridView.ShowEditingIcon = false;
      this.dataGridView.ShowRowErrors = false;
      this.dataGridView.Size = new System.Drawing.Size(836, 237);
      this.dataGridView.TabIndex = 2;
      // 
      // ColumnSeverity
      // 
      this.ColumnSeverity.Frozen = true;
      this.ColumnSeverity.HeaderText = "";
      this.ColumnSeverity.MinimumWidth = 16;
      this.ColumnSeverity.Name = "ColumnSeverity";
      this.ColumnSeverity.ReadOnly = true;
      this.ColumnSeverity.Width = 16;
      // 
      // ColumnDescription
      // 
      this.ColumnDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.ColumnDescription.HeaderText = "Description";
      this.ColumnDescription.Name = "ColumnDescription";
      this.ColumnDescription.ReadOnly = true;
      // 
      // ColumnFile
      // 
      this.ColumnFile.HeaderText = "File";
      this.ColumnFile.Name = "ColumnFile";
      this.ColumnFile.ReadOnly = true;
      // 
      // ColumnLine
      // 
      this.ColumnLine.HeaderText = "Line";
      this.ColumnLine.Name = "ColumnLine";
      this.ColumnLine.ReadOnly = true;
      // 
      // ColumnColumn
      // 
      this.ColumnColumn.HeaderText = "Column";
      this.ColumnColumn.Name = "ColumnColumn";
      this.ColumnColumn.ReadOnly = true;
      // 
      // toolStripTop
      // 
      this.toolStripTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonErrors,
            this.toolStripSeparator2,
            this.toolStripButtonWarnings,
            this.toolStripSeparator3,
            this.toolStripButtonMessages,
            this.toolStripSeparator1,
            this.toolStripButtonClear});
      this.toolStripTop.Location = new System.Drawing.Point(0, 0);
      this.toolStripTop.Name = "toolStripTop";
      this.toolStripTop.Padding = new System.Windows.Forms.Padding(3, 0, 1, 0);
      this.toolStripTop.Size = new System.Drawing.Size(836, 25);
      this.toolStripTop.TabIndex = 1;
      this.toolStripTop.Text = "toolStrip1";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripButtonWarnings
      // 
      this.toolStripButtonWarnings.Checked = true;
      this.toolStripButtonWarnings.CheckOnClick = true;
      this.toolStripButtonWarnings.CheckState = System.Windows.Forms.CheckState.Checked;
      this.toolStripButtonWarnings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonWarnings.Image")));
      this.toolStripButtonWarnings.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonWarnings.Name = "toolStripButtonWarnings";
      this.toolStripButtonWarnings.Size = new System.Drawing.Size(86, 22);
      this.toolStripButtonWarnings.Text = "0 Warnings";
      this.toolStripButtonWarnings.Click += new System.EventHandler(this.toolStripFilterButton_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripButtonMessages
      // 
      this.toolStripButtonMessages.Checked = true;
      this.toolStripButtonMessages.CheckOnClick = true;
      this.toolStripButtonMessages.CheckState = System.Windows.Forms.CheckState.Checked;
      this.toolStripButtonMessages.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMessages.Image")));
      this.toolStripButtonMessages.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonMessages.Name = "toolStripButtonMessages";
      this.toolStripButtonMessages.Size = new System.Drawing.Size(87, 22);
      this.toolStripButtonMessages.Text = "0 Messages";
      this.toolStripButtonMessages.Click += new System.EventHandler(this.toolStripFilterButton_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripButtonClear
      // 
      this.toolStripButtonClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonClear.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonClear.Image")));
      this.toolStripButtonClear.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonClear.Name = "toolStripButtonClear";
      this.toolStripButtonClear.Size = new System.Drawing.Size(23, 22);
      this.toolStripButtonClear.Text = "Clear";
      this.toolStripButtonClear.Click += new System.EventHandler(this.toolStripButtonClear_Click);
      // 
      // toolStripButtonErrors
      // 
      this.toolStripButtonErrors.Checked = true;
      this.toolStripButtonErrors.CheckOnClick = true;
      this.toolStripButtonErrors.CheckState = System.Windows.Forms.CheckState.Checked;
      this.toolStripButtonErrors.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonErrors.Image")));
      this.toolStripButtonErrors.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonErrors.Name = "toolStripButtonErrors";
      this.toolStripButtonErrors.Size = new System.Drawing.Size(66, 22);
      this.toolStripButtonErrors.Text = "0 Errors";
      this.toolStripButtonErrors.Click += new System.EventHandler(this.toolStripFilterButton_Click);
      // 
      // MessagePanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(836, 262);
      this.Controls.Add(this.dataGridView);
      this.Controls.Add(this.toolStripTop);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.HideOnClose = true;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Image = global::NReflect.Studio.Properties.Resources.Messages;
      this.Name = "MessagePanel";
      this.Text = "Messages";
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
      this.toolStripTop.ResumeLayout(false);
      this.toolStripTop.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStripTop;
    private System.Windows.Forms.ToolStripButton toolStripButtonErrors;
    private System.Windows.Forms.ToolStripButton toolStripButtonWarnings;
    private System.Windows.Forms.ToolStripButton toolStripButtonMessages;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton toolStripButtonClear;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ImageList imageList;
    private System.Windows.Forms.DataGridView dataGridView;
    private System.Windows.Forms.DataGridViewImageColumn ColumnSeverity;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescription;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFile;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLine;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnColumn;
  }
}