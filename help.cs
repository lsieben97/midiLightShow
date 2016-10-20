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
    public partial class help : Form
    {
        private Dictionary<string, string> helpArticles = new Dictionary<string, string>();
        public help()
        {
            InitializeComponent();
        }

        private void help_Load(object sender, EventArgs e)
        {
            string[] heads = Directory.GetDirectories("Manual");
            foreach(string head in heads)
            {
                TreeNode t = new TreeNode();
                t.BackColor = SystemColors.ControlDarkDark;
                t.ForeColor = SystemColors.Highlight;
                t.Text = Path.GetFileName(head);
                t.Name = Path.GetFileName(head);
                t.Tag = head;
                this.tvIndex.Nodes.Add(t);
            }
            foreach(TreeNode t  in this.tvIndex.Nodes)
            {
                string[] subParagraphs = Directory.GetFiles(t.Tag.ToString());
                foreach(string file in subParagraphs)
                {
                    string name = Path.GetFileNameWithoutExtension(file);
                    if(name == t.Text)
                    {
                        this.helpArticles.Add(t.Text, File.ReadAllText(file));
                    }
                    else
                    {
                        TreeNode subNode = new TreeNode();
                        subNode.BackColor = SystemColors.ControlDarkDark;
                        subNode.ForeColor = SystemColors.Highlight;
                        subNode.Text = name;
                        subNode.Tag = file;
                        t.Nodes.Add(subNode);
                        this.helpArticles.Add(subNode.Text, File.ReadAllText(file));
                    }
                }
            }
            foreach(TreeNode t in this.tvIndex.Nodes)
            {
                t.Toggle();
            }
            this.tvIndex.SelectedNode = this.tvIndex.Nodes.Find("Welcome", false)[0];
            rtbHelpContent.Text = this.helpArticles[this.tvIndex.SelectedNode.Text];
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tvIndex_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            rtbHelpContent.Text = this.helpArticles[e.Node.Text];
        }
    }
}
