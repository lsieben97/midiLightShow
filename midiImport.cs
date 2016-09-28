using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace midiLightShow
{
    public partial class midiImport : Form
    {
        midiReader mr = new midiReader();
        System.Windows.Forms.Timer loadTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer finishTimer = new System.Windows.Forms.Timer();
        string path;
        public midiImport(string path)
        {
            InitializeComponent();
            loadTimer.Interval = 500;
            finishTimer.Interval = 1000;
            finishTimer.Tick += finishTimer_Tick;
            loadTimer.Tick += loadTimer_Tick;
            this.mr.form = this;
            this.path = path;           
        }

        void finishTimer_Tick(object sender, EventArgs e)
        {
            this.finishTimer.Stop();
            this.Close();
        }

        void loadTimer_Tick(object sender, EventArgs e)
        {
            this.loadTimer.Stop();
            rtbStatus.AppendText("Done\n");
            rtbStatus.AppendText("Loading MIDI file '" + path + "'...");
            mr.loadFile(path);
            rtbStatus.AppendText("Done\n");
            rtbStatus.AppendText("Parsing file...\n");
            mr.parseFile();
            rtbStatus.AppendText("MIDI import complete!");
            this.finishTimer.Start();
            
        }

        private void midiImport_Load(object sender, EventArgs e)
        {
            rtbStatus.AppendText("Initializing MIDI reader...");
            this.mr.init();
            this.loadTimer.Start();
        }
    }
}
