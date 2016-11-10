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
        private Dictionary<string, Image> helpPictures = new Dictionary<string, Image>();
        public help()
        {
            InitializeComponent();
            this.menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomProfessionalColors());
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
                        string[] contents = File.ReadAllLines(file);
                        if(contents[0].StartsWith("#USE_PIC"))
                        {
                            if(File.Exists(contents[1].Trim()))
                            {
                                this.helpPictures.Add(name,Image.FromFile(contents[1].Trim(),true));
                            }
                            this.helpPictures[name].Tag = new System.Drawing.Size(Convert.ToInt32(contents[2]), Convert.ToInt32(contents[3]));
                            contents = contents.ToList().GetRange(5, contents.Length - 5).ToArray();
                            this.helpArticles.Add(t.Text, string.Join("\n", contents));
                        }
                        else
                        {
                            this.helpArticles.Add(t.Text, string.Join("\n",contents));
                        }
                        
                    }
                    else
                    {
                        TreeNode subNode = new TreeNode();
                        subNode.BackColor = SystemColors.ControlDarkDark;
                        subNode.ForeColor = SystemColors.Highlight;
                        subNode.Text = name;
                        subNode.Tag = file;
                        string[] contents = File.ReadAllLines(file);
                        if (contents[0].StartsWith("#USE_PIC"))
                        {
                            if (File.Exists(contents[1].Trim()))
                            {
                                this.helpPictures.Add(name, Image.FromFile(contents[1].Trim(), true));
                            }
                            this.helpPictures[name].Tag = new System.Drawing.Size(Convert.ToInt32(contents[2]), Convert.ToInt32(contents[3]));
                            contents = contents.ToList().GetRange(5, contents.Length - 5).ToArray();
                            this.helpArticles.Add(subNode.Text, string.Join("\n", contents));
                        }
                        else
                        {
                            this.helpArticles.Add(subNode.Text, string.Join("\n", contents));
                        }
                        t.Nodes.Add(subNode);
                    }
                }
            }
            foreach(TreeNode t in this.tvIndex.Nodes)
            {
                t.Toggle();
            }
            this.tvIndex.SelectedNode = this.tvIndex.Nodes.Find("Welcome", false)[0];
            rtbHelpContent.Text = this.helpArticles[this.tvIndex.SelectedNode.Text];
            pbArticle.Image = this.helpPictures[this.tvIndex.SelectedNode.Text];
            System.Drawing.Size picSize = (System.Drawing.Size) this.helpPictures[this.tvIndex.SelectedNode.Text].Tag;
            pbArticle.Size = picSize;
            tbTitle.Text = "User manual - " + this.tvIndex.SelectedNode.FullPath;
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tvIndex_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            rtbHelpContent.Text = this.helpArticles[e.Node.Text];
            if(this.helpPictures.ContainsKey(e.Node.Text))
            {
                pbArticle.Image = this.helpPictures[e.Node.Text];
                System.Drawing.Size picSize = (System.Drawing.Size)this.helpPictures[e.Node.Text].Tag;
                this.pbArticle.Size = picSize;
            }
            else
            {
                pbArticle.Image = null;
            }
            tbTitle.Text = "User manual - " + e.Node.FullPath;
        }
    }
}
