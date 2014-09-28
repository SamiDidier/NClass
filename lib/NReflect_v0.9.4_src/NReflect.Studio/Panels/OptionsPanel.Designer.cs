namespace NReflect.Studio.Panels
{
  partial class OptionsPanel
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsPanel));
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageGeneral = new System.Windows.Forms.TabPage();
      this.cmdChooseTestCaseDirectory = new System.Windows.Forms.Button();
      this.txtTestCaseDirectory = new System.Windows.Forms.TextBox();
      this.lblTestCaseDirectory = new System.Windows.Forms.Label();
      this.chkClearMessagesOnRun = new System.Windows.Forms.CheckBox();
      this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.tabMain.SuspendLayout();
      this.tabPageGeneral.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabMain
      // 
      this.tabMain.Controls.Add(this.tabPageGeneral);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.Location = new System.Drawing.Point(0, 0);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(507, 377);
      this.tabMain.TabIndex = 0;
      // 
      // tabPageGeneral
      // 
      this.tabPageGeneral.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageGeneral.Controls.Add(this.cmdChooseTestCaseDirectory);
      this.tabPageGeneral.Controls.Add(this.txtTestCaseDirectory);
      this.tabPageGeneral.Controls.Add(this.lblTestCaseDirectory);
      this.tabPageGeneral.Controls.Add(this.chkClearMessagesOnRun);
      this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
      this.tabPageGeneral.Name = "tabPageGeneral";
      this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageGeneral.Size = new System.Drawing.Size(499, 351);
      this.tabPageGeneral.TabIndex = 0;
      this.tabPageGeneral.Text = "General";
      // 
      // cmdChooseTestCaseDirectory
      // 
      this.cmdChooseTestCaseDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdChooseTestCaseDirectory.Location = new System.Drawing.Point(465, 27);
      this.cmdChooseTestCaseDirectory.Name = "cmdChooseTestCaseDirectory";
      this.cmdChooseTestCaseDirectory.Size = new System.Drawing.Size(30, 23);
      this.cmdChooseTestCaseDirectory.TabIndex = 5;
      this.cmdChooseTestCaseDirectory.Text = "...";
      this.cmdChooseTestCaseDirectory.UseVisualStyleBackColor = true;
      this.cmdChooseTestCaseDirectory.Click += new System.EventHandler(this.cmdChooseTestCaseDirectory_Click);
      // 
      // txtTestCaseDirectory
      // 
      this.txtTestCaseDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtTestCaseDirectory.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::NReflect.Studio.Properties.Settings.Default, "TestCaseDirectory", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.txtTestCaseDirectory.Location = new System.Drawing.Point(106, 29);
      this.txtTestCaseDirectory.Name = "txtTestCaseDirectory";
      this.txtTestCaseDirectory.ReadOnly = true;
      this.txtTestCaseDirectory.Size = new System.Drawing.Size(353, 20);
      this.txtTestCaseDirectory.TabIndex = 4;
      this.txtTestCaseDirectory.Text = global::NReflect.Studio.Properties.Settings.Default.TestCaseDirectory;
      // 
      // lblTestCaseDirectory
      // 
      this.lblTestCaseDirectory.AutoSize = true;
      this.lblTestCaseDirectory.Location = new System.Drawing.Point(3, 32);
      this.lblTestCaseDirectory.Name = "lblTestCaseDirectory";
      this.lblTestCaseDirectory.Size = new System.Drawing.Size(97, 13);
      this.lblTestCaseDirectory.TabIndex = 3;
      this.lblTestCaseDirectory.Text = "Test case directory";
      // 
      // chkClearMessagesOnRun
      // 
      this.chkClearMessagesOnRun.AutoSize = true;
      this.chkClearMessagesOnRun.Checked = global::NReflect.Studio.Properties.Settings.Default.OptionsClearMessagesOnRun;
      this.chkClearMessagesOnRun.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkClearMessagesOnRun.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::NReflect.Studio.Properties.Settings.Default, "OptionsClearMessagesOnRun", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.chkClearMessagesOnRun.Location = new System.Drawing.Point(6, 6);
      this.chkClearMessagesOnRun.Name = "chkClearMessagesOnRun";
      this.chkClearMessagesOnRun.Size = new System.Drawing.Size(133, 17);
      this.chkClearMessagesOnRun.TabIndex = 0;
      this.chkClearMessagesOnRun.Text = "Clear messages on run";
      this.chkClearMessagesOnRun.UseVisualStyleBackColor = true;
      // 
      // OptionsPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(507, 377);
      this.Controls.Add(this.tabMain);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Image = global::NReflect.Studio.Properties.Resources.Options;
      this.Name = "OptionsPanel";
      this.Text = "Options";
      this.tabMain.ResumeLayout(false);
      this.tabPageGeneral.ResumeLayout(false);
      this.tabPageGeneral.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageGeneral;
    private System.Windows.Forms.CheckBox chkClearMessagesOnRun;
    private System.Windows.Forms.Button cmdChooseTestCaseDirectory;
    private System.Windows.Forms.TextBox txtTestCaseDirectory;
    private System.Windows.Forms.Label lblTestCaseDirectory;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
  }
}