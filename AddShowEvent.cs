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
    /// <summary>
    /// Form class for adding and editing showEvents
    /// </summary>
    public partial class AddShowEvent : Form
    {
        #region Global variables
        /// <summary>
        /// The dmxLight object required to acces the executeFunction method to check parameters
        /// </summary>
        public dmxLight light;
        /// <summary>
        /// List of methods that are present in the selected light class
        /// </summary>
        public List<MethodInfo> functions = new List<MethodInfo>();
        /// <summary>
        /// The duration of the event being added/edited
        /// </summary>
        public int duration = 100;
        /// <summary>
        /// List of parameter controls to display parameters
        /// </summary>
        public List<Control> parameterControls = new List<Control>();
        /// <summary>
        /// The start time of the event being added/edited
        /// </summary>
        public int startTime = 0;
        /// <summary>
        /// The default vertical space between two parameter controls
        /// </summary>
        private int controlOffset = 15;
        /// <summary>
        /// Indicates if this form is used as edit form or as add form
        /// </summary>
        public bool isEditForm = false;
        /// <summary>
        /// The original function name used when editing an existing showEvent
        /// </summary>
        public string originalFunction = "";
        /// <summary>
        /// Animation timer to animate the parameter controls
        /// </summary>
        public Timer aniTimer = new Timer();
        /// <summary>
        /// Indicates how many controls have been animated
        /// </summary>
        private int controlsDone = 0;
        /// <summary>
        /// The parameter string for the showEvent
        /// </summary>
        public string paraString = "";
        /// <summary>
        /// List with all the parameters from the current function
        /// </summary>
        public List<ParameterInfo> parameterList = new List<ParameterInfo>();
        /// <summary>
        /// Dictionary of parameter name - parameter value pairs
        /// </summary>
        public List<string> parameters = new List<string>();
        #endregion
        #region Constructors
        /// <summary>
        /// Create new adShowEvent form
        /// </summary>
        public AddShowEvent()
        {
            InitializeComponent();
            aniTimer.Interval = 200;
            aniTimer.Tick += aniTimer_Tick;
        }
        #endregion
        #region AddShowEvent methods
        /// <summary>
        /// Tick event handler for the animation timer
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
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
        /// <summary>
        /// Load event handler for this form
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void AddShowEvent_Load(object sender, EventArgs e)
        {
            init();
        }

        /// <summary>
        /// Initialize functions and get parameters for the current function
        /// </summary>
        private void init()
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
        /// <summary>
        /// Gets a comma seperated string of parameter descriptions for the specified method
        /// </summary>
        /// <param name="m">methodInfo object from the method to get parameter descriptions from</param>
        /// <returns>Comma seperated string with parameter descriptions</returns>
        private string getParameterDescription(MethodInfo m)
        {
            ParameterDataAtribute a = m.GetCustomAttribute(new ParameterDataAtribute().GetType(), false) as ParameterDataAtribute;
            return string.Join("", a.parameterDescription);
        }

        /// <summary>
        /// Gets the method description for the specified method
        /// </summary>
        /// <param name="m">methodInfo object to get the method description from</param>
        /// <returns>String with method description</returns>
        private string getMethodDiscription(MethodInfo m)
        {
            MethodDescriptionAtribute a = m.GetCustomAttribute(new MethodDescriptionAtribute().GetType(), false) as MethodDescriptionAtribute;
            return a.methodDescription;
        }

        /// <summary>
        /// SelectedIndexChanged event handler for the functions combobox
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void cbFunctions_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateParameters();
        }

        /// <summary>
        /// Get parameter descriptions and method description from the newly chosen function
        /// </summary>
        private void updateParameters()
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
                    case "UInt32":
                        TextBox tbpara = new TextBox();
                        tbpara.Tag = p.Name;
                        tbpara.BackColor = SystemColors.ControlDark;
                        tbpara.ForeColor = SystemColors.HotTrack;
                        tbpara.BorderStyle = BorderStyle.FixedSingle;
                        tbpara.Visible = false;
                        this.parameterControls.Add(tbpara);
                        break;
                    case "Boolean":
                        CheckBox cbpara = new CheckBox();
                        cbpara.Tag = p.Name;
                        cbpara.BackColor = SystemColors.ControlDark;
                        cbpara.ForeColor = SystemColors.HotTrack;
                        cbpara.Visible = false;
                        this.parameterControls.Add(cbpara);
                        break;
                    default:
                        break;
                }
            }
            this.drawParameterControls();
            this.fillParameters();
        }

        /// <summary>
        /// Gets an array of parameterInfo objects representing the parameters of the methods index in the global function list 
        /// </summary>
        /// <param name="index">Index of the method in the global function list</param>
        /// <returns>Array of parameterInfo objects</returns>
        private ParameterInfo[] getParameters(int index)
        {
            return this.functions[index].GetParameters();
        }

        /// <summary>
        /// Draw the parameter controls of the parameters from the current function
        /// </summary>
        private void drawParameterControls()
        {
            pParameterControls.Controls.Clear();
            foreach (Control parameterControl in this.parameterControls)
            {
                parameterControl.Location = new Point(60, this.pParameterControls.Controls.Count * this.controlOffset);
                Label lbParameterName = new Label();
                lbParameterName.Text = parameterControl.Tag.ToString() + ":";
                lbParameterName.ForeColor = SystemColors.Highlight;
                lbParameterName.Location = new Point(5, this.pParameterControls.Controls.Count * this.controlOffset);
                lbParameterName.Visible = false;
                pParameterControls.Controls.Add(parameterControl);
                pParameterControls.Controls.Add(lbParameterName);

            }
            this.controlsDone = 0;
            this.aniTimer.Start();
        }

        /// <summary>
        /// Click event handler for the ok button
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            checkAndSendInput();
        }

        /// <summary>
        /// Check and if valid send the input to the track
        /// </summary>
        private void checkAndSendInput()
        {
            if (int.TryParse(tbDuration.Text, out this.duration) == true && int.TryParse(tbStartTime.Text, out this.startTime) == true)
            {
                this.prepareParameters();

                if (this.light.executeFunction(cbFunctions.Text, this.parameters))
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid parameters!");
                }
            }
            else
            {
                MessageBox.Show("Invalid duration!");
            }
        }

        /// <summary>
        /// Click event handler for the cancel button
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Reset this form for re-use
        /// </summary>
        public void reset()
        {
            this.duration = 0;
            tbDuration.Text = "";
            cbFunctions.Items.Clear();
            tbParameters.Clear();
            this.functions.Clear();
            this.parameterList.Clear();
            this.parameters.Clear();
        }

        /// <summary>
        /// Click event handler for the remove button
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.Close();
        }

        /// <summary>
        /// Prepare the entered parameters to be send to the showEvent object by the track
        /// </summary>
        private void prepareParameters()
        {
            this.parameters.Clear();
            for (int i = 0; i < this.parameterList.Count(); i++)
            {
                switch (this.parameterControls[i].GetType().Name)
                {
                    case "TextBox":
                    case "ComboBox":
                        this.parameters.Add(this.parameterControls[i].Text);
                        break;
                    case "CheckBox":
                        CheckBox para = this.parameterControls[i] as CheckBox;
                        this.parameters.Add(para.Checked.ToString());
                        break;
                }
                this.paraString = string.Join(",", this.parameters);

            }
        }

        /// <summary>
        /// Enter the correct values into the textboxes when editing an existing showEvent
        /// </summary>
        public void fillParameters()
        {
            if (this.cbFunctions.Text == this.originalFunction)
            {
                for(int i = 0; i < this.parameterControls.Count; i++)
                {
                    this.parameterControls[i].Text = this.parameters[i];
                }
            }
        }
        #endregion
    }
}
