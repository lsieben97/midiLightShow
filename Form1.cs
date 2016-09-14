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
    public partial class Form1 : Form
    {
        midiReader mr = new midiReader();
        Dmx512UsbRs485Driver d = new Dmx512UsbRs485Driver();
        List<List<midiEvent>> midiTracks = new List<List<midiEvent>>();
        Timer showtimer = new Timer();
        List<midiEvent> notesToPlay = new List<midiEvent>();
        int midiClock = 0;
        public Form1()
        {
            InitializeComponent();
            //Console.WriteLine(d.PortNameAt(1));
            showtimer.Interval = 100;
            d.DmxToDefault("COM3");
            showtimer.Tick += t_Tick;
            //d.DmxLoadBuffer(1, 255, 1);
            //d.DmxSendCommand(1);
            mr.init();
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
                mr.loadFile(this.ofdMidi.FileName);
                mr.parseFile();
                mr.calculateTimeLine();
                this.midiTracks = mr.getSeparateTracks();
                this.showtimer.Interval = Convert.ToInt32(this.mr.interval * 1000);
                this.showtimer.Start();

            }
        }
    }
}
