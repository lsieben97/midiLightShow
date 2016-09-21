using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiLightShow
{
    [AttributeUsage(AttributeTargets.Method,Inherited = false,AllowMultiple = false)]
    public class ParameterDataAtribute : Attribute
    {
        public string[] parameterDescription;
        public ParameterDataAtribute(string[] parameterDescription)
        {
            this.parameterDescription = parameterDescription;
        }
        public ParameterDataAtribute()
        {

        }
    }
}
