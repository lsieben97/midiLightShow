namespace midiLightShow
{
    partial class help
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.tvIndex = new System.Windows.Forms.TreeView();
            this.rtbHelpContent = new System.Windows.Forms.RichTextBox();
            this.xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.xToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(543, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.toolStripTextBox1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 20);
            this.toolStripTextBox1.Text = "User manual";
            // 
            // tvIndex
            // 
            this.tvIndex.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tvIndex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvIndex.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tvIndex.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.tvIndex.Location = new System.Drawing.Point(12, 37);
            this.tvIndex.Name = "tvIndex";
            this.tvIndex.ShowPlusMinus = false;
            this.tvIndex.Size = new System.Drawing.Size(227, 368);
            this.tvIndex.TabIndex = 1;
            this.tvIndex.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvIndex_NodeMouseClick);
            // 
            // rtbHelpContent
            // 
            this.rtbHelpContent.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.rtbHelpContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbHelpContent.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rtbHelpContent.Location = new System.Drawing.Point(245, 37);
            this.rtbHelpContent.Name = "rtbHelpContent";
            this.rtbHelpContent.Size = new System.Drawing.Size(286, 368);
            this.rtbHelpContent.TabIndex = 2;
            this.rtbHelpContent.Text = "";
            // 
            // xToolStripMenuItem
            // 
            this.xToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xToolStripMenuItem.Name = "xToolStripMenuItem";
            this.xToolStripMenuItem.Size = new System.Drawing.Size(26, 20);
            this.xToolStripMenuItem.Text = "X";
            this.xToolStripMenuItem.Click += new System.EventHandler(this.xToolStripMenuItem_Click);
            // 
            // help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(543, 417);
            this.Controls.Add(this.rtbHelpContent);
            this.Controls.Add(this.tvIndex);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "help";
            this.Text = "help";
            this.Load += new System.EventHandler(this.help_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.TreeView tvIndex;
        private System.Windows.Forms.RichTextBox rtbHelpContent;
        private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem;
    }
}