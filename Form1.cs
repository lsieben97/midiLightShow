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
using System.Reflection;

namespace midiLightShow
{
    #region Editor
    /// <summary>
    /// Editor form. Contains all editor stuff.
    /// </summary>
    public partial class frmEditor : Form
    {
        #region Global variables
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
        /// Indicates the default offset for the timeline
        /// </summary>
        private int timelineOffsetX = 160;
        private int timelineOffsetY = 25;
        /// <summary>
        /// The zoom level for the timeline
        /// </summary>
        private int zoom = 100;
        private int lastSoloTrack;
        private string lastSavedPath = "";
        private bool saved = true;
        private string currentTutorialStep = "Intro";


        /// <summary>
        /// The default message to show when no help text is shown
        /// </summary>
        public string defaultHelpMessage = "Ready";
        /// <summary>
        /// List of all the track objects used in the show
        /// </summary>
        private List<track> tracks = new List<track>();
        /// <summary>
        /// Timer to play the show
        /// </summary>
        private Timer showTimer = new Timer();
        private Timer tutorialTimer = new Timer();
        private Control tutorialTarget;
        /// <summary>
        /// Debug dialog for debugging
        /// </summary>
        private debug db = new debug();
        /// <summary>
        /// Standard Color object for lines
        /// </summary>
        private Color lineColor = SystemColors.Highlight;
        /// <summary>
        /// AddShowEvent form for editing existing showEvents
        /// </summary>
        public AddShowEvent frmEditShowEvent = new AddShowEvent();
        public lightshow currentLightshow = new lightshow();

        #endregion
        #region Constructors
        /// <summary>
        /// Initialize new editor form
        /// </summary>
        public frmEditor()
        {
            Console.WriteLine("Initializing application...");
            this.InitializeComponent();
            this.calculateTime();
            // timers
            this.showTimer.Interval = this.pixelsPer16thNote;
            this.showTimer.Interval = this.milisecondsPer16thNote;
            this.showTimer.Tick += showTimer_Tick;
            this.tutorialTimer.Interval = 800;
            this.tutorialTimer.Tick += tutorialTimer_Tick;
            // editShowEvent form
            this.frmEditShowEvent.isEditForm = true;
            this.frmEditShowEvent.Text = "Edit event";
            // diverse statements
            this.pTimeLine.BackColor = Color.FromArgb(255, 170, 213, 255);
            this.nudBeatsPerMinute.Value = this.currentLightshow.bpm;
            this.pixelsPer16thNote = trbZoom.Value;
            this.lbZoom.Text = (trbZoom.Value / 10 * 100).ToString() + "%";
            this.Size = new Size(Screen.GetWorkingArea(this).Width, Screen.GetWorkingArea(this).Height);
            this.Location = new Point(0, 0);
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, this.pTimeLine, new object[] { true });
            this.pTutorial.Visible = false;
            this.tbLightshowName.Text = "New lighshow";
            track.makeTypeMap();
            // customize menu strip
            msControl.Renderer = new ToolStripProfessionalRenderer(new CustomProfessionalColors());
            msBottom.Renderer = new ToolStripProfessionalRenderer(new CustomProfessionalColors());
            msTutorial.Renderer = new ToolStripProfessionalRenderer(new CustomProfessionalColors());
            fileToolStripMenuItem.ForeColor = this.lineColor;
            toolsToolStripMenuItem.ForeColor = this.lineColor;
            loadMIDIToolStripMenuItem.ForeColor = this.lineColor;
            exitToolStripMenuItem.ForeColor = this.lineColor;
            exportToolStripMenuItem.ForeColor = this.lineColor;
            debugToolStripMenuItem.ForeColor = this.lineColor;
            importToolStripMenuItem.ForeColor = this.lineColor;
            minimizeToolStripMenuItem.ForeColor = this.lineColor;
            closeToolStripMenuItem.ForeColor = this.lineColor;
            newShowToolStripMenuItem.ForeColor = this.lineColor;
            Console.WriteLine("Done!");
        }

        void tutorialTimer_Tick(object sender, EventArgs e)
        {
            string type = this.tutorialTarget.Tag.ToString();
            Console.WriteLine(type);
            if (type == "Track")
            {
                if (this.tutorialTarget.BackColor == SystemColors.HotTrack)
                {
                    this.tutorialTarget.BackColor = Color.FromArgb(255, 170, 213, 255);
                }
                else
                {
                    this.tutorialTarget.BackColor = SystemColors.HotTrack;
                }
            }
            else
            {
                if (this.tutorialTarget.BackColor == SystemColors.HotTrack)
                {
                    this.tutorialTarget.BackColor = SystemColors.ControlDarkDark;
                }
                else
                {
                    this.tutorialTarget.BackColor = SystemColors.HotTrack;
                }
            }
        }
        #endregion
        #region Editor form methods
        /// <summary>
        /// Tick event handler for the showtimer
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        void showTimer_Tick(object sender, EventArgs e)
        {
            updatePlayBack();
        }

        /// <summary>
        /// Updates time variables and triggers an timeline update
        /// </summary>
        private void updatePlayBack()
        {
            if (this.currentTime == this.currentLightshow.showTime)
            {
                this.currentTime = 0;
            }
            foreach (track t in this.currentLightshow.tracks)
            {
                if (t.events.Count(ev => ev.startTime == this.currentTime) == 1)
                {
                    showEvent currentEvent = t.events.Single(ev => ev.startTime == this.currentTime);
                    t.light.executeFunction(currentEvent.function, currentEvent.parameters, true);
                }
                else
                {
                    t.light.driver.DmxSendCommand(512);
                }
            }
            this.currentTime++;
            this.pTimeLine.Invalidate();
        }

        /// <summary>
        /// Exports the current show to an xml file using xml serialization
        /// </summary>
        /// <param name="path">The file path to export to</param>
        private void exportShow(string path)
        {
            XmlWriterSettings ws = new XmlWriterSettings();
            ws.Indent = true;
            XmlWriter w = XmlWriter.Create(path, ws);
            XmlSerializer xmlse = new XmlSerializer(this.currentLightshow.GetType());
            xmlse.Serialize(w, this.currentLightshow);
            w.Flush();
            w.Close();
            tbHelp.Text = "Exported to " + path;
            this.saved = true;
            return;
        }

        /// <summary>
        /// Click event handler for the load MIDI toolstripMenuItem
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void loadMIDIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importMIDI();
        }

        /// <summary>
        /// Imports a MIDI file and creates corresponding showEvents based on the specified MIDI file
        /// </summary>
        private void importMIDI()
        {
            if (this.ofdMidi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<showEvent> events = new List<showEvent>();
                midiImport mi = new midiImport(this.ofdMidi.FileName);
                mi.ShowDialog();
                List<List<midiEvent>> tracks = mi.mr.getSeparateTracks();
                // remove tempomap track and empty tracks
                if (mi.mr.trackChunkCount > 1)
                {
                    tracks.RemoveAt(0);
                }
                tracks.RemoveAll(mt => mt.Count < 2);
                this.tbHelp.Text = "Importing MIDI file...";
                if (this.currentLightshow.tracks.Count < tracks.Count)
                {
                    // we need more tracks
                    int tracksNeeded = tracks.Count - this.currentLightshow.tracks.Count;
                    for (int i = 0; i < tracksNeeded; i++)
                    {
                        this.btnAddTrack_Click(this, EventArgs.Empty);
                    }
                }
                int currentTrack = 0;
                foreach (List<midiEvent> track in tracks)
                {
                    for (int i = 0; i < track.Count; i++)
                    {
                        if (track[i].name == "Note On")
                        {
                            showEvent ev = new showEvent((int)track[i].timeFrame / this.milisecondsPer16thNote, 0, "Empty", new List<string>(), "", this.currentLightshow.tracks[currentTrack].eventCount);
                            ev.note = track[i].note;
                            ev.canPlay = false;
                            events.Add(ev);
                        }
                        else if (track[i].name == "Note Off")
                        {
                            showEvent s = events.Single(e => e.note == track[i].note);
                            s.duration = ((int)track[i].timeFrame / this.milisecondsPer16thNote) - s.startTime;
                            this.currentLightshow.tracks[currentTrack].events.Add(s);
                            this.currentLightshow.tracks[currentTrack].maxEventLength += s.duration;
                            events.Remove(s);
                        }
                    }
                    currentTrack++;
                }
                this.pTimeLine.Invalidate();
                this.tbHelp.Text = this.defaultHelpMessage;
            }
        }

        /// <summary>
        /// Paint event handler for the timeline panel
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.FromArgb(255, 170, 213, 255));
            deleteTracks();
            recalculatePanelWidth();
            drawTracks(e);
            drawBeatLines(e);
            drawShowEvents(e);
            updatePlayHead(e);
        }

        /// <summary>
        /// Draws all tracks and processes mute/solo changes
        /// </summary>
        /// <param name="e">PaintEventArgs with graphics object</param>
        private void drawTracks(PaintEventArgs e)
        {
            if (this.currentLightshow.tracks.Count > 0)
            {
                int currentHeight = 0;
                foreach (track t in this.currentLightshow.tracks)
                {
                    if (currentHeight + this.trackHeight >= this.pTimeLine.Height)
                    {
                        this.pTimeLine.Height += this.trackHeight;
                    }
                    e.Graphics.FillRectangle(new Pen(this.lineColor).Brush, 0, t.yEnd, this.pTimeLine.Width, 3);
                    currentHeight += this.trackHeight;
                    t.repositionControls();
                }
                if (this.currentLightshow.tracks.Count(t => t.solo == true) == 1)
                {
                    int index = this.currentLightshow.tracks.FindIndex(t => t.solo == true);
                    this.lastSoloTrack = index;
                    this.currentLightshow.tracks[index].cbMute.Checked = false;
                    for (int i = 0; i < this.currentLightshow.tracks.Count; i++)
                    {
                        if (i != index)
                        {
                            this.currentLightshow.tracks[i].cbMute.Checked = true;
                            this.currentLightshow.tracks[i].lbName.ForeColor = SystemColors.ControlDarkDark;
                        }
                    }
                }
                if (this.currentLightshow.tracks.Count(t => t.soloSwitch == true) == 1)
                {
                    foreach (track t in this.currentLightshow.tracks)
                    {
                        t.cbMute.Checked = false;
                        t.soloSwitch = false;
                        t.lbName.ForeColor = Color.Black;
                    }
                }
                if (this.currentLightshow.tracks.Count(t => t.solo == true) > 1)
                {
                    for (int i = 0; i < this.currentLightshow.tracks.Count; i++)
                    {
                        if (i != this.lastSoloTrack)
                        {
                            this.currentLightshow.tracks[i].cbSolo.Checked = false;
                            this.currentLightshow.tracks[i].soloSwitch = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Recalculate the position of the playhead
        /// </summary>
        /// <param name="e">PaintEventArgs with graphics object</param>
        private void updatePlayHead(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new Pen(this.lineColor).Brush, 0, this.timelineOffsetY, this.pTimeLine.Width, 3);
            if (this.timelineOffsetX + Convert.ToInt32(this.currentTime * (this.pixelsPer16thNote)) > this.pTimeLine.Size.Width)
            {
                this.currentTime = 0;
            }
            e.Graphics.FillRectangle(new Pen(this.lineColor).Brush, this.timelineOffsetX, this.timelineOffsetY, 3, pTimeLine.Height - this.timelineOffsetY);
            int xpos = this.timelineOffsetX + Convert.ToInt32(this.currentTime * (this.pixelsPer16thNote));
            e.Graphics.FillRectangle(new Pen(SystemColors.HotTrack).Brush, xpos, this.timelineOffsetY, 3, pTimeLine.Height - this.timelineOffsetY);
            e.Graphics.FillPolygon(new Pen(SystemColors.HotTrack).Brush, new Point[] { new Point(xpos - 9, 10), new Point(xpos + 10, 10), new Point(xpos + 1, this.timelineOffsetY), new Point(xpos - 9, 10) });
        }

        /// <summary>
        /// Draw all showEvents for all tracks with showEvent information
        /// </summary>
        /// <param name="e">PaintEventArgs with graphics object</param>
        private void drawShowEvents(PaintEventArgs e)
        {
            foreach (track t in this.currentLightshow.tracks)
            {
                foreach (showEvent se in t.events)
                {
                    Rectangle r = new Rectangle(this.timelineOffsetX + (int)(se.startTime * this.pixelsPer16thNote), t.yPos + 4, (int)(se.duration * this.pixelsPer16thNote), 44);
                    if (t.mute)
                    {
                        e.Graphics.FillRectangle(new Pen(SystemColors.ControlDark).Brush, r);
                        se.bounds = r;
                        e.Graphics.DrawRectangle(new Pen(this.lineColor), r);
                        e.Graphics.DrawString(se.function + "(" + se.paraString + ")", btnPlay.Font, new Pen(SystemColors.ControlDarkDark).Brush, 164 + (int)(se.startTime * this.pixelsPer16thNote), t.yPos + 4);
                        e.Graphics.DrawString("S: " + se.startTime.ToString(), btnPlay.Font, new Pen(SystemColors.ControlDarkDark).Brush, 164 + (int)(se.startTime * this.pixelsPer16thNote), t.yPos + 19);
                        e.Graphics.DrawString("D: " + se.duration.ToString(), btnPlay.Font, new Pen(SystemColors.ControlDarkDark).Brush, 164 + (int)(se.startTime * this.pixelsPer16thNote), t.yPos + 34);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new Pen(Color.FromName(t.eventColor)).Brush, r);
                        se.bounds = r;
                        e.Graphics.DrawRectangle(new Pen(this.lineColor), r);
                        e.Graphics.DrawString(se.function + "(" + se.paraString + ")", btnPlay.Font, new Pen(this.lineColor).Brush, 164 + (int)(se.startTime * this.pixelsPer16thNote), t.yPos + 4);
                        e.Graphics.DrawString("S: " + se.startTime.ToString(), btnPlay.Font, new Pen(this.lineColor).Brush, 164 + (int)(se.startTime * this.pixelsPer16thNote), t.yPos + 19);
                        e.Graphics.DrawString("D: " + se.duration.ToString(), btnPlay.Font, new Pen(this.lineColor).Brush, 164 + (int)(se.startTime * this.pixelsPer16thNote), t.yPos + 34);
                    }
                }
            }
        }

        private void drawBeatLines(PaintEventArgs e)
        {
            if (this.zoom >= 200)
            {
                for (int i = 0; i < ((this.pTimeLine.Width - this.timelineOffsetX) / this.pixelsPer16thNote | 0); i++)
                {
                    e.Graphics.DrawLine(new Pen(Color.Black), this.timelineOffsetX + i * this.pixelsPer16thNote, this.timelineOffsetY, this.timelineOffsetX + i * this.pixelsPer16thNote, this.pTimeLine.Height);
                    e.Graphics.DrawString(i.ToString(), this.btnPlay.Font, new Pen(this.lineColor).Brush, this.timelineOffsetX + i * this.pixelsPer16thNote, 5);
                }
            }
            else if (this.zoom < 200 && this.zoom > 70)
            {
                for (int i = 0; i < ((this.pTimeLine.Width - this.timelineOffsetX) / 2 / this.pixelsPer16thNote | 0); i++)
                {
                    e.Graphics.DrawLine(new Pen(Color.Black), this.timelineOffsetX + i * this.pixelsPer16thNote * 2, this.timelineOffsetY, this.timelineOffsetX + i * this.pixelsPer16thNote * 2, this.pTimeLine.Height);
                    e.Graphics.DrawString((i * 2).ToString(), this.btnPlay.Font, new Pen(this.lineColor).Brush, this.timelineOffsetX + i * this.pixelsPer16thNote * 2, 5);
                }
            }
            else if (this.zoom <= 70)
            {
                for (int i = 0; i < ((this.pTimeLine.Width - this.timelineOffsetX) / 4 / this.pixelsPer16thNote | 0); i++)
                {
                    e.Graphics.DrawLine(new Pen(Color.Black), this.timelineOffsetX + i * this.pixelsPer16thNote * 4, this.timelineOffsetY, this.timelineOffsetX + i * this.pixelsPer16thNote * 4, this.pTimeLine.Height);
                    e.Graphics.DrawString((i * 4).ToString(), this.btnPlay.Font, new Pen(this.lineColor).Brush, this.timelineOffsetX + i * this.pixelsPer16thNote * 4, 5);
                }
            }
            Rectangle rEnd = new Rectangle(this.timelineOffsetX + this.currentLightshow.showTime * this.pixelsPer16thNote, 0, 2, this.timelineOffsetY);
            e.Graphics.FillRectangle(new Pen(SystemColors.HotTrack).Brush, rEnd);
        }
        /// <summary>
        /// Recalculate the panel width based on max event end time
        /// </summary>
        private void recalculatePanelWidth()
        {
            this.panel1.Size = new Size(this.Size.Width - 20, this.pTimeLine.Size.Height + 17);
            if (this.currentLightshow.tracks.Count > 0)
            {
                this.currentLightshow.showTime = this.currentLightshow.tracks.Max(tr => tr.maxEventLength);
                if (this.currentLightshow.showTime < 16)
                {
                    this.currentLightshow.showTime = 16;
                }
                int max = this.timelineOffsetX + Convert.ToInt32(this.currentLightshow.tracks.Max(t => t.maxEventLength) * this.pixelsPer16thNote + 5);
                if (max > this.Size.Width - 3)
                {
                    this.pTimeLine.Size = new Size(max + 5, this.pTimeLine.Size.Height);
                }
                else
                {
                    this.pTimeLine.Size = new Size(this.Size.Width - 20, this.pTimeLine.Size.Height);
                }
            }
        }

        /// <summary>
        /// Delete tracks if indicated
        /// </summary>
        private void deleteTracks()
        {
            foreach (track t in this.currentLightshow.tracks)
            {
                if (t.delete)
                {
                    if (this.currentLightshow.tracks.Count > 1)
                    {
                        this.saved = false;
                        int index = this.currentLightshow.tracks.IndexOf(t);
                        foreach (track tr in this.currentLightshow.tracks.SkipWhile(trk => trk.yPos <= t.yPos))
                        {
                            tr.yPos -= this.trackHeight;
                            tr.yEnd -= this.trackHeight;
                        }
                    }
                    t.removeControls();
                    //e.Graphics.Clear(pTimeLine.BackColor);
                    Console.WriteLine("Deleted track '" + t.name + "'.");
                    this.currentLightshow.tracks.Remove(t);
                    this.pTimeLine.Invalidate();
                    break;
                }
            }
        }

        /// <summary>
        /// Click event handler for the play button
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
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

        /// <summary>
        /// Click event for the stop button
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            this.currentTime = 0;
            this.showTimer.Stop();
            this.pTimeLine.Invalidate();
            this.btnPlay.Text = "Play";
        }

        /// <summary>
        /// Click event for the add event button
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnAddTrack_Click(object sender, EventArgs e)
        {
            if (this.currentTutorialStep == "newTrack")
            {
                this.tutorialTarget.BackColor = SystemColors.ControlDarkDark;
                this.tutorialTarget = this.btnNextTutorial;
            }
            addTrack();
        }

        /// <summary>
        /// Adds a new track if possible
        /// </summary>
        private void addTrack()
        {
            if (this.currentLightshow.tracks.Count == 12)
            {
                MessageBox.Show("Cannot add more tracks!");
                return;
            }
            this.saved = false;
            this.currentLightshow.tracks.Add(new track("Track " + this.trackCounter.ToString(), this.currentLightshow.tracks.Count * this.trackHeight + this.timelineOffsetY, this.timelineOffsetY + ((this.currentLightshow.tracks.Count + 1) * this.trackHeight), this.pTimeLine));
            this.currentLightshow.tracks[this.currentLightshow.tracks.Count - 1].drawControls();
            this.trackCounter++;
            this.pTimeLine.Invalidate();
        }

        /// <summary>
        /// Click event handler for the timeline panel
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void pTimeLine_Click(object sender, EventArgs e)
        {
            handleTimelineClick();
        }

        /// <summary>
        /// checks if the user clicked on an showEvent and opens an new edit showEvent dialog
        /// </summary>
        private void handleTimelineClick()
        {
            Point cursorPos = this.pTimeLine.PointToClient(Cursor.Position);
            if (cursorPos.Y < this.timelineOffsetY && cursorPos.X > this.timelineOffsetX)
            {
                int targetTime = Convert.ToInt32(Math.Round(((double)(cursorPos.X - this.timelineOffsetX) / (double)this.pixelsPer16thNote)));
                if (targetTime <= this.currentLightshow.showTime)
                {
                    this.currentTime = targetTime;
                    this.pTimeLine.Invalidate();
                }
            }
            else
            {
                foreach (track t in this.currentLightshow.tracks)
                {
                    int remove = -1;
                    foreach (showEvent se in t.events)
                    {
                        if (se.bounds.Contains(this.pTimeLine.PointToClient(Cursor.Position)))
                        {
                            // setup edit dialog
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
                            // show dialog
                            DialogResult dr = this.frmEditShowEvent.ShowDialog();
                            // check start and end times
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
                                this.saved = false;
                                // apply changes to the showEvent object
                                se.duration = this.frmEditShowEvent.duration;
                                se.startTime = this.frmEditShowEvent.startTime;
                                se.function = this.frmEditShowEvent.cbFunctions.Text;
                                se.paraString = this.frmEditShowEvent.paraString;
                                se.parameters = this.frmEditShowEvent.parameters;
                                t.currentMaxTime = t.events.Max(ev => ev.startTime + ev.duration);
                                // trigger an timeline update
                                pTimeLine.Invalidate();

                            }
                            else if (dr == System.Windows.Forms.DialogResult.Abort)
                            {
                                remove = t.events.IndexOf(se);
                            }
                        }
                    }
                    // delete an showEvent if needed
                    if (remove != -1)
                    {
                        t.events.RemoveAt(remove);
                        this.pTimeLine.Invalidate();
                    }
                }
            }
        }

        /// <summary>
        /// Scroll event handler. Trigger an update when the user scrolls the timeline
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void pTimeLine_Scroll(object sender, ScrollEventArgs e)
        {
            this.pTimeLine.Invalidate();
        }

        /// <summary>
        /// Click event handler for the save show menuStripItem
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentLightshow.tracks.Count > 0 && this.saved == false)
            {
                if (this.lastSavedPath.Length == 0)
                {
                    if (this.sfdLightShow.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        this.exportShow(this.sfdLightShow.FileName);
                    }
                }
                else
                {
                    this.exportShow(this.lastSavedPath);
                }
            }
        }

        /// <summary>
        /// Click event handler for the debug menuStripItem. Shows the debug window
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.db.Show();
        }

        /// <summary>
        /// Click event for the load show menuStripItem. Loads a show
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.saved)
            {
                if (this.ofdLoad.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.import(ofdLoad.FileName);
                }
            }
            else
            {
                if (MessageBox.Show("The current lightshow has not been saved, are you sure you want to close this lightshow?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (this.ofdLoad.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        this.import(ofdLoad.FileName);
                    }
                }
            }
        }

        /// <summary>
        /// loads a show in xml file format using xml serialization
        /// </summary>
        /// <param name="path">'.lightshow' file to load</param>
        private void import(string path)
        {
            XmlSerializer xmlse = new XmlSerializer(this.currentLightshow.GetType());
            XmlReader xmlr = XmlReader.Create(path);
            try
            {
                if (xmlse.CanDeserialize(xmlr))
                {
                    this.currentLightshow = xmlse.Deserialize(xmlr) as lightshow;
                    this.pTimeLine.Invalidate();
                    foreach (track t in this.currentLightshow.tracks)
                    {
                        t.pTimeLine = this.pTimeLine;
                        t.afterImport();
                        t.drawControls();
                    }
                    this.lastSavedPath = path;
                    this.tbLightshowName.Text = this.currentLightshow.name;
                    this.nudBeatsPerMinute.Value = this.currentLightshow.bpm;
                    this.calculateTime();
                }
                else
                {
                    MessageBox.Show("There was an error while reading the file, it's probably corrupted!");
                    return;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("There was an error while reading the file.\nError details: " + e.Message);
            }
            xmlr.Close();
        }

        /// <summary>
        /// ValueChanged event handler for the numericUpDown control witch sets the bpm
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void nudBeatsPerMinute_ValueChanged(object sender, EventArgs e)
        {
            this.currentLightshow.bpm = Convert.ToInt32(nudBeatsPerMinute.Value);
            this.btnStop_Click(this, EventArgs.Empty);
            this.calculateTime();
        }

        /// <summary>
        /// Recalculate the miliseconds per 16th beat
        /// </summary>
        private void calculateTime()
        {
            this.milisecondsPer16thNote = 60000 / this.currentLightshow.bpm / 4;
            this.showTimer.Interval = this.milisecondsPer16thNote;
        }

        /// <summary>
        /// Click event for the close menuStripItem. Closes the application
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saved)
            {
                Application.Exit();
            }
            else
            {
                if (MessageBox.Show("The current lightshow has not been saved, are you sure you want to close this lightshow?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }

        /// <summary>
        /// Click event handler for the minimize menuStripItem. Minimizes the application
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void minimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void trbZoom_Scroll(object sender, EventArgs e)
        {
            this.pixelsPer16thNote = trbZoom.Value;
            this.zoom = Convert.ToInt32(((double)trbZoom.Value / 10 * 100));
            this.lbZoom.Text = ((double)trbZoom.Value / 10 * 100).ToString() + "%";
            this.pTimeLine.Invalidate();
        }
        #endregion

        private void btnStop_MouseEnter(object sender, EventArgs e)
        {
            Button b = sender as Button;
            switch (b.Name)
            {
                case "btnPlay":
                    this.tbHelp.Text = "Play the show.";
                    break;
                case "btnStop":
                    this.tbHelp.Text = "Stop playing the show.";
                    break;
                case "btnAddTrack":
                    this.tbHelp.Text = "Add a new track to the show.";
                    break;
            }
        }

        private void btnPlay_MouseLeave(object sender, EventArgs e)
        {
            this.tbHelp.Text = this.defaultHelpMessage;
        }

        private void loadMIDIToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.tbHelp.Text = "Import a MIDI file to convert it to a light show.";
        }

        private void importToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.tbHelp.Text = "Load a previously saved light show.";
        }

        private void exportToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.tbHelp.Text = "Save this light show.";
        }

        private void exitToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.tbHelp.Text = "Close DMX studio.";
        }

        private void loadMIDIToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            this.tbHelp.Text = this.defaultHelpMessage;
        }

        private void btnResetZoom_Click(object sender, EventArgs e)
        {
            this.trbZoom.Value = 10;
            this.trbZoom_Scroll(this, EventArgs.Empty);
        }

        private void newShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (track t in this.currentLightshow.tracks)
            {
                t.removeControls();
            }
            this.currentLightshow.tracks.Clear();
            this.pTimeLine.Invalidate();
        }

        private void frmEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.D)
            {
                this.btnResetZoom_Click(this, EventArgs.Empty);
            }
        }

        private void resetZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnResetZoom_Click(this, EventArgs.Empty);
        }

        private void userManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            help h = new help();
            h.ShowDialog();
        }

        private void startTutorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pTutorial.Visible = true;
            pTutorial.Location = new Point(500, 150);
        }
        private void btnNextTutorial_Click(object sender, EventArgs e)
        {
            this.tutorialTimer.Stop();
            this.btnNextTutorial.BackColor = Color.FromArgb(64, 64, 64);
            switch (this.currentTutorialStep)
            {
                case "Intro":
                    this.tbTutorial.Text = "Tutorial - Getting started";
                    this.rtbTutorialContent.Text = "When DMX studio launches you will start with nothing, so let's change that.\nClick the 'Add track' button to start making your own lightshow!";
                    this.tutorialTarget = this.btnAddTrack;
                    this.btnNextTutorial.Text = "Next";
                    this.tutorialTimer.Start();
                    this.currentTutorialStep = "newTrack";
                    break;
                case "newTrack":
                    if (this.currentLightshow.tracks.Count == 0)
                    {
                        this.currentTutorialStep = "Intro";
                        this.btnNextTutorial_Click(this, EventArgs.Empty);
                    }
                    else
                    {
                        this.tbTutorial.Text = "Tutorial - About tracks";
                        this.rtbTutorialContent.Text = "A track represents a light (fixture). By adding more tracks you can control more lights.\n\nClick Next to continue.";
                        this.currentTutorialStep = "aboutTracks";
                    }
                    break;
                case "aboutTracks":
                    this.tbTutorial.Text = "Tutorial - Changing track options";
                    this.rtbTutorialContent.Text = "The track you just added controls by default an PAR56 RGB spotlight. We can change this by viewing the track options dialog.\n\nClick the gear icon next to the track name, then select the '360 spotlight' in the lights dropdown. You can also change the track name and the color of events that this track contains.\nWhen you are done, Click 'Next'";
                    this.tutorialTarget = this.currentLightshow.tracks[0].pbOptions;
                    this.tutorialTimer.Start();
                    this.currentTutorialStep = "trackOptions";
                    break;
                case "trackOptions":
                    this.currentLightshow.tracks[0].pbOptions.BackColor = Color.FromArgb(255, 170, 213, 255);
                    this.tbTutorial.Text = "Tutorial - About show events";
                    this.rtbTutorialContent.Text = "A show event represents an event in your lightshow this can be anything: a light needs to be turned on, an option of a light needs to be changed etc. show events are a vital part of DMX studio, so understanding how they work is key.\n\nClick 'Next' to continue.";
                    this.currentTutorialStep = "aboutShowevents";
                    break;
                case "aboutShowevents":
                    this.tbTutorial.Text = "Tutorial - Timing in DMX studio";
                    this.rtbTutorialContent.Text = "A important part of learning how show events work is understanding how DMX studio uses timing.\n DMX studio uses BPM to to calculate timing. BPM stands for Beats Per Minute and is a common phrase in the music industry. The smallest time unit is a 16th beat. By changing the bpm you can make time go faster or slower.\n\nClick 'Next' to continue.";
                    this.currentTutorialStep = "timing";
                    break;
                case "timing":
                    this.tbTutorial.Text = "Tutorial - Timing when adding show events";
                    this.rtbTutorialContent.Text = "When adding a new show event you need to specify a start time and a duration. These are entered in 16th beats. It's recommended to set the BPM of the lighshow to be the BPM of the song that this lightshow will be based on. \n\nClick 'Next' to continue.";
                    this.currentTutorialStep = "timing2";
                    break;
                case "timing2":
                    this.tbTutorial.Text = "Tutorial - Adding a show event";
                    this.rtbTutorialContent.Text = "Now you know how timing works you can add a show event to the track!\nClick the 'Add event' button. The add show event dialog will show. Here you can select an function to execute when the show event is executing. You can also specify parameters to send to the function.\nWhen you are done, Click 'Next'";
                    this.tutorialTarget = this.currentLightshow.tracks[0].bAddEvent;
                    this.tutorialTimer.Start();
                    this.currentTutorialStep = "addShowevent";
                    break;
                case "addShowevent":
                    this.currentLightshow.tracks[0].bAddEvent.BackColor = SystemColors.ControlDarkDark;
                    this.tbTutorial.Text = "Tutorial - That's it!";
                    this.rtbTutorialContent.Text = "And that's it! You can expand your lightshow by adding more tracks and show events. If you own a light supported by DMX studio you can check the 'live' checkbox in the track options dialog to make DMX studio control the phisical light!\nFor more information view the user manual under Help -> User manual.\n\nClick 'Finish' to close this tutorial.";
                    this.currentTutorialStep = "Intro";
                    this.btnNextTutorial.Text = "Finish!";
                    break;
            }
        }

        private void tbLightshowName_TextChanged(object sender, EventArgs e)
        {
            this.currentLightshow.name = this.tbLightshowName.Text;
        }
    }
    #endregion
    #region Custom colors
    /// <summary>
    /// Class to set custom menustrip colors
    /// </summary>
    class CustomProfessionalColors : ProfessionalColorTable
    {
        #region Overrides
        /// <summary>
        /// The start gradient for the upper menuStripItem
        /// </summary>
        public override Color MenuItemSelectedGradientBegin
        {
            get
            {
                return SystemColors.ControlDark;
            }
        }
        /// <summary>
        /// The end gradient for the upper menuStripItem
        /// </summary>
        public override Color MenuItemSelectedGradientEnd
        {
            get
            {
                return SystemColors.ControlDarkDark;
            }
        }
        /// <summary>
        /// The border color of the menuStrip
        /// </summary>
        public override Color MenuItemBorder
        {
            get
            {
                return SystemColors.ControlDarkDark;
            }
        }

        /// <summary>
        /// The color of the sub-menuStripItem
        /// </summary>
        public override Color MenuItemSelected
        {
            get
            {
                return Color.FromArgb(64, 64, 64);
            }
        }

        /// <summary>
        /// The start gradient for the sub menuStripItem when pressed
        /// </summary>
        public override Color MenuItemPressedGradientBegin
        {
            get
            {
                return SystemColors.ControlDark;
            }
        }

        /// <summary>
        /// The middle gradient for the sub menuStripItem when pressed
        /// </summary>
        public override Color MenuItemPressedGradientMiddle
        {
            get
            {
                return SystemColors.ControlDarkDark;
            }
        }

        /// <summary>
        /// The end gradient for the sub menuStripItem when pressed
        /// </summary>
        public override Color MenuItemPressedGradientEnd
        {
            get
            {
                return SystemColors.ControlDark;
            }
        }

        /// <summary>
        /// The start gradient for the menuStrip
        /// </summary>
        public override Color MenuStripGradientBegin
        {
            get
            {
                return SystemColors.ControlDarkDark;
            }
        }

        /// <summary>
        /// The end gradient for the menuStrip
        /// </summary>
        public override Color MenuStripGradientEnd
        {
            get
            {
                return SystemColors.ControlDark;
            }
        }

        /// <summary>
        /// The border color of the dropdownMenu
        /// </summary>
        public override Color MenuBorder
        {
            get
            {
                return SystemColors.ControlDarkDark;
            }
        }

        /// <summary>
        /// The background color of the dropdown menu
        /// </summary>
        public override Color ToolStripDropDownBackground
        {
            get
            {
                return SystemColors.ControlDarkDark;
            }
        }
        /// <summary>
        /// The imageMargin (left side) start gradient
        /// </summary>
        public override Color ImageMarginGradientBegin
        {
            get
            {
                return SystemColors.ControlDarkDark;
            }
        }

        /// <summary>
        /// The imageMargin (left side) end gradient
        /// </summary>
        public override Color ImageMarginGradientEnd
        {
            get
            {
                return SystemColors.ControlDarkDark;
            }
        }

        /// <summary>
        /// The imageMargin (left side) middle gradient
        /// </summary>
        public override Color ImageMarginGradientMiddle
        {
            get
            {
                return SystemColors.ControlDarkDark;
            }
        }
        #endregion
    }
    #endregion
}
