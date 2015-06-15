namespace NClass.GUI
{
    partial class BatchLogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchLogForm));
            this.progressBarBatch = new System.Windows.Forms.ProgressBar();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // progressBarBatch
            // 
            this.progressBarBatch.Location = new System.Drawing.Point(12, 432);
            this.progressBarBatch.Name = "progressBarBatch";
            this.progressBarBatch.Size = new System.Drawing.Size(856, 27);
            this.progressBarBatch.TabIndex = 0;
            this.progressBarBatch.UseWaitCursor = true;
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Location = new System.Drawing.Point(12, 6);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.ShortcutsEnabled = false;
            this.richTextBoxLog.Size = new System.Drawing.Size(856, 419);
            this.richTextBoxLog.TabIndex = 1;
            this.richTextBoxLog.Text = "";
            this.richTextBoxLog.UseWaitCursor = true;
            // 
            // BatchLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 471);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.progressBarBatch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BatchLogForm";
            this.Text = "Log :";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.BatchLogForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarBatch;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
    }
}