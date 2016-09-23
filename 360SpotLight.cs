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
        [MethodDescriptionAtribute(methodDescription = "Rotate Horizontally")]
        public void func_setPan(uint rotation)
        {
            while(rotation > 540)
            {
                rotation = rotation - 360;
            }

            this.driver.DmxLoadBuffer(1,(byte)(rotation / 540 * 255),8);
            this.driver.DmxSendCommand(1);
        }

        [ParameterDataAtribute(parameterDescription = new string[] { "Rotation angle" })]
        [MethodDescriptionAtribute(methodDescription = "Rotate Vertically")]
        public void func_setTilt(uint rotation)
        {

            while(rotation > 180)
            {
                rotation = rotation - 180;
            }

            this.driver.DmxLoadBuffer(3, (byte)(rotation / 180 * 255), 8);
            this.driver.DmxSendCommand(1);
        }

        [ParameterDataAtribute(parameterDescription = new string[] { "Shutter closed" })]
        [MethodDescriptionAtribute(methodDescription = "Open or close the shutter")]
        public void func_Shutter(bool shutter)
        {
            byte s = 255;

            if(shutter)
            {
                s = 0;
            }

            this.driver.DmxLoadBuffer(6, s, 8);
            this.driver.DmxSendCommand(1);
        }

        [ParameterDataAtribute(parameterDescription = new string[] { "Dimmer value" })]
        [MethodDescriptionAtribute(methodDescription = "Turn on the dimmer")]
        public void func_dimmer(uint value)
        {
            while(value > 100)
            {
                value = value - 100;
            }

            this.driver.DmxLoadBuffer(6, (byte)(value / 100 * 127), 8);
            this.driver.DmxSendCommand(1);
        }

        [ParameterDataAtribute(parameterDescription = new string[] { "Stroboscope value" })]
        [MethodDescriptionAtribute(methodDescription = "Turn on the stroboscope")]
        public void func_strobo(uint value)
        {
            while (value > 100)
            {
                value = value - 100;
            }

            this.driver.DmxLoadBuffer(6, (byte)(value / 100 * 105), 8);
            this.driver.DmxSendCommand(1);
        }

        [ParameterDataAtribute(parameterDescription = new string[] { "Red", "Green" , "Blue"})]
        [MethodDescriptionAtribute(methodDescription = "Set rgb values")]
        public void func_rgb(byte r = 0, byte g = 0, byte b = 0)
        {
            this.driver.DmxLoadBuffer(7, r, 8);
            this.driver.DmxLoadBuffer(8, g, 8);
            this.driver.DmxLoadBuffer(9, b, 8);
            this.driver.DmxSendCommand(3);
        }

        [ParameterDataAtribute(parameterDescription = new string[] { "Moving speed" })]
        [MethodDescriptionAtribute(methodDescription = "Set movement speed")]
        public void func_setSpeed(byte mSpeed)
        {
            this.driver.DmxLoadBuffer(5, mSpeed, 8);
            this.driver.DmxSendCommand(1);
        }

        [ParameterDataAtribute(parameterDescription = new string[] { "Direction of color order", "Speed of changing colors" })]
        [MethodDescriptionAtribute(methodDescription = "Turn on a rainbow effect")]
        public void func_rainBow(bool direction,byte speed)
        {
            byte d = 255;

            if(direction)
            {
                d = 232;
            }

            this.driver.DmxLoadBuffer(10, d, 8);
            this.driver.DmxLoadBuffer(11, (byte)(255 / speed + 1), 8);
        }
    }
}
