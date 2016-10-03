using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmx512UsbRs485;

namespace midiLightShow
{
    public class dmxLight
    {
        public Dmx512UsbRs485Driver driver = new Dmx512UsbRs485Driver();
        public string comPort = "";
        public virtual bool executeFunction(string function, Dictionary<string, string> parameters, bool execute = false) { return true; }
    }
}
