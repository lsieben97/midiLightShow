using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiLightShow
{
    public class dmxLight
    {
        public string comPort = "";
        public virtual bool executeFunction(string function, Dictionary<string, string> parameters, bool execute = false) { return true; }
    }
}
