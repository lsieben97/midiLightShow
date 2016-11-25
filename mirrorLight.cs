using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiLightShow
{
    public class mirrorLight : dmxLight
    {
        #region Global variables
        byte rotateLights;
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
        public override bool executeFunction(string function, List<string> parameters, bool execute = false)
        {
            //execute function switch case
            switch (function)
            {
                case "color":
                    bool red;
                    bool green;
                    bool blue;
                    bool white;
                    if(bool.TryParse(parameters[0],out red) && bool.TryParse(parameters[1],out green) && bool.TryParse(parameters[2],out blue) && bool.TryParse(parameters[3],out white))
                    {
                        if(execute)
                        {
                            this.func_color(red, green, blue, white);
                        } else { return true; }
                    } else { return false; }
                    break;
                case "rainbow":
                    uint speed;
                    if(uint.TryParse(parameters[0],out speed))
                    {
                        if(execute)
                        {
                            this.func_rainbow(speed);
                        } else { return true; }
                    } else { return false; }
                    break;
                case "rotateX":
                    uint rotationX;
                    if (uint.TryParse(parameters[0], out rotationX))
                    {
                        if (execute)
                        {
                            this.func_rotateX(rotationX);
                        } else { return true; }
                    } else { return false; }
                    break;
                case "rotateY":
                    uint rotationY;
                    if (uint.TryParse(parameters[0], out rotationY))
                    {
                        if (execute)
                        {
                            this.func_rotateY(rotationY);
                        } else { return true; }
                    } else { return false; }
                    break;
                case "rotateLights":
                    if(execute)
                    {
                        this.func_rotateLights();
                    } else { return true; }
                    break;
            }
            return false;
        }
        #endregion
        #region Functions
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="white"></param>
        //

        [ParameterDataAtribute(parameterDescription = new string[] { "Red (true/false)", "\nGreen (true/false)", "\nBlue (true/false)", "\nWhite (true/false)" })]
        [MethodDescriptionAtribute(methodDescription = "Set Light colors")]
        public void func_color(bool red, bool green, bool blue, bool white)
        {
            //
            byte[] mix = { 0, 53, 38, 128, 23, 143, 113, 203, 8, 98, 83, 188, 68, 173, 158, 218 };
            //
            bool[] colors = { red, green, blue, white };
            //
            string binary = "";

            //
            foreach (bool color in colors)
            {
                if (color)
                    binary += "1";
                else
                    binary += "0";
            }

            //
            this.driver.DmxLoadBuffer(4, mix[Convert.ToInt32(binary, 2)], 512);
            this.driver.DmxSendCommand(1);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="speed"></param>
        [ParameterDataAtribute(parameterDescription = new string[] { "Speed (0 - 23)" })]
        [MethodDescriptionAtribute(methodDescription = "Turn on a rainbow effect")]
        public void func_rainbow(uint speed)
        {
            //
            while (speed > 23)
            {
                speed = speed - 23;
            }

            //Load and send the value to the moving head
            this.driver.DmxLoadBuffer(4, (byte)(speed + 232), 13);
            this.driver.DmxSendCommand(1);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rotation"></param>
        [ParameterDataAtribute(parameterDescription = new string[] { "Rotation (0 - 180)" })]
        [MethodDescriptionAtribute(methodDescription = "Surface rotation")]
        public void func_rotateX(uint rotation)
        {
            //
            while (rotation > 180)
            {
                rotation = rotation - 180;
            }

            //Load and send the value to the moving head
            this.driver.DmxLoadBuffer(1, (byte)(rotation / 180 * 255), 13);
            this.driver.DmxSendCommand(1);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rotation"></param>
        [ParameterDataAtribute(parameterDescription = new string[] { "Rotation (0 - 90)" })]
        [MethodDescriptionAtribute(methodDescription = "Head rotation")]
        public void func_rotateY(uint rotation)
        {
            while (rotation > 90)
            {
                rotation = rotation - 90;
            }

            //Load and send the value to the moving head
            this.driver.DmxLoadBuffer(3, (byte)(rotation / 90 * 255), 13);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// 
        /// </summary>
        [ParameterDataAtribute(parameterDescription = new string[] { "No parameter description needed" })]
        [MethodDescriptionAtribute(methodDescription = "Automatically rotate lights")]
        public void func_rotateLights()
        {
            if(rotateLights == 0)
                rotateLights = 0;
            else
                rotateLights = 255;
            //
            this.driver.DmxLoadBuffer(1,rotateLights,13);
        }
        #endregion
    }
}
