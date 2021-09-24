namespace TestStuff
{
	partial class Form1
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
			this.btnLoadXML = new System.Windows.Forms.Button();
			this.lblFilePath = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtBefore = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtAfter = new System.Windows.Forms.TextBox();
			this.btnTest = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.myTeamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadPageOptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sQLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sPWithRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.btnParseTest = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnLoadXML
			// 
			this.btnLoadXML.Location = new System.Drawing.Point(12, 46);
			this.btnLoadXML.Name = "btnLoadXML";
			this.btnLoadXML.Size = new System.Drawing.Size(75, 23);
			this.btnLoadXML.TabIndex = 0;
			this.btnLoadXML.Text = "Load XML";
			this.btnLoadXML.UseVisualStyleBackColor = true;
			this.btnLoadXML.Click += new System.EventHandler(this.OnClickLoadXML);
			// 
			// lblFilePath
			// 
			this.lblFilePath.AutoSize = true;
			this.lblFilePath.Location = new System.Drawing.Point(111, 51);
			this.lblFilePath.Name = "lblFilePath";
			this.lblFilePath.Size = new System.Drawing.Size(59, 13);
			this.lblFilePath.TabIndex = 1;
			this.lblFilePath.Text = "File path ...";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 89);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Before";
			// 
			// txtBefore
			// 
			this.txtBefore.Location = new System.Drawing.Point(16, 106);
			this.txtBefore.Multiline = true;
			this.txtBefore.Name = "txtBefore";
			this.txtBefore.Size = new System.Drawing.Size(442, 421);
			this.txtBefore.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(575, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "After";
			// 
			// txtAfter
			// 
			this.txtAfter.Location = new System.Drawing.Point(578, 106);
			this.txtAfter.Multiline = true;
			this.txtAfter.Name = "txtAfter";
			this.txtAfter.Size = new System.Drawing.Size(494, 421);
			this.txtAfter.TabIndex = 5;
			// 
			// btnTest
			// 
			this.btnTest.Location = new System.Drawing.Point(382, 45);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(75, 23);
			this.btnTest.TabIndex = 6;
			this.btnTest.Text = "Test";
			this.btnTest.UseVisualStyleBackColor = true;
			this.btnTest.Click += new System.EventHandler(this.OnClickTest);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(495, 45);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 7;
			this.button1.Text = "Email test";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.OnClickEmailTest);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.testToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1121, 24);
			this.menuStrip1.TabIndex = 8;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnClickExit);
			// 
			// testToolStripMenuItem
			// 
			this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myTeamToolStripMenuItem,
            this.sQLToolStripMenuItem});
			this.testToolStripMenuItem.Name = "testToolStripMenuItem";
			this.testToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
			this.testToolStripMenuItem.Text = "Test";
			// 
			// myTeamToolStripMenuItem
			// 
			this.myTeamToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadPageOptsToolStripMenuItem});
			this.myTeamToolStripMenuItem.Name = "myTeamToolStripMenuItem";
			this.myTeamToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.myTeamToolStripMenuItem.Text = "MyTeams";
			// 
			// loadPageOptsToolStripMenuItem
			// 
			this.loadPageOptsToolStripMenuItem.Name = "loadPageOptsToolStripMenuItem";
			this.loadPageOptsToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.loadPageOptsToolStripMenuItem.Text = "LoadPageOpts";
			this.loadPageOptsToolStripMenuItem.Click += new System.EventHandler(this.OnClickLoadPageOptions);
			// 
			// sQLToolStripMenuItem
			// 
			this.sQLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sPWithRowsToolStripMenuItem});
			this.sQLToolStripMenuItem.Name = "sQLToolStripMenuItem";
			this.sQLToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.sQLToolStripMenuItem.Text = "SQL";
			// 
			// sPWithRowsToolStripMenuItem
			// 
			this.sPWithRowsToolStripMenuItem.Name = "sPWithRowsToolStripMenuItem";
			this.sPWithRowsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.sPWithRowsToolStripMenuItem.Text = "SP With Rows";
			this.sPWithRowsToolStripMenuItem.Click += new System.EventHandler(this.OnClickTestSQLSPWithRows);
			// 
			// btnParseTest
			// 
			this.btnParseTest.Location = new System.Drawing.Point(678, 45);
			this.btnParseTest.Name = "btnParseTest";
			this.btnParseTest.Size = new System.Drawing.Size(75, 23);
			this.btnParseTest.TabIndex = 9;
			this.btnParseTest.Text = "Parse Test";
			this.btnParseTest.UseVisualStyleBackColor = true;
			this.btnParseTest.Click += new System.EventHandler(this.OnClickParseTest);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1121, 701);
			this.Controls.Add(this.btnParseTest);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnTest);
			this.Controls.Add(this.txtAfter);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtBefore);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lblFilePath);
			this.Controls.Add(this.btnLoadXML);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "Form1";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnLoadXML;
		private System.Windows.Forms.Label lblFilePath;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtBefore;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtAfter;
		private System.Windows.Forms.Button btnTest;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem myTeamToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadPageOptsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sQLToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sPWithRowsToolStripMenuItem;
		private System.Windows.Forms.Button btnParseTest;
	}
}

