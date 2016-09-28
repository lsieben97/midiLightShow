using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace midiLightShow
{
    public class midiReader
    {
        public List<byte> data = new List<byte>();
        public List<midiEvent> timeLine = new List<midiEvent>();
        public Dictionary<string,midiEvent> events = new Dictionary<string,midiEvent>();
        public int totalLengthRead = 0;
        public int byteCount = 0;
        public int division = 0;
        public double interval = 0;
        public midiImport form;
        public List<int> trackIndexes = new List<int>();

        public void init()
        {
            this.events.Add("255127", new midiEvent("Sequencer-Specific Meta-event", "255127", true));
            this.events.Add("255001", new midiEvent("Text Event", "2551", true));
            this.events.Add("255081003", new midiEvent("Set Tempo", "255813", true));
            this.events.Add("255088", new midiEvent("Time Signature", "25588", true));
            this.events.Add("255047", new midiEvent("End of Track", "255470", true));
            this.events.Add("255003", new midiEvent("Track Name", "2553", true));
            this.events.Add("255032001", new midiEvent("MIDI Channel Prefix", "255321", true));
            this.events.Add("144", new midiEvent("Note On", "144", true));
            this.events.Add("128", new midiEvent("Note Off", "128", true));
            this.events.Add("192", new midiEvent("Program Change", "192", true));
        }
        public void loadFile(string path)
        {
            if(File.Exists(path))
            {
                this.data = File.ReadAllBytes(path).ToList();
                this.byteCount = this.data.Count;
            }
        }

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
                    int trackChunkCount = 0;
                    this.getChunkGroup(2, out trackChunkCount);
                    this.division = this.calculateDivision(this.getChunkGroup(2));
                    this.form.rtbStatus.AppendText(string.Format("Found MIDI header chunk with following data:\n\tFile format: {0}\n\tDivision: {1}\n\tTrack chunk count: {2}", fileFormat.ToString(), this.division.ToString(), trackChunkCount.ToString()));
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
                            this.form.rtbStatus.AppendText("Found " + e.name + "\n");
                            Thread.Sleep(25);
                            
                            this.timeLine.Add(e);
                            if(e.name == "End of Track")
                            {
                                this.trackIndexes.Add(this.timeLine.Count - 1);
                                endOfTrack = true;
                            }
                        }
                        else
                        {
                            midiEvent e = this.getChannelEvent();
                            e.deltaTime = delta;
                            this.form.rtbStatus.AppendText("Found " + e.name + "\n");
                            Thread.Sleep(25);
                            this.timeLine.Add(e);
                        }
                    }
                }

            }
        }
        public void calculateTimeLine()
        {
            double prevTime = 0;
            foreach(midiEvent e in this.timeLine)
            {
                prevTime += e.deltaTime * this.interval;
                e.timeFrame = prevTime;
            }
        }
        public void reset()
        {
            this.data.Clear();
            this.division = 0;
            this.interval = 0;
            this.timeLine.Clear();
            this.totalLengthRead = 0;
            this.byteCount = 0;
        }
        public List<List<midiEvent>> getSeparateTracks()
        {
            List<List<midiEvent>> result = new List<List<midiEvent>>();
            int startIndex = 0;
            foreach(int index in this.trackIndexes)
            {
                result.Add(this.timeLine.GetRange(startIndex, index + 1 - startIndex));
                startIndex = index;
            }
            return result;
        }
        #region Helper methods
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
        private int getVariableLengthQuantity()
        {
            List<byte> deltaTimeBytes = new List<byte>();
            bool deltaCompleted = false;
            string binaryData = "";
            byte fbyte = this.data[this.totalLengthRead];
            if(fbyte == 0)
            {
                this.totalLengthRead++;
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

        private string byteToString(byte input, bool shortPadLength = false)
        {
            return shortPadLength ? Convert.ToString(input, 2).PadLeft(8, '0') : Convert.ToString(input, 2).PadLeft(8, '0');
        }
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
                else if(kvPair.Key.StartsWith(prefix) == true && this.events.ContainsKey(prefix + this.data[this.totalLengthRead].ToString().PadLeft(3,'0')))
                {
                    string newPrefix = prefix + this.data[this.totalLengthRead].ToString().PadLeft(3, '0');
                    e.name = this.events[newPrefix].name;
                    e.length = this.events[newPrefix].length;
                    e.prefix = this.events[newPrefix].prefix;
                    e.prefixLength = this.events[newPrefix].prefixLength;
                    e.hasLength = this.events[newPrefix].hasLength;
                    this.totalLengthRead++;
                    break;
                }
            }
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

            return e;
        }
        private midiEvent getChannelEvent()
        {
            midiEvent e = new midiEvent();
            if(this.data[this.totalLengthRead] == 144)
            {
                e.name = "Note On";
                this.totalLengthRead++;
                e.note = this.data[this.totalLengthRead];
            }
            else if(this.data[this.totalLengthRead] == 128)
            {
                e.name = "Note Off";
                this.totalLengthRead++;
                e.note = this.data[this.totalLengthRead];
            }
            else if(this.data[this.totalLengthRead] == 192)
            {
                e.name = "Program Change";
            }
            else
            {

            }
            this.totalLengthRead += 2;
            return e;
        }
        #endregion
        #region Static functions
        public static midiEvent copyEvent(midiEvent input)
        {
            midiEvent e = new midiEvent();
            e.name = input.name;
            e.length = input.length;
            e.deltaTime = input.deltaTime;
            e.hasLength = input.hasLength;
            e.isCompleted = input.isCompleted;
            e.note = input.note;
            e.prefix = input.prefix;
            e.prefixLength = input.prefixLength;
            e.timeFrame = input.timeFrame;
            return e;
        }
        #endregion
    }
    public class midiEvent
    {
        public string name = "";
        public string prefix = "";
        public int length = 0;
        public int prefixLength = 0;
        public bool hasLength = false;
        public byte note = 0;
        public int deltaTime = 0;
        public double timeFrame = 0;
        public bool isCompleted = false;

        public midiEvent(string name,string prefix,bool hasLength = false)
        {
            this.name = name;
            this.prefix = prefix;
            this.hasLength = hasLength;
        }
        public midiEvent()
        {

        }
    }
    public class midiNote
    {
        public int noteValue = 0;
        public midiNote(int noteValue)
        {
            this.noteValue = noteValue;
        }
    }
}
