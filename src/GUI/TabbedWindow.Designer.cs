namespace NClass.GUI
{
	partial class TabbedWindow
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Canvas = new NClass.DiagramEditor.Canvas();
			this.TabBar = new NClass.GUI.TabBar();
			this.SuspendLayout();
			// 
			// canvas
			// 
			this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Canvas.Document = null;
			this.Canvas.Location = new System.Drawing.Point(0, 25);
			this.Canvas.Margin = new System.Windows.Forms.Padding(0);
			this.Canvas.Name = "Canvas";
			this.Canvas.Offset = new System.Drawing.Point(0, 0);
			this.Canvas.Size = new System.Drawing.Size(150, 125);
			this.Canvas.TabIndex = 3;
			this.Canvas.Zoom = 1F;
			// 
			// tabBar
			// 
			this.TabBar.Dock = System.Windows.Forms.DockStyle.Top;
			this.TabBar.DocumentManager = null;
			this.TabBar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (238)));
			this.TabBar.Location = new System.Drawing.Point(0, 0);
			this.TabBar.Margin = new System.Windows.Forms.Padding(0);
			this.TabBar.Name = "TabBar";
			this.TabBar.Size = new System.Drawing.Size(150, 25);
			this.TabBar.TabIndex = 2;
			// 
			// TabbedWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.Controls.Add(this.Canvas);
			this.Controls.Add(this.TabBar);
			this.Name = "TabbedWindow";
			this.ResumeLayout(false);

		}

		#endregion
	}
}
