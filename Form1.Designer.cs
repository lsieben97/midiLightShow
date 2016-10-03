namespace midiLightShow
{
    partial class frmEditor
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
            this.msControl = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMIDIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdMidi = new System.Windows.Forms.OpenFileDialog();
            this.pTimeLine = new System.Windows.Forms.Panel();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnAddTrack = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sfdLightShow = new System.Windows.Forms.SaveFileDialog();
            this.tpDescription = new System.Windows.Forms.ToolTip(this.components);
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdLoad = new System.Windows.Forms.OpenFileDialog();
            this.msControl.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // msControl
            // 
            this.msControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.msControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.msControl.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.msControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.msControl.Location = new System.Drawing.Point(0, 0);
            this.msControl.Name = "msControl";
            this.msControl.Size = new System.Drawing.Size(1022, 24);
            this.msControl.TabIndex = 1;
            this.msControl.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadMIDIToolStripMenuItem,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.fileToolStripMenuItem.ImageTransparentColor = System.Drawing.SystemColors.ControlDarkDark;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // loadMIDIToolStripMenuItem
            // 
            this.loadMIDIToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.loadMIDIToolStripMenuItem.Name = "loadMIDIToolStripMenuItem";
            this.loadMIDIToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadMIDIToolStripMenuItem.Text = "Load MIDI";
            this.loadMIDIToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.loadMIDIToolStripMenuItem.Click += new System.EventHandler(this.loadMIDIToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportToolStripMenuItem.Text = "Save show";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugToolStripMenuItem});
            this.toolsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.debugToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugToolStripMenuItem_Click);
            // 
            // ofdMidi
            // 
            this.ofdMidi.FileName = "Select a MIDI file";
            this.ofdMidi.Filter = "MIDI files|*.mid";
            // 
            // pTimeLine
            // 
            this.pTimeLine.BackColor = System.Drawing.Color.White;
            this.pTimeLine.Location = new System.Drawing.Point(3, 3);
            this.pTimeLine.Name = "pTimeLine";
            this.pTimeLine.Size = new System.Drawing.Size(1344, 621);
            this.pTimeLine.TabIndex = 2;
            this.pTimeLine.Scroll += new System.Windows.Forms.ScrollEventHandler(this.pTimeLine_Scroll);
            this.pTimeLine.Click += new System.EventHandler(this.pTimeLine_Click);
            this.pTimeLine.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(12, 37);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 3;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(93, 37);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnAddTrack
            // 
            this.btnAddTrack.Location = new System.Drawing.Point(174, 37);
            this.btnAddTrack.Name = "btnAddTrack";
            this.btnAddTrack.Size = new System.Drawing.Size(75, 23);
            this.btnAddTrack.TabIndex = 5;
            this.btnAddTrack.Text = "Add track";
            this.btnAddTrack.UseVisualStyleBackColor = true;
            this.btnAddTrack.Click += new System.EventHandler(this.btnAddTrack_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pTimeLine);
            this.panel1.Location = new System.Drawing.Point(12, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1350, 641);
            this.panel1.TabIndex = 6;
            // 
            // sfdLightShow
            // 
            this.sfdLightShow.Filter = "Light shows|*.lightshow";
            this.sfdLightShow.RestoreDirectory = true;
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.importToolStripMenuItem.Text = "Load show";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // ofdLoad
            // 
            this.ofdLoad.FileName = "openFileDialog1";
            // 
            // frmEditor
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1022, 602);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnAddTrack);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.msControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.msControl;
            this.Name = "frmEditor";
            this.Text = "Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmEditor_Load);
            this.msControl.ResumeLayout(false);
            this.msControl.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msControl;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMIDIToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ofdMidi;
        private System.Windows.Forms.Panel pTimeLine;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnAddTrack;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog sfdLightShow;
        private System.Windows.Forms.ToolTip tpDescription;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ofdLoad;
    }
}

