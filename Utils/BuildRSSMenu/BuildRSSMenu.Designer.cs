namespace BuildRSSMenu
{
	partial class BuildRSSMenu
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
			this.btnBuildFile = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnBuildFile
			// 
			this.btnBuildFile.Location = new System.Drawing.Point(250, 41);
			this.btnBuildFile.Name = "btnBuildFile";
			this.btnBuildFile.Size = new System.Drawing.Size(75, 23);
			this.btnBuildFile.TabIndex = 3;
			this.btnBuildFile.Text = "Build File";
			this.btnBuildFile.UseVisualStyleBackColor = true;
			this.btnBuildFile.Click += new System.EventHandler(this.OnClickBuildFile);
			// 
			// BuildRSSMenu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(598, 118);
			this.Controls.Add(this.btnBuildFile);
			this.Name = "BuildRSSMenu";
			this.Text = "Build RSS Menu";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnBuildFile;
	}
}

