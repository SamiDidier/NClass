namespace NReflect.Studio
{
  partial class CompileForm
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
      this.grpRunFor = new System.Windows.Forms.GroupBox();
      this.radioOnlySelected = new System.Windows.Forms.RadioButton();
      this.radioAll = new System.Windows.Forms.RadioButton();
      this.cmdClose = new System.Windows.Forms.Button();
      this.cmdRun = new System.Windows.Forms.Button();
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.grpDestination = new System.Windows.Forms.GroupBox();
      this.cmdChooseDllName = new System.Windows.Forms.Button();
      this.txtDllName = new System.Windows.Forms.TextBox();
      this.cmdChooseDir = new System.Windows.Forms.Button();
      this.txtSaveDir = new System.Windows.Forms.TextBox();
      this.radioSingleDll = new System.Windows.Forms.RadioButton();
      this.radioMultipleDlls = new System.Windows.Forms.RadioButton();
      this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
      this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
      this.grpRunFor.SuspendLayout();
      this.grpDestination.SuspendLayout();
      this.SuspendLayout();
      // 
      // grpRunFor
      // 
      this.grpRunFor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.grpRunFor.Controls.Add(this.radioOnlySelected);
      this.grpRunFor.Controls.Add(this.radioAll);
      this.grpRunFor.Location = new System.Drawing.Point(12, 12);
      this.grpRunFor.Name = "grpRunFor";
      this.grpRunFor.Size = new System.Drawing.Size(395, 44);
      this.grpRunFor.TabIndex = 11;
      this.grpRunFor.TabStop = false;
      this.grpRunFor.Text = "Run visitors for...";
      // 
      // radioOnlySelected
      // 
      this.radioOnlySelected.AutoSize = true;
      this.radioOnlySelected.Location = new System.Drawing.Point(102, 19);
      this.radioOnlySelected.Name = "radioOnlySelected";
      this.radioOnlySelected.Size = new System.Drawing.Size(87, 17);
      this.radioOnlySelected.TabIndex = 1;
      this.radioOnlySelected.Text = "only selected";
      this.radioOnlySelected.UseVisualStyleBackColor = true;
      // 
      // radioAll
      // 
      this.radioAll.AutoSize = true;
      this.radioAll.Checked = true;
      this.radioAll.Location = new System.Drawing.Point(10, 19);
      this.radioAll.Name = "radioAll";
      this.radioAll.Size = new System.Drawing.Size(86, 17);
      this.radioAll.TabIndex = 0;
      this.radioAll.TabStop = true;
      this.radioAll.Text = "all test cases";
      this.radioAll.UseVisualStyleBackColor = true;
      // 
      // cmdClose
      // 
      this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdClose.Location = new System.Drawing.Point(12, 218);
      this.cmdClose.Name = "cmdClose";
      this.cmdClose.Size = new System.Drawing.Size(75, 23);
      this.cmdClose.TabIndex = 14;
      this.cmdClose.Text = "Close";
      this.cmdClose.UseVisualStyleBackColor = true;
      // 
      // cmdRun
      // 
      this.cmdRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdRun.Location = new System.Drawing.Point(332, 218);
      this.cmdRun.Name = "cmdRun";
      this.cmdRun.Size = new System.Drawing.Size(75, 23);
      this.cmdRun.TabIndex = 13;
      this.cmdRun.Text = "Run";
      this.cmdRun.UseVisualStyleBackColor = true;
      this.cmdRun.Click += new System.EventHandler(this.cmdRun_Click);
      // 
      // progressBar
      // 
      this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar.Location = new System.Drawing.Point(12, 189);
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(395, 23);
      this.progressBar.TabIndex = 12;
      // 
      // grpDestination
      // 
      this.grpDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.grpDestination.Controls.Add(this.cmdChooseDllName);
      this.grpDestination.Controls.Add(this.txtDllName);
      this.grpDestination.Controls.Add(this.cmdChooseDir);
      this.grpDestination.Controls.Add(this.txtSaveDir);
      this.grpDestination.Controls.Add(this.radioSingleDll);
      this.grpDestination.Controls.Add(this.radioMultipleDlls);
      this.grpDestination.Location = new System.Drawing.Point(12, 62);
      this.grpDestination.Name = "grpDestination";
      this.grpDestination.Size = new System.Drawing.Size(395, 121);
      this.grpDestination.TabIndex = 15;
      this.grpDestination.TabStop = false;
      this.grpDestination.Text = "Destination";
      // 
      // cmdChooseDllName
      // 
      this.cmdChooseDllName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdChooseDllName.Enabled = false;
      this.cmdChooseDllName.Location = new System.Drawing.Point(364, 90);
      this.cmdChooseDllName.Name = "cmdChooseDllName";
      this.cmdChooseDllName.Size = new System.Drawing.Size(25, 23);
      this.cmdChooseDllName.TabIndex = 11;
      this.cmdChooseDllName.Text = "...";
      this.cmdChooseDllName.UseVisualStyleBackColor = true;
      this.cmdChooseDllName.Click += new System.EventHandler(this.cmdChooseDllName_Click);
      // 
      // txtDllName
      // 
      this.txtDllName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDllName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::NReflect.Studio.Properties.Settings.Default, "CompileFormDllName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.txtDllName.Enabled = false;
      this.txtDllName.Location = new System.Drawing.Point(10, 92);
      this.txtDllName.Name = "txtDllName";
      this.txtDllName.Size = new System.Drawing.Size(348, 20);
      this.txtDllName.TabIndex = 10;
      this.txtDllName.Text = global::NReflect.Studio.Properties.Settings.Default.CompileFormDllName;
      // 
      // cmdChooseDir
      // 
      this.cmdChooseDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdChooseDir.Location = new System.Drawing.Point(364, 40);
      this.cmdChooseDir.Name = "cmdChooseDir";
      this.cmdChooseDir.Size = new System.Drawing.Size(25, 23);
      this.cmdChooseDir.TabIndex = 9;
      this.cmdChooseDir.Text = "...";
      this.cmdChooseDir.UseVisualStyleBackColor = true;
      this.cmdChooseDir.Click += new System.EventHandler(this.cmdChooseDir_Click);
      // 
      // txtSaveDir
      // 
      this.txtSaveDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSaveDir.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::NReflect.Studio.Properties.Settings.Default, "CompileFormSaveDir", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.txtSaveDir.Location = new System.Drawing.Point(10, 42);
      this.txtSaveDir.Name = "txtSaveDir";
      this.txtSaveDir.Size = new System.Drawing.Size(348, 20);
      this.txtSaveDir.TabIndex = 8;
      this.txtSaveDir.Text = global::NReflect.Studio.Properties.Settings.Default.CompileFormSaveDir;
      // 
      // radioSingleDll
      // 
      this.radioSingleDll.AutoSize = true;
      this.radioSingleDll.Location = new System.Drawing.Point(10, 69);
      this.radioSingleDll.Name = "radioSingleDll";
      this.radioSingleDll.Size = new System.Drawing.Size(198, 17);
      this.radioSingleDll.TabIndex = 1;
      this.radioSingleDll.Text = "Compile all test cases into a single dll";
      this.radioSingleDll.UseVisualStyleBackColor = true;
      this.radioSingleDll.CheckedChanged += new System.EventHandler(this.radioCompileType_CheckedChanged);
      // 
      // radioMultipleDlls
      // 
      this.radioMultipleDlls.AutoSize = true;
      this.radioMultipleDlls.Checked = true;
      this.radioMultipleDlls.Location = new System.Drawing.Point(10, 19);
      this.radioMultipleDlls.Name = "radioMultipleDlls";
      this.radioMultipleDlls.Size = new System.Drawing.Size(207, 17);
      this.radioMultipleDlls.TabIndex = 0;
      this.radioMultipleDlls.TabStop = true;
      this.radioMultipleDlls.Text = "Compile each test case into a single dll";
      this.radioMultipleDlls.UseVisualStyleBackColor = true;
      this.radioMultipleDlls.CheckedChanged += new System.EventHandler(this.radioCompileType_CheckedChanged);
      // 
      // saveFileDialog
      // 
      this.saveFileDialog.DefaultExt = "dll";
      this.saveFileDialog.Filter = "Dll files|*.dll|All files|*.*";
      this.saveFileDialog.Title = "Save dll to...";
      // 
      // backgroundWorker
      // 
      this.backgroundWorker.WorkerReportsProgress = true;
      this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
      this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
      this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
      // 
      // CompileForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdClose;
      this.ClientSize = new System.Drawing.Size(419, 253);
      this.Controls.Add(this.grpDestination);
      this.Controls.Add(this.cmdClose);
      this.Controls.Add(this.cmdRun);
      this.Controls.Add(this.progressBar);
      this.Controls.Add(this.grpRunFor);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(435, 291);
      this.Name = "CompileForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Compile test cases...";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CompileForm_FormClosing);
      this.grpRunFor.ResumeLayout(false);
      this.grpRunFor.PerformLayout();
      this.grpDestination.ResumeLayout(false);
      this.grpDestination.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox grpRunFor;
    private System.Windows.Forms.RadioButton radioOnlySelected;
    private System.Windows.Forms.RadioButton radioAll;
    private System.Windows.Forms.Button cmdClose;
    private System.Windows.Forms.Button cmdRun;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.GroupBox grpDestination;
    private System.Windows.Forms.RadioButton radioSingleDll;
    private System.Windows.Forms.RadioButton radioMultipleDlls;
    private System.Windows.Forms.Button cmdChooseDllName;
    private System.Windows.Forms.TextBox txtDllName;
    private System.Windows.Forms.Button cmdChooseDir;
    private System.Windows.Forms.TextBox txtSaveDir;
    private System.Windows.Forms.SaveFileDialog saveFileDialog;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    private System.ComponentModel.BackgroundWorker backgroundWorker;
  }
}