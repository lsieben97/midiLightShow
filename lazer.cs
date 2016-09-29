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
        bool color = false;

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
            this.driver.DmxSendCommand(1);
        }

        public void func_color()
        {
            if (color)
                color = false;
            else
                color = true;

            this.driver.DmxLoadBuffer(3, Convert.ToByte(Convert.ToByte(color) * 25 + 50), 10);
            this.driver.DmxSendCommand(1);
        }

        public void func_colorCycle()
        {
            this.driver.DmxLoadBuffer(3, 125, 10);
            this.driver.DmxSendCommand(1);
        }

        public void func_frequency(byte speed)
        {
            this.driver.DmxLoadBuffer(4, speed, 10);
            this.driver.DmxSendCommand(1);
        }

        public void func_zoom(byte magnify)
        {
            this.driver.DmxLoadBuffer(5, (byte)(magnify / 2), 10);
            this.driver.DmxSendCommand(1);
        }

        public void func_autoZoom(byte speed)
        {
            this.driver.DmxLoadBuffer(5, (byte)(speed / 255 * 45), 10);
            this.driver.DmxSendCommand(1);
        }

        public void func_xy(byte x, byte y)
        {
            this.driver.DmxLoadBuffer(6, (byte)(x / 2), 10);
            this.driver.DmxLoadBuffer(7, (byte)(y / 2), 10);
            this.driver.DmxSendCommand(2);
        }

    }
}
