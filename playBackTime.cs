using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiLightShow
{
    public class playBackTime
    {
        public int sixteenthBeats = 0;
        public int beats = 0;
        public int measures = 0;

        public void increment()
        {
            sixteenthBeats++;
            if(this.sixteenthBeats == 17)
            {
                this.sixteenthBeats = 0;
                this.beats++;
            }
            if(this.beats == 5)
            {
                this.beats = 0;
                this.measures++;
            }
        }
        public string getTimeString()
        {
            return this.measures.ToString().PadLeft(2, '0') + ":" + this.beats.ToString().PadLeft(2, '0') + ":" + this.sixteenthBeats.ToString().PadLeft(2, '0');
        }
        public void reset()
        {
            this.sixteenthBeats = 0;
            this.measures = 0;
            this.beats = 0;
        }
    }
}
