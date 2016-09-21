using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiLightShow
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class MethodDescriptionAtribute : Attribute
    {
        public string methodDescription;
        public MethodDescriptionAtribute(string methodDescription)
        {
            this.methodDescription = methodDescription;
        }
        public MethodDescriptionAtribute()
        {

        }
    }
}
