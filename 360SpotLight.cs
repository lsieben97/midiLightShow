using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiLightShow
{
    /// <summary>
    /// Moving head class. Functions for the Moving head DMX light
    /// Inherited from dmxLight
    /// </summary>
    class _360SpotLight : dmxLight
    {
        #region Global variables
        /// <summary>
        /// A dictionary for all the gobo patterns
        /// </summary>
        private Dictionary<string, byte> goboMap = new Dictionary<string, byte>();
        #endregion
        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public _360SpotLight() { }
        /// <summary>
        /// Constructor for moving head class
        /// </summary>
        /// <param name="comPort">Connection port</param>
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
            //parameter variables that occur in several functions
            uint rotation = 0;
            uint value = 0;
            byte speed = 0;

            //execute function switch case
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
        #endregion
        #region Functions

        /// <summary>
        /// Sets the horizontal rotation of the moving head
        /// </summary>
        /// <param name="rotation">Rotation degree variable</param>
        [ParameterDataAtribute(parameterDescription = new string[] { "Rotation angle" })]
        [MethodDescriptionAtribute(methodDescription = "Rotate Horizontally")]
        public void func_setPan(uint rotation)
        {
            //Escape too big degree numbers to readable ones
            while(rotation > 540)
            {
                rotation = rotation - 360;
            }

            //Load and send the value to the moving head
            this.driver.DmxLoadBuffer(1,(byte)(rotation / 540 * 255),13);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// Sets the vertical rotation of the moving head
        /// </summary>
        /// <param name="rotation">Rotation degree variable</param>
        [ParameterDataAtribute(parameterDescription = new string[] { "Rotation angle" })]
        [MethodDescriptionAtribute(methodDescription = "Rotate Vertically")]
        public void func_setTilt(uint rotation)
        {
            //Escape too big degree numbers to readable ones
            while(rotation > 180)
            {
                rotation = rotation - 180;
            }

            //Load and send the value to the moving head
            this.driver.DmxLoadBuffer(3, (byte)(rotation / 180 * 255), 13);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// Sets the shutter open or closed
        /// </summary>
        /// <param name="shutter">Variables to open or close the shutter</param>
        [ParameterDataAtribute(parameterDescription = new string[] { "Shutter closed" })]
        [MethodDescriptionAtribute(methodDescription = "Open or close the shutter")]
        public void func_shutter(bool shutter)
        {
            //set default value shutter open
            byte s = 255;

            //checks on behalf of the parameter if the value of the shutter should be closed
            if(shutter)
            {
                s = 0;
            }

            //Loads and sends the value to the moving head
            this.driver.DmxLoadBuffer(6, s, 13);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// Sets the value of how bright the light should shine in percentage
        /// </summary>
        /// <param name="value">Percentage value to set the dimmer brightness</param>
        [ParameterDataAtribute(parameterDescription = new string[] { "Dimmer value" })]
        [MethodDescriptionAtribute(methodDescription = "Turn on the dimmer")]
        public void func_dimmer(uint value)
        {
            //Escape too big number to readable ones
            while(value > 100)
            {
                value = value - 100;
            }

            //Loads and sends the value to the moving head
            this.driver.DmxLoadBuffer(6, (byte)(value / 100 * 127), 13);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// Turns on the stroboscope
        /// </summary>
        /// <param name="value">Sets the interval of the stroboscope</param>
        [ParameterDataAtribute(parameterDescription = new string[] { "Stroboscope value" })]
        [MethodDescriptionAtribute(methodDescription = "Turn on the stroboscope")]
        public void func_strobo(uint value)
        {
            //Escape too big numbers to readable ones
            while (value > 100)
            {
                value = value - 100;
            }

            //Loads and sends the value to the moving head
            this.driver.DmxLoadBuffer(6, (byte)(value / 100 * 105), 13);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// Sets the color of the moving head
        /// </summary>
        /// <param name="r">The red light value</param>
        /// <param name="g">The green light value</param>
        /// <param name="b">The blue light value</param>
        [ParameterDataAtribute(parameterDescription = new string[] { "Red", "Green" , "Blue"})]
        [MethodDescriptionAtribute(methodDescription = "Set rgb values")]
        public void func_rgb(byte r = 0, byte g = 0, byte b = 0)
        {
            //Loads and sends the values to the moving head
            this.driver.DmxLoadBuffer(7, r, 13);
            this.driver.DmxLoadBuffer(8, g, 13);
            this.driver.DmxLoadBuffer(9, b, 13);
            this.driver.DmxSendCommand(3);
        }

        /// <summary>
        /// Sets the movement speed
        /// </summary>
        /// <param name="mSpeed">Movement speed value</param>
        [ParameterDataAtribute(parameterDescription = new string[] { "Moving speed" })]
        [MethodDescriptionAtribute(methodDescription = "Set movement speed")]
        public void func_setSpeed(byte mSpeed)
        {
            //Loads and sends the value to the moving head
            this.driver.DmxLoadBuffer(5, mSpeed, 13);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// Turns on a rainbow effect at a certain speed
        /// </summary>
        /// <param name="direction">Direction of color order</param>
        /// <param name="speed">Speed of color change</param>
        [ParameterDataAtribute(parameterDescription = new string[] { "Direction of color order", "Speed of color change" })]
        [MethodDescriptionAtribute(methodDescription = "Turn on a rainbow effect")]
        public void func_rainBow(bool direction,byte speed)
        {
            //Set default value clockwise
            byte d = 232;

            //Checks wether the value needs to be counter clockwise
            if(direction)
            {
                d = 255;
            }

            //Loads an sends the values to the moving head
            this.driver.DmxLoadBuffer(10, d, 13);
            this.driver.DmxLoadBuffer(11, (byte)(255 / speed + 1), 13);
            this.driver.DmxSendCommand(2);
        }

        /// <summary>
        /// Sets a gobo pattern
        /// </summary>
        /// <param name="gobo">Gobo name variable</param>
        [ParameterDataAtribute(parameterDescription = new string[] { "Gobo:\n- None\n- bananaSpiral\n- eclipseSun\n- flower\n- portal\n- heatWaves\n- virus\n- sun\n- tangledLines\n- bubbles" })]
        [MethodDescriptionAtribute(methodDescription = "Set a gobo")]
        public void func_gobo(string gobo)
        {
            //Set default gobo
            byte val = 0;

            //Selects gobo if given parameter is a valid gobo name
            if(this.goboMap.ContainsKey(gobo))
            {
                val = this.goboMap[gobo];
            }

            //Loads and sends the value to the moving head
            this.driver.DmxLoadBuffer(13, val, 13);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// Sets the direction and speed of gobo patterns
        /// </summary>
        /// <param name="clockWise">Sets the direction</param>
        /// <param name="speed"></param>
        [ParameterDataAtribute(parameterDescription = new string[] { "Direction of gobo order", "Speed of scrolling" })]
        [MethodDescriptionAtribute(methodDescription = "Turn on a scrollthrough the gobos")]
        public void func_goboScroll(bool clockWise, byte speed)
        {
            //Sets default value + the speed
            byte val = Convert.ToByte(128+speed/2);

            //Checks wether the rotation is clockWise or not
            if(!clockWise)
            {
                val = Convert.ToByte(val + 128);
            }

            //Loads and sends the value to the moving head
            this.driver.DmxLoadBuffer(13, val, 13);
            this.driver.DmxSendCommand(1);
        }
        #endregion
    }
}
