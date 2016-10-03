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
        public List<Control> parameterControls = new List<Control>();
        public int startTime = 0;
        private int controlOffset = 15;
        public bool isEditForm = false;
        public string originalFunction = "";
        public Timer aniTimer = new Timer();
        private int controlsDone = 0;
        public string paraString = "";
        public List<ParameterInfo> parameterList = new List<ParameterInfo>();
        public List<Type> parameterTypes = new List<Type>();
        public Dictionary<string, string> parameters = new Dictionary<string, string>();
        public AddShowEvent()
        {
            InitializeComponent();
            aniTimer.Interval = 200;
            aniTimer.Tick += aniTimer_Tick;
        }

        void aniTimer_Tick(object sender, EventArgs e)
        {
            if (this.controlsDone != this.pParameterControls.Controls.Count)
            {
                this.pParameterControls.Controls[this.controlsDone].Visible = true;
                this.controlsDone++;
            }
            else
            {
                this.aniTimer.Stop();
            }
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
            foreach (MethodInfo m in this.functions)
            {
                cbFunctions.Items.Add(m.Name.Substring(5));
            }
            cbFunctions.SelectedIndex = 0;
            btnRemove.Visible = this.isEditForm;
        }
        private string getParameterDescription(MethodInfo m)
        {
            ParameterDataAtribute a = m.GetCustomAttribute(new ParameterDataAtribute().GetType(), false) as ParameterDataAtribute;
            return string.Join(", ", a.parameterDescription);
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
            this.parameterList = this.getParameters(cbFunctions.SelectedIndex).ToList(); ;
            this.parameterControls.Clear();
            foreach (ParameterInfo p in this.parameterList)
            {
                switch (p.ParameterType.Name)
                {
                    case "String":
                    case "Byte":
                        TextBox tbpara = new TextBox();
                        tbpara.Tag = p.Name;
                        tbpara.Visible = false;
                        this.parameterControls.Add(tbpara);
                        break;
                    default:
                        break;
                }
            }
            this.drawParameterControls();
            this.fillParameters();

        }
        private ParameterInfo[] getParameters(int index)
        {
            return this.functions[index].GetParameters();
        }

        private void drawParameterControls()
        {
            pParameterControls.Controls.Clear();
            foreach (Control parameterControl in this.parameterControls)
            {
                parameterControl.Location = new Point(60, this.pParameterControls.Controls.Count * this.controlOffset);
                Label lbParameterName = new Label();
                lbParameterName.Text = parameterControl.Tag.ToString();
                lbParameterName.Location = new Point(5, this.pParameterControls.Controls.Count * this.controlOffset);
                lbParameterName.Visible = false;
                pParameterControls.Controls.Add(parameterControl);
                pParameterControls.Controls.Add(lbParameterName);

            }
            this.controlsDone = 0;
            this.aniTimer.Start();
        }
        private object checkConversion(string value, Type target)
        {
            return Convert.ChangeType(value, target);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            if (int.TryParse(tbDuration.Text, out this.duration) == true && int.TryParse(tbStartTime.Text, out this.startTime) == true)
            {
                this.prepareParameters();
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
            cbFunctions.Items.Clear();
            tbParameters.Clear();
            this.functions.Clear();
            this.parameterList.Clear();
            this.parameters.Clear();
            this.parameterTypes.Clear();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.Close();
        }

        private void prepareParameters()
        {
            this.parameters.Clear();
            for (int i = 0; i < this.parameterList.Count(); i++)
            {
                switch (this.parameterControls[i].GetType().Name)
                {
                    case "TextBox":
                    case "ComboBox":
                        this.parameters.Add(this.parameterList.ElementAt(i).Name, this.parameterControls[i].Text);
                        break;
                }
                this.parameterTypes.Add(this.parameterList[i].ParameterType);
                this.paraString = string.Join(",", this.parameters.Values);

            }
        }

        public void fillParameters()
        {
            if (this.cbFunctions.Text == this.originalFunction)
            {
                foreach (KeyValuePair<string, string> para in this.parameters)
                {
                    this.parameterControls.Find(pc => pc.Tag.ToString() == para.Key).Text = para.Value;
                }
            }
        }
    }
}
