using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace midiLightShow
{
    #region MIDI reader
    /// <summary>
    /// Class for reading MIDI files
    /// </summary>
    public class midiReader
    {
        #region Global variables
        /// <summary>
        /// List with the bytes of the MIDI file
        /// </summary>
        public List<byte> data = new List<byte>();
        /// <summary>
        /// List of midiEvents objects representing a time line
        /// </summary>
        public List<midiEvent> timeLine = new List<midiEvent>();
        /// <summary>
        /// List of all MIDI event information to test against
        /// </summary>
        public Dictionary<string,midiEvent> events = new Dictionary<string,midiEvent>();
        /// <summary>
        /// Amount of bytes allready read
        /// </summary>
        public int totalLengthRead = 0;
        /// <summary>
        /// Total number of bytes in the MIDI file
        /// </summary>
        public int byteCount = 0;
        /// <summary>
        /// MIDI Devision (see official MIDI specification)
        /// </summary>
        public int division = 0;
        /// <summary>
        /// nember of miliseconds per MIDI tick
        /// </summary>
        public double interval = 0;
        /// <summary>
        /// midiImport form object to write information to
        /// </summary>
        public midiImport form;
        /// <summary>
        /// Indexes of the different MIDI tracks on the timeline
        /// </summary>
        public List<int> trackIndexes = new List<int>();

        public int trackChunkCount = 0;
        #endregion
        #region Main methods
        /// <summary>
        /// Initialize the events dictionary
        /// </summary>
        public void init()
        {
            // Meta events
            this.events.Add("255127", new midiEvent("Sequencer-Specific Meta-event", "255127"));
            this.events.Add("255001", new midiEvent("Text Event", "2551"));
            this.events.Add("255088", new midiEvent("Time Signature", "25588", true,4));
            this.events.Add("255081", new midiEvent("Set Tempo", "255813",true, 4));
            this.events.Add("255047", new midiEvent("End of Track", "255470", true, 1));
            this.events.Add("255003", new midiEvent("Track Name", "2553"));
            this.events.Add("255032", new midiEvent("MIDI Channel Prefix", "255321",true,1));
            this.events.Add("255000", new midiEvent("Sequence Number", "25502",true,3));
            this.events.Add("255002", new midiEvent("Copyright Notice", "2552"));
            this.events.Add("255004", new midiEvent("Instrument Name", "2554"));
            this.events.Add("255005", new midiEvent("Lyric", "2555"));
            this.events.Add("255006", new midiEvent("Marker", "2556"));
            this.events.Add("255007", new midiEvent("Cue Point", "2557"));
            this.events.Add("255033", new midiEvent("MIDI Port", "255331",true,1));
            this.events.Add("255089", new midiEvent("Key Signature", "255892",true,3));
            this.events.Add("255084", new midiEvent("SMPTE Offset", "25575",true,6));

            // channel events
            this.events.Add("1100", new midiEvent("Program Change", "1100", true, 1));
            this.events.Add("1001", new midiEvent("Note On", "1001", true, 2));
            this.events.Add("1000", new midiEvent("Note Off", "1000", true, 2));
            this.events.Add("1010", new midiEvent("Polyphonic Key Pressure", "1010", true, 2));
            this.events.Add("1011", new midiEvent("Control Change", "1011", true, 2));
            this.events.Add("1101", new midiEvent("Channel Pressure", "1101", true, 1));
            this.events.Add("1110", new midiEvent("Pitch Wheel Change", "1110", true, 2));
        }
        /// <summary>
        /// Loads a MIDI file
        /// </summary>
        /// <param name="path">Path to the MIDI file to read</param>
        public void loadFile(string path)
        {
            if(File.Exists(path))
            {
                this.data = File.ReadAllBytes(path).ToList();
                this.byteCount = this.data.Count;
            }
        }

        /// <summary>
        /// Parses the loaded file and saves the results in the global timeline variable
        /// </summary>
        public void parseFile()
        {
            while(this.totalLengthRead != this.byteCount)
            {
                string currentChunkType = this.getChunkType();
                int currentLength = 0;
                this.getChunkGroup(4,out currentLength);
                if(currentChunkType == "HEAD")
                {
                    int fileFormat = 0;
                    this.getChunkGroup(2, out fileFormat);
                    this.getChunkGroup(2, out this.trackChunkCount);
                    this.division = this.calculateDivision(this.getChunkGroup(2));
                    //this.form.rtbStatus.AppendText(string.Format("Found MIDI header chunk with following data:\n\tFile format: {0}\n\tDivision: {1}\n\tTrack chunk count: {2}", fileFormat.ToString(), this.division.ToString(), trackChunkCount.ToString()));
                }
                else
                {
                    bool endOfTrack = false;
                    while(!endOfTrack)
                    {
                        int delta = this.getVariableLengthQuantity();
                        if(this.data[this.totalLengthRead] == 255)
                        {
                            // found meta event
                            midiEvent e = this.getMetaEvent();
                            e.deltaTime = delta;
                            //this.form.rtbStatus.AppendText("Found " + e.name + "\n");
                            this.form.pbProgress.Value = this.totalLengthRead;
                            this.form.lbPercent.Text = Convert.ToInt32(this.totalLengthRead / this.data.Count * 100).ToString() + "%";
                            //Thread.Sleep(10);
                            
                            this.timeLine.Add(e);
                            if(e.name == "")
                            {

                            }
                            if(e.name == "End of Track")
                            {
                                this.trackIndexes.Add(this.timeLine.Count - 1);
                                endOfTrack = true;
                            }
                            else if(e.name != "Set Tempo")
                            {
                                this.totalLengthRead++;
                            }
                            
                        }
                        else
                        {
                            midiEvent e = this.getChannelEvent();
                            e.deltaTime = delta;
                            this.form.pbProgress.Value = this.totalLengthRead;
                            this.form.lbPercent.Text = Convert.ToInt32(this.totalLengthRead / this.data.Count * 100).ToString() + "%";
                            //Thread.Sleep(10);
                            this.timeLine.Add(e);
                        }
                    }
                }

            }
        }
        /// <summary>
        /// calculates the timeframe for each MIDI event found
        /// </summary>
        public void calculateTimeLine()
        {
            double prevTime = 0;
            foreach(midiEvent e in this.timeLine)
            {
                prevTime += e.deltaTime * this.interval;
                e.timeFrame = prevTime;
            }
        }

        /// <summary>
        /// Reset this MIDI reader for re-use
        /// </summary>
        public void reset()
        {
            this.data.Clear();
            this.division = 0;
            this.interval = 0;
            this.timeLine.Clear();
            this.totalLengthRead = 0;
            this.byteCount = 0;
        }

        /// <summary>
        /// Splits the timeline into multiple timelines, one per MIDI track
        /// </summary>
        /// <returns>List of a List of midiEvents</returns>
        public List<List<midiEvent>> getSeparateTracks()
        {
            List<List<midiEvent>> result = new List<List<midiEvent>>();
            int startIndex = 0;
            foreach(int index in this.trackIndexes)
            {
                result.Add(this.timeLine.GetRange(startIndex, index + 1 - startIndex));
                startIndex = index + 1;
            }
            return result;
        }
        #endregion
        #region Helper methods
        /// <summary>
        /// Get the chunktype of the current chunk
        /// </summary>
        /// <returns>Returns either HEAD or TRACK as a string</returns>
        private string getChunkType()
        {
            string result = "";
            for(int i = 0; i < 4; i++)
            {
                result += this.data[this.totalLengthRead + i].ToString();
            }
            this.totalLengthRead += 4;
            if(result == "7784104100")
            {
                return "HEAD";
            }
            else if(result == "7784114107")
            {
                return "TRACK";
            }
            else
            {
                return "ERROR";
            }
        }

        /// <summary>
        /// Gets the specified amount of bytes from the input array
        /// </summary>
        /// <param name="groupSize">The amount of bytes to get</param>
        /// <param name="total">The sum of all the bytes in the group</param>
        /// <returns>The result byte array </returns>
        private byte[] getChunkGroup(int groupSize, out int total)
        {
            List<byte> result = new List<byte>();
            int totals = 0;
            for(int b = this.totalLengthRead; b < this.totalLengthRead + groupSize; b++)
            {
                
                    totals += Convert.ToInt32(this.data[b]);
                
                result.Add(this.data[b]);
            }
            this.totalLengthRead += groupSize;
            total = totals;
            return result.ToArray();
        }
        /// <summary>
        /// Gets the specified amount of bytes from the input array
        /// </summary>
        /// <param name="groupSize">The amount of bytes to get</param>
        /// <returns>The result byte array</returns>
        private byte[] getChunkGroup(int groupSize)
        {
            List<byte> result = new List<byte>();
            for (int b = this.totalLengthRead; b < this.totalLengthRead + groupSize; b++)
            {
                result.Add(this.data[b]);
            }
            this.totalLengthRead += groupSize;
            return result.ToArray();
        }

        /// <summary>
        /// Calculate the MIDI division using the specified bytes
        /// </summary>
        /// <param name="data">The bytes to use to calculate the MIDI division</param>
        /// <returns>The MIDI division</returns>
        private int calculateDivision(byte[] data)
        {
            int result = 0;
            int counter = 0;
            string stringData = "";
            stringData += this.byteToString(data[0]);
            stringData += this.byteToString(data[1]);
            foreach(char c in stringData)
            {
                if(c == '1')
                {
                    int value = Convert.ToInt32(Math.Pow(2, counter));
                    result += value;
                }
                counter++;
            }

            return result;
        }

        /// <summary>
        /// Helper method for calculation
        /// </summary>
        /// <param name="data">The data to use</param>
        /// <returns>Intermidiate value for the time calculation</returns>
        private int calculateTime(byte[] data)
        {
            int result = 0;
            int counter = 0;
            string stringData = "";
            stringData += this.byteToString(data[0]);
            stringData += this.byteToString(data[1]);
            stringData += this.byteToString(data[2]);
            foreach (char c in stringData)
            {
                if (c == '1')
                {
                    int value = Convert.ToInt32(Math.Pow(2, counter));
                    result += value;
                }
                counter++;
            }

            return result;
        }
        /// <summary>
        /// Transforms an variable length quantity into it's decimal representation
        /// </summary>
        /// <returns>The dicimal representation of the variable length quantity</returns>
        private int getVariableLengthQuantity()
        {
            List<byte> deltaTimeBytes = new List<byte>();
            bool deltaCompleted = false;
            string binaryData = "";
            byte fbyte = this.data[this.totalLengthRead];
            if(fbyte == 0)
            {
                this.totalLengthRead ++;
                return 0;
            }
            else
            {
                while(!deltaCompleted)
                {
                    this.totalLengthRead++;
                    char fbit = this.byteToString(fbyte)[0];
                    deltaTimeBytes.Add(fbyte);
                    if(fbit == '0')
                    {
                        deltaCompleted = true;
                    }
                    else
                    {
                        fbyte = this.data[this.totalLengthRead];
                    }
                }
                for(int b = 0; b < deltaTimeBytes.Count; b++)
                {
                    if(b != deltaTimeBytes.Count -1)
                    {
                        binaryData += this.byteToString( Convert.ToByte(deltaTimeBytes[b] - 128)).Substring(1);
                    }
                    else
                    {
                        binaryData += this.byteToString(deltaTimeBytes[b]).Substring(1);
                    }
                }
                return Convert.ToInt32(binaryData,2);
            }
        }

        /// <summary>
        /// Convert the specified byte to it's binairy representation
        /// </summary>
        /// <param name="input">The byte to convert</param>
        /// <param name="shortPadLength">Use short pad length</param>
        /// <returns></returns>
        private string byteToString(byte input, bool shortPadLength = false)
        {
            return shortPadLength ? Convert.ToString(input, 2).PadLeft(8, '0') : Convert.ToString(input, 2).PadLeft(8, '0');
        }

        /// <summary>
        /// Gets the next MIDI meta event from the input
        /// </summary>
        /// <returns>The found MIDI meta event</returns>
        private midiEvent getMetaEvent()
        {
            byte[] prefixBytes = this.getChunkGroup(2);
            string prefix = prefixBytes[0].ToString() + prefixBytes[1].ToString().PadLeft(3,'0');
            midiEvent e = new midiEvent();
            // get the correct event
            foreach(KeyValuePair<string,midiEvent> kvPair in this.events)
            {
                if(kvPair.Key == prefix)
                {
                    e.name = this.events[prefix].name;
                    e.length = this.events[prefix].length;
                    e.prefix = this.events[prefix].prefix;
                    e.prefixLength = this.events[prefix].prefixLength;
                    e.hasLength = this.events[prefix].hasLength;
                    break;
                }
            }
            if(e.hasLength)
            {
                if (e.name == "Set Tempo")
                {
                    this.totalLengthRead++;
                    this.interval = (double)1 / (this.division / (this.calculateTime(this.getChunkGroup(3)) / 1000));
                }
                else
                {
                    this.totalLengthRead += e.length;
                }
            }
            else
            {
                this.totalLengthRead += this.getVariableLengthQuantity();
            }
            return e;
            /*
            if(e.name == "")
            {

            }
            // setup event object
            if(e.name == "Sequencer-Specific Meta-event")
            {
                e.length = this.getVariableLengthQuantity();
                this.totalLengthRead += e.length;
            }
            else if(e.name == "Text Event")
            {
                e.length = this.getVariableLengthQuantity();
                this.totalLengthRead += e.length;
            }
            else if(e.name == "End of Track")
            {
                e.length = 0;
                this.totalLengthRead++;
            }
            else if(e.name == "Set Tempo")
            {
                e.length = 3;
                this.interval = (double) 1 / (this.division / (this.calculateTime(this.getChunkGroup(3)) / 1000));
            }
            else if(e.name == "Time Signature")
            {
                e.length = 5;
                this.totalLengthRead += e.length;
            }
            else if(e.name == "Track Name")
            {
                e.length = this.getVariableLengthQuantity();
                this.totalLengthRead += e.length;
            }
            else if(e.name == "MIDI Channel Prefix")
            {
                e.length = 1;
                this.totalLengthRead += 1;
            }
            else
            {
                throw new Exception("Unknown meta event with prefix" + prefix);
            }
             * */
        }

        /// <summary>
        /// Gets the next MIDI channel event from the input
        /// </summary>
        /// <returns>The found MIDI channel event</returns>
        private midiEvent getChannelEvent()
        {
            midiEvent e = new midiEvent("System Exclusive","1111");
            string binaryByte = this.byteToString(this.data[this.totalLengthRead]);
            if (binaryByte == "11110000")
            {
                // system exclusive message
                bool foundEnd = false;
                while (foundEnd == false)
                {
                    if (this.data[this.totalLengthRead] == 247)
                    {
                        foundEnd = true;
                    }
                    else
                    {
                        this.totalLengthRead++;
                    }
                }
            }
            else
            {
                foreach (KeyValuePair<string, midiEvent> kvPair in this.events)
                {
                    if (binaryByte.StartsWith(kvPair.Key))
                    {
                        e.name = this.events[kvPair.Key].name;
                        e.length = this.events[kvPair.Key].length;
                        e.prefix = this.events[kvPair.Key].prefix;
                        e.prefixLength = this.events[kvPair.Key].prefixLength;
                        e.hasLength = this.events[kvPair.Key].hasLength;
                        break;
                    }
                }
                this.totalLengthRead++;
                if (e.name == "Note On")
                {
                    e.note = this.data[this.totalLengthRead];
                    this.totalLengthRead += e.length;
                }
                else if (e.name == "Note Off")
                {
                    e.note = this.data[this.totalLengthRead];
                    this.totalLengthRead += e.length;
                }
                else
                {
                    this.totalLengthRead += e.length;
                }
            }
            return e;
        }
        #endregion
    }
    #endregion
    #region MIDI event
    /// <summary>
    /// Class that represents an MIDI event in a MIDI file
    /// </summary>
    public class midiEvent
    {
        #region Global variables
        /// <summary>
        /// The name of this MIDI event
        /// </summary>
        public string name = "";
        /// <summary>
        /// The byte prefix for this MIDI event
        /// </summary>
        public string prefix = "";
        /// <summary>
        /// The length in bytes of this MIDI event
        /// </summary>
        public int length = 0;
        /// <summary>
        /// The length in bytes of the prefix
        /// </summary>
        public int prefixLength = 0;
        /// <summary>
        /// Indicates if this MIDI event has a default length
        /// </summary>
        public bool hasLength = false;
        /// <summary>
        /// The byte representing a note value if this MIDI event is either a 'Note On' or a 'Note Off'
        /// </summary>
        public byte note = 0;
        /// <summary>
        /// The delta time value of this MIDI event (see the official MIDI 1.0 specification)
        /// </summary>
        public int deltaTime = 0;
        /// <summary>
        /// The time on the time line for this MIDI event
        /// </summary>
        public double timeFrame = 0;
        /// <summary>
        /// Indicates if this MIDI event has allready ben executed (currently not used)
        /// </summary>
        public bool isCompleted = false;
        #endregion
        #region Constructors
        /// <summary>
        /// Create a new midiEvent object
        /// </summary>
        /// <param name="name">The name of the MIDI event</param>
        /// <param name="prefix">The prefix of the event</param>
        /// <param name="hasLength">Does the event has a default length?</param>
        public midiEvent(string name,string prefix, bool hasLength = false,int length = 0)
        {
            this.name = name;
            this.prefix = prefix;
            this.hasLength = hasLength;
            this.length = length;
        }

        /// <summary>
        /// Default empty constructor
        /// </summary>
        public midiEvent()
        {

        }
        #endregion
    }
#endregion
}
