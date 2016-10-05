using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace midiLightShow
{
    /// <summary>
    /// form class for editing various track options
    /// </summary>
    public partial class TrackOptionsForm : Form
    {
        #region Global variables
        /// <summary>
        /// The port name for the DMX512 driver to connect to 
        /// </summary>
        public string portName = "";
        #endregion
        #region Constructors
        /// <summary>
        /// Default constructor from visual studio
        /// </summary>
        public TrackOptionsForm()
        {
            InitializeComponent();

        }
        #endregion
        #region TrackOptionsForm methods
        /// <summary>
        /// Click event handler for the ok button
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if(tbName.Text.Length > 0)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("You need to name this track!");
            }
        }

        /// <summary>
        /// Click event handler for the concel button
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Click event handler for the delete button
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.Close();
        }

        /// <summary>
        /// Load event handler for this form
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void TrackOptionsForm_Load(object sender, EventArgs e)
        {
            cbComPorts.Items.Clear();
            cbComPorts.Items.Add("None");
            string[] ports = SerialPort.GetPortNames();
            foreach(string port in ports)
            {
                cbComPorts.Items.Add(port);
            }
            if(cbComPorts.Items.Contains(this.portName))
            {
                cbComPorts.SelectedItem = this.portName;
            }
            else
            {
                cbComPorts.Text = this.portName;
            }
        }
        #endregion
    }
}
