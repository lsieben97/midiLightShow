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
    #region Debug
    /// <summary>
    /// Debug form for debuging
    /// </summary>
    public partial class debug : Form
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public debug()
        {
            InitializeComponent();
            Console.SetOut(new ControlWriter(rtbDebug));
            this.menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomProfessionalColors());
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.GetWorkingArea(this).Width - this.Size.Width, 0);
        }
        #endregion
        #region Debug methods
        /// <summary>
        /// TextChanged event handler for the information rich text box. scrolls down to the last line
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void rtbDebug_TextChanged(object sender, EventArgs e)
        {
            rtbDebug.SelectionStart = rtbDebug.Text.Length;
            rtbDebug.ScrollToCaret();
        }
        #endregion

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
    #endregion
    #region ControlWriter
    public class ControlWriter : TextWriter
    {
        #region Global variables
        /// <summary>
        /// Represents the control to write the text to
        /// </summary>
        private Control textbox;
        #endregion
        #region Constructors
        /// <summary>
        /// Create a new ControlWriter object to write to the specified control
        /// </summary>
        /// <param name="textbox">The control to write to</param>
        public ControlWriter(Control textbox)
        {
            this.textbox = textbox;
        }
        #endregion
        #region ControlWriter methods
        /// <summary>
        /// Write the char value to the control
        /// </summary>
        /// <param name="value">Char to write to the control</param>
        public override void Write(char value)
        {
            textbox.Text += value;
        }

        /// <summary>
        /// Write the string value to the control
        /// </summary>
        /// <param name="value">String to write</param>
        public override void Write(string value)
        {
            textbox.Text = "";
            textbox.Text += value;
        }

        /// <summary>
        /// Sets the encoding to be ASCII
        /// </summary>
        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
        #endregion
    }
    #endregion
}
