using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace midiLightShow
{
    public class showEventButton : Button
    {
        public bool dragging = false;
        public string name = "hoi";
        public showEventButton(string name)
        {
            this.name = name;
            this.Width = 50;
            this.Height = 50;
            this.Click += showEventButton_Click;
        }

        public void showEventButton_Click(object sender, EventArgs e)
        {
            // dummy method for click animation
        }
    }
}
