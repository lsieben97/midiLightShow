using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace midiLightShow
{
    public class showEvent
    {
        public int startTime;
        public int duration;
        public Rectangle bounds;
        public string function;
        public string paraString;
        public Dictionary<string,string> parameters;
        public int index = 0;
        public bool canPlay = true;

        public showEvent() { }
        public showEvent(int startTime, int duration, string function, Dictionary<string,string> parameters, string paraString, int index)
        {
            this.startTime = startTime;
            this.duration = duration;
            this.function = function;
            this.paraString = paraString;
            this.parameters = parameters;
            this.index = index;
        }
    }
}
