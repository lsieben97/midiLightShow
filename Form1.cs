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

namespace midiLightShow
{
    public partial class frmEditor : Form
    {
        midiReader mr = new midiReader();
        Dmx512UsbRs485Driver d = new Dmx512UsbRs485Driver();
        List<List<midiEvent>> midiTracks = new List<List<midiEvent>>();
        List<track> tracks = new List<track>();
        List<showEventButton> showEvents = new List<showEventButton>();
        Timer showtimer = new Timer();
        Timer testTimer = new Timer();
        Timer aniTimer = new Timer();
        ToolStripMenuItem aniTarget;
        int currentTime = 0;
        public int showTime = 20000;
        public int trackHeight = 50;
        int currentHeight = 0;
        public AddShowEvent frmEditShowEvent = new AddShowEvent();
        public double pixelsPerMiliSecond = 0;
        List<midiEvent> notesToPlay = new List<midiEvent>();
        int midiClock = 0;
        public frmEditor()
        {
            InitializeComponent();
            this.testTimer.Interval = 100;
            this.testTimer.Tick += testTimer_Tick;
            this.pTimeLine.AllowDrop = true;
            showtimer.Interval = 125;
            track.makeTypeMap();
            //rgbSpotLight s = new rgbSpotLight("COM3");
            //s.fade();
            //s.rgb(255, 0, 0);
            this.frmEditShowEvent.isEditForm = true;
            rgbSpotLight s = new rgbSpotLight();
            string t = s.GetType().AssemblyQualifiedName;
            Console.WriteLine(s.GetType().AssemblyQualifiedName);
            showtimer.Tick += showtimer_Tick;
            this.pixelsPerMiliSecond = 0.2;
            mr.init();
            this.frmEditShowEvent.Text = "Edit event";
        }

        void testTimer_Tick(object sender, EventArgs e)
        {

        }

        private void exportShow(string path)
        {
            XmlWriterSettings ws = new XmlWriterSettings();
            ws.Indent = true;
            XmlWriter w = XmlWriter.Create(path, ws);
            w.WriteStartDocument();
            w.WriteStartElement("LightShow");
            w.WriteStartElement("Tracks");
            foreach (track t in this.tracks)
            {
                w.WriteStartElement("Track");
                w.WriteAttributeString("Name", t.name);
                w.WriteAttributeString("Light", track.typeMap.FirstOrDefault(x => x.Value == t.LightName).Key);
                w.WriteAttributeString("yPos", t.yPos.ToString());
                w.WriteEndElement();

            }
            w.WriteEndDocument();
            w.Flush();
            w.Close();
            w.Dispose();
        }

        void showtimer_Tick(object sender, EventArgs e)
        {

            this.currentTime += 125;
            this.pTimeLine.Invalidate();
        }

        void t_Tick(object sender, EventArgs e)
        {
            foreach (midiEvent ev in this.midiTracks[1])
            {
                if (ev.timeFrame <= this.midiClock && ev.isCompleted == false)
                {
                    ev.isCompleted = true;
                }
                if (ev.timeFrame <= this.midiClock && ev.name == "Note On")
                {
                    this.notesToPlay.Add(ev);
                }
                if (ev.timeFrame <= this.midiClock && ev.name == "Note Off")
                {
                    this.notesToPlay.Add(ev);
                }
            }
            int notesPlayed = 0;
            if (this.notesToPlay.Count == 0)
            {
                d.DmxLoadBuffer(1, 0, 8);
                d.DmxLoadBuffer(2, 0, 8);
                d.DmxLoadBuffer(3, 0, 8);
            }
            foreach (midiEvent ev in this.notesToPlay)
            {
                if (ev.name == "Note Off")
                {
                    switch (notesPlayed)
                    {
                        case 0:
                            d.DmxLoadBuffer(1, 0, 8);
                            notesPlayed++;
                            break;
                        case 1:
                            d.DmxLoadBuffer(2, 0, 8);
                            notesPlayed++;
                            break;
                        case 2:
                            d.DmxLoadBuffer(3, 0, 8);
                            notesPlayed++;
                            break;
                        default:
                            notesPlayed = 0;
                            break;
                    }
                }
                else if (ev.name == "Note On")
                {
                    switch (notesPlayed)
                    {
                        case 0:
                            d.DmxLoadBuffer(1, ev.note, 8);
                            notesPlayed++;
                            break;
                        case 1:
                            d.DmxLoadBuffer(2, ev.note, 8);
                            notesPlayed++;
                            break;
                        case 2:
                            d.DmxLoadBuffer(3, ev.note, 8);
                            notesPlayed++;
                            break;
                        default:
                            notesPlayed = 0;
                            break;
                    }
                }

            }
            this.notesToPlay.Clear();
            d.DmxSendCommand(8);
        }

        private void loadMIDIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ofdMidi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                /*
                mr.loadFile(this.ofdMidi.FileName);
                mr.parseFile();
                mr.calculateTimeLine();
                this.midiTracks = mr.getSeparateTracks();
                this.showtimer.Interval = Convert.ToInt32(this.mr.interval * 1000);
                this.showtimer.Start();
                 * */
                midiPlayer p = new midiPlayer();
                midiPlayer.playMidi(ofdMidi.FileName, "derp");

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // recalculate panel width.
            if (this.tracks.Count > 0)
            {
                int max = 160 + Convert.ToInt32(this.tracks.Max(t => t.currentMaxTime) * this.pixelsPerMiliSecond + 5);
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
                foreach (KeyValuePair<int, showEvent> se in t.events)
                {
                    Rectangle r = new Rectangle(164 + (int)(se.Value.startTime * this.pixelsPerMiliSecond), t.yPos + 4, (int)(se.Value.duration * this.pixelsPerMiliSecond), 44);
                    e.Graphics.FillRectangle(new Pen(Color.Red).Brush, r);
                    se.Value.bounds = r;
                    e.Graphics.DrawRectangle(new Pen(Color.Black), r);
                    e.Graphics.DrawString(se.Value.function + "(" + se.Value.paraString + ")", btnPlay.Font, new Pen(Color.Black).Brush, 164 + (int)(se.Value.startTime * this.pixelsPerMiliSecond), t.yPos + 4);
                    e.Graphics.DrawString("S: "+ se.Value.startTime.ToString(), btnPlay.Font, new Pen(Color.Black).Brush, 164 + (int)(se.Value.startTime * this.pixelsPerMiliSecond), t.yPos + 19);
                    e.Graphics.DrawString("D: " + se.Value.duration.ToString(), btnPlay.Font, new Pen(Color.Black).Brush, 164 + (int)(se.Value.startTime * this.pixelsPerMiliSecond), t.yPos + 34);
                }
                //t.bAddEvent.Location = new Point(164 + (int)(t.currentMaxTime * this.pixelsPerMiliSecond), t.bAddEvent.Location.Y);
            }
            e.Graphics.FillRectangle(new Pen(Color.Black).Brush, 0, 0, this.pTimeLine.Width, 3);
            //e.Graphics.FillRectangle(new Pen(Color.Black).Brush, 0, this.pTimeLine.Height - 3, this.pTimeLine.Width, 3);
            if (160 + Convert.ToInt32(this.currentTime * (this.pixelsPerMiliSecond)) > this.pTimeLine.Size.Width)
            {
                this.currentTime = 0;
            }
            e.Graphics.FillRectangle(new Pen(Color.Black).Brush, 160 + Convert.ToInt32(this.currentTime * (this.pixelsPerMiliSecond)), 0, 3, pTimeLine.Height);
            e.Graphics.FillRectangle(new Pen(Color.Black).Brush, 160, 0, 3, pTimeLine.Height);

            if (this.tracks.Count > 0)
            {
                int currentHeight = 0;
                foreach (track t in this.tracks)
                {
                    if (currentHeight + this.trackHeight >= this.pTimeLine.Height)
                    {
                        this.pTimeLine.Height += this.trackHeight;
                    }
                    e.Graphics.FillRectangle(new Pen(Color.Black).Brush, 0, currentHeight + this.trackHeight, this.pTimeLine.Width, 3);
                    currentHeight += this.trackHeight;
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
                this.showtimer.Start();
                this.btnPlay.Text = "Pause";
            }
            else
            {
                this.showtimer.Stop();
                this.btnPlay.Text = "Play";
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.currentTime = 0;
            this.showtimer.Stop();
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
            this.tracks.Add(new track("Track " + (this.tracks.Count + 1).ToString(), this.tracks.Count * this.trackHeight, ((this.tracks.Count + 1) * this.trackHeight), this.pTimeLine));
            this.tracks[this.tracks.Count - 1].drawControls();
            this.pTimeLine.Invalidate();
        }

        private void pTimeLine_Click(object sender, EventArgs e)
        {
            foreach (track t in this.tracks)
            {
                int remove = -1;
                foreach (KeyValuePair<int, showEvent> se in t.events)
                {
                    if (se.Value.bounds.Contains(this.pTimeLine.PointToClient(Cursor.Position)))
                    {
                        this.frmEditShowEvent.light = t.light;
                        this.frmEditShowEvent.tbDuration.Text = se.Value.duration.ToString();
                        this.frmEditShowEvent.tbStartTime.Text = se.Value.startTime.ToString();
                        this.frmEditShowEvent.tbParameters.Text = se.Value.paraString;
                        this.frmEditShowEvent.cbFunctions.SelectedItem = se.Value.function;
                        int oldDuration = se.Value.duration;
                        DialogResult dr = this.frmEditShowEvent.ShowDialog();
                        if (dr == System.Windows.Forms.DialogResult.OK)
                        {
                            int duration = Convert.ToInt32(this.frmEditShowEvent.tbDuration.Text);
                            int start = Convert.ToInt32(this.frmEditShowEvent.tbStartTime.Text);
                            bool valid = true;
                            foreach (showEvent ev in t.events.Values)
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
                            se.Value.duration = this.frmEditShowEvent.duration;
                            se.Value.startTime = this.frmEditShowEvent.startTime;
                            se.Value.function = this.frmEditShowEvent.cbFunctions.Text;
                            se.Value.paraString = this.frmEditShowEvent.tbParameters.Text;
                            t.currentMaxTime = t.events.Values.Max(ev => ev.startTime + ev.duration);
                            pTimeLine.Invalidate();

                        }
                        else if (dr == System.Windows.Forms.DialogResult.Abort)
                        {
                            remove = se.Key;
                        }
                        this.frmEditShowEvent.reset();
                    }
                }
                if (remove != -1)
                {
                    t.events.Remove(remove);
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

    }
}
