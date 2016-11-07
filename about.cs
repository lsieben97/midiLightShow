using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace midiLightShow
{
    public partial class about : Form
    {
        public Timer startTimer = new Timer();
        public Timer creditsTimer = new Timer();
        public int targetWindowY = 0;
        public about()
        {
            InitializeComponent();
            startTimer.Interval = 1;
            startTimer.Tick += startTimer_Tick;
            creditsTimer.Interval = 30;
            creditsTimer.Tick += creditsTimer_Tick;
        }

        void creditsTimer_Tick(object sender, EventArgs e)
        {
            if(this.rtbCredits.Location.Y > -500)
            {
                int newY = this.rtbCredits.Location.Y - 1;
                this.rtbCredits.Location = new Point(this.rtbCredits.Location.X, newY);
            }
            else
            {
                this.rtbCredits.Location = new Point(this.rtbCredits.Location.X, 420);
            }
        }

        void startTimer_Tick(object sender, EventArgs e)
        {
            if (this.Location.Y < this.targetWindowY)
            {
                int newY = this.Location.Y + 6;
                this.Location = new Point(this.Location.X, newY);
            }
            else
            {
                this.startTimer.Stop();
            }
        }

        private void about_Load(object sender, EventArgs e)
        {
            this.menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomProfessionalColors());
            this.targetWindowY = this.Location.Y;
            this.Location = new Point(this.Location.X, 0 - this.Size.Height);
            this.startTimer.Start();
            this.creditsTimer.Start();
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
