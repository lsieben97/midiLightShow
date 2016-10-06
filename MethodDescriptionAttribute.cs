using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiLightShow
{
    /// <summary>
    /// Custom attribute for sending a method description to the addShowEvent dialog
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class MethodDescriptionAtribute : Attribute
    {
        #region Global variables
        /// <summary>
        /// The description for the method that has this attribute
        /// </summary>
        public string methodDescription;
        #endregion
        #region Constructors
        /// <summary>
        /// Create a new methodDescriptionAttribute object with the specified description
        /// </summary>
        /// <param name="methodDescription">The description for the method</param>
        public MethodDescriptionAtribute(string methodDescription)
        {
            this.methodDescription = methodDescription;
        }
        /// <summary>
        /// Empty constructor
        /// </summary>
        public MethodDescriptionAtribute()
        {

        }
        #endregion
    }
}
