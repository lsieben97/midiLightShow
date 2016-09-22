using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmx512UsbRs485;

namespace midiLightShow
{
    class _360SpotLight : dmxLight
    {
        Dmx512UsbRs485Driver driver = new Dmx512UsbRs485Driver();
        
        public _360SpotLight(string comPort)
        {
            this.comPort = comPort;
            this.driver.DmxToDefault(this.comPort);
        }

        //Functions below

        [ParameterDataAtribute(parameterDescription = new string[] { "Rotation angle" })]
        public void func_setPan(uint rotation)
        {
            while(rotation > 540)
            {
                rotation = rotation - 360;
            }

            this.driver.DmxLoadBuffer(1,(byte)(rotation / 540 * 255),8);
            this.driver.DmxSendCommand(1);
        }

        public void func_setTilt(uint rotation, bool channelExtended)
        {
            int c = 2;
            if(channelExtended)
            {
                c = 3;
            }

            while(rotation > 180)
            {
                rotation = rotation - 180;
            }

            this.driver.DmxLoadBuffer(c, (byte)(rotation / 180 * 255), 8);
            this.driver.DmxSendCommand(1);
        }

        public void func_closeShutter(bool channelExtended)
        {
            int c = 3;
            if(channelExtended)
            {
                c = 4;
            }

            this.driver.DmxLoadBuffer(c, 0, 8);
            this.driver.DmxSendCommand(1);
        }
        
        public void func_dimmer(uint value, bool channelExtended)
        {
            int c = 3;
            if (channelExtended)
            {
                c = 4;
            }

            while(value > 100)
            {
                value = value - 100;
            }

            this.driver.DmxLoadBuffer(c, (byte)(value / 100 * 127), 8);
            this.driver.DmxSendCommand(1);
        }

        public void func_strobo(uint value, bool channelExtended)
        {
            int c = 3;
            if (channelExtended)
            {
                c = 4;
            }

            while (value > 100)
            {
                value = value - 100;
            }

            this.driver.DmxLoadBuffer(c, (byte)(value / 100 * 105), 8);
            this.driver.DmxSendCommand(1);
        }
    }
}
