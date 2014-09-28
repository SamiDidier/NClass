namespace NReflect.Studio.Panels
{
  partial class VisitorPanel
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisitorPanel));
      this.lblVisitor = new System.Windows.Forms.Label();
      this.cboVisitor = new System.Windows.Forms.ComboBox();
      this.lblVisitorResult = new System.Windows.Forms.Label();
      this.txtVisitorResult = new FastColoredTextBoxNS.FastColoredTextBox();
      this.SuspendLayout();
      // 
      // lblVisitor
      // 
      this.lblVisitor.AutoSize = true;
      this.lblVisitor.Location = new System.Drawing.Point(12, 9);
      this.lblVisitor.Name = "lblVisitor";
      this.lblVisitor.Size = new System.Drawing.Size(35, 13);
      this.lblVisitor.TabIndex = 3;
      this.lblVisitor.Text = "Visitor";
      // 
      // cboVisitor
      // 
      this.cboVisitor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cboVisitor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboVisitor.FormattingEnabled = true;
      this.cboVisitor.Location = new System.Drawing.Point(53, 6);
      this.cboVisitor.Name = "cboVisitor";
      this.cboVisitor.Size = new System.Drawing.Size(488, 21);
      this.cboVisitor.TabIndex = 2;
      this.cboVisitor.SelectedIndexChanged += new System.EventHandler(this.cboVisitor_SelectedIndexChanged);
      // 
      // lblVisitorResult
      // 
      this.lblVisitorResult.AutoSize = true;
      this.lblVisitorResult.Location = new System.Drawing.Point(12, 30);
      this.lblVisitorResult.Name = "lblVisitorResult";
      this.lblVisitorResult.Size = new System.Drawing.Size(37, 13);
      this.lblVisitorResult.TabIndex = 1;
      this.lblVisitorResult.Text = "Result";
      // 
      // txtVisitorResult
      // 
      this.txtVisitorResult.AllowDrop = true;
      this.txtVisitorResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtVisitorResult.AutoScrollMinSize = new System.Drawing.Size(27, 14);
      this.txtVisitorResult.BackBrush = null;
      this.txtVisitorResult.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtVisitorResult.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtVisitorResult.IsReplaceMode = false;
      this.txtVisitorResult.Location = new System.Drawing.Point(12, 46);
      this.txtVisitorResult.Name = "txtVisitorResult";
      this.txtVisitorResult.Paddings = new System.Windows.Forms.Padding(0);
      this.txtVisitorResult.ReadOnly = true;
      this.txtVisitorResult.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtVisitorResult.Size = new System.Drawing.Size(529, 461);
      this.txtVisitorResult.TabIndex = 4;
      // 
      // VisitorPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(553, 519);
      this.Controls.Add(this.txtVisitorResult);
      this.Controls.Add(this.lblVisitor);
      this.Controls.Add(this.cboVisitor);
      this.Controls.Add(this.lblVisitorResult);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.HideOnClose = true;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Image = global::NReflect.Studio.Properties.Resources.Visitor;
      this.Name = "VisitorPanel";
      this.Text = "Visitor";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblVisitorResult;
    private System.Windows.Forms.Label lblVisitor;
    private System.Windows.Forms.ComboBox cboVisitor;
    private FastColoredTextBoxNS.FastColoredTextBox txtVisitorResult;


  }
}