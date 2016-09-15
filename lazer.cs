using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmx512UsbRs485

namespace midiLightShow
{
    class lazer
    {
        Dmx512UsbRs485Driver driver = new Dmx512UsbRs485Driver();

        public string comPort = "";

        public lazer(string comPort)
        {
            this.comPort = comPort;
            this.driver.DmxToDefault(this.comPort);
        }

        //Fire up your lazor
    }
}
