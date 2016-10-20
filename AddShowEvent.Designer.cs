namespace midiLightShow
{
    partial class AddShowEvent
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbDuration = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbFunctions = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbParameters = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbStartTime = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.rtbFunctionDescription = new System.Windows.Forms.RichTextBox();
            this.rtbParameterDescription = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pParameterControls = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tbTitle = new System.Windows.Forms.ToolStripTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(13, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Duration (in 16th beats):";
            // 
            // tbDuration
            // 
            this.tbDuration.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tbDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDuration.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tbDuration.Location = new System.Drawing.Point(146, 58);
            this.tbDuration.Name = "tbDuration";
            this.tbDuration.Size = new System.Drawing.Size(210, 20);
            this.tbDuration.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(86, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Function:";
            // 
            // cbFunctions
            // 
            this.cbFunctions.BackColor = System.Drawing.SystemColors.ControlDark;
            this.cbFunctions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFunctions.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbFunctions.FormattingEnabled = true;
            this.cbFunctions.Location = new System.Drawing.Point(146, 83);
            this.cbFunctions.Name = "cbFunctions";
            this.cbFunctions.Size = new System.Drawing.Size(210, 21);
            this.cbFunctions.TabIndex = 3;
            this.cbFunctions.SelectedIndexChanged += new System.EventHandler(this.cbFunctions_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(31, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Function parameters:";
            // 
            // tbParameters
            // 
            this.tbParameters.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tbParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbParameters.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tbParameters.Location = new System.Drawing.Point(146, 172);
            this.tbParameters.Name = "tbParameters";
            this.tbParameters.Size = new System.Drawing.Size(210, 20);
            this.tbParameters.TabIndex = 5;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnOk.Location = new System.Drawing.Point(16, 476);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCancel.Location = new System.Drawing.Point(412, 476);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label4.Location = new System.Drawing.Point(32, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Function description:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label5.Location = new System.Drawing.Point(9, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Start time (in 16th beats):";
            // 
            // tbStartTime
            // 
            this.tbStartTime.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tbStartTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbStartTime.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tbStartTime.Location = new System.Drawing.Point(146, 36);
            this.tbStartTime.Name = "tbStartTime";
            this.tbStartTime.Size = new System.Drawing.Size(210, 20);
            this.tbStartTime.TabIndex = 1;
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnRemove.Location = new System.Drawing.Point(208, 476);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 12;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // rtbFunctionDescription
            // 
            this.rtbFunctionDescription.BackColor = System.Drawing.SystemColors.ControlDark;
            this.rtbFunctionDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbFunctionDescription.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.rtbFunctionDescription.Location = new System.Drawing.Point(146, 110);
            this.rtbFunctionDescription.Name = "rtbFunctionDescription";
            this.rtbFunctionDescription.ReadOnly = true;
            this.rtbFunctionDescription.Size = new System.Drawing.Size(210, 56);
            this.rtbFunctionDescription.TabIndex = 13;
            this.rtbFunctionDescription.Text = "";
            // 
            // rtbParameterDescription
            // 
            this.rtbParameterDescription.BackColor = System.Drawing.SystemColors.ControlDark;
            this.rtbParameterDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbParameterDescription.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.rtbParameterDescription.Location = new System.Drawing.Point(146, 198);
            this.rtbParameterDescription.Name = "rtbParameterDescription";
            this.rtbParameterDescription.ReadOnly = true;
            this.rtbParameterDescription.Size = new System.Drawing.Size(210, 106);
            this.rtbParameterDescription.TabIndex = 13;
            this.rtbParameterDescription.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label6.Location = new System.Drawing.Point(23, 242);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Parameter Description:";
            // 
            // pParameterControls
            // 
            this.pParameterControls.Location = new System.Drawing.Point(146, 310);
            this.pParameterControls.Name = "pParameterControls";
            this.pParameterControls.Size = new System.Drawing.Size(341, 160);
            this.pParameterControls.TabIndex = 15;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbTitle});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(495, 27);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tbTitle
            // 
            this.tbTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbTitle.ForeColor = System.Drawing.SystemColors.Highlight;
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.ReadOnly = true;
            this.tbTitle.Size = new System.Drawing.Size(300, 23);
            // 
            // AddShowEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(495, 511);
            this.Controls.Add(this.pParameterControls);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.rtbParameterDescription);
            this.Controls.Add(this.rtbFunctionDescription);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbParameters);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbFunctions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbStartTime);
            this.Controls.Add(this.tbDuration);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AddShowEvent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add event";
            this.Load += new System.EventHandler(this.AddShowEvent_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.ComboBox cbFunctions;
        public System.Windows.Forms.TextBox tbParameters;
        public System.Windows.Forms.TextBox tbDuration;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox tbStartTime;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.RichTextBox rtbFunctionDescription;
        private System.Windows.Forms.RichTextBox rtbParameterDescription;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pParameterControls;
        private System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ToolStripTextBox tbTitle;
    }
}