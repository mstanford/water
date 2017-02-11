namespace bamboo
{
	partial class StartPage
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
			this.labelBamboo = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labelBamboo
			// 
			this.labelBamboo.AutoSize = true;
			this.labelBamboo.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelBamboo.ForeColor = System.Drawing.Color.White;
			this.labelBamboo.Location = new System.Drawing.Point(30, 26);
			this.labelBamboo.Name = "labelBamboo";
			this.labelBamboo.Size = new System.Drawing.Size(204, 55);
			this.labelBamboo.TabIndex = 0;
			this.labelBamboo.Text = "Bamboo";
			// 
			// StartPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.YellowGreen;
			this.Controls.Add(this.labelBamboo);
			this.Name = "StartPage";
			this.Size = new System.Drawing.Size(648, 512);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelBamboo;
	}
}
