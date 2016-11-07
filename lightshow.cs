using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiLightShow
{
    public class lightshow
    {
        public string name;
        public List<track> tracks = new List<track>();
        public int showTime = 16;
        public int bpm = 120;
        public bool containsAudio = false;
        public string audioPath;
    }
}
