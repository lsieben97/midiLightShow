using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace midiLightShow
{
    public class track
    {
        public string name = "";
        public bool solo = false;
        public bool soloSwitch = false;
        public bool mute = false;
        public int yPos = 0;
        public int yEnd = 0;
        private Panel pTimeLine;
        public Label lbName = new Label();
        public CheckBox cbMute = new CheckBox();
        public CheckBox cbSolo = new CheckBox();
        public Dictionary<int, showEvent> events = new Dictionary<int, showEvent>();

        public track(string name, int yPos, int yEnd, Panel p)
        {
            this.name = name;
            this.yPos = yPos;
            this.yEnd = yEnd;
            this.pTimeLine = p;
        }

        public void drawControls()
        {
            this.lbName.Text = this.name;
            this.lbName.Location = new Point(4, this.yPos + 3);
            this.lbName.Bounds = new Rectangle(lbName.Location, new Size(100, 18));
            this.pTimeLine.Controls.Add(lbName);

            this.cbMute.Text = "Mute";
            this.cbMute.Location = new Point(4, this.yPos + 22);
            this.cbMute.Bounds = new Rectangle(cbMute.Location, new Size(50, 20));
            this.cbMute.CheckedChanged += cbMute_CheckedChanged;
            this.pTimeLine.Controls.Add(cbMute);

            this.cbSolo.Text = "Solo";
            this.cbSolo.Location = new Point(60, this.yPos + 22);
            this.cbSolo.Bounds = new Rectangle(cbSolo.Location, new Size(50, 20));
            this.cbSolo.CheckedChanged += cbSolo_CheckedChanged;
            this.pTimeLine.Controls.Add(cbSolo);
        }

        void cbMute_CheckedChanged(object sender, EventArgs e)
        {
           if(!this.cbMute.Checked)
           {
               this.mute = false;
           }
           else
           {
               this.mute = true;
           }
        }

        void cbSolo_CheckedChanged(object sender, EventArgs e)
        {
            if(!this.cbSolo.Checked)
            {
                this.solo = false;
                this.soloSwitch = true;
                this.pTimeLine.Invalidate();
            }
            else
            {
                this.solo = true;
                this.pTimeLine.Invalidate();
            }
        }
        public void drawEvents(Graphics g)
        {

        }
    }
}
