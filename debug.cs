using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace midiLightShow
{
    public partial class debug : Form
    {
        public debug()
        {
            InitializeComponent();
            Console.SetOut(new ControlWriter(rtbDebug));
        }

        private void debug_Load(object sender, EventArgs e)
        {

        }

        private void rtbDebug_TextChanged(object sender, EventArgs e)
        {
            rtbDebug.SelectionStart = rtbDebug.Text.Length;
            rtbDebug.ScrollToCaret();
        }
    }
    public class ControlWriter : TextWriter
    {
        private Control textbox;
        public ControlWriter(Control textbox)
        {
            this.textbox = textbox;
        }

        public override void Write(char value)
        {
            textbox.Text += value;
        }

        public override void Write(string value)
        {
            textbox.Text = "";
            textbox.Text += value;
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
}
