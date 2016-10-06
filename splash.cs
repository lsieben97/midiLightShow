using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace midiLightShow
{
    /// <summary>
    /// Class for showing the splash screen on startup
    /// </summary>
    public partial class splash : Form
    {
        #region Global variables
        /// <summary>
        /// Timer to delay the showing of the editor form
        /// </summary>
        private Timer formTimer = new Timer();
        #endregion
        #region Constructors
        /// <summary>
        /// Create a new splash screen (only at startup)
        /// </summary>
        public splash()
        {
            InitializeComponent();
            this.formTimer.Interval = 3000;
            this.formTimer.Tick += formTimer_Tick;
        }
        #endregion
        #region Splash methods
        /// <summary>
        /// Tick event handler for the form timer
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        void formTimer_Tick(object sender, EventArgs e)
        {
            this.formTimer.Stop();
            this.Hide();
            frmEditor ed = new frmEditor();
            ed.Show();
        }

        /// <summary>
        /// Load event handler for this splash screen
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void splash_Load(object sender, EventArgs e)
        {
            this.formTimer.Start();
        }
        #endregion
    }
}
