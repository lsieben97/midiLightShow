using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiLightShow
{
    /// <summary>
    /// Lazer class. Functions for the Lazer DMX light
    /// Inherited from dmxLight
    /// </summary>
    class lazer : dmxLight
    {
        #region Global variables
        private Dictionary<string, byte> patternMap = new Dictionary<string, byte>();

        bool lamp = false;
        bool color = false;

        #endregion
        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public lazer() { }
        /// <summary>
        /// Constructor for lazer class
        /// </summary>
        /// <param name="comPort">Connection port</param>
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
            byte speed = 0;
            bool clockWiseX = false;
            bool clockWiseY = false;

            //execute function switch case
            switch (function)
            {
                case "autoRun":
                    if(execute)
                    {
                        this.func_autoRun();
                    } else { return true; }
                    break;
                case "symbol":
                    if (execute)
                    {
                        this.func_symbol(parameters.ElementAt(0).Value);
                    } else { return true; }
                    break;
                case "toggleLamp":
                    if (execute)
                    {
                        this.func_toggleLamp();
                    } else { return true; }
                    break;
                case "color":
                    if (execute)
                    {
                        this.func_color();
                    } else { return true; }
                    break;
                case "colorCycle":
                    if (execute)
                    {
                        this.func_colorCycle();
                    } else { return true; }
                    break;
                case "frequency":
                    speed = 0;
                    if (byte.TryParse(parameters.ElementAt(0).Value, out speed))
                    {
                        if (execute)
                        {
                            this.func_frequency(speed);
                        } else { return true; }
                    } else { return false; }
                    break;
                case "zoom":
                    byte magnify = 0;
                    if (byte.TryParse(parameters.ElementAt(0).Value, out magnify))
                    {
                        if (execute)
                        {
                            this.func_zoom(magnify);
                        } else { return true; }
                    } else { return false; }
                    break;
                case "autoZoom":
                    speed = 0;
                    if (byte.TryParse(parameters.ElementAt(0).Value, out speed))
                    {
                        if (execute)
                        {
                            this.func_autoZoom(speed);
                        } else { return true; }
                    } else { return false; }
                    break;
                case "xy":
                    byte x = 0;
                    byte y = 0;
                    if (byte.TryParse(parameters.ElementAt(0).Value, out x) && byte.TryParse(parameters.ElementAt(1).Value, out y))
                    {
                        if (execute)
                        {
                            this.func_xy(x, y);
                        } else { return true; }
                    } else { return false; }
                    break;
                case "autoXY":
                    clockWiseX = false;
                    clockWiseY = false;
                    if (bool.TryParse(parameters.ElementAt(0).Value, out clockWiseX) && bool.TryParse(parameters.ElementAt(1).Value, out clockWiseY))
                    {
                        if (execute)
                        {
                            this.func_autoXY(clockWiseX, clockWiseY);
                        }
                        else { return true; }
                    }
                    else { return false; }
                    break;
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
                    break;
                case "setRotation":
                    break;
                case "autoRotation":
                    break;
            }
            return false;
        }
        #endregion
        #region Functions
        
        /// <summary>
        /// Makes the lazer run an automated program
        /// </summary>
        public void func_autoRun()
        {
            //Loads and sends the value to the lazer
            this.driver.DmxLoadBuffer(1,0,10);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// Sets a symbol pattern
        /// </summary>
        /// <param name="symbol">Symbol name variable</param>
        public void func_symbol(string symbol)
        {
            //Set default symbol
            byte val = 0;

            //Selects symbol if given parameter is a valid symbol name
            if (this.patternMap.ContainsKey(symbol))
            {
                val = this.patternMap[symbol];
            }

            //Loads and sends the value to the lazer
            this.driver.DmxLoadBuffer(2, val, 10);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// Switches the lamp state
        /// </summary>
        public void func_toggleLamp()
        {
            //Switch value
            if(lamp)
                lamp = false;
            else
                lamp = true;

            //Loads and sends the value to the lazer
            this.driver.DmxLoadBuffer(3, Convert.ToByte(Convert.ToByte(lamp) * 25), 10);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// Switches between green or red
        /// </summary>
        public void func_color()
        {
            //Switch value
            if (color)
                color = false;
            else
                color = true;

            //Loads and sends the value to the lazer
            this.driver.DmxLoadBuffer(3, Convert.ToByte(Convert.ToByte(color) * 25 + 50), 10);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// Sets a rainbow effect
        /// </summary>
        public void func_colorCycle()
        {
            //Loads and sends the value to the lazer
            this.driver.DmxLoadBuffer(3, 125, 10);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// Sets a frequency for either the color change or the stroboscope
        /// </summary>
        /// <param name="speed">Frequency speed</param>
        public void func_frequency(byte speed)
        {
            //Loads and sends the value to the lazer
            this.driver.DmxLoadBuffer(4, speed, 10);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// Makes the pattern symbol smaller
        /// </summary>
        /// <param name="magnify">Magnify scale</param>
        public void func_zoom(byte magnify)
        {
            //Loads and sends the value to the lazer
            this.driver.DmxLoadBuffer(5, (byte)(magnify / 2), 10);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// Makes it automatically zoom in and out
        /// </summary>
        /// <param name="speed">Speed of zooming</param>
        public void func_autoZoom(byte speed)
        {
            //Loads and sends the value to the lazer
            this.driver.DmxLoadBuffer(5, (byte)(speed / 255 * 45), 10);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// Sets location of center
        /// </summary>
        /// <param name="x">Sets the x location</param>
        /// <param name="y">Sets the y location</param>
        public void func_xy(byte x, byte y)
        {
            //Loads and sends the values to the lazer
            this.driver.DmxLoadBuffer(6, (byte)(x / 2), 10);
            this.driver.DmxLoadBuffer(7, (byte)(y / 2), 10);
            this.driver.DmxSendCommand(2);
        }

        /// <summary>
        /// Turns on an automatic movement of the pattern
        /// </summary>
        /// <param name="clockWiseX">Sets the x direction</param>
        /// <param name="clockWiseY">Sets the y direction</param>
        public void func_autoXY(bool clockWiseX, bool clockWiseY)
        {
            //Loads and sends the value to the lazer
            this.driver.DmxLoadBuffer(6, (byte)(Convert.ToByte(clockWiseX) * 64 + 128), 10);
            this.driver.DmxLoadBuffer(7, (byte)(Convert.ToByte(clockWiseY) * 64 + 128), 10);
            this.driver.DmxSendCommand(2);
        }

        /// <summary>
        /// Sets the horizontal rotation
        /// </summary>
        /// <param name="rotation">Horizontal rotation degree</param>
        [ParameterDataAtribute(parameterDescription = new string[] { "Rotation angle" })]
        [MethodDescriptionAtribute(methodDescription = "Rotate Horizontally")]
        public void func_setPan(uint rotation)
        {
            //Escape too big numbers to readable ones
            while (rotation > 360)
            {
                rotation = rotation - 360;
            }

            //Loads and sends the value to the lazer
            this.driver.DmxLoadBuffer(8, (byte)(rotation / 360 * 127), 10);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// Sets the vertical rotation
        /// </summary>
        /// <param name="rotation">Vertical rotation degree</param>
        [ParameterDataAtribute(parameterDescription = new string[] { "Rotation angle" })]
        [MethodDescriptionAtribute(methodDescription = "Rotate Vertically")]
        public void func_setTilt(uint rotation)
        {
            //Escape too big numbers to readable ones
            while (rotation > 360)
            {
                rotation = rotation - 360;
            }

            //Loads and sends the value to the lazer
            this.driver.DmxLoadBuffer(9, (byte)(rotation / 360 * 127), 10);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// Sets the surface rotation
        /// </summary>
        /// <param name="rotation">Surface rotation degree</param>
        public void func_setRotation(uint rotation)
        {
            //Escape too big numbers to readable ones
            while (rotation > 360)
            {
                rotation = rotation - 360;
            }

            //Loads and sends the value to the lazer
            this.driver.DmxLoadBuffer(10, (byte)(rotation / 360 * 127), 10);
            this.driver.DmxSendCommand(1);
        }

        /// <summary>
        /// Turns on automatic rotation
        /// </summary>
        /// <param name="clockWiseX">Sets the x direction</param>
        /// <param name="clockWiseY">Sets the y direction</param>
        public void func_autoRotate(bool clockWiseX, bool clockWiseY)
        {
            //Loads and sends the values to the lazer
            this.driver.DmxLoadBuffer(8, (byte)(Convert.ToByte(clockWiseX) * 64 + 128), 10);
            this.driver.DmxLoadBuffer(9, (byte)(Convert.ToByte(clockWiseY) * 64 + 128), 10);
            this.driver.DmxLoadBuffer(10, (byte)(Convert.ToByte(clockWiseY) * 64 + 128), 10);
            this.driver.DmxSendCommand(3);
        }
        #endregion
    }
}
