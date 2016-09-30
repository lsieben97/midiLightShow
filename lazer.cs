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
        private Dictionary<string, byte> patternMap = new Dictionary<string, byte>();

        bool lamp = false;
        bool color = false;

        public lazer() { }
        public lazer(string comPort)
        {
            this.comPort = comPort;
            this.driver.DmxToDefault(this.comPort);
            this.driver.DmxLoadBuffer(1, 255, 10);
            this.driver.DmxSendCommand(1);

            this.patternMap.Add("morse circle",8);
            this.patternMap.Add("triangle",16);
            this.patternMap.Add("square",24);
            this.patternMap.Add("dashed square",32);
            this.patternMap.Add("frame",40);
            this.patternMap.Add("filled plus",48);
            this.patternMap.Add("saw",56);
            this.patternMap.Add("corner",64);
            this.patternMap.Add("bow tie",72);
            this.patternMap.Add("spiral",80);
            this.patternMap.Add("mirrored half circle",88);
            this.patternMap.Add("snail",96);
            this.patternMap.Add("two dashed circle",104);
            this.patternMap.Add("rooftops",112);
            this.patternMap.Add("zigzag",120);
            this.patternMap.Add("v",128);
            this.patternMap.Add("w",136);
            this.patternMap.Add("snake",144);
            this.patternMap.Add("line",152);
            this.patternMap.Add("dashed line",160);
            this.patternMap.Add("bold dashed line",168);
            this.patternMap.Add("two lines",176);
            this.patternMap.Add("plus",184);
            this.patternMap.Add("two half lines",192);
            this.patternMap.Add("unfinished diamond",200);
            this.patternMap.Add("two cubes",208);
            this.patternMap.Add("four cubes",216);
            this.patternMap.Add("small circle",224);
            this.patternMap.Add("short dashed line",232);
            this.patternMap.Add("dashed arc",240);
            this.patternMap.Add("dots",248);
        }

        //Fire up your lazor
        public void autoRun()
        {
            this.driver.DmxLoadBuffer(1,0,10);
            this.driver.DmxSendCommand(1);
        }

        public void func_symbol(string symbol)
        {
            byte val = 0;
            if (this.patternMap.ContainsKey(symbol))
            {
                val = this.patternMap[symbol];
            }

            this.driver.DmxLoadBuffer(2, val, 10);
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

        public void func_autoXY(bool clockWiseX, bool clockWiseY)
        {
            this.driver.DmxLoadBuffer(6, (byte)(Convert.ToByte(clockWiseX) * 64 + 128), 10);
            this.driver.DmxLoadBuffer(7, (byte)(Convert.ToByte(clockWiseY) * 64 + 128), 10);
            this.driver.DmxSendCommand(2);
        }

        [ParameterDataAtribute(parameterDescription = new string[] { "Rotation angle" })]
        [MethodDescriptionAtribute(methodDescription = "Rotate Horizontally")]
        public void func_setPan(uint rotation)
        {
            while (rotation > 360)
            {
                rotation = rotation - 360;
            }

            this.driver.DmxLoadBuffer(8, (byte)(rotation / 360 * 127), 10);
            this.driver.DmxSendCommand(1);
        }

        [ParameterDataAtribute(parameterDescription = new string[] { "Rotation angle" })]
        [MethodDescriptionAtribute(methodDescription = "Rotate Vertically")]
        public void func_setTilt(uint rotation)
        {
            while (rotation > 360)
            {
                rotation = rotation - 360;
            }

            this.driver.DmxLoadBuffer(9, (byte)(rotation / 360 * 127), 10);
            this.driver.DmxSendCommand(1);
        }

        public void func_setRotation(uint rotation)
        {
            while (rotation > 360)
            {
                rotation = rotation - 360;
            }

            this.driver.DmxLoadBuffer(10, (byte)(rotation / 360 * 127), 10);
            this.driver.DmxSendCommand(1);
        }

        public void func_autoRotate(bool clockWiseX, bool clockWiseY)
        {
            this.driver.DmxLoadBuffer(8, (byte)(Convert.ToByte(clockWiseX) * 64 + 128), 10);
            this.driver.DmxLoadBuffer(9, (byte)(Convert.ToByte(clockWiseY) * 64 + 128), 10);
            this.driver.DmxLoadBuffer(10, (byte)(Convert.ToByte(clockWiseY) * 64 + 128), 10);
            this.driver.DmxSendCommand(3);
        }


    }
}
