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
        public object[] parameters;
        public Type[] parameterTypes;
        public int index = 0;

        public showEvent(int startTime, int duration, string function, object[] parameters, Type[] parameterTypes,int index)
        {
            this.startTime = startTime;
            this.duration = duration;
            this.function = function;
            this.parameters = parameters;
            this.parameterTypes = parameterTypes;
            this.index = index;
        }
    }
}
