﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiLightShow
{
    class _360SpotLight : dmxLight
    {
        private Dictionary<string, byte> goboMap = new Dictionary<string, byte>();

        public _360SpotLight() { }
        public _360SpotLight(string comPort)
        {
            this.comPort = comPort;
            this.driver.DmxToDefault(this.comPort);
            this.goboMap.Add("bananaSpiral", 13);
            this.goboMap.Add("eclipseSun", 26);
            this.goboMap.Add("flower", 39);
            this.goboMap.Add("portal", 52);
            this.goboMap.Add("heatWaves", 66);
            this.goboMap.Add("virus", 78);
            this.goboMap.Add("sun", 91);
            this.goboMap.Add("tangledLines", 104);
            this.goboMap.Add("bubbles", 117);
        }


        public override bool executeFunction(string function, Dictionary<string, string> parameters, bool execute = false)
        {
            List<string> strings = new List<string>();
            List<int> ints = new List<int>();
            List<bool> bools = new List<bool>();

            uint rotation = 0;
            uint value = 0;
            byte speed = 0;

            switch (function)
            {
                case "setPan":
                    rotation = 0;
                    if (uint.TryParse(parameters.ElementAt(0).Value, out rotation))
                    {
                        if (execute)
                        {
                            this.func_setPan(rotation);
                        } else { return true; }
                    } else { return false; }
                    break;
                case "setTilt":
                    rotation = 0;
                    if (uint.TryParse(parameters.ElementAt(0).Value, out rotation))
                    {
                        if (execute)
                        {
                            this.func_setTilt(rotation);
                        }
                        else { return true; }
                    } else { return false; }
                    break;
                case "shutter":
                    bool shutter = false;
                    if (bool.TryParse(parameters.ElementAt(0).Value, out shutter))
                    {
                        if (execute)
                        {
                            this.func_shutter(shutter);
                        }
                        else { return true; }
                    } else { return false; }
                    break;
                case "dimmer":
                    value = 0;
                    if(uint.TryParse(parameters.ElementAt(0).Value, out value))
                    {
                        if (execute)
                        {
                            this.func_dimmer(value);
                        }
                        else { return true; }
                    } else { return false; }
                    break;
                case "strobo":
                    value = 0;
                    if(uint.TryParse(parameters.ElementAt(0).Value, out value))
                    {
                        if (execute)
                        {
                            this.func_strobo(value);
                        }
                        else { return true; }
                    } else { return false; }
                    break;
                case "rgb":
                    byte r = 0;
                    byte g = 0;
                    byte b = 0;
                    if (byte.TryParse(parameters.ElementAt(0).Value, out r) && byte.TryParse(parameters.ElementAt(1).Value, out g) && byte.TryParse(parameters.ElementAt(2).Value, out b))
                    {
                        if (execute)
                        {
                            this.func_rgb(r, g, b);
                        }
                        else { return true; }
                    } else { return false; }
                    break;
                case "setSpeed":
                    byte mSpeed = 0;
                    if (byte.TryParse(parameters.ElementAt(0).Value, out mSpeed))
                    {
                        if (execute)
                        {
                            this.func_setSpeed(mSpeed);
                        } else { return true; }
                    } else { return false; }
                    break;
                case "rainBow":
                    bool direction = false;
                    speed = 0;
                    if(bool.TryParse(parameters.ElementAt(0).Value, out direction) && byte.TryParse(parameters.ElementAt(1).Value, out speed))
                    {
                        if (execute)
                        {
                            this.func_rainBow(direction, speed);
                        }
                        else { return true; }
                    } else { return false; }
                    break;
                case "gobo":
                    this.func_gobo(parameters.ElementAt(0).Value);
                    break;
                case "goboScroll":
                    bool clockWise = false;
                    speed = 0;
                    if(bool.TryParse(parameters.ElementAt(0).Value, out clockWise) && byte.TryParse(parameters.ElementAt(1).Value, out speed))
                    {
                        if (execute)
                        {
                            this.func_goboScroll(clockWise, speed);
                        } else { return true; }
                    } else { return false; }
                    break;
            }
            return false;
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

            this.driver.DmxLoadBuffer(1,(byte)(rotation / 540 * 255),13);
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

            this.driver.DmxLoadBuffer(3, (byte)(rotation / 180 * 255), 13);
            this.driver.DmxSendCommand(1);
        }

        [ParameterDataAtribute(parameterDescription = new string[] { "Shutter closed" })]
        [MethodDescriptionAtribute(methodDescription = "Open or close the shutter")]
        public void func_shutter(bool shutter)
        {
            byte s = 255;

            if(shutter)
            {
                s = 0;
            }

            this.driver.DmxLoadBuffer(6, s, 13);
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

            this.driver.DmxLoadBuffer(6, (byte)(value / 100 * 127), 13);
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

            this.driver.DmxLoadBuffer(6, (byte)(value / 100 * 105), 13);
            this.driver.DmxSendCommand(1);
        }

        [ParameterDataAtribute(parameterDescription = new string[] { "Red", "Green" , "Blue"})]
        [MethodDescriptionAtribute(methodDescription = "Set rgb values")]
        public void func_rgb(byte r = 0, byte g = 0, byte b = 0)
        {
            this.driver.DmxLoadBuffer(7, r, 13);
            this.driver.DmxLoadBuffer(8, g, 13);
            this.driver.DmxLoadBuffer(9, b, 13);
            this.driver.DmxSendCommand(3);
        }

        [ParameterDataAtribute(parameterDescription = new string[] { "Moving speed" })]
        [MethodDescriptionAtribute(methodDescription = "Set movement speed")]
        public void func_setSpeed(byte mSpeed)
        {
            this.driver.DmxLoadBuffer(5, mSpeed, 13);
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

            this.driver.DmxLoadBuffer(10, d, 13);
            this.driver.DmxLoadBuffer(11, (byte)(255 / speed + 1), 13);
        }

        [ParameterDataAtribute(parameterDescription = new string[] { "Gobo:\n- None\n- bananaSpiral\n- eclipseSun\n- flower\n- portal\n- heatWaves\n- virus\n- sun\n- tangledLines\n- bubbles" })]
        [MethodDescriptionAtribute(methodDescription = "Set a gobo")]
        public void func_gobo(string gobo)
        {
            byte val = 0;
            if(this.goboMap.ContainsKey(gobo))
            {
                val = this.goboMap[gobo];
            }

            this.driver.DmxLoadBuffer(13, val, 13);
            this.driver.DmxSendCommand(1);
        }

        [ParameterDataAtribute(parameterDescription = new string[] { "Direction of gobo order", "Speed of scrolling" })]
        [MethodDescriptionAtribute(methodDescription = "Turn on a scrollthrough the gobos")]
        public void func_goboScroll(bool clockWise, byte speed)
        {
            byte val = Convert.ToByte(128+speed/2);

            if(!clockWise)
            {
                val = Convert.ToByte(val + 128);
            }

            this.driver.DmxLoadBuffer(13, val, 13);
            this.driver.DmxSendCommand(1);
        }

    }
}
