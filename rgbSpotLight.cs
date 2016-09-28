using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmx512UsbRs485;

namespace midiLightShow
{
    public class rgbSpotLight : dmxLight
    {
        Dmx512UsbRs485Driver driver = new Dmx512UsbRs485Driver();

        public rgbSpotLight(string comPort)
        {
            this.comPort = comPort;
            this.driver.DmxToDefault(this.comPort);
        }
        public rgbSpotLight() { }

        public override void executeFunction(string function, string[] parameters)
        {
            List<string> strings = new List<string>();
            List<int> ints = new List<int>();
            List<bool> bools = new List<bool>();
            switch (function)
            {
                case "rgb":
                    byte r = 0;
                    byte g = 0;
                    byte b = 0;
                    if (byte.TryParse(parameters[0], out r) && byte.TryParse(parameters[1], out g) && byte.TryParse(parameters[2], out b))
                    {
                        this.func_rgb(r, g, b);
                    }
                    break;
            }
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

        [ParameterDataAtribute(parameterDescription = new string[] { "No parameters needed." })]
        [MethodDescriptionAtribute(methodDescription = "Fade lights")]
        public void func_fade()
        {
            this.driver.DmxLoadBuffer(6, 64, 8);
            this.driver.DmxSendCommand(1);
        }


    }
}
