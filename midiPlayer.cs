using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace midiLightShow
{
    /// <summary>
    /// Class for playing a MIDI file
    /// </summary>
    static class midiPlayer
    {
        #region Static methods
        /// <summary>
        /// Sends a command to the Windows Media library
        /// </summary>
        /// <param name="command">The command to send</param>
        /// <param name="buffer">Buffer for return information</param>
        /// <param name="bufferSize">Size of the buffer</param>
        /// <param name="hwndCallback">Callback handle</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(String command, StringBuilder buffer, Int32 bufferSize, IntPtr hwndCallback);

        /// <summary>
        /// Play a MIDI file
        /// </summary>
        /// <param name="fileName">The path to the file to play</param>
        /// <param name="alias">Alias for the MIDI file. Used to stop the playback</param>
        public static void playMidi(String fileName, String alias)
        {
            mciSendString("open " + fileName + " type sequencer alias " + alias, new StringBuilder(), 0, new IntPtr());
            mciSendString("play " + alias, new StringBuilder(), 0, new IntPtr());
        }
        /// <summary>
        /// Stop playing a MIDI file
        /// </summary>
        /// <param name="alias">The alias of the MIDI file to stop</param>
        public static void stopMidi(String alias)
        {
            mciSendString("stop " + alias, null, 0, new IntPtr());
            mciSendString("close " + alias, null, 0, new IntPtr());
        }
        #endregion

    }
}
