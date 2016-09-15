using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmx512UsbRs485;

namespace midiLightShow
{
    class rgbSpotLight
    {
        Dmx512UsbRs485Driver driver = new Dmx512UsbRs485Driver();

        public string comPort = "";

        public rgbSpotLight(string comPort)
        {
            this.comPort = comPort;
            this.driver.DmxToDefault(this.comPort);
        }

        //Functions below for this light
        public void rgb (byte r, byte g, byte b)
        {
            this.driver.DmxLoadBuffer(1, r, 8);
            this.driver.DmxLoadBuffer(2, g, 8);
            this.driver.DmxLoadBuffer(3, b, 8);
            this.driver.DmxSendCommand(3);
        }

        public void fade ()
        {
            this.driver.DmxLoadBuffer(6, 64, 8);
            this.driver.DmxSendCommand(1);
        }


    }
}
