namespace NClass.CodeGenerator
{
    partial class CopyrightHeader
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.rtbCopyrightHeader = new System.Windows.Forms.RichTextBox();
            this.tbCompagnyName = new System.Windows.Forms.TextBox();
            this.tbAuthor = new System.Windows.Forms.TextBox();
            this.lblCompagnyName = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnCancel.Location = new System.Drawing.Point(297, 386);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(216, 386);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // rtbCopyrightHeader
            // 
            this.rtbCopyrightHeader.Location = new System.Drawing.Point(5, 80);
            this.rtbCopyrightHeader.Name = "rtbCopyrightHeader";
            this.rtbCopyrightHeader.Size = new System.Drawing.Size(376, 300);
            this.rtbCopyrightHeader.TabIndex = 25;
            this.rtbCopyrightHeader.Text = global::NClass.CodeGenerator.Settings.Default.CopyrightHeader;
            // 
            // tbCompagnyName
            // 
            this.tbCompagnyName.Location = new System.Drawing.Point(102, 12);
            this.tbCompagnyName.Name = "tbCompagnyName";
            this.tbCompagnyName.Size = new System.Drawing.Size(279, 20);
            this.tbCompagnyName.TabIndex = 26;
            this.tbCompagnyName.Text = global::NClass.CodeGenerator.Settings.Default.CompagnyName;
            // 
            // tbAuthor
            // 
            this.tbAuthor.Location = new System.Drawing.Point(102, 38);
            this.tbAuthor.Name = "tbAuthor";
            this.tbAuthor.Size = new System.Drawing.Size(279, 20);
            this.tbAuthor.TabIndex = 27;
            this.tbAuthor.Text = global::NClass.CodeGenerator.Settings.Default.Author;
            // 
            // lblCompagnyName
            // 
            this.lblCompagnyName.AutoSize = true;
            this.lblCompagnyName.Location = new System.Drawing.Point(2, 15);
            this.lblCompagnyName.Name = "lblCompagnyName";
            this.lblCompagnyName.Size = new System.Drawing.Size(94, 13);
            this.lblCompagnyName.TabIndex = 28;
            this.lblCompagnyName.Text = "Compagny Name :";
            this.lblCompagnyName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(52, 41);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(44, 13);
            this.lblAuthor.TabIndex = 29;
            this.lblAuthor.Text = "Author :";
            this.lblAuthor.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new System.Drawing.Point(12, 64);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(57, 13);
            this.lblCopyright.TabIndex = 30;
            this.lblCopyright.Text = "Copyright :";
            // 
            // CopyrightHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 421);
            this.ControlBox = false;
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.lblCompagnyName);
            this.Controls.Add(this.tbAuthor);
            this.Controls.Add(this.tbCompagnyName);
            this.Controls.Add(this.rtbCopyrightHeader);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CopyrightHeader";
            this.ShowIcon = false;
            this.Text = "Copyright header";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RichTextBox rtbCopyrightHeader;
        private System.Windows.Forms.TextBox tbCompagnyName;
        private System.Windows.Forms.TextBox tbAuthor;
        private System.Windows.Forms.Label lblCompagnyName;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblCopyright;
    }
}