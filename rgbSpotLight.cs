using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiLightShow
{
    /// <summary>
    /// <summary>
    /// thgfhhgfgjhghggrd
    /// </summary>
    /// ddadf
    /// </summary>
    public class rgbSpotLight : dmxLight
    {

        byte speed = 0;

        public rgbSpotLight() { }
        public rgbSpotLight(string comPort)
        {
            this.comPort = comPort;
            this.driver.DmxToDefault(this.comPort);
        }

        public override bool executeFunction(string function, List<string> parameters, bool execute = false)
        {
            switch (function)
            {
                case "rgb":
                    byte r = 0;
                    byte g = 0;
                    byte b = 0;
                    if (byte.TryParse(parameters[0], out r) && byte.TryParse(parameters[1], out g) && byte.TryParse(parameters[2], out b))
                    {
                        if (execute)
                        {
                            this.func_rgb(r, g, b);
                        }
                        else { return true; }
                    }
                    else { return false; }
                    break;
                case "fade":
                    speed = 0;
                    if (byte.TryParse(parameters[0], out speed))
                    {
                        if (execute)
                        {
                            this.func_fade(speed);
                        }
                        else { return true; }
                    } else { return false; }
                    break;
                case "rainBow":
                    speed = 0;
                    if (byte.TryParse(parameters[0], out speed))
                    {
                        if (execute)
                        {
                            this.func_rainBow(speed);
                        }
                        else { return true; }
                    }
                    else { return false; }
                    break;
            }
            return false;
        }
        //Functions below for this light
        [ParameterDataAtribute(parameterDescription = new string[] { "Red", "Green", "Blue" })]
        [MethodDescriptionAtribute(methodDescription = "Set Light color")]
        public void func_rgb(byte r, byte g, byte b)
        {
            this.driver.DmxLoadBuffer(1, r, 8);
            this.driver.DmxLoadBuffer(2, g, 8);
            this.driver.DmxLoadBuffer(3, b, 8);
            this.driver.DmxSendCommand(3);
        }

        [ParameterDataAtribute(parameterDescription = new string[] { "Speed" })]
        [MethodDescriptionAtribute(methodDescription = "Fade lights")]
        public void func_fade(byte speed = 0)
        {
            if(speed != 0)
                this.driver.DmxLoadBuffer(6, (byte)(64 + 64 / (speed + 1)), 8);
            else
                this.driver.DmxLoadBuffer(6, 0, 8);
            this.driver.DmxSendCommand(1);
        }

        [ParameterDataAtribute(parameterDescription = new string[] { "Speed" })]
        [MethodDescriptionAtribute(methodDescription = "Rainbow effect")]
        public void func_rainBow(byte speed = 0)
        {
            if (speed != 0)
                this.driver.DmxLoadBuffer(6, (byte)(128 + 64 / (speed + 1)), 8);
            else
                this.driver.DmxLoadBuffer(6, 0, 8);
            this.driver.DmxSendCommand(1);
        }
    }
}