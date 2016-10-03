using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiLightShow
{
    public class rgbSpotLight : dmxLight
    {

        public rgbSpotLight() { }
        public rgbSpotLight(string comPort)
        {
            this.comPort = comPort;
            this.driver.DmxToDefault(this.comPort);
        }

        public override bool executeFunction(string function, Dictionary<string,string> parameters, bool execute = false)
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
                    if (byte.TryParse(parameters.ElementAt(0).Value, out r) && byte.TryParse(parameters.ElementAt(1).Value, out g) && byte.TryParse(parameters.ElementAt(2).Value, out b))
                    {
                        if(execute)
                        {
                            this.func_rgb(r, g, b);
                        }
                        else { return true; }
                    } else { return false; }
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

        [ParameterDataAtribute(parameterDescription = new string[] { "No parameters needed." })]
        [MethodDescriptionAtribute(methodDescription = "Fade lights")]
        public void func_fade()
        {
            this.driver.DmxLoadBuffer(6, 64, 8);
            this.driver.DmxSendCommand(1);
        }


    }
}
