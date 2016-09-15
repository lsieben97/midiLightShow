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

namespace midiLightShow
{
    public partial class frmEditor : Form
    {
        midiReader mr = new midiReader();
        Dmx512UsbRs485Driver d = new Dmx512UsbRs485Driver();
        List<List<midiEvent>> midiTracks = new List<List<midiEvent>>();
        List<track> tracks = new List<track>();
        Timer showtimer = new Timer();
        int currentTime = 0;
        public int showTime = 20;
        public int trackHeight = 50;
        private showEventButton locIndicator = new showEventButton();
        public double pixelsPerMiliSecond = 0;
        List<midiEvent> notesToPlay = new List<midiEvent>();
        int midiClock = 0;
        public frmEditor()
        {
            InitializeComponent();
            showEventButton seb = new showEventButton();
            seb.Location = new Point(5, 5);
            seb.Size = new System.Drawing.Size(100, 100);
            seb.AllowDrop = true;
            this.AllowDrop = true;
            this.pTimeLine.AllowDrop = true;
            this.gbEventTray.AllowDrop = true;
            this.panel1.AllowDrop = true;
            gbEventTray.Controls.Add(seb);
            this.locIndicator.Size = new System.Drawing.Size(50, 50);
            this.locIndicator.Visible = false;
            //Console.WriteLine(d.PortNameAt(1));
            showtimer.Interval = 125;
            //d.DmxToDefault("COM3");
            showtimer.Tick += showtimer_Tick;
            //d.DmxLoadBuffer(1, 255, 1);
            //d.DmxSendCommand(1);
            mr.init();
        }

        void showtimer_Tick(object sender, EventArgs e)
        {

            this.currentTime++;
            this.pixelsPerMiliSecond = (double) (this.pTimeLine.Width - 110) / this.showTime;
            this.pTimeLine.Invalidate();
        }

        void t_Tick(object sender, EventArgs e)
        {
            foreach(midiEvent ev in this.midiTracks[1])
            {
                if(ev.timeFrame <= this.midiClock && ev.isCompleted == false)
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
            if(this.notesToPlay.Count == 0)
            {
                d.DmxLoadBuffer(1, 0, 8);
                d.DmxLoadBuffer(2, 0, 8);
                d.DmxLoadBuffer(3, 0, 8);
            }
            foreach(midiEvent ev in this.notesToPlay)
            {
                if(ev.name == "Note Off")
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
                else if(ev.name == "Note On")
                {
                    switch(notesPlayed)
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
            if(this.ofdMidi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
            e.Graphics.FillRectangle(new Pen(Color.Black).Brush,0, 0, this.pTimeLine.Width, 3);
            //e.Graphics.FillRectangle(new Pen(Color.Black).Brush, 0, this.pTimeLine.Height - 3, this.pTimeLine.Width, 3);
            if(this.currentTime == this.showTime * 8)
            {
                this.currentTime = 0;
            }
            e.Graphics.FillRectangle(new Pen(Color.Black).Brush, 110 + Convert.ToInt32(this.currentTime * (this.pixelsPerMiliSecond / 8)), 0, 3, pTimeLine.Height);

            if(this.tracks.Count > 0)
            {
                int currentHeight = 0;
                foreach(track t in this.tracks)
                {
                    if(currentHeight + this.trackHeight >= this.pTimeLine.Height)
                    {
                        this.pTimeLine.Height += this.trackHeight;
                    }
                    e.Graphics.FillRectangle(new Pen(Color.Black).Brush,0, currentHeight + this.trackHeight, this.pTimeLine.Width, 3);
                    currentHeight += this.trackHeight;
                }
                if(this.tracks.Count(t => t.solo == true) == 1)
                {
                    int index = this.tracks.FindIndex(t => t.solo == true);
                    for(int i = 0; i < this.tracks.Count; i++)
                    {
                        if(i != index)
                        {
                            this.tracks[i].cbMute.Checked = true;
                        }
                    }
                }
                else if (this.tracks.Count(t => t.soloSwitch == true) == 1)
                {
                    foreach(track t in this.tracks)
                    {
                        t.cbMute.Checked = false;
                        t.soloSwitch = false;
                    }
                }
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if(this.btnPlay.Text == "Play")
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
            this.tracks.Add(new track("Track " + (this.tracks.Count + 1).ToString(), this.tracks.Count * this.trackHeight, ((this.tracks.Count + 1) * this.trackHeight), this.pTimeLine));
            this.tracks[this.tracks.Count - 1].drawControls();
            this.pTimeLine.Invalidate();
        }

        private void pTimeLine_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            Point pos = this.pTimeLine.PointToClient(new Point(e.X, e.Y));
            foreach(track t in this.tracks)
            {
                if(pos.Y > t.yPos && pos.Y < t.yEnd)
                {
                    Graphics g = this.pTimeLine.CreateGraphics();
                    g.DrawRectangle(new Pen(Color.Red), 120, t.yPos + 3, 50, 46);
                }
                else
                {
                    Graphics g = this.pTimeLine.CreateGraphics();
                    g.DrawRectangle(new Pen(this.pTimeLine.BackColor), 120, t.yPos + 3, 50, 46);
                }
            }
        }

        private void pTimeLine_DragDrop(object sender, DragEventArgs e)
        {
            this.pTimeLine.Invalidate();
        }

    }
}
