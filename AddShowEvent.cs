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
        public int startTime = 0;
        public bool isEditForm = false;
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
            cbFunctions.SelectedIndex = 0;
            btnRemove.Visible = this.isEditForm;
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
            rtbParameterDescription.Text = this.getParameterDescription(this.functions[cbFunctions.SelectedIndex]);
            rtbFunctionDescription.Text = this.getMethodDiscription(this.functions[cbFunctions.SelectedIndex]);
        }

        
        private object checkConversion(string value, Type target)
        {
            return Convert.ChangeType(value, target);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbDuration.Text, out this.duration) == true && int.TryParse(tbStartTime.Text, out this.startTime) == true)
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

        private void btnRemove_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.Close();
        }
    }
}
