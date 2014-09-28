namespace NStub.Gui
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this._assemblyGraphTreeView = new System.Windows.Forms.TreeView();
			this._objectIconsImageList = new System.Windows.Forms.ImageList(this.components);
			this._browseOutputDirectoryButton = new System.Windows.Forms.Button();
			this._outputDirectoryLabel = new System.Windows.Forms.Label();
			this._outputDirectoryTextBox = new System.Windows.Forms.TextBox();
			this._outputFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this._goButton = new System.Windows.Forms.Button();
			this._browseInputAssemblyButton = new System.Windows.Forms.Button();
			this._inputAssemblyTextBox = new System.Windows.Forms.TextBox();
			this._inputAssemblyOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this._inputAssemblyLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// _assemblyGraphTreeView
			// 
			this._assemblyGraphTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._assemblyGraphTreeView.CheckBoxes = true;
			this._assemblyGraphTreeView.ImageIndex = 0;
			this._assemblyGraphTreeView.ImageList = this._objectIconsImageList;
			this._assemblyGraphTreeView.Location = new System.Drawing.Point(15, 58);
			this._assemblyGraphTreeView.Name = "_assemblyGraphTreeView";
			this._assemblyGraphTreeView.SelectedImageIndex = 0;
			this._assemblyGraphTreeView.Size = new System.Drawing.Size(490, 265);
			this._assemblyGraphTreeView.TabIndex = 15;
			this._assemblyGraphTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvAssemblyGraph_AfterCheck);
			this._assemblyGraphTreeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvAssemblyGraph_BeforeSelect);
			// 
			// _objectIconsImageList
			// 
			this._objectIconsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_objectIconsImageList.ImageStream")));
			this._objectIconsImageList.TransparentColor = System.Drawing.Color.Magenta;
			this._objectIconsImageList.Images.SetKeyName(0, "imgAssembly");
			this._objectIconsImageList.Images.SetKeyName(1, "imgModule");
			this._objectIconsImageList.Images.SetKeyName(2, "imgNamespace");
			this._objectIconsImageList.Images.SetKeyName(3, "imgClass");
			this._objectIconsImageList.Images.SetKeyName(4, "imgMethod");
			// 
			// _browseOutputDirectoryButton
			// 
			this._browseOutputDirectoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._browseOutputDirectoryButton.Enabled = false;
			this._browseOutputDirectoryButton.Location = new System.Drawing.Point(430, 30);
			this._browseOutputDirectoryButton.Name = "_browseOutputDirectoryButton";
			this._browseOutputDirectoryButton.Size = new System.Drawing.Size(75, 23);
			this._browseOutputDirectoryButton.TabIndex = 14;
			this._browseOutputDirectoryButton.Text = "Browse...";
			this._browseOutputDirectoryButton.UseVisualStyleBackColor = true;
			this._browseOutputDirectoryButton.Click += new System.EventHandler(this.btnBrowseOutputDirectory_Click);
			// 
			// _outputDirectoryLabel
			// 
			this._outputDirectoryLabel.AutoSize = true;
			this._outputDirectoryLabel.Location = new System.Drawing.Point(12, 35);
			this._outputDirectoryLabel.Name = "_outputDirectoryLabel";
			this._outputDirectoryLabel.Size = new System.Drawing.Size(84, 13);
			this._outputDirectoryLabel.TabIndex = 12;
			this._outputDirectoryLabel.Text = "Output Directory";
			// 
			// _outputDirectoryTextBox
			// 
			this._outputDirectoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._outputDirectoryTextBox.Location = new System.Drawing.Point(102, 32);
			this._outputDirectoryTextBox.Name = "_outputDirectoryTextBox";
			this._outputDirectoryTextBox.ReadOnly = true;
			this._outputDirectoryTextBox.Size = new System.Drawing.Size(322, 20);
			this._outputDirectoryTextBox.TabIndex = 13;
			// 
			// _goButton
			// 
			this._goButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._goButton.Enabled = false;
			this._goButton.Location = new System.Drawing.Point(430, 329);
			this._goButton.Name = "_goButton";
			this._goButton.Size = new System.Drawing.Size(75, 23);
			this._goButton.TabIndex = 11;
			this._goButton.Text = "Go";
			this._goButton.UseVisualStyleBackColor = true;
			this._goButton.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// _browseInputAssemblyButton
			// 
			this._browseInputAssemblyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._browseInputAssemblyButton.Location = new System.Drawing.Point(430, 4);
			this._browseInputAssemblyButton.Name = "_browseInputAssemblyButton";
			this._browseInputAssemblyButton.Size = new System.Drawing.Size(75, 23);
			this._browseInputAssemblyButton.TabIndex = 8;
			this._browseInputAssemblyButton.Text = "Browse...";
			this._browseInputAssemblyButton.UseVisualStyleBackColor = true;
			this._browseInputAssemblyButton.Click += new System.EventHandler(this.btnBrowseInputAssembly_Click);
			// 
			// _inputAssemblyTextBox
			// 
			this._inputAssemblyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._inputAssemblyTextBox.Location = new System.Drawing.Point(102, 6);
			this._inputAssemblyTextBox.Name = "_inputAssemblyTextBox";
			this._inputAssemblyTextBox.ReadOnly = true;
			this._inputAssemblyTextBox.Size = new System.Drawing.Size(322, 20);
			this._inputAssemblyTextBox.TabIndex = 9;
			// 
			// _inputAssemblyOpenFileDialog
			// 
			this._inputAssemblyOpenFileDialog.Filter = "Valid Assemblies | *.dll; *.exe";
			this._inputAssemblyOpenFileDialog.Multiselect = true;
			// 
			// _inputAssemblyLabel
			// 
			this._inputAssemblyLabel.AutoSize = true;
			this._inputAssemblyLabel.Location = new System.Drawing.Point(12, 9);
			this._inputAssemblyLabel.Name = "_inputAssemblyLabel";
			this._inputAssemblyLabel.Size = new System.Drawing.Size(78, 13);
			this._inputAssemblyLabel.TabIndex = 10;
			this._inputAssemblyLabel.Text = "Input Assembly";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(512, 366);
			this.Controls.Add(this._assemblyGraphTreeView);
			this.Controls.Add(this._browseOutputDirectoryButton);
			this.Controls.Add(this._outputDirectoryLabel);
			this.Controls.Add(this._outputDirectoryTextBox);
			this.Controls.Add(this._goButton);
			this.Controls.Add(this._browseInputAssemblyButton);
			this.Controls.Add(this._inputAssemblyTextBox);
			this.Controls.Add(this._inputAssemblyLabel);
			this.Name = "MainForm";
			this.Text = "NStub";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView _assemblyGraphTreeView;
		private System.Windows.Forms.Button _browseOutputDirectoryButton;
		private System.Windows.Forms.Label _outputDirectoryLabel;
		private System.Windows.Forms.TextBox _outputDirectoryTextBox;
		private System.Windows.Forms.FolderBrowserDialog _outputFolderBrowserDialog;
		private System.Windows.Forms.Button _goButton;
		private System.Windows.Forms.Button _browseInputAssemblyButton;
		private System.Windows.Forms.TextBox _inputAssemblyTextBox;
		private System.Windows.Forms.OpenFileDialog _inputAssemblyOpenFileDialog;
		private System.Windows.Forms.Label _inputAssemblyLabel;
		private System.Windows.Forms.ImageList _objectIconsImageList;

	}
}

