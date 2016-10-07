using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dmx512UsbRs485;

namespace midiLightShow
{
    /// <summary>
    /// Base DMX light class
    /// Parent class
    /// </summary>
    public class dmxLight
    {
        #region Global variables
        /// <summary>
        /// DMX 512 driver to communicate with DMX lights
        /// </summary>
        public Dmx512UsbRs485Driver driver = new Dmx512UsbRs485Driver();
        /// <summary>
        /// The port to communicate between the application and the DMX lights
        /// </summary>
        public string comPort = "";
        /// <summary>
        /// Indicates if there is a DMX light connected
        /// </summary>
        public bool live = false;
        #endregion
        #region DMX light methods
        /// <summary>
        /// execute function method base, will be overriden by inherited classes
        /// </summary>
        /// <param name="function">Function given from track</param>
        /// <param name="parameters">Paramaters given with the function</param>
        /// <param name="execute">Indicator variable for executing the function</param>
        /// <returns>Parameter validation</returns>
        public virtual bool executeFunction(string function, Dictionary<string, string> parameters, bool execute = false) { return true; }
        /// <summary>
        /// Sets up the DMX 512 driver
        /// </summary>
        public void connectDMX()
        {
            if (this.live)
            {
                try
                {
                    this.driver.DmxToDefault(this.comPort);
                }
                catch
                {
                    MessageBox.Show("An error occured when connecting to this light, when not connected uncheck 'enable'.");
                }
            }
        }
        #endregion
    }
}
