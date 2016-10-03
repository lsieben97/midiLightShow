using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Xml.Serialization;

namespace midiLightShow
{
    [XmlInclude(typeof(rgbSpotLight))]
    [Serializable]
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
        public string LightName = "RGB Spotlight";
        [XmlIgnore]
        public Panel pTimeLine;
        [XmlIgnore]
        public Label lbName = new Label();
        [XmlIgnore]
        public CheckBox cbMute = new CheckBox();
        [XmlIgnore]
        public CheckBox cbSolo = new CheckBox();
        [XmlIgnore]
        public Button bOptions = new Button();
        [XmlIgnore]
        public Button bAddEvent = new Button();
        public dmxLight light = new dmxLight();
        [XmlIgnore]
        public TrackOptionsForm frmOptions = new TrackOptionsForm();
        [XmlIgnore]
        public AddShowEvent frmAddShowEvent = new AddShowEvent();
        public int lastBlockXPos = 110;
        public bool delete = false;

        public List<showEvent> events = new List<showEvent>();
        [XmlIgnore]
        public static Dictionary<string, string> typeMap = new Dictionary<string, string>();

        public static void makeTypeMap()
        {
            track.typeMap.Add("RGB Spotlight", "midiLightShow.rgbSpotLight, midiLightShow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            track.typeMap.Add("360 Spotlight", "midiLightShow._360SpotLight, midiLightShow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            track.typeMap.Add("Lazer", "midiLightShow.lazer, midiLightShow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            track.typeMap.Add("Disc Light", "midiLightShow.discLight, midiLightShow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            Console.WriteLine("Generated TypeMap.");
            
        }

        public track() { }
        public track(string name, int yPos, int yEnd, Panel p)
        {
            this.name = name;
            this.yPos = yPos;
            this.yEnd = yEnd;
            this.pTimeLine = p;
            this.frmOptions.tbName.Text = this.name;
            this.frmOptions.Text = "Options for '" + this.name + "'";
            this.frmOptions.cbLights.Text = this.LightName;
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

            Console.WriteLine("Generated controls for track '" + this.name + "'.");
        }

        public void repositionControls()
        {
            this.lbName.Location = new Point(4, this.yPos + 3);
            this.cbMute.Location = new Point(4, this.yPos + 30);
            this.cbSolo.Location = new Point(60, this.yPos + 30);
            this.bOptions.Location = new Point(90, this.yPos + 3);
            this.bAddEvent.Location = new Point(110, this.yPos + 3);
        }

        void bAddEvent_Click(object sender, EventArgs e)
        {
            this.frmAddShowEvent.reset();
            this.frmAddShowEvent.light = this.light;
            DialogResult dr = this.frmAddShowEvent.ShowDialog();
            if(dr == DialogResult.OK)
            {
                Console.WriteLine("Trying to create new event...");
                int duration = Convert.ToInt32(this.frmAddShowEvent.tbDuration.Text);
                int start = Convert.ToInt32(this.frmAddShowEvent.tbStartTime.Text);
                bool valid = true;
                foreach(showEvent ev in this.events)
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
                    Console.WriteLine("Failed! (Invalid event timing for new event).");
                    return;
                }

                this.events.Add(new showEvent(Convert.ToInt32(this.frmAddShowEvent.tbStartTime.Text), this.frmAddShowEvent.duration, this.frmAddShowEvent.cbFunctions.Text, this.frmAddShowEvent.parameters, this.frmAddShowEvent.paraString, this.eventCount));
                this.currentMaxTime += this.frmAddShowEvent.duration;
                this.debugNewEvent(this.events[this.eventCount -1]);
                this.eventCount++;
                this.pTimeLine.Invalidate();
            }
        }
        private void debugNewEvent(showEvent sEvent)
        {
            Console.WriteLine("Complete, created event with the following data:");
            FieldInfo[] fields = sEvent.GetType().GetFields();
            foreach(FieldInfo field in fields)
            {
                Console.WriteLine(string.Format("{0}: {1}", field.Name, field.GetValue(sEvent).ToString()));
            }
        }
        void bOptions_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.frmOptions.ShowDialog();
            if(dr == DialogResult.OK)
            {
                this.name = frmOptions.tbName.Text;
                this.lbName.Text = this.name;
                this.LightName = this.frmOptions.cbLights.SelectedItem.ToString();
                this.light = Activator.CreateInstance(Type.GetType(track.typeMap[this.frmOptions.cbLights.SelectedItem.ToString()])) as dmxLight;
                this.frmAddShowEvent.light = this.light;
                this.frmOptions.Text = "Options for '" + this.name + "'";
            }
            else if(dr == DialogResult.Abort)
            {
                this.delete = true;
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
           Console.WriteLine("Toggled mute option for track '" + this.name + "'.");
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
        public void removeControls()
        {
            this.pTimeLine.Controls.Remove(this.lbName);
            this.pTimeLine.Controls.Remove(this.bAddEvent);
            this.pTimeLine.Controls.Remove(this.bOptions);
            this.pTimeLine.Controls.Remove(this.cbMute);
            this.pTimeLine.Controls.Remove(this.cbSolo);
        }

        public void afterImport()
        {
            this.frmOptions.tbName.Text = this.name;
            this.frmOptions.Text = "Options for '" + this.name + "'";
            this.frmOptions.cbLights.Text = this.LightName;
        }
    }
}
