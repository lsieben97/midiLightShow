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
    public partial class splash : Form
    {
        private Timer formTimer = new Timer();
        public splash()
        {
            InitializeComponent();
            this.formTimer.Interval = 3000;
            this.formTimer.Tick += formTimer_Tick;
        }

        void formTimer_Tick(object sender, EventArgs e)
        {
            this.formTimer.Stop();
            this.Hide();
            frmEditor ed = new frmEditor();
            ed.Show();
        }

        private void splash_Load(object sender, EventArgs e)
        {
            this.formTimer.Start();
        }
    }
}
