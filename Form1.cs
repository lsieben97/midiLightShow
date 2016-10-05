using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dmx512UsbRs485;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;

namespace midiLightShow
{
    /// <summary>
    /// Editor form. Contains all editor stuff.
    /// </summary>
    public partial class frmEditor : Form
    {
        #region variables
        /// <summary>
        /// The current time of the playhead in 16th beats
        /// </summary>
        private int currentTime = 0;
        /// <summary>
        /// Indicates the beats per minute for the show
        /// </summary>
        private int bpm = 120;
        /// <summary>
        /// Indicates the amount of pixels per 16th beat for the show (can be used as zoom)
        /// </summary>
        private int pixelsPer16thNote = 10;
        /// <summary>
        /// Indicates how many miliseconds fit into 1 16th beat 
        /// </summary>
        private int milisecondsPer16thNote = 0;
        /// <summary>
        /// Indicates how many tracks are being used
        /// </summary>
        private int trackCounter = 1;
        /// <summary>
        /// Indicates the length of the show in 16th beats
        /// </summary>
        private int showTime = 16;
        /// <summary>
        /// Indicates the standard height of each track
        /// </summary>
        private int trackHeight = 50;

        /// <summary>
        /// List of all the track objects used in the show
        /// </summary>
        private List<track> tracks = new List<track>();
        /// <summary>
        /// Timer to play the show
        /// </summary>
        private Timer showTimer = new Timer();
        /// <summary>
        /// Debug dialog
        /// </summary>
        private debug db = new debug();
        private Color lineColor = SystemColors.Highlight;
       
        public AddShowEvent frmEditShowEvent = new AddShowEvent();

        public static Dmx512UsbRs485Driver driver = new Dmx512UsbRs485Driver();

        #endregion
        public frmEditor()
        {
            Console.WriteLine("Initializing application...");
            InitializeComponent();
            this.calculateTime();
            showTimer.Interval = this.pixelsPer16thNote;
            track.makeTypeMap();
            this.showTimer.Interval = this.milisecondsPer16thNote;
            this.showTimer.Tick += showTimer_Tick;
            this.frmEditShowEvent.isEditForm = true;
            this.frmEditShowEvent.Text = "Edit event";
            pTimeLine.BackColor = Color.FromArgb(255, 170, 213, 255);
            nudBeatsPerMinute.Value = this.bpm;
            Console.WriteLine("Done!");

            // customize menu strip
            msControl.Renderer = new ToolStripProfessionalRenderer(new CustomProfessionalColors());
            fileToolStripMenuItem.ForeColor = this.lineColor;
            toolsToolStripMenuItem.ForeColor = this.lineColor;
            loadMIDIToolStripMenuItem.ForeColor = this.lineColor;
            exitToolStripMenuItem.ForeColor = this.lineColor;
            exportToolStripMenuItem.ForeColor = this.lineColor;
            debugToolStripMenuItem.ForeColor = this.lineColor;
            importToolStripMenuItem.ForeColor = this.lineColor;
            minimizeToolStripMenuItem.ForeColor = this.lineColor;
            closeToolStripMenuItem.ForeColor = this.lineColor;
            
        }

        void showTimer_Tick(object sender, EventArgs e)
        {
            if(this.currentTime == this.showTime)
            {
                this.currentTime = 0;
            }
            foreach(track t in this.tracks)
            {
                if(t.events.Count(ev => ev.startTime == this.currentTime) == 1)
                {
                    showEvent currentEvent = t.events.Single(ev => ev.startTime == this.currentTime);
                    t.light.executeFunction(currentEvent.function, currentEvent.parameters, true);
                }
            }
            this.currentTime++;
            this.pTimeLine.Invalidate();
        }

        private void exportShow(string path)
        {
            XmlWriterSettings ws = new XmlWriterSettings();
            ws.Indent = true;
            XmlWriter w = XmlWriter.Create(path, ws);
            XmlSerializer xmlse = new XmlSerializer(this.tracks.GetType());
            xmlse.Serialize(w, this.tracks);
            w.Flush();
            w.Close();
            w.Dispose();
            MessageBox.Show("Exported to " + path);
            return;
        }

        private void loadMIDIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ofdMidi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                midiImport mi = new midiImport(this.ofdMidi.FileName);
                mi.ShowDialog();
                List<List<midiEvent>> tracks = mi.mr.getSeparateTracks();
                // remove tempomap track and empty tracks
                tracks.RemoveAt(0);
                tracks.RemoveAll(mt => mt.Count < 2);

                if(this.tracks.Count < tracks.Count)
                {
                    // we need more tracks
                    int tracksNeeded = tracks.Count - this.tracks.Count;
                    for(int i = 0; i < tracksNeeded; i++)
                    {
                        this.btnAddTrack_Click(this, EventArgs.Empty);
                    }
                }
                int currentTrack = 0;
                foreach(List<midiEvent> track in tracks)
                {
                    for(int i = 0; i < track.Count; i++)
                    {
                        if (track[i].name == "End of Track")
                        {
                            return;
                        }
                        if(track[i].name == "Note On" && track[i+1].name == "Note Off")
                        {
                            showEvent ev = new showEvent((int) track[i].timeFrame,(int)(track[i+1].timeFrame - track[i].timeFrame),"",new Dictionary<string,string>(),"",this.tracks[currentTrack].eventCount);
                            ev.canPlay = false;
                            this.tracks[currentTrack].events.Add(ev);
                            this.tracks[currentTrack].eventCount++;
                            this.tracks[currentTrack].currentMaxTime += ev.duration;
                        }
                    }
                    currentTrack++;
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // delete tracks if needed
            foreach (track t in this.tracks)
            {
                if (t.delete)
                {
                    if (this.tracks.Count > 1)
                    {
                        int index = this.tracks.IndexOf(t);
                        foreach (track tr in this.tracks.SkipWhile(trk => trk.yPos <= t.yPos))
                        {
                            tr.yPos -= this.trackHeight;
                            tr.yEnd -= this.trackHeight;
                        }
                    }
                    t.removeControls();
                    e.Graphics.Clear(pTimeLine.BackColor);
                    Console.WriteLine("Deleted track '" + t.name + "'.");
                    this.tracks.Remove(t);
                    this.pTimeLine.Invalidate();
                    break;
                }
            }
            // recalculate panel width.
            if (this.tracks.Count > 0)
            {
                this.showTime = this.tracks.Max(tr => tr.maxEventLength);
                int max = 160 + Convert.ToInt32(this.tracks.Max(t => t.maxEventLength) * this.pixelsPer16thNote + 5);
                if (max > 1344)
                {
                    this.pTimeLine.Size = new Size(max + 5, this.pTimeLine.Size.Height);
                }
                else
                {
                    this.pTimeLine.Size = new Size(1344, this.pTimeLine.Size.Height);
                }
            }
            foreach (track t in this.tracks)
            {
                foreach (showEvent se in t.events)
                {
                    Rectangle r = new Rectangle(164 + (int)(se.startTime * this.pixelsPer16thNote), t.yPos + 4, (int)(se.duration * this.pixelsPer16thNote), 44);
                    e.Graphics.FillRectangle(new Pen(Color.Red).Brush, r);
                    se.bounds = r;
                    e.Graphics.DrawRectangle(new Pen(this.lineColor), r);
                    e.Graphics.DrawString(se.function + "(" + se.paraString + ")", btnPlay.Font, new Pen(this.lineColor).Brush, 164 + (int)(se.startTime * this.pixelsPer16thNote), t.yPos + 4);
                    e.Graphics.DrawString("S: " + se.startTime.ToString(), btnPlay.Font, new Pen(this.lineColor).Brush, 164 + (int)(se.startTime * this.pixelsPer16thNote), t.yPos + 19);
                    e.Graphics.DrawString("D: " + se.duration.ToString(), btnPlay.Font, new Pen(this.lineColor).Brush, 164 + (int)(se.startTime * this.pixelsPer16thNote), t.yPos + 34);
                }
                //t.bAddEvent.Location = new Point(164 + (int)(t.currentMaxTime * this.pixelsPer16thNote), t.bAddEvent.Location.Y);
            }
            e.Graphics.FillRectangle(new Pen(this.lineColor).Brush, 0, 0, this.pTimeLine.Width, 3);
            //e.Graphics.FillRectangle(new Pen(this.lineColor).Brush, 0, this.pTimeLine.Height - 3, this.pTimeLine.Width, 3);
            if (160 + Convert.ToInt32(this.currentTime * (this.pixelsPer16thNote)) > this.pTimeLine.Size.Width)
            {
                this.currentTime = 0;
            }
            //Console.WriteLine("t: " + this.currentTime.ToString() + "st: " + this.preciseCurrentTime.ToString());
            e.Graphics.FillRectangle(new Pen(this.lineColor).Brush, 160 + Convert.ToInt32(this.currentTime * (this.pixelsPer16thNote)), 0, 3, pTimeLine.Height);
            e.Graphics.FillRectangle(new Pen(this.lineColor).Brush, 160, 0, 3, pTimeLine.Height);

            if (this.tracks.Count > 0)
            {
                int currentHeight = 0;
                foreach (track t in this.tracks)
                {
                    if (currentHeight + this.trackHeight >= this.pTimeLine.Height)
                    {
                        this.pTimeLine.Height += this.trackHeight;
                    }
                    e.Graphics.FillRectangle(new Pen(this.lineColor).Brush, 0, t.yEnd, this.pTimeLine.Width, 3);
                    currentHeight += this.trackHeight;
                    t.repositionControls();
                }
                if (this.tracks.Count(t => t.solo == true) == 1)
                {
                    int index = this.tracks.FindIndex(t => t.solo == true);
                    this.tracks[index].cbMute.Checked = false;
                    for (int i = 0; i < this.tracks.Count; i++)
                    {
                        if (i != index)
                        {
                            this.tracks[i].cbMute.Checked = true;
                        }
                    }
                }
                else if (this.tracks.Count(t => t.soloSwitch == true) == 1)
                {
                    foreach (track t in this.tracks)
                    {
                        t.cbMute.Checked = false;
                        t.soloSwitch = false;
                    }
                }
            }

        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (this.btnPlay.Text == "Play")
            {
                this.showTimer.Start();
                this.btnPlay.Text = "Pause";
            }
            else
            {
                this.showTimer.Stop();
                this.btnPlay.Text = "Play";
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.currentTime = 0;
            this.showTimer.Stop();
            this.pTimeLine.Invalidate();
            this.btnPlay.Text = "Play";
        }

        private void btnAddTrack_Click(object sender, EventArgs e)
        {
            if (this.tracks.Count == 12)
            {
                MessageBox.Show("Cannot add more tracks!");
                return;
            }
            this.tracks.Add(new track("Track " + this.trackCounter.ToString(), this.tracks.Count * this.trackHeight, ((this.tracks.Count + 1) * this.trackHeight), this.pTimeLine));
            this.tracks[this.tracks.Count - 1].drawControls();
            this.trackCounter++;
            this.pTimeLine.Invalidate();
        }

        private void pTimeLine_Click(object sender, EventArgs e)
        {
            foreach (track t in this.tracks)
            {
                int remove = -1;
                foreach (showEvent se in t.events)
                {
                    if (se.bounds.Contains(this.pTimeLine.PointToClient(Cursor.Position)))
                    {
                        this.frmEditShowEvent = new AddShowEvent();
                        this.frmEditShowEvent.isEditForm = true;
                        this.frmEditShowEvent.originalFunction = se.function;
                        this.frmEditShowEvent.light = t.light;
                        this.frmEditShowEvent.tbDuration.Text = se.duration.ToString();
                        this.frmEditShowEvent.tbStartTime.Text = se.startTime.ToString();
                        this.frmEditShowEvent.tbParameters.Text = se.paraString;
                        this.frmEditShowEvent.cbFunctions.SelectedItem = se.function;
                        this.frmEditShowEvent.parameters = se.parameters;
                        this.frmEditShowEvent.fillParameters();
                        int oldDuration = se.duration;
                        DialogResult dr = this.frmEditShowEvent.ShowDialog();
                        if (dr == System.Windows.Forms.DialogResult.OK)
                        {
                            int duration = Convert.ToInt32(this.frmEditShowEvent.tbDuration.Text);
                            int start = Convert.ToInt32(this.frmEditShowEvent.tbStartTime.Text);
                            bool valid = true;
                            foreach (showEvent ev in t.events.SkipWhile(sv => sv.index == se.index))
                            {
                                if (start > ev.startTime && start < ev.startTime + ev.duration)
                                {
                                    valid = false;
                                }
                                if (start + duration > ev.startTime && start + duration < ev.startTime + ev.duration)
                                {
                                    valid = false;
                                }
                            }
                            if (!valid)
                            {
                                MessageBox.Show("event cannot overlap!");
                                return;
                            }
                            se.duration = this.frmEditShowEvent.duration;
                            se.startTime = this.frmEditShowEvent.startTime;
                            se.function = this.frmEditShowEvent.cbFunctions.Text;
                            se.paraString = this.frmEditShowEvent.paraString;
                            se.parameters = this.frmEditShowEvent.parameters;
                            t.currentMaxTime = t.events.Max(ev => ev.startTime + ev.duration);
                            pTimeLine.Invalidate();

                        }
                        else if (dr == System.Windows.Forms.DialogResult.Abort)
                        {
                            remove = t.events.IndexOf(se);
                        }
                    }
                }
                if (remove != -1)
                {
                    t.events.RemoveAt(remove);
                    this.pTimeLine.Invalidate();
                }
            }
        }

        private void pTimeLine_Scroll(object sender, ScrollEventArgs e)
        {
            this.pTimeLine.Invalidate();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tracks.Count > 1)
            {
                if (this.sfdLightShow.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.exportShow(this.sfdLightShow.FileName);
                }
            }
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.db.Show();
        }

        private void frmEditor_Load(object sender, EventArgs e)
        {
            panel1.Size = this.Size;
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.ofdLoad.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.import(ofdLoad.FileName);
            }
        }

        private void import(string path)
        {
            XmlSerializer xmlse = new XmlSerializer(this.tracks.GetType());
            XmlReader xmlr = XmlReader.Create(path);
            if(xmlse.CanDeserialize(xmlr))
            {
                this.tracks = xmlse.Deserialize(xmlr) as List<track>;
                this.pTimeLine.Invalidate();
                foreach(track t in this.tracks)
                {
                    t.pTimeLine = this.pTimeLine;
                    t.drawControls();
                }
            }
            else
            {
                MessageBox.Show("There was an error while reading the file, it's probably corrupted!");
                return;
            }
        }

        private void nudBeatsPerMinute_ValueChanged(object sender, EventArgs e)
        {
            this.bpm = Convert.ToInt32(nudBeatsPerMinute.Value);
            this.btnStop_Click(this, EventArgs.Empty);
            this.calculateTime();
        }

        private void calculateTime()
        {
            this.milisecondsPer16thNote = 60000 / this.bpm / 4;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void minimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

    }
    // custom menustrip colors
    class CustomProfessionalColors : ProfessionalColorTable
    {
        public override Color MenuItemSelectedGradientBegin
        {
            get
            {
                return SystemColors.ControlDark;
            }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get
            {
                return SystemColors.ControlDarkDark;
            }
        }
        public override Color MenuItemBorder
        {
            get
            {
                return SystemColors.ControlDarkDark;
            }
        }

        public override Color MenuItemSelected
        {
            get
            {
                return Color.FromArgb(64, 64, 64);
            }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get
            {
                return SystemColors.ControlDark;
            }
        }

        public override Color MenuItemPressedGradientMiddle
        {
            get
            {
                return SystemColors.ControlDarkDark;
            }
        }
        public override Color MenuItemPressedGradientEnd
        {
            get
            {
                return SystemColors.ControlDark;
            }
        }
        public override Color MenuStripGradientBegin
        {
            get
            {
                return SystemColors.ControlDarkDark;
            }
        }

        public override Color MenuStripGradientEnd
        {
            get
            {
                return SystemColors.ControlDark;
            }
        }

        public override Color MenuBorder
        {
            get
            {
                return SystemColors.ControlDarkDark;
            }
        }
        public override Color ToolStripDropDownBackground
        {
            get
            {
                return SystemColors.ControlDarkDark;
            }
        }
        public override Color ImageMarginGradientBegin
        {
            get
            {
                return SystemColors.ControlDarkDark;
            }
        }

        public override Color ImageMarginGradientEnd
        {
            get
            {
                return SystemColors.ControlDarkDark;
            }
        }
        public override Color ImageMarginGradientMiddle
        {
            get
            {
                return SystemColors.ControlDarkDark;
            }
        }

    }
}
