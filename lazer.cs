using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmx512UsbRs485;

namespace midiLightShow
{
    class lazer
    {
        Dmx512UsbRs485Driver driver = new Dmx512UsbRs485Driver();

        

        public lazer(string comPort)
        {
            this.comPort = comPort;
            this.driver.DmxToDefault(this.comPort);
        }

        //Fire up your lazor
        public void autoRun(uint programNr)
        {
            if(programNr < 5)
            {
                this.driver.DmxLoadBuffer(1,Convert.ToByte(programNr),8);
                this.driver.DmxSendCommand(1);
            }
            else
            {
                Console.WriteLine("Error: Program number \"" + programNr + "\" in autoRun doesn't exist");
            }

        }

    }
}
