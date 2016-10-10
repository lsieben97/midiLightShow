using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiLightShow
{
    /// <summary>
    /// RGB light class. Functions for the RGB spotlight
    /// Inherited from dmxLight
    /// </summary>
    public class rgbSpotLight : dmxLight
    {
        #region Global variables
        /// <summary>
        /// Speed variable used for
        /// </summary>
        byte speed = 0;
        #endregion
        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public rgbSpotLight() { }
        /// <summary>
        /// Constructor for RGB spot light class
        /// </summary>
        /// <param name="comPort">Connection port</param>
        public rgbSpotLight(string comPort)
        {
            this.comPort = comPort;
            this.driver.DmxToDefault(this.comPort);
        }
        #endregion
        #region Execute method
        /// <summary>
        /// execute function method override from parent dmxLight
        /// </summary>
        /// <param name="function">Function given from track</param>
        /// <param name="parameters">Paramaters given with the function</param>
        /// <param name="execute">Indicator variable for executing the function</param>
        /// <returns>Parameter validation</returns>
        public override bool executeFunction(string function, Dictionary<string, string> parameters, bool execute = false)
        {
            //execute function switch case
            switch (function)
            {
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
                    }
                    else { return false; }
                    break;
                case "fade":
                    speed = 0;
                    if (byte.TryParse(parameters.ElementAt(0).Value, out speed))
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
                    if (byte.TryParse(parameters.ElementAt(0).Value, out speed))
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
        #endregion
        #region Functions
        /// <summary>
        /// Set the color
        /// </summary>
        /// <param name="r">Red value</param>
        /// <param name="g">Green value</param>
        /// <param name="b">Blue value</param>
        [ParameterDataAtribute(parameterDescription = new string[] { "Red", "Green", "Blue" })]
        [MethodDescriptionAtribute(methodDescription = "Set Light color")]
        public void func_rgb(byte r, byte g, byte b)
        {
            //Loads and sends the value to the RGB spotlight
            this.driver.DmxLoadBuffer(1, r, 512);
            this.driver.DmxLoadBuffer(2, g, 512);
            this.driver.DmxLoadBuffer(3, b, 512);
            this.driver.DmxSendCommand(3);
        }

        [ParameterDataAtribute(parameterDescription = new string[] { "Speed" })]
        [MethodDescriptionAtribute(methodDescription = "Fade lights")]
        public void func_fade(byte speed = 0)
        {
            //Switches the value
            if(speed != 0)
                this.driver.DmxLoadBuffer(6, (byte)(64 + 64 / (speed + 1)), 8);
            else
                this.driver.DmxLoadBuffer(6, 0, 8);
            
            //Sends the value to the RGB spotlight
            this.driver.DmxSendCommand(1);
        }

        [ParameterDataAtribute(parameterDescription = new string[] { "Speed" })]
        public void func_rainBow(byte speed = 0)
        {
            if (speed != 0)
                this.driver.DmxLoadBuffer(6, (byte)(128 + 64 / (speed + 1)), 8);
            else
                this.driver.DmxLoadBuffer(6, 0, 8);
            this.driver.DmxSendCommand(1);
        }
        #endregion
    }
}