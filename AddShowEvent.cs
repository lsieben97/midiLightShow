using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace midiLightShow
{
    public partial class AddShowEvent : Form
    {
        public dmxLight light;
        public List<MethodInfo> functions = new List<MethodInfo>();
        public AddShowEvent()
        {
            InitializeComponent();
        }

        private void AddShowEvent_Load(object sender, EventArgs e)
        {
            Type t = this.light.GetType();
            MethodInfo[] methInfo = t.GetMethods();
            MethodInfo[] valMethods = methInfo.Where(meth => meth.Name.StartsWith("func_")).ToArray();
            foreach (MethodInfo m in valMethods)
            {
                this.functions.Add(m);
            }
            foreach(MethodInfo m in this.functions)
            {
                cbFunctions.Items.Add(m.Name.Substring(5));
            }
        }
        private string getParameterDescription(MethodInfo m)
        {
            ParameterDataAtribute a = m.GetCustomAttribute(new ParameterDataAtribute().GetType(), false) as ParameterDataAtribute;
            return string.Join(",",a.parameterDescription);
        }

        private void cbFunctions_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbParaDescription.Text = this.getParameterDescription(this.functions[cbFunctions.SelectedIndex]);
            lbParaDescription.Visible = true;
        }
    }
}
