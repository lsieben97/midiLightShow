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
        Timer t = new Timer();
        public Form1()
        {
            InitializeComponent();
            //Console.WriteLine(d.PortNameAt(1));
            t.Interval = 2000;
            d.DmxToDefault("COM3");
            t.Tick += t_Tick;
            t.Start();
            //d.DmxTestForPar56();
            //d.DmxLoadBuffer(1, 255, 1);
            //d.DmxSendCommand(1);
            mr.init();
        }

        void t_Tick(object sender, EventArgs e)
        {
            //d.DmxLoadBuffer(5, 255, 8);
            d.DmxLoadBuffer(6, 32, 8);
            d.DmxLoadBuffer(1, 255, 8);
            d.DmxLoadBuffer(3, 0, 8);
            d.DmxSendCommand(7);
            System.Threading.Thread.Sleep(500);
            d.DmxLoadBuffer(2, 255, 8);
            d.DmxLoadBuffer(1, 0, 8);
            d.DmxSendCommand(7);
            System.Threading.Thread.Sleep(500);
            d.DmxLoadBuffer(3, 255, 8);
            d.DmxLoadBuffer(2, 0, 8);
            d.DmxSendCommand(7);
            System.Threading.Thread.Sleep(500);
            

        }

        private void loadMIDIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.ofdMidi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mr.loadFile(this.ofdMidi.FileName);
                mr.parseFile();
                mr.calculateTimeLine();
                this.midiTracks = mr.getSeparateTracks();
            }
        }
    }
}
