namespace midiLightShow
{
    partial class debug
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
            this.rtbDebug = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbDebug
            // 
            this.rtbDebug.BackColor = System.Drawing.SystemColors.ControlDark;
            this.rtbDebug.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbDebug.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.rtbDebug.Location = new System.Drawing.Point(12, 35);
            this.rtbDebug.Name = "rtbDebug";
            this.rtbDebug.ReadOnly = true;
            this.rtbDebug.Size = new System.Drawing.Size(481, 212);
            this.rtbDebug.TabIndex = 0;
            this.rtbDebug.Text = "";
            this.rtbDebug.TextChanged += new System.EventHandler(this.rtbDebug_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.xToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(505, 27);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripTextBox1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(110, 23);
            this.toolStripTextBox1.Text = "Debug Information";
            // 
            // xToolStripMenuItem
            // 
            this.xToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.xToolStripMenuItem.Name = "xToolStripMenuItem";
            this.xToolStripMenuItem.Size = new System.Drawing.Size(26, 23);
            this.xToolStripMenuItem.Text = "X";
            this.xToolStripMenuItem.Click += new System.EventHandler(this.xToolStripMenuItem_Click);
            // 
            // debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(505, 269);
            this.Controls.Add(this.rtbDebug);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(665, 24);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "debug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "debug";
            this.TopMost = true;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbDebug;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem;
    }
}