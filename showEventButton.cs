using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace midiLightShow
{
    public class showEventButton : Button
    {
        private bool dragging = false;
        public string name = "";
        public showEventButton()
        {
            this.MouseDown += showEventButton_MouseDown;
        }
        public void showEventButton_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left && this.dragging == false)
            {
                this.DoDragDrop(this.name, DragDropEffects.All);
                this.dragging = true;
            }
        }
    }
}
