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
        public string parameters;
        public int index = 0;

        public showEvent(int startTime, int duration, string function, string parameters,int index)
        {
            this.startTime = startTime;
            this.duration = duration;
            this.function = function;
            this.parameters = parameters;
            this.index = index;
        }
    }
}
