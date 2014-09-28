using System.Windows.Forms;
using NReflect.Studio.Models;

namespace NReflect.Studio.Panels
{
  partial class CodePanel
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodePanel));
      this.txtCode = new FastColoredTextBoxNS.FastColoredTextBox();
      this.testCaseBindingSource = new System.Windows.Forms.BindingSource(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.testCaseBindingSource)).BeginInit();
      this.SuspendLayout();
      // 
      // txtCode
      // 
      this.txtCode.AllowDrop = true;
      this.txtCode.AutoScrollMinSize = new System.Drawing.Size(27, 14);
      this.txtCode.BackBrush = null;
      this.txtCode.CurrentLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
      this.txtCode.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.testCaseBindingSource, "Code", true, DataSourceUpdateMode.OnPropertyChanged));
      this.txtCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtCode.IsReplaceMode = false;
      this.txtCode.Language = FastColoredTextBoxNS.Language.CSharp;
      this.txtCode.LeftBracket = '{';
      this.txtCode.Location = new System.Drawing.Point(0, 0);
      this.txtCode.Name = "txtCode";
      this.txtCode.Paddings = new System.Windows.Forms.Padding(0);
      this.txtCode.RightBracket = '}';
      this.txtCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtCode.ShowFoldingLines = true;
      this.txtCode.Size = new System.Drawing.Size(602, 486);
      this.txtCode.TabIndex = 0;
      this.txtCode.TabLength = 2;
      // 
      // testCaseBindingSource
      // 
      this.testCaseBindingSource.DataSource = typeof(NReflect.Studio.Models.TestCaseCSharp);
      // 
      // CodePanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(602, 486);
      this.Controls.Add(this.txtCode);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.HideOnClose = true;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Image = global::NReflect.Studio.Properties.Resources.CSharpFile;
      this.KeyPreview = true;
      this.Name = "CodePanel";
      this.Text = "Code";
      ((System.ComponentModel.ISupportInitialize)(this.testCaseBindingSource)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.BindingSource testCaseBindingSource;
    private FastColoredTextBoxNS.FastColoredTextBox txtCode;



  }
}