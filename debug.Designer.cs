namespace midiLightShow
{
    partial class debug
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbDebug = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbDebug
            // 
            this.rtbDebug.Location = new System.Drawing.Point(16, 15);
            this.rtbDebug.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rtbDebug.Name = "rtbDebug";
            this.rtbDebug.ReadOnly = true;
            this.rtbDebug.Size = new System.Drawing.Size(640, 260);
            this.rtbDebug.TabIndex = 0;
            this.rtbDebug.Text = "";
            this.rtbDebug.TextChanged += new System.EventHandler(this.rtbDebug_TextChanged);
            // 
            // debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 290);
            this.Controls.Add(this.rtbDebug);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "debug";
            this.Text = "debug";
            this.Load += new System.EventHandler(this.debug_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbDebug;
    }
}