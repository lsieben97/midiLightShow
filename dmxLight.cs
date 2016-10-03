using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dmx512UsbRs485;

namespace midiLightShow
{
    public class dmxLight
    {
        public Dmx512UsbRs485Driver driver = new Dmx512UsbRs485Driver();
        public string comPort = "";
        public bool live = false;
        public virtual bool executeFunction(string function, Dictionary<string, string> parameters, bool execute = false) { return true; }
        public void connectDMX()
        {
            if(this.live)
            {
                try
                {
                    this.driver.DmxToDefault(this.comPort);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("An error occured when connecting to this light, when not connected uncheck 'enable'.");
                }
            }
        }
    }
}
