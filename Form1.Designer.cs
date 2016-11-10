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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditor));
            this.msControl = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newShowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMIDIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importMusicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startTutorialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdMidi = new System.Windows.Forms.OpenFileDialog();
            this.pTimeLine = new System.Windows.Forms.Panel();
            this.tbLightshowName = new System.Windows.Forms.TextBox();
            this.pTutorial = new System.Windows.Forms.Panel();
            this.rtbTutorialContent = new System.Windows.Forms.RichTextBox();
            this.msTutorial = new System.Windows.Forms.MenuStrip();
            this.tbTutorial = new System.Windows.Forms.ToolStripTextBox();
            this.btnNextTutorial = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnResetZoom = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sfdLightShow = new System.Windows.Forms.SaveFileDialog();
            this.tpDescription = new System.Windows.Forms.ToolTip(this.components);
            this.ofdLoad = new System.Windows.Forms.OpenFileDialog();
            this.nudBeatsPerMinute = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trbZoom = new System.Windows.Forms.TrackBar();
            this.lbZoom = new System.Windows.Forms.Label();
            this.msBottom = new System.Windows.Forms.MenuStrip();
            this.tbHelp = new System.Windows.Forms.ToolStripTextBox();
            this.btnAddTrack = new System.Windows.Forms.Button();
            this.lbPlayBackTime = new System.Windows.Forms.Label();
            this.ofdMusic = new System.Windows.Forms.OpenFileDialog();
            this.msControl.SuspendLayout();
            this.pTimeLine.SuspendLayout();
            this.pTutorial.SuspendLayout();
            this.msTutorial.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBeatsPerMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbZoom)).BeginInit();
            this.msBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // msControl
            // 
            this.msControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.msControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.msControl.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.msControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.minimizeToolStripMenuItem,
            this.helpToolStripMenuItem});
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
            this.newShowToolStripMenuItem,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.loadMIDIToolStripMenuItem,
            this.importMusicToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Corbel", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.fileToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.fileToolStripMenuItem.ImageTransparentColor = System.Drawing.SystemColors.ControlDarkDark;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // newShowToolStripMenuItem
            // 
            this.newShowToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.newShowToolStripMenuItem.Font = new System.Drawing.Font("Corbel", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newShowToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.newShowToolStripMenuItem.Name = "newShowToolStripMenuItem";
            this.newShowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newShowToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.newShowToolStripMenuItem.Text = "New show";
            this.newShowToolStripMenuItem.Click += new System.EventHandler(this.newShowToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.importToolStripMenuItem.Font = new System.Drawing.Font("Corbel", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.importToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.importToolStripMenuItem.Text = "Load show";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            this.importToolStripMenuItem.MouseEnter += new System.EventHandler(this.importToolStripMenuItem_MouseEnter);
            this.importToolStripMenuItem.MouseLeave += new System.EventHandler(this.loadMIDIToolStripMenuItem_MouseLeave);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.exportToolStripMenuItem.Font = new System.Drawing.Font("Corbel", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.exportToolStripMenuItem.Text = "Save show";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            this.exportToolStripMenuItem.MouseEnter += new System.EventHandler(this.exportToolStripMenuItem_MouseEnter);
            this.exportToolStripMenuItem.MouseLeave += new System.EventHandler(this.loadMIDIToolStripMenuItem_MouseLeave);
            // 
            // loadMIDIToolStripMenuItem
            // 
            this.loadMIDIToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.loadMIDIToolStripMenuItem.Font = new System.Drawing.Font("Corbel", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadMIDIToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.loadMIDIToolStripMenuItem.Name = "loadMIDIToolStripMenuItem";
            this.loadMIDIToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.loadMIDIToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.loadMIDIToolStripMenuItem.Text = "Import MIDI";
            this.loadMIDIToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.loadMIDIToolStripMenuItem.Click += new System.EventHandler(this.loadMIDIToolStripMenuItem_Click);
            this.loadMIDIToolStripMenuItem.MouseEnter += new System.EventHandler(this.loadMIDIToolStripMenuItem_MouseEnter);
            this.loadMIDIToolStripMenuItem.MouseLeave += new System.EventHandler(this.loadMIDIToolStripMenuItem_MouseLeave);
            // 
            // importMusicToolStripMenuItem
            // 
            this.importMusicToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.importMusicToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.importMusicToolStripMenuItem.Name = "importMusicToolStripMenuItem";
            this.importMusicToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.importMusicToolStripMenuItem.Text = "Import music";
            this.importMusicToolStripMenuItem.Click += new System.EventHandler(this.importMusicToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.exitToolStripMenuItem.Font = new System.Drawing.Font("Corbel", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.MouseEnter += new System.EventHandler(this.exitToolStripMenuItem_MouseEnter);
            this.exitToolStripMenuItem.MouseLeave += new System.EventHandler(this.loadMIDIToolStripMenuItem_MouseLeave);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetZoomToolStripMenuItem,
            this.zoomInToolStripMenuItem,
            this.zoomOutToolStripMenuItem});
            this.viewToolStripMenuItem.Font = new System.Drawing.Font("Corbel", 9F);
            this.viewToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // resetZoomToolStripMenuItem
            // 
            this.resetZoomToolStripMenuItem.Font = new System.Drawing.Font("Corbel", 9F);
            this.resetZoomToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.resetZoomToolStripMenuItem.Name = "resetZoomToolStripMenuItem";
            this.resetZoomToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
            this.resetZoomToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.resetZoomToolStripMenuItem.Text = "Reset zoom";
            this.resetZoomToolStripMenuItem.Click += new System.EventHandler(this.resetZoomToolStripMenuItem_Click);
            // 
            // zoomInToolStripMenuItem
            // 
            this.zoomInToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            this.zoomInToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus)));
            this.zoomInToolStripMenuItem.ShowShortcutKeys = false;
            this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.zoomInToolStripMenuItem.Text = "Zoom in";
            this.zoomInToolStripMenuItem.Click += new System.EventHandler(this.zoomInToolStripMenuItem_Click);
            // 
            // zoomOutToolStripMenuItem
            // 
            this.zoomOutToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            this.zoomOutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus)));
            this.zoomOutToolStripMenuItem.ShowShortcutKeys = false;
            this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.zoomOutToolStripMenuItem.Text = "Zoom out";
            this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomOutToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugToolStripMenuItem});
            this.toolsToolStripMenuItem.Font = new System.Drawing.Font("Corbel", 9F);
            this.toolsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.debugToolStripMenuItem.Font = new System.Drawing.Font("Corbel", 9F);
            this.debugToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.closeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(26, 20);
            this.closeToolStripMenuItem.Text = "X";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // minimizeToolStripMenuItem
            // 
            this.minimizeToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.minimizeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.minimizeToolStripMenuItem.Name = "minimizeToolStripMenuItem";
            this.minimizeToolStripMenuItem.Size = new System.Drawing.Size(24, 20);
            this.minimizeToolStripMenuItem.Text = "_";
            this.minimizeToolStripMenuItem.Click += new System.EventHandler(this.minimizeToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userManualToolStripMenuItem,
            this.startTutorialToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // userManualToolStripMenuItem
            // 
            this.userManualToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.userManualToolStripMenuItem.Name = "userManualToolStripMenuItem";
            this.userManualToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.userManualToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.userManualToolStripMenuItem.Text = "User manual";
            this.userManualToolStripMenuItem.Click += new System.EventHandler(this.userManualToolStripMenuItem_Click);
            // 
            // startTutorialToolStripMenuItem
            // 
            this.startTutorialToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.startTutorialToolStripMenuItem.Name = "startTutorialToolStripMenuItem";
            this.startTutorialToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.startTutorialToolStripMenuItem.Text = "Start Tutorial";
            this.startTutorialToolStripMenuItem.Click += new System.EventHandler(this.startTutorialToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // ofdMidi
            // 
            this.ofdMidi.FileName = "Select a MIDI file";
            this.ofdMidi.Filter = "MIDI files|*.mid";
            // 
            // pTimeLine
            // 
            this.pTimeLine.BackColor = System.Drawing.Color.White;
            this.pTimeLine.Controls.Add(this.tbLightshowName);
            this.pTimeLine.Location = new System.Drawing.Point(0, 0);
            this.pTimeLine.Name = "pTimeLine";
            this.pTimeLine.Size = new System.Drawing.Size(1344, 616);
            this.pTimeLine.TabIndex = 2;
            this.pTimeLine.Scroll += new System.Windows.Forms.ScrollEventHandler(this.pTimeLine_Scroll);
            this.pTimeLine.Click += new System.EventHandler(this.pTimeLine_Click);
            this.pTimeLine.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.pTimeLine.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pTimeLine_MouseMove);
            // 
            // tbLightshowName
            // 
            this.tbLightshowName.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tbLightshowName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLightshowName.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tbLightshowName.Location = new System.Drawing.Point(3, 1);
            this.tbLightshowName.Name = "tbLightshowName";
            this.tbLightshowName.Size = new System.Drawing.Size(167, 20);
            this.tbLightshowName.TabIndex = 14;
            this.tbLightshowName.TextChanged += new System.EventHandler(this.tbLightshowName_TextChanged);
            // 
            // pTutorial
            // 
            this.pTutorial.Controls.Add(this.rtbTutorialContent);
            this.pTutorial.Controls.Add(this.msTutorial);
            this.pTutorial.Controls.Add(this.btnNextTutorial);
            this.pTutorial.Location = new System.Drawing.Point(723, 32);
            this.pTutorial.Name = "pTutorial";
            this.pTutorial.Size = new System.Drawing.Size(348, 225);
            this.pTutorial.TabIndex = 14;
            // 
            // rtbTutorialContent
            // 
            this.rtbTutorialContent.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.rtbTutorialContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbTutorialContent.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbTutorialContent.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rtbTutorialContent.Location = new System.Drawing.Point(3, 27);
            this.rtbTutorialContent.Name = "rtbTutorialContent";
            this.rtbTutorialContent.Size = new System.Drawing.Size(342, 160);
            this.rtbTutorialContent.TabIndex = 1;
            this.rtbTutorialContent.Text = "Welcome to the tutorial for DMX studio!\nThis tutorial will guide you through the " +
    "application and help you setup your first lightshow";
            // 
            // msTutorial
            // 
            this.msTutorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.msTutorial.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbTutorial});
            this.msTutorial.Location = new System.Drawing.Point(0, 0);
            this.msTutorial.Name = "msTutorial";
            this.msTutorial.Size = new System.Drawing.Size(348, 24);
            this.msTutorial.TabIndex = 0;
            this.msTutorial.Text = "menuStrip1";
            // 
            // tbTutorial
            // 
            this.tbTutorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbTutorial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbTutorial.ForeColor = System.Drawing.SystemColors.Highlight;
            this.tbTutorial.Name = "tbTutorial";
            this.tbTutorial.ReadOnly = true;
            this.tbTutorial.Size = new System.Drawing.Size(250, 20);
            this.tbTutorial.Text = "Tutorial - Welcome!";
            // 
            // btnNextTutorial
            // 
            this.btnNextTutorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnNextTutorial.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnNextTutorial.FlatAppearance.BorderSize = 0;
            this.btnNextTutorial.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnNextTutorial.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnNextTutorial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextTutorial.Font = new System.Drawing.Font("Corbel", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextTutorial.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnNextTutorial.Location = new System.Drawing.Point(270, 193);
            this.btnNextTutorial.Name = "btnNextTutorial";
            this.btnNextTutorial.Size = new System.Drawing.Size(75, 29);
            this.btnNextTutorial.TabIndex = 4;
            this.btnNextTutorial.Tag = "button";
            this.btnNextTutorial.Text = "Next";
            this.btnNextTutorial.UseVisualStyleBackColor = false;
            this.btnNextTutorial.Click += new System.EventHandler(this.btnNextTutorial_Click);
            this.btnNextTutorial.MouseEnter += new System.EventHandler(this.btnStop_MouseEnter);
            this.btnNextTutorial.MouseLeave += new System.EventHandler(this.btnPlay_MouseLeave);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnPlay.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnPlay.FlatAppearance.BorderSize = 0;
            this.btnPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnPlay.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("Corbel", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnPlay.Location = new System.Drawing.Point(12, 32);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 29);
            this.btnPlay.TabIndex = 3;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            this.btnPlay.MouseEnter += new System.EventHandler(this.btnStop_MouseEnter);
            this.btnPlay.MouseLeave += new System.EventHandler(this.btnPlay_MouseLeave);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnStop.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnStop.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Corbel", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnStop.Location = new System.Drawing.Point(93, 32);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 29);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            this.btnStop.MouseEnter += new System.EventHandler(this.btnStop_MouseEnter);
            this.btnStop.MouseLeave += new System.EventHandler(this.btnPlay_MouseLeave);
            // 
            // btnResetZoom
            // 
            this.btnResetZoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnResetZoom.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnResetZoom.FlatAppearance.BorderSize = 0;
            this.btnResetZoom.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnResetZoom.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnResetZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetZoom.Font = new System.Drawing.Font("Corbel", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetZoom.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnResetZoom.Location = new System.Drawing.Point(628, 32);
            this.btnResetZoom.Name = "btnResetZoom";
            this.btnResetZoom.Size = new System.Drawing.Size(85, 29);
            this.btnResetZoom.TabIndex = 5;
            this.btnResetZoom.Text = "Reset zoom";
            this.btnResetZoom.UseVisualStyleBackColor = false;
            this.btnResetZoom.Click += new System.EventHandler(this.btnResetZoom_Click);
            this.btnResetZoom.MouseEnter += new System.EventHandler(this.btnStop_MouseEnter);
            this.btnResetZoom.MouseLeave += new System.EventHandler(this.btnPlay_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pTimeLine);
            this.panel1.Location = new System.Drawing.Point(12, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1350, 707);
            this.panel1.TabIndex = 6;
            // 
            // sfdLightShow
            // 
            this.sfdLightShow.Filter = "Light shows|*.lightshow";
            this.sfdLightShow.RestoreDirectory = true;
            // 
            // ofdLoad
            // 
            this.ofdLoad.FileName = "openFileDialog1";
            this.ofdLoad.Filter = "Lightshow Files|*.lightshow";
            // 
            // nudBeatsPerMinute
            // 
            this.nudBeatsPerMinute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nudBeatsPerMinute.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nudBeatsPerMinute.Font = new System.Drawing.Font("Corbel", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudBeatsPerMinute.ForeColor = System.Drawing.SystemColors.Highlight;
            this.nudBeatsPerMinute.Location = new System.Drawing.Point(291, 39);
            this.nudBeatsPerMinute.Margin = new System.Windows.Forms.Padding(2);
            this.nudBeatsPerMinute.Maximum = new decimal(new int[] {
            140,
            0,
            0,
            0});
            this.nudBeatsPerMinute.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.nudBeatsPerMinute.Name = "nudBeatsPerMinute";
            this.nudBeatsPerMinute.Size = new System.Drawing.Size(57, 20);
            this.nudBeatsPerMinute.TabIndex = 7;
            this.nudBeatsPerMinute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudBeatsPerMinute.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.nudBeatsPerMinute.ValueChanged += new System.EventHandler(this.nudBeatsPerMinute_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Corbel", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(254, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "BPM:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Corbel", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(353, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Zoom:";
            // 
            // trbZoom
            // 
            this.trbZoom.Location = new System.Drawing.Point(396, 32);
            this.trbZoom.Maximum = 50;
            this.trbZoom.Minimum = 5;
            this.trbZoom.Name = "trbZoom";
            this.trbZoom.Size = new System.Drawing.Size(180, 45);
            this.trbZoom.TabIndex = 10;
            this.trbZoom.TickFrequency = 2;
            this.trbZoom.Value = 10;
            this.trbZoom.Scroll += new System.EventHandler(this.trbZoom_Scroll);
            // 
            // lbZoom
            // 
            this.lbZoom.AutoSize = true;
            this.lbZoom.Font = new System.Drawing.Font("Corbel", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbZoom.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lbZoom.Location = new System.Drawing.Point(582, 40);
            this.lbZoom.Name = "lbZoom";
            this.lbZoom.Size = new System.Drawing.Size(22, 17);
            this.lbZoom.TabIndex = 11;
            this.lbZoom.Text = "__";
            // 
            // msBottom
            // 
            this.msBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.msBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.msBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbHelp});
            this.msBottom.Location = new System.Drawing.Point(0, 731);
            this.msBottom.Name = "msBottom";
            this.msBottom.Size = new System.Drawing.Size(1022, 24);
            this.msBottom.TabIndex = 12;
            this.msBottom.Text = "menuStrip1";
            // 
            // tbHelp
            // 
            this.tbHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbHelp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbHelp.ForeColor = System.Drawing.SystemColors.Highlight;
            this.tbHelp.Name = "tbHelp";
            this.tbHelp.ReadOnly = true;
            this.tbHelp.Size = new System.Drawing.Size(1000, 20);
            this.tbHelp.Text = "Ready";
            // 
            // btnAddTrack
            // 
            this.btnAddTrack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAddTrack.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnAddTrack.FlatAppearance.BorderSize = 0;
            this.btnAddTrack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAddTrack.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnAddTrack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTrack.Font = new System.Drawing.Font("Corbel", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddTrack.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnAddTrack.Location = new System.Drawing.Point(174, 32);
            this.btnAddTrack.Name = "btnAddTrack";
            this.btnAddTrack.Size = new System.Drawing.Size(75, 29);
            this.btnAddTrack.TabIndex = 4;
            this.btnAddTrack.Tag = "button";
            this.btnAddTrack.Text = "Add track";
            this.btnAddTrack.UseVisualStyleBackColor = false;
            this.btnAddTrack.Click += new System.EventHandler(this.btnAddTrack_Click);
            this.btnAddTrack.MouseEnter += new System.EventHandler(this.btnStop_MouseEnter);
            this.btnAddTrack.MouseLeave += new System.EventHandler(this.btnPlay_MouseLeave);
            // 
            // lbPlayBackTime
            // 
            this.lbPlayBackTime.AutoSize = true;
            this.lbPlayBackTime.Font = new System.Drawing.Font("Corbel", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPlayBackTime.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lbPlayBackTime.Location = new System.Drawing.Point(719, 35);
            this.lbPlayBackTime.Name = "lbPlayBackTime";
            this.lbPlayBackTime.Size = new System.Drawing.Size(80, 24);
            this.lbPlayBackTime.TabIndex = 13;
            this.lbPlayBackTime.Text = "00:00:00";
            // 
            // ofdMusic
            // 
            this.ofdMusic.FileName = "Select a sound file";
            // 
            // frmEditor
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1022, 755);
            this.Controls.Add(this.pTutorial);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbPlayBackTime);
            this.Controls.Add(this.msBottom);
            this.Controls.Add(this.lbZoom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudBeatsPerMinute);
            this.Controls.Add(this.btnResetZoom);
            this.Controls.Add(this.btnAddTrack);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.msControl);
            this.Controls.Add(this.trbZoom);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msControl;
            this.Name = "frmEditor";
            this.Text = "DMX Studio v1.2";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEditor_KeyDown);
            this.msControl.ResumeLayout(false);
            this.msControl.PerformLayout();
            this.pTimeLine.ResumeLayout(false);
            this.pTimeLine.PerformLayout();
            this.pTutorial.ResumeLayout(false);
            this.pTutorial.PerformLayout();
            this.msTutorial.ResumeLayout(false);
            this.msTutorial.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudBeatsPerMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbZoom)).EndInit();
            this.msBottom.ResumeLayout(false);
            this.msBottom.PerformLayout();
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
        private System.Windows.Forms.Button btnResetZoom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog sfdLightShow;
        private System.Windows.Forms.ToolTip tpDescription;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ofdLoad;
        private System.Windows.Forms.NumericUpDown nudBeatsPerMinute;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem minimizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trbZoom;
        private System.Windows.Forms.Label lbZoom;
        private System.Windows.Forms.MenuStrip msBottom;
        private System.Windows.Forms.ToolStripTextBox tbHelp;
        private System.Windows.Forms.Button btnAddTrack;
        private System.Windows.Forms.ToolStripMenuItem newShowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetZoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startTutorialToolStripMenuItem;
        private System.Windows.Forms.Panel pTutorial;
        private System.Windows.Forms.RichTextBox rtbTutorialContent;
        private System.Windows.Forms.MenuStrip msTutorial;
        private System.Windows.Forms.ToolStripTextBox tbTutorial;
        private System.Windows.Forms.Button btnNextTutorial;
        private System.Windows.Forms.TextBox tbLightshowName;
        private System.Windows.Forms.Label lbPlayBackTime;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importMusicToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ofdMusic;
    }
}

