using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace midiLightShow
{
    class midiPlayer
    {
        //play midifile
        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(String command, StringBuilder buffer, Int32 bufferSize, IntPtr hwndCallback);

        public static void playMidi(String fileName, String alias)
        {
            mciSendString("open " + fileName + " type sequencer alias " + alias, new StringBuilder(), 0, new IntPtr());
            mciSendString("play " + alias, new StringBuilder(), 0, new IntPtr());
        }

        public static void stopMidi(String alias)
        {
            mciSendString("stop " + alias, null, 0, new IntPtr());
            mciSendString("close " + alias, null, 0, new IntPtr());
        }

    }
}
