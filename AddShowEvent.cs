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
        public int duration = 100;
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
            return string.Join(", ",a.parameterDescription);
        }

        private string getMethodDiscription(MethodInfo m)
        {
            MethodDescriptionAtribute a = m.GetCustomAttribute(new MethodDescriptionAtribute().GetType(), false) as MethodDescriptionAtribute;
            return a.methodDescription;
        }

        private void cbFunctions_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbParaDescription.Text = this.getParameterDescription(this.functions[cbFunctions.SelectedIndex]);
            lbParaDescription.Visible = true;
            lbMethodDescription.Text = this.getMethodDiscription(this.functions[cbFunctions.SelectedIndex]);
            lbMethodDescription.Visible = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(int.TryParse(tbDuration.Text,out this.duration))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid duration!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        public void reset()
        {
            this.duration = 0;
            tbDuration.Text = "";
            cbFunctions.SelectedIndex = 0;
            cbFunctions.Items.Clear();
            tbParameters.Clear();
            this.functions.Clear();
        }

        private void btnEmpty_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbDuration.Text, out this.duration))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Ignore;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid duration!");
            }
        }
    }
}
