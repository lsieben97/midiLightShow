using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiLightShow
{
    /// <summary>
    /// Custom attribute for sending parameter descriptions to the addShowEvent dialog
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,Inherited = false,AllowMultiple = false)]
    public class ParameterDataAtribute : Attribute
    {
        #region Global variables
        /// <summary>
        /// The parameter descriptions to send
        /// </summary>
        public string[] parameterDescription;
        #endregion
        #region Constructors
        /// <summary>
        /// Create a new parameterDataAttribute object with the specified descriptions
        /// </summary>
        /// <param name="parameterDescription">The descriptions of the parameters from the method this attribute is linked to</param>
        public ParameterDataAtribute(string[] parameterDescription)
        {
            this.parameterDescription = parameterDescription;
        }
        /// <summary>
        /// Default empty constructor
        /// </summary>
        public ParameterDataAtribute()
        {

        }
        #endregion
    }
}
