using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiLightShow
{
    public static class DMXStudioMessageBox
    {
        public static System.Windows.Forms.DialogResult Show(string contents, string title = "", System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.OK)
        {
            customMessageBox cm = new customMessageBox();
            if(buttons == System.Windows.Forms.MessageBoxButtons.YesNo)
            {
                cm.btnOk.Visible = false;
                cm.btnYes.Visible = true;
                cm.btnNo.Visible = true;
            }
            else
            {
                cm.btnNo.Visible = false;
                cm.btnYes.Visible = false;
            }
            cm.tbTitle.Text = title;
            cm.rtbContent.Text = contents;
            return cm.ShowDialog();
        }
    }
}
