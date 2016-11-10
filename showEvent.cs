using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Serialization;

namespace midiLightShow
{
    /// <summary>
    /// Class that represents a show event in a lightshow track object
    /// </summary>
    public class showEvent
    {
        #region Global variables
        /// <summary>
        /// The start time of this show event in 16th beats
        /// </summary>
        public int startTime;
        /// <summary>
        /// The duration of this show event in 16th beats
        /// </summary>
        public int duration;
        /// <summary>
        /// Bounds of the show event on the timeline. Used to check if the user clicks on this show event
        /// </summary>
        public Rectangle bounds;
        /// <summary>
        /// The function to call when this event needs to be executed
        /// </summary>
        public string function;
        /// <summary>
        /// String of the parameters of this show event
        /// </summary>
        public string paraString;
        /// <summary>
        /// Dictionary with 'parameter name, parameter value' pairs.
        /// </summary>
        
        public List<string> parameters;
        /// <summary>
        /// The index of this show event. Used for deleting this show event
        /// </summary>
        public int index = 0;
        /// <summary>
        /// Indicates of this show event should be executed
        /// </summary>
        public bool canPlay = true;
        public byte note;
        public bool selected = false;
        #endregion
        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public showEvent() { }

        /// <summary>
        /// Create an new showEvent object with the specified parameters
        /// </summary>
        /// <param name="startTime">The start time of the show event in 16th beats</param>
        /// <param name="duration">The duration of the show event in 16th beats</param>
        /// <param name="function">The function to call when the event is executed</param>
        /// <param name="parameters">The parameters to send when executing the show event</param>
        /// <param name="paraString">The parameter string for the show event</param>
        /// <param name="index">The index for the show event</param>
        public showEvent(int startTime, int duration, string function, string paraString, int index)
        {
            this.startTime = startTime;
            this.duration = duration;
            this.function = function;
            this.paraString = paraString;
            this.parameters = parameters;
            this.index = index;
        }
        #endregion
    }
}
