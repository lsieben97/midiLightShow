using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmx512UsbRs485;

namespace midiLightShow
{
    class lazer : dmxLight
    {
        Dmx512UsbRs485Driver driver = new Dmx512UsbRs485Driver();

        bool lamp = false;

        public lazer() { }
        public lazer(string comPort)
        {
            this.comPort = comPort;
            this.driver.DmxToDefault(this.comPort);
            this.driver.DmxLoadBuffer(1, 255, 10);
            this.driver.DmxSendCommand(1);
        }

        //Fire up your lazor
        public void autoRun()
        {
            this.driver.DmxLoadBuffer(1,0,10);
            this.driver.DmxSendCommand(1);
        }

        public void func_toggleLamp()
        {
            if(lamp)
                lamp = false;
            else
                lamp = true;

            this.driver.DmxLoadBuffer(3, Convert.ToByte(Convert.ToByte(lamp) * 25), 10);
        }


    }
}
