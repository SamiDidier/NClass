namespace NReflect.Studio
{
  partial class RunVisitorsForm
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
      this.lstVisitors = new System.Windows.Forms.CheckedListBox();
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.lblVisitors = new System.Windows.Forms.Label();
      this.chkRunTestCases = new System.Windows.Forms.CheckBox();
      this.cmdRun = new System.Windows.Forms.Button();
      this.cmdClose = new System.Windows.Forms.Button();
      this.cmdChoose = new System.Windows.Forms.Button();
      this.lblSaveLocation = new System.Windows.Forms.Label();
      this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
      this.cmdAll = new System.Windows.Forms.Button();
      this.cmdNone = new System.Windows.Forms.Button();
      this.grpRunFor = new System.Windows.Forms.GroupBox();
      this.radioOnlySelected = new System.Windows.Forms.RadioButton();
      this.radioAll = new System.Windows.Forms.RadioButton();
      this.txtSaveLocation = new System.Windows.Forms.TextBox();
      this.grpRunFor.SuspendLayout();
      this.SuspendLayout();
      // 
      // lstVisitors
      // 
      this.lstVisitors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lstVisitors.CheckOnClick = true;
      this.lstVisitors.FormattingEnabled = true;
      this.lstVisitors.IntegralHeight = false;
      this.lstVisitors.Location = new System.Drawing.Point(12, 81);
      this.lstVisitors.Name = "lstVisitors";
      this.lstVisitors.Size = new System.Drawing.Size(240, 49);
      this.lstVisitors.TabIndex = 0;
      // 
      // progressBar
      // 
      this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar.Location = new System.Drawing.Point(12, 198);
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(240, 23);
      this.progressBar.TabIndex = 1;
      // 
      // lblVisitors
      // 
      this.lblVisitors.AutoSize = true;
      this.lblVisitors.Location = new System.Drawing.Point(9, 65);
      this.lblVisitors.Name = "lblVisitors";
      this.lblVisitors.Size = new System.Drawing.Size(71, 13);
      this.lblVisitors.TabIndex = 2;
      this.lblVisitors.Text = "Vistors to run:";
      // 
      // chkRunTestCases
      // 
      this.chkRunTestCases.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.chkRunTestCases.AutoSize = true;
      this.chkRunTestCases.Checked = global::NReflect.Studio.Properties.Settings.Default.RunTestCaseBeforeVisitors;
      this.chkRunTestCases.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::NReflect.Studio.Properties.Settings.Default, "RunTestCaseBeforeVisitors", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.chkRunTestCases.Location = new System.Drawing.Point(12, 136);
      this.chkRunTestCases.Name = "chkRunTestCases";
      this.chkRunTestCases.Size = new System.Drawing.Size(229, 17);
      this.chkRunTestCases.TabIndex = 3;
      this.chkRunTestCases.Text = "Run all test cases before running the visitor";
      this.chkRunTestCases.UseVisualStyleBackColor = true;
      // 
      // cmdRun
      // 
      this.cmdRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdRun.Location = new System.Drawing.Point(177, 227);
      this.cmdRun.Name = "cmdRun";
      this.cmdRun.Size = new System.Drawing.Size(75, 23);
      this.cmdRun.TabIndex = 4;
      this.cmdRun.Text = "Run";
      this.cmdRun.UseVisualStyleBackColor = true;
      this.cmdRun.Click += new System.EventHandler(this.cmdRun_Click);
      // 
      // cmdClose
      // 
      this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdClose.Location = new System.Drawing.Point(12, 227);
      this.cmdClose.Name = "cmdClose";
      this.cmdClose.Size = new System.Drawing.Size(75, 23);
      this.cmdClose.TabIndex = 5;
      this.cmdClose.Text = "Close";
      this.cmdClose.UseVisualStyleBackColor = true;
      // 
      // cmdChoose
      // 
      this.cmdChoose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdChoose.Location = new System.Drawing.Point(227, 170);
      this.cmdChoose.Name = "cmdChoose";
      this.cmdChoose.Size = new System.Drawing.Size(25, 23);
      this.cmdChoose.TabIndex = 7;
      this.cmdChoose.Text = "...";
      this.cmdChoose.UseVisualStyleBackColor = true;
      this.cmdChoose.Click += new System.EventHandler(this.cmdChoose_Click);
      // 
      // lblSaveLocation
      // 
      this.lblSaveLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lblSaveLocation.AutoSize = true;
      this.lblSaveLocation.Location = new System.Drawing.Point(12, 156);
      this.lblSaveLocation.Name = "lblSaveLocation";
      this.lblSaveLocation.Size = new System.Drawing.Size(95, 13);
      this.lblSaveLocation.TabIndex = 8;
      this.lblSaveLocation.Text = "Save the results at";
      // 
      // backgroundWorker
      // 
      this.backgroundWorker.WorkerReportsProgress = true;
      this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
      this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
      this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
      // 
      // cmdAll
      // 
      this.cmdAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdAll.Location = new System.Drawing.Point(170, 59);
      this.cmdAll.Name = "cmdAll";
      this.cmdAll.Size = new System.Drawing.Size(39, 21);
      this.cmdAll.TabIndex = 9;
      this.cmdAll.Text = "All";
      this.cmdAll.UseVisualStyleBackColor = true;
      this.cmdAll.Click += new System.EventHandler(this.cmdAll_Click);
      // 
      // cmdNone
      // 
      this.cmdNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdNone.Location = new System.Drawing.Point(212, 59);
      this.cmdNone.Name = "cmdNone";
      this.cmdNone.Size = new System.Drawing.Size(40, 21);
      this.cmdNone.TabIndex = 9;
      this.cmdNone.Text = "None";
      this.cmdNone.UseVisualStyleBackColor = true;
      this.cmdNone.Click += new System.EventHandler(this.cmdNone_Click);
      // 
      // grpRunFor
      // 
      this.grpRunFor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.grpRunFor.Controls.Add(this.radioOnlySelected);
      this.grpRunFor.Controls.Add(this.radioAll);
      this.grpRunFor.Location = new System.Drawing.Point(12, 12);
      this.grpRunFor.Name = "grpRunFor";
      this.grpRunFor.Size = new System.Drawing.Size(240, 44);
      this.grpRunFor.TabIndex = 10;
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
      // txtSaveLocation
      // 
      this.txtSaveLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSaveLocation.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::NReflect.Studio.Properties.Settings.Default, "VisitorResultsDirectory", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.txtSaveLocation.Location = new System.Drawing.Point(12, 172);
      this.txtSaveLocation.Name = "txtSaveLocation";
      this.txtSaveLocation.Size = new System.Drawing.Size(209, 20);
      this.txtSaveLocation.TabIndex = 6;
      this.txtSaveLocation.Text = global::NReflect.Studio.Properties.Settings.Default.VisitorResultsDirectory;
      // 
      // RunVisitorsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdClose;
      this.ClientSize = new System.Drawing.Size(264, 262);
      this.Controls.Add(this.grpRunFor);
      this.Controls.Add(this.cmdNone);
      this.Controls.Add(this.cmdAll);
      this.Controls.Add(this.lblSaveLocation);
      this.Controls.Add(this.cmdChoose);
      this.Controls.Add(this.txtSaveLocation);
      this.Controls.Add(this.cmdClose);
      this.Controls.Add(this.cmdRun);
      this.Controls.Add(this.chkRunTestCases);
      this.Controls.Add(this.lblVisitors);
      this.Controls.Add(this.progressBar);
      this.Controls.Add(this.lstVisitors);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(280, 300);
      this.Name = "RunVisitorsForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Run visitors...";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RunVisitorsForm_FormClosing);
      this.grpRunFor.ResumeLayout(false);
      this.grpRunFor.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckedListBox lstVisitors;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Label lblVisitors;
    private System.Windows.Forms.CheckBox chkRunTestCases;
    private System.Windows.Forms.Button cmdRun;
    private System.Windows.Forms.Button cmdClose;
    private System.Windows.Forms.TextBox txtSaveLocation;
    private System.Windows.Forms.Button cmdChoose;
    private System.Windows.Forms.Label lblSaveLocation;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    private System.ComponentModel.BackgroundWorker backgroundWorker;
    private System.Windows.Forms.Button cmdAll;
    private System.Windows.Forms.Button cmdNone;
    private System.Windows.Forms.GroupBox grpRunFor;
    private System.Windows.Forms.RadioButton radioOnlySelected;
    private System.Windows.Forms.RadioButton radioAll;
  }
}