namespace NReflect.Studio.Visitor
{
  partial class CSharpVisitorConfigControl
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
      if(disposing && (components != null))
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
      this.chkCreateAttributes = new System.Windows.Forms.CheckBox();
      this.chkNewLineAfterType = new System.Windows.Forms.CheckBox();
      this.chkNewLineAfterMember = new System.Windows.Forms.CheckBox();
      this.grpCodeGeneration = new System.Windows.Forms.GroupBox();
      this.txtKnownNamespaces = new System.Windows.Forms.TextBox();
      this.lblKnownNamespaces = new System.Windows.Forms.Label();
      this.chkUseNamespaces = new System.Windows.Forms.CheckBox();
      this.grpCodeFormatting = new System.Windows.Forms.GroupBox();
      this.grpCodeGeneration.SuspendLayout();
      this.grpCodeFormatting.SuspendLayout();
      this.SuspendLayout();
      // 
      // chkCreateAttributes
      // 
      this.chkCreateAttributes.AutoSize = true;
      this.chkCreateAttributes.Location = new System.Drawing.Point(6, 19);
      this.chkCreateAttributes.Name = "chkCreateAttributes";
      this.chkCreateAttributes.Size = new System.Drawing.Size(104, 17);
      this.chkCreateAttributes.TabIndex = 0;
      this.chkCreateAttributes.Text = "Create Attributes";
      this.chkCreateAttributes.UseVisualStyleBackColor = true;
      // 
      // chkNewLineAfterType
      // 
      this.chkNewLineAfterType.AutoSize = true;
      this.chkNewLineAfterType.Location = new System.Drawing.Point(6, 19);
      this.chkNewLineAfterType.Name = "chkNewLineAfterType";
      this.chkNewLineAfterType.Size = new System.Drawing.Size(170, 17);
      this.chkNewLineAfterType.TabIndex = 0;
      this.chkNewLineAfterType.Text = "Add a new line after each type";
      this.chkNewLineAfterType.UseVisualStyleBackColor = true;
      // 
      // chkNewLineAfterMember
      // 
      this.chkNewLineAfterMember.AutoSize = true;
      this.chkNewLineAfterMember.Location = new System.Drawing.Point(6, 42);
      this.chkNewLineAfterMember.Name = "chkNewLineAfterMember";
      this.chkNewLineAfterMember.Size = new System.Drawing.Size(187, 17);
      this.chkNewLineAfterMember.TabIndex = 0;
      this.chkNewLineAfterMember.Text = "Add a new line after each member";
      this.chkNewLineAfterMember.UseVisualStyleBackColor = true;
      // 
      // grpCodeGeneration
      // 
      this.grpCodeGeneration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.grpCodeGeneration.Controls.Add(this.txtKnownNamespaces);
      this.grpCodeGeneration.Controls.Add(this.lblKnownNamespaces);
      this.grpCodeGeneration.Controls.Add(this.chkUseNamespaces);
      this.grpCodeGeneration.Controls.Add(this.chkCreateAttributes);
      this.grpCodeGeneration.Location = new System.Drawing.Point(3, 3);
      this.grpCodeGeneration.Name = "grpCodeGeneration";
      this.grpCodeGeneration.Size = new System.Drawing.Size(508, 214);
      this.grpCodeGeneration.TabIndex = 1;
      this.grpCodeGeneration.TabStop = false;
      this.grpCodeGeneration.Text = "Code generation";
      // 
      // txtKnownNamespaces
      // 
      this.txtKnownNamespaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtKnownNamespaces.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtKnownNamespaces.Location = new System.Drawing.Point(6, 78);
      this.txtKnownNamespaces.Multiline = true;
      this.txtKnownNamespaces.Name = "txtKnownNamespaces";
      this.txtKnownNamespaces.Size = new System.Drawing.Size(496, 130);
      this.txtKnownNamespaces.TabIndex = 2;
      // 
      // lblKnownNamespaces
      // 
      this.lblKnownNamespaces.AutoSize = true;
      this.lblKnownNamespaces.Location = new System.Drawing.Point(6, 62);
      this.lblKnownNamespaces.Name = "lblKnownNamespaces";
      this.lblKnownNamespaces.Size = new System.Drawing.Size(69, 13);
      this.lblKnownNamespaces.TabIndex = 1;
      this.lblKnownNamespaces.Text = "Namespaces";
      // 
      // chkUseNamespaces
      // 
      this.chkUseNamespaces.AutoSize = true;
      this.chkUseNamespaces.Location = new System.Drawing.Point(6, 42);
      this.chkUseNamespaces.Name = "chkUseNamespaces";
      this.chkUseNamespaces.Size = new System.Drawing.Size(108, 17);
      this.chkUseNamespaces.TabIndex = 0;
      this.chkUseNamespaces.Text = "Use namespaces";
      this.chkUseNamespaces.UseVisualStyleBackColor = true;
      // 
      // grpCodeFormatting
      // 
      this.grpCodeFormatting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.grpCodeFormatting.Controls.Add(this.chkNewLineAfterType);
      this.grpCodeFormatting.Controls.Add(this.chkNewLineAfterMember);
      this.grpCodeFormatting.Location = new System.Drawing.Point(3, 223);
      this.grpCodeFormatting.Name = "grpCodeFormatting";
      this.grpCodeFormatting.Size = new System.Drawing.Size(508, 100);
      this.grpCodeFormatting.TabIndex = 2;
      this.grpCodeFormatting.TabStop = false;
      this.grpCodeFormatting.Text = "Code formatting";
      // 
      // CSharpVisitorConfigControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.grpCodeFormatting);
      this.Controls.Add(this.grpCodeGeneration);
      this.Name = "CSharpVisitorConfigControl";
      this.Size = new System.Drawing.Size(514, 439);
      this.grpCodeGeneration.ResumeLayout(false);
      this.grpCodeGeneration.PerformLayout();
      this.grpCodeFormatting.ResumeLayout(false);
      this.grpCodeFormatting.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.CheckBox chkCreateAttributes;
    private System.Windows.Forms.CheckBox chkNewLineAfterType;
    private System.Windows.Forms.CheckBox chkNewLineAfterMember;
    private System.Windows.Forms.GroupBox grpCodeGeneration;
    private System.Windows.Forms.CheckBox chkUseNamespaces;
    private System.Windows.Forms.GroupBox grpCodeFormatting;
    private System.Windows.Forms.TextBox txtKnownNamespaces;
    private System.Windows.Forms.Label lblKnownNamespaces;
  }
}
