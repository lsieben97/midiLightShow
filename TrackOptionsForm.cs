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
    public partial class TrackOptionsForm : Form
    {
        public string portName = "";
        public TrackOptionsForm()
        {
            InitializeComponent();

        }

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.Close();
        }

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
    }
}
