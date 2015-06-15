namespace NClass.CodeGenerator
{
	partial class Dialog
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
			if (disposing && (components != null)) {
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
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lstImportList = new System.Windows.Forms.ListBox();
            this.importToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolDelete = new System.Windows.Forms.ToolStripButton();
            this.toolMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolMoveUp = new System.Windows.Forms.ToolStripButton();
            this.toolImportList = new System.Windows.Forms.ToolStripLabel();
            this.txtNewImport = new System.Windows.Forms.TextBox();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.chkNotImplemented = new System.Windows.Forms.CheckBox();
            this.lblSolutionType = new System.Windows.Forms.Label();
            this.cboSolutionType = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.lblDestination = new System.Windows.Forms.Label();
            this.tabCntOptions = new System.Windows.Forms.TabControl();
            this.tabPageCommon = new System.Windows.Forms.TabPage();
            this.tabPageCSharpExt = new System.Windows.Forms.TabPage();
            this.btnCustomFormatStyle = new System.Windows.Forms.Button();
            this.btnCopyrightHeader = new System.Windows.Forms.Button();
            this.cboTemplateFile = new System.Windows.Forms.ComboBox();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.chkSortUsing = new System.Windows.Forms.CheckBox();
            this.lblCSharpFormatStyle = new System.Windows.Forms.Label();
            this.cboCSharpFormat = new System.Windows.Forms.ComboBox();
            this.chkXMLDocFood = new System.Windows.Forms.CheckBox();
            this.chkGenerateNunitTests = new System.Windows.Forms.CheckBox();
            this.tabPageCSharpJava = new System.Windows.Forms.TabPage();
            this.lblIndentSize = new System.Windows.Forms.Label();
            this.chkUseTabs = new System.Windows.Forms.CheckBox();
            this.updIndentSize = new System.Windows.Forms.NumericUpDown();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.importToolStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabCntOptions.SuspendLayout();
            this.tabPageCommon.SuspendLayout();
            this.tabPageCSharpExt.SuspendLayout();
            this.tabPageCSharpJava.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updIndentSize)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGenerate.Location = new System.Drawing.Point(635, 339);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 21;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(716, 339);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lstImportList
            // 
            this.lstImportList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstImportList.FormattingEnabled = true;
            this.lstImportList.Location = new System.Drawing.Point(12, 90);
            this.lstImportList.Name = "lstImportList";
            this.lstImportList.Size = new System.Drawing.Size(413, 238);
            this.lstImportList.TabIndex = 4;
            this.lstImportList.SelectedValueChanged += new System.EventHandler(this.lstImportList_SelectedValueChanged);
            this.lstImportList.Leave += new System.EventHandler(this.lstImportList_Leave);
            // 
            // importToolStrip
            // 
            this.importToolStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importToolStrip.AutoSize = false;
            this.importToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.importToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.importToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolDelete,
            this.toolMoveDown,
            this.toolMoveUp,
            this.toolImportList});
            this.importToolStrip.Location = new System.Drawing.Point(111, 62);
            this.importToolStrip.Name = "importToolStrip";
            this.importToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.importToolStrip.Size = new System.Drawing.Size(314, 25);
            this.importToolStrip.TabIndex = 3;
            this.importToolStrip.Text = "toolStrip1";
            // 
            // toolDelete
            // 
            this.toolDelete.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDelete.Enabled = false;
            this.toolDelete.Image = global::NClass.CodeGenerator.Properties.Resources.Delete;
            this.toolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDelete.Name = "toolDelete";
            this.toolDelete.Size = new System.Drawing.Size(23, 22);
            this.toolDelete.Text = "Delete";
            this.toolDelete.Click += new System.EventHandler(this.toolDelete_Click);
            // 
            // toolMoveDown
            // 
            this.toolMoveDown.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolMoveDown.Enabled = false;
            this.toolMoveDown.Image = global::NClass.CodeGenerator.Properties.Resources.MoveDown;
            this.toolMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolMoveDown.Name = "toolMoveDown";
            this.toolMoveDown.Size = new System.Drawing.Size(23, 22);
            this.toolMoveDown.Text = "Move Down";
            this.toolMoveDown.Click += new System.EventHandler(this.toolMoveDown_Click);
            // 
            // toolMoveUp
            // 
            this.toolMoveUp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolMoveUp.Enabled = false;
            this.toolMoveUp.Image = global::NClass.CodeGenerator.Properties.Resources.MoveUp;
            this.toolMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolMoveUp.Name = "toolMoveUp";
            this.toolMoveUp.Size = new System.Drawing.Size(23, 22);
            this.toolMoveUp.Text = "Move Up";
            this.toolMoveUp.Click += new System.EventHandler(this.toolMoveUp_Click);
            // 
            // toolImportList
            // 
            this.toolImportList.Name = "toolImportList";
            this.toolImportList.Size = new System.Drawing.Size(61, 22);
            this.toolImportList.Text = "Import list";
            // 
            // txtNewImport
            // 
            this.txtNewImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewImport.Location = new System.Drawing.Point(12, 341);
            this.txtNewImport.Name = "txtNewImport";
            this.txtNewImport.Size = new System.Drawing.Size(332, 20);
            this.txtNewImport.TabIndex = 5;
            this.txtNewImport.TextChanged += new System.EventHandler(this.txtNewImport_TextChanged);
            this.txtNewImport.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewImport_KeyDown);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddItem.Enabled = false;
            this.btnAddItem.Location = new System.Drawing.Point(350, 339);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(75, 23);
            this.btnAddItem.TabIndex = 6;
            this.btnAddItem.Text = "Add item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // chkNotImplemented
            // 
            this.chkNotImplemented.AutoSize = true;
            this.chkNotImplemented.Location = new System.Drawing.Point(6, 6);
            this.chkNotImplemented.Name = "chkNotImplemented";
            this.chkNotImplemented.Size = new System.Drawing.Size(243, 17);
            this.chkNotImplemented.TabIndex = 8;
            this.chkNotImplemented.Text = "Fill methods with \'Not implemented\' exceptions";
            this.chkNotImplemented.UseVisualStyleBackColor = true;
            // 
            // lblSolutionType
            // 
            this.lblSolutionType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSolutionType.AutoSize = true;
            this.lblSolutionType.Location = new System.Drawing.Point(3, 31);
            this.lblSolutionType.Name = "lblSolutionType";
            this.lblSolutionType.Size = new System.Drawing.Size(101, 13);
            this.lblSolutionType.TabIndex = 12;
            this.lblSolutionType.Text = "Type of solution file:";
            // 
            // cboSolutionType
            // 
            this.cboSolutionType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSolutionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSolutionType.FormattingEnabled = true;
            this.cboSolutionType.Items.AddRange(new object[] {
            "Visual Studio 2005",
            "Visual Studio 2008"});
            this.cboSolutionType.Location = new System.Drawing.Point(6, 47);
            this.cboSolutionType.Name = "cboSolutionType";
            this.cboSolutionType.Size = new System.Drawing.Size(271, 21);
            this.cboSolutionType.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.btnBrowse, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtDestination, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDestination, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(779, 37);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBrowse.Location = new System.Drawing.Point(704, 7);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtDestination
            // 
            this.txtDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDestination.Location = new System.Drawing.Point(66, 8);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(632, 20);
            this.txtDestination.TabIndex = 0;
            // 
            // lblDestination
            // 
            this.lblDestination.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDestination.AutoSize = true;
            this.lblDestination.Location = new System.Drawing.Point(0, 11);
            this.lblDestination.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(63, 13);
            this.lblDestination.TabIndex = 4;
            this.lblDestination.Text = "Destination:";
            // 
            // tabCntOptions
            // 
            this.tabCntOptions.Controls.Add(this.tabPageCommon);
            this.tabCntOptions.Controls.Add(this.tabPageCSharpExt);
            this.tabCntOptions.Controls.Add(this.tabPageCSharpJava);
            this.tabCntOptions.Location = new System.Drawing.Point(431, 62);
            this.tabCntOptions.Name = "tabCntOptions";
            this.tabCntOptions.SelectedIndex = 0;
            this.tabCntOptions.Size = new System.Drawing.Size(360, 266);
            this.tabCntOptions.TabIndex = 18;
            // 
            // tabPageCommon
            // 
            this.tabPageCommon.Controls.Add(this.chkNotImplemented);
            this.tabPageCommon.Controls.Add(this.cboSolutionType);
            this.tabPageCommon.Controls.Add(this.lblSolutionType);
            this.tabPageCommon.Location = new System.Drawing.Point(4, 22);
            this.tabPageCommon.Name = "tabPageCommon";
            this.tabPageCommon.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCommon.Size = new System.Drawing.Size(352, 240);
            this.tabPageCommon.TabIndex = 2;
            this.tabPageCommon.Text = "Common";
            this.tabPageCommon.UseVisualStyleBackColor = true;
            // 
            // tabPageCSharpExt
            // 
            this.tabPageCSharpExt.Controls.Add(this.btnCustomFormatStyle);
            this.tabPageCSharpExt.Controls.Add(this.btnCopyrightHeader);
            this.tabPageCSharpExt.Controls.Add(this.cboTemplateFile);
            this.tabPageCSharpExt.Controls.Add(this.lblTemplate);
            this.tabPageCSharpExt.Controls.Add(this.chkSortUsing);
            this.tabPageCSharpExt.Controls.Add(this.lblCSharpFormatStyle);
            this.tabPageCSharpExt.Controls.Add(this.cboCSharpFormat);
            this.tabPageCSharpExt.Controls.Add(this.chkXMLDocFood);
            this.tabPageCSharpExt.Controls.Add(this.chkGenerateNunitTests);
            this.tabPageCSharpExt.Location = new System.Drawing.Point(4, 22);
            this.tabPageCSharpExt.Name = "tabPageCSharpExt";
            this.tabPageCSharpExt.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCSharpExt.Size = new System.Drawing.Size(352, 240);
            this.tabPageCSharpExt.TabIndex = 3;
            this.tabPageCSharpExt.Text = "C# extended";
            this.tabPageCSharpExt.UseVisualStyleBackColor = true;
            // 
            // btnCustomFormatStyle
            // 
            this.btnCustomFormatStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCustomFormatStyle.Location = new System.Drawing.Point(220, 67);
            this.btnCustomFormatStyle.Name = "btnCustomFormatStyle";
            this.btnCustomFormatStyle.Size = new System.Drawing.Size(126, 23);
            this.btnCustomFormatStyle.TabIndex = 14;
            this.btnCustomFormatStyle.Text = "Custom Format Style";
            this.btnCustomFormatStyle.UseVisualStyleBackColor = true;
            this.btnCustomFormatStyle.Click += new System.EventHandler(this.btnCustomFormatStyle_Click);
            // 
            // btnCopyrightHeader
            // 
            this.btnCopyrightHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyrightHeader.Location = new System.Drawing.Point(6, 157);
            this.btnCopyrightHeader.Name = "btnCopyrightHeader";
            this.btnCopyrightHeader.Size = new System.Drawing.Size(105, 23);
            this.btnCopyrightHeader.TabIndex = 17;
            this.btnCopyrightHeader.Text = "Copyright header";
            this.btnCopyrightHeader.UseVisualStyleBackColor = true;
            this.btnCopyrightHeader.Click += new System.EventHandler(this.btnCopyrightHeader_Click);
            // 
            // cboTemplateFile
            // 
            this.cboTemplateFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTemplateFile.FormattingEnabled = true;
            this.cboTemplateFile.Items.AddRange(new object[] {
            "No template",
            "Standard"});
            this.cboTemplateFile.Location = new System.Drawing.Point(6, 130);
            this.cboTemplateFile.Name = "cboTemplateFile";
            this.cboTemplateFile.Size = new System.Drawing.Size(193, 21);
            this.cboTemplateFile.TabIndex = 16;
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.Location = new System.Drawing.Point(3, 114);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(70, 13);
            this.lblTemplate.TabIndex = 18;
            this.lblTemplate.Text = "Template file:";
            // 
            // chkSortUsing
            // 
            this.chkSortUsing.AutoSize = true;
            this.chkSortUsing.Location = new System.Drawing.Point(6, 94);
            this.chkSortUsing.Name = "chkSortUsing";
            this.chkSortUsing.Size = new System.Drawing.Size(73, 17);
            this.chkSortUsing.TabIndex = 15;
            this.chkSortUsing.Text = "Sort using";
            this.chkSortUsing.UseVisualStyleBackColor = true;
            // 
            // lblCSharpFormatStyle
            // 
            this.lblCSharpFormatStyle.AutoSize = true;
            this.lblCSharpFormatStyle.Location = new System.Drawing.Point(3, 51);
            this.lblCSharpFormatStyle.Name = "lblCSharpFormatStyle";
            this.lblCSharpFormatStyle.Size = new System.Drawing.Size(85, 13);
            this.lblCSharpFormatStyle.TabIndex = 17;
            this.lblCSharpFormatStyle.Text = "C# Format Style:";
            // 
            // cboCSharpFormat
            // 
            this.cboCSharpFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCSharpFormat.Items.AddRange(new object[] {
            "Empty",
            "Mono",
            "SharpDevelop / K&R style",
            "Allman (Visual Studio)",
            "Whitesmiths",
            "GNU",
            "Custom"});
            this.cboCSharpFormat.Location = new System.Drawing.Point(6, 67);
            this.cboCSharpFormat.Name = "cboCSharpFormat";
            this.cboCSharpFormat.Size = new System.Drawing.Size(193, 21);
            this.cboCSharpFormat.TabIndex = 13;
            this.cboCSharpFormat.SelectedIndexChanged += new System.EventHandler(this.cboCSharpFormat_SelectedIndexChanged);
            // 
            // chkXMLDocFood
            // 
            this.chkXMLDocFood.AutoSize = true;
            this.chkXMLDocFood.Location = new System.Drawing.Point(6, 6);
            this.chkXMLDocFood.Name = "chkXMLDocFood";
            this.chkXMLDocFood.Size = new System.Drawing.Size(193, 17);
            this.chkXMLDocFood.TabIndex = 11;
            this.chkXMLDocFood.Text = "Generate XML Documentation tags";
            this.chkXMLDocFood.UseVisualStyleBackColor = true;
            // 
            // chkGenerateNunitTests
            // 
            this.chkGenerateNunitTests.AutoSize = true;
            this.chkGenerateNunitTests.Location = new System.Drawing.Point(6, 29);
            this.chkGenerateNunitTests.Name = "chkGenerateNunitTests";
            this.chkGenerateNunitTests.Size = new System.Drawing.Size(151, 17);
            this.chkGenerateNunitTests.TabIndex = 12;
            this.chkGenerateNunitTests.Text = "Generate NUnit test cases";
            this.chkGenerateNunitTests.UseVisualStyleBackColor = true;
            // 
            // tabPageCSharpJava
            // 
            this.tabPageCSharpJava.Controls.Add(this.lblIndentSize);
            this.tabPageCSharpJava.Controls.Add(this.chkUseTabs);
            this.tabPageCSharpJava.Controls.Add(this.updIndentSize);
            this.tabPageCSharpJava.Location = new System.Drawing.Point(4, 22);
            this.tabPageCSharpJava.Name = "tabPageCSharpJava";
            this.tabPageCSharpJava.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCSharpJava.Size = new System.Drawing.Size(352, 240);
            this.tabPageCSharpJava.TabIndex = 4;
            this.tabPageCSharpJava.Text = "C#/Java";
            this.tabPageCSharpJava.UseVisualStyleBackColor = true;
            // 
            // lblIndentSize
            // 
            this.lblIndentSize.AutoSize = true;
            this.lblIndentSize.Enabled = false;
            this.lblIndentSize.Location = new System.Drawing.Point(6, 31);
            this.lblIndentSize.Name = "lblIndentSize";
            this.lblIndentSize.Size = new System.Drawing.Size(61, 13);
            this.lblIndentSize.TabIndex = 1;
            this.lblIndentSize.Text = "Indent size:";
            // 
            // chkUseTabs
            // 
            this.chkUseTabs.AutoSize = true;
            this.chkUseTabs.Location = new System.Drawing.Point(6, 6);
            this.chkUseTabs.Name = "chkUseTabs";
            this.chkUseTabs.Size = new System.Drawing.Size(120, 17);
            this.chkUseTabs.TabIndex = 19;
            this.chkUseTabs.Text = "Use tabs for indents";
            this.chkUseTabs.UseVisualStyleBackColor = true;
            // 
            // updIndentSize
            // 
            this.updIndentSize.Location = new System.Drawing.Point(6, 47);
            this.updIndentSize.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.updIndentSize.Name = "updIndentSize";
            this.updIndentSize.Size = new System.Drawing.Size(82, 20);
            this.updIndentSize.TabIndex = 20;
            this.updIndentSize.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // cboLanguage
            // 
            this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            /*
            this.cboLanguage.Items.AddRange(new object[] {
            "C#",
            "Java",
            "C# extended"});
            */
            this.cboLanguage.Location = new System.Drawing.Point(12, 62);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(96, 21);
            this.cboLanguage.TabIndex = 2;
            this.cboLanguage.SelectedIndexChanged += new System.EventHandler(this.cboLanguage_SelectedIndexChanged);
            // 
            // Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 373);
            this.Controls.Add(this.cboLanguage);
            this.Controls.Add(this.tabCntOptions);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.txtNewImport);
            this.Controls.Add(this.importToolStrip);
            this.Controls.Add(this.lstImportList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGenerate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 309);
            this.Name = "Dialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Code Generation";
            this.importToolStrip.ResumeLayout(false);
            this.importToolStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabCntOptions.ResumeLayout(false);
            this.tabPageCommon.ResumeLayout(false);
            this.tabPageCommon.PerformLayout();
            this.tabPageCSharpExt.ResumeLayout(false);
            this.tabPageCSharpExt.PerformLayout();
            this.tabPageCSharpJava.ResumeLayout(false);
            this.tabPageCSharpJava.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updIndentSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ListBox lstImportList;
		private System.Windows.Forms.ToolStrip importToolStrip;
		private System.Windows.Forms.ToolStripButton toolDelete;
		private System.Windows.Forms.ToolStripButton toolMoveDown;
        private System.Windows.Forms.ToolStripButton toolMoveUp;
		private System.Windows.Forms.TextBox txtNewImport;
        private System.Windows.Forms.Button btnAddItem;
		private System.Windows.Forms.Label lblSolutionType;
		private System.Windows.Forms.ComboBox cboSolutionType;
        private System.Windows.Forms.CheckBox chkNotImplemented;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.ToolStripLabel toolImportList;
        private System.Windows.Forms.TabControl tabCntOptions;
        private System.Windows.Forms.TabPage tabPageCommon;
        private System.Windows.Forms.TabPage tabPageCSharpExt;
        private System.Windows.Forms.CheckBox chkSortUsing;
        private System.Windows.Forms.Label lblCSharpFormatStyle;
        private System.Windows.Forms.ComboBox cboCSharpFormat;
        private System.Windows.Forms.CheckBox chkXMLDocFood;
        private System.Windows.Forms.CheckBox chkGenerateNunitTests;
        private System.Windows.Forms.TabPage tabPageCSharpJava;
        private System.Windows.Forms.Label lblIndentSize;
        private System.Windows.Forms.CheckBox chkUseTabs;
        private System.Windows.Forms.NumericUpDown updIndentSize;
        private System.Windows.Forms.ComboBox cboTemplateFile;
        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.Button btnCopyrightHeader;
        private System.Windows.Forms.Button btnCustomFormatStyle;
        private System.Windows.Forms.ComboBox cboLanguage;
	}
}