using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmx512UsbRs485;

namespace midiLightShow
{
    class discLight : dmxLight
    {
        Dmx512UsbRs485Driver driver = new Dmx512UsbRs485Driver();

        public discLight(string comPort)
        {
            this.comPort = comPort;
            this.driver.DmxToDefault(this.comPort);
        }

        //Effect functions
        public void controls(string function)
        {
            byte b = 0;

            switch(function)
            {
                case "off":
                    b = 1;
                    break;

                case "on":
                    b = 66;
                    break;

                case "auto":
                    b = 255;
                    break;
            }

            if(b > 0)
            {
                this.driver.DmxLoadBuffer(1, 0, 8);
                this.driver.DmxSendCommand(1);
            }
            else
            {
                Console.WriteLine("Invalid function \"" + function + "\" in control(" + function + ")");
            }
            
        }

        public void motor (string function, int speed)
        {
            byte b = 0;

            switch(function)
            {
                case "counterClockWise":
                    b = 1;
                    break;

                case "clockWise":
                    b = 86;
                    break;

                case "shake":
                    b = 171;
                    break;
            }

            if(b > 0 && speed < 85)
            {
                b = Convert.ToByte(b + speed);
            }
            else if(speed > 84)
            {
                Console.WriteLine("Error: Motor speed limit exceeded! [Speed > 84 in motor("+function + speed.ToString()+")]");
            }
            else
            {
                Console.WriteLine("Invalid function \"" + function + "\" in motor(" + function + speed.ToString() + ")");
            }
        }

    }
}
