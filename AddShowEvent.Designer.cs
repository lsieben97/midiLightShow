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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Duration (in miliseconds):";
            // 
            // tbDuration
            // 
            this.tbDuration.Location = new System.Drawing.Point(142, 28);
            this.tbDuration.Name = "tbDuration";
            this.tbDuration.Size = new System.Drawing.Size(210, 20);
            this.tbDuration.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Function:";
            // 
            // cbFunctions
            // 
            this.cbFunctions.FormattingEnabled = true;
            this.cbFunctions.Location = new System.Drawing.Point(142, 53);
            this.cbFunctions.Name = "cbFunctions";
            this.cbFunctions.Size = new System.Drawing.Size(210, 21);
            this.cbFunctions.TabIndex = 3;
            this.cbFunctions.SelectedIndexChanged += new System.EventHandler(this.cbFunctions_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Function parameters:";
            // 
            // tbParameters
            // 
            this.tbParameters.Location = new System.Drawing.Point(142, 142);
            this.tbParameters.Name = "tbParameters";
            this.tbParameters.Size = new System.Drawing.Size(210, 20);
            this.tbParameters.TabIndex = 5;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(12, 280);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(272, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Function description:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Start time (in miliseconds):";
            // 
            // tbStartTime
            // 
            this.tbStartTime.Location = new System.Drawing.Point(142, 6);
            this.tbStartTime.Name = "tbStartTime";
            this.tbStartTime.Size = new System.Drawing.Size(210, 20);
            this.tbStartTime.TabIndex = 1;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(133, 280);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 12;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // rtbFunctionDescription
            // 
            this.rtbFunctionDescription.Location = new System.Drawing.Point(142, 80);
            this.rtbFunctionDescription.Name = "rtbFunctionDescription";
            this.rtbFunctionDescription.ReadOnly = true;
            this.rtbFunctionDescription.Size = new System.Drawing.Size(210, 56);
            this.rtbFunctionDescription.TabIndex = 13;
            this.rtbFunctionDescription.Text = "";
            // 
            // rtbParameterDescription
            // 
            this.rtbParameterDescription.Location = new System.Drawing.Point(142, 168);
            this.rtbParameterDescription.Name = "rtbParameterDescription";
            this.rtbParameterDescription.ReadOnly = true;
            this.rtbParameterDescription.Size = new System.Drawing.Size(210, 106);
            this.rtbParameterDescription.TabIndex = 13;
            this.rtbParameterDescription.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 212);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Parameter Description:";
            // 
            // AddShowEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 315);
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
            this.Name = "AddShowEvent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add event";
            this.Load += new System.EventHandler(this.AddShowEvent_Load);
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
    }
}