using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

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
        public int currentMaxTime = 0;
        public int eventCount = 1;
        public string LightName = "";
        private Panel pTimeLine;
        public Label lbName = new Label();
        public CheckBox cbMute = new CheckBox();
        public CheckBox cbSolo = new CheckBox();
        public Button bOptions = new Button();
        public Button bAddEvent = new Button();
        public dmxLight light = new dmxLight();
        public TrackOptionsForm frmOptions = new TrackOptionsForm();
        public AddShowEvent frmAddShowEvent = new AddShowEvent();
        public int lastBlockXPos = 110;
        public Dictionary<int, showEvent> events = new Dictionary<int, showEvent>();

        public static Dictionary<string, string> typeMap = new Dictionary<string, string>();

        public static void makeTypeMap()
        {
            track.typeMap.Add("RGB Spotlight", "midiLightShow.rgbSpotLight, midiLightShow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            track.typeMap.Add("360 Spotlight", "midiLightShow._360SpotLight, midiLightShow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            track.typeMap.Add("Lazer", "midiLightShow.lazer, midiLightShow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            track.typeMap.Add("Disc Light", "midiLightShow.discLight, midiLightShow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
        }
        public track(string name, int yPos, int yEnd, Panel p)
        {
            this.name = name;
            this.yPos = yPos;
            this.yEnd = yEnd;
            this.pTimeLine = p;
            this.frmOptions.tbName.Text = this.name;
            this.frmOptions.Text = "Options for '" + this.name + "'";
            this.frmOptions.cbLights.SelectedIndex = 0;
            this.LightName = this.frmOptions.cbLights.SelectedItem.ToString();
            Type targetType = Type.GetType(track.typeMap[this.frmOptions.cbLights.SelectedItem.ToString()],true);
            this.light = Activator.CreateInstance(targetType) as dmxLight;
            this.frmAddShowEvent.light = this.light;
        }

        public void drawControls()
        {
            this.lbName.Text = this.name;
            this.lbName.Location = new Point(4, this.yPos + 3);
            this.lbName.Bounds = new Rectangle(lbName.Location, new Size(this.lbName.Text.Length * 7, 18));
            this.pTimeLine.Controls.Add(lbName);

            this.cbMute.Text = "Mute";
            this.cbMute.Location = new Point(4, this.yPos + 30);
            this.cbMute.Bounds = new Rectangle(cbMute.Location, new Size(50, 20));
            this.cbMute.CheckedChanged += cbMute_CheckedChanged;
            this.pTimeLine.Controls.Add(cbMute);

            this.cbSolo.Text = "Solo";
            this.cbSolo.Location = new Point(60, this.yPos + 30);
            this.cbSolo.Bounds = new Rectangle(cbSolo.Location, new Size(50, 20));
            this.cbSolo.CheckedChanged += cbSolo_CheckedChanged;
            this.pTimeLine.Controls.Add(cbSolo);

            this.bOptions.Text = "O";
            this.bOptions.Location = new Point(90, this.yPos + 3);
            this.bOptions.BackColor = SystemColors.Control;
            this.bOptions.Size = new Size(20, 20);
            //this.bOptions.Bounds = new Rectangle(bOptions.Location, new Size(20, 50));
            this.bOptions.Click += bOptions_Click;
            this.pTimeLine.Controls.Add(bOptions);

            this.bAddEvent.Text = "Add event";
            this.bAddEvent.Location = new Point(110, this.yPos + 3);
            this.bAddEvent.Size = new Size(50, 47);
            this.bAddEvent.BackColor = SystemColors.Control;
            this.bAddEvent.Bounds = new Rectangle(this.bAddEvent.Location, new Size(50, 47));
            this.bAddEvent.Click += bAddEvent_Click;
            this.pTimeLine.Controls.Add(bAddEvent);
        }

        void bAddEvent_Click(object sender, EventArgs e)
        {
            this.frmAddShowEvent.light = this.light;
            DialogResult dr = this.frmAddShowEvent.ShowDialog();
            if(dr == DialogResult.OK)
            {
                int duration = Convert.ToInt32(this.frmAddShowEvent.tbDuration.Text);
                int start = Convert.ToInt32(this.frmAddShowEvent.tbStartTime.Text);
                bool valid = true;
                foreach(showEvent ev in this.events.Values)
                {
                    if(start > ev.startTime && start < ev.startTime + ev.duration)
                    {
                        valid = false;
                    }
                    if(start + duration > ev.startTime && start + duration < ev.startTime + ev.duration)
                    {
                        valid = false;
                    }
                }
                if(!valid)
                {
                    MessageBox.Show("event cannot overlap!");
                    return;
                }

                this.events.Add(this.eventCount, new showEvent(Convert.ToInt32(this.frmAddShowEvent.tbStartTime.Text), this.frmAddShowEvent.duration, this.frmAddShowEvent.cbFunctions.Text, this.frmAddShowEvent.parameters.ToArray(), this.eventCount));
                this.currentMaxTime += this.frmAddShowEvent.duration;
                this.frmAddShowEvent.reset();
                this.eventCount++;
                this.pTimeLine.Invalidate();
            }
        }

        void bOptions_Click(object sender, EventArgs e)
        {
            if(this.frmOptions.ShowDialog() == DialogResult.OK)
            {
                this.name = frmOptions.tbName.Text;
                this.lbName.Text = this.name;
                this.LightName = this.frmOptions.cbLights.SelectedItem.ToString();
                this.light = Activator.CreateInstance(Type.GetType(track.typeMap[this.frmOptions.cbLights.SelectedItem.ToString()])) as dmxLight;
                this.frmAddShowEvent.light = this.light;
                this.frmOptions.Text = "Options for '" + this.name + "'";
            }
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
    }
}
