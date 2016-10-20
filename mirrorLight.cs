using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiLightShow
{
    class mirrorLight : dmxLight
    {
        #region Global variables
        #endregion
        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public mirrorLight() { }
        /// <summary>
        /// Constructor for RGB spot light class
        /// </summary>
        /// <param name="comPort">Connection port</param>
        public mirrorLight(string comPort)
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
                case "":
                    break;
            }
            return false;
        }
        #endregion
        #region Functions
        public void func_color(bool red, bool green, bool blue, bool white)
        {
            bool[] colors = { red, green, blue, white };

            string binary = "";
            foreach (bool color in colors)
            {
                if (color)
                    binary += "1";
                else
                    binary += "0";
            }

            this.driver.DmxLoadBuffer(4, (byte)(Convert.ToInt32(binary, 2) / 256 * 224), 512);
            this.driver.DmxSendCommand(1);
        }


        #endregion
    }
}
