﻿using System;
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
    /// <summary>
    /// Midi inmport dialog for showing progress on reading the midi file
    /// </summary>
    public partial class midiImport : Form
    {
        #region Global variables
        /// <summary>
        /// The midiReader object used to read the specified MIDI file
        /// </summary>
        public midiReader mr = new midiReader();
        /// <summary>
        /// Timer to delay parsing of midi file so the form can fully appear
        /// </summary>
        private System.Windows.Forms.Timer loadTimer = new System.Windows.Forms.Timer();
        /// <summary>
        /// Timer to delay closing of the dialog so the user can see the result of the parsing
        /// </summary>
        private System.Windows.Forms.Timer finishTimer = new System.Windows.Forms.Timer();
        /// <summary>
        /// The path to the MIDI file to parse
        /// </summary>
        private string path;
        public bool success = false;
        #endregion
        #region Constructors
        /// <summary>
        /// Create a new midiImport dialog with the specified path
        /// </summary>
        /// <param name="path">The path to the MIDI file to read</param>
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
        #endregion
        #region midiImport methods
        /// <summary>
        /// Tick event handler for the finish timer
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        void finishTimer_Tick(object sender, EventArgs e)
        {
            this.finishTimer.Stop();
            this.Close();
        }
        /// <summary>
        /// Tick event handler for the load timer 
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        void loadTimer_Tick(object sender, EventArgs e)
        {
            import();
            
        }
        /// <summary>
        /// import an MIDI file
        /// </summary>
        private void import()
        {
            try
            {
                this.loadTimer.Stop();
                lbStatus.Text = "Done\n";
                lbStatus.Text = "Loading MIDI file '" + path + "'...";
                mr.loadFile(path);
                this.pbProgress.Maximum = mr.data.Count;
                lbStatus.Text = "Done\n";
                lbStatus.Text = "Parsing file...\n";
                mr.parseFile();
                lbStatus.Text = "Done\n";
                lbStatus.Text = "Calculating timeline...\n";
                mr.calculateTimeLine();
                lbStatus.Text = "Done\n";
                lbStatus.Text = "MIDI import complete!";
                this.success = true;
                this.finishTimer.Start();
            }
            catch(Exception e)
            {
                MessageBox.Show("This MIDI file is not supported by DMX studio!");
                this.DialogResult = System.Windows.Forms.DialogResult.No;
                this.Close();
            }
        }

        /// <summary>
        /// Load event handler for this dialog
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void midiImport_Load(object sender, EventArgs e)
        {
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomProfessionalColors());
            tbTitle.Text = "Importing " + this.path;
            lbStatus.Text = "Initializing MIDI reader...";
            this.mr.init();
            this.loadTimer.Start();
        }
        #endregion
    }
}
