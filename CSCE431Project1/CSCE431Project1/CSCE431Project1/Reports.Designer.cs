namespace CSCE431Project1
{
    partial class Reports
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
            this.reqAddButton = new System.Windows.Forms.Button();
            this.reqComboBox = new System.Windows.Forms.ComboBox();
            this.reqListBox = new System.Windows.Forms.ListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.reqNoneCheckBox = new System.Windows.Forms.CheckBox();
            this.reqAllCheckBox = new System.Windows.Forms.CheckBox();
            this.genReportButton = new System.Windows.Forms.Button();
            this.reqOpenCheckBox = new System.Windows.Forms.CheckBox();
            this.reqInProgressCheckBox = new System.Windows.Forms.CheckBox();
            this.reqClosedCheckBox = new System.Windows.Forms.CheckBox();
            this.bugClosedCheckBox = new System.Windows.Forms.CheckBox();
            this.bugInProgressCheckBox = new System.Windows.Forms.CheckBox();
            this.bugOpenCheckBox = new System.Windows.Forms.CheckBox();
            this.bugAllCheckBox = new System.Windows.Forms.CheckBox();
            this.bugNoneCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bugAddButton = new System.Windows.Forms.Button();
            this.bugComboBox = new System.Windows.Forms.ComboBox();
            this.bugListBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // reqAddButton
            // 
            this.reqAddButton.Location = new System.Drawing.Point(275, 48);
            this.reqAddButton.Name = "reqAddButton";
            this.reqAddButton.Size = new System.Drawing.Size(27, 27);
            this.reqAddButton.TabIndex = 52;
            this.reqAddButton.Text = "Add";
            this.reqAddButton.UseVisualStyleBackColor = true;
            this.reqAddButton.Click += new System.EventHandler(this.reqAddButton_Click);
            // 
            // reqComboBox
            // 
            this.reqComboBox.FormattingEnabled = true;
            this.reqComboBox.Location = new System.Drawing.Point(98, 52);
            this.reqComboBox.Name = "reqComboBox";
            this.reqComboBox.Size = new System.Drawing.Size(171, 21);
            this.reqComboBox.TabIndex = 51;
            // 
            // reqListBox
            // 
            this.reqListBox.FormattingEnabled = true;
            this.reqListBox.Location = new System.Drawing.Point(98, 85);
            this.reqListBox.Name = "reqListBox";
            this.reqListBox.Size = new System.Drawing.Size(207, 69);
            this.reqListBox.TabIndex = 50;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 49;
            this.label10.Text = "Requirement:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(319, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 54;
            this.label1.Text = "Status";
            // 
            // reqNoneCheckBox
            // 
            this.reqNoneCheckBox.AutoSize = true;
            this.reqNoneCheckBox.Location = new System.Drawing.Point(98, 161);
            this.reqNoneCheckBox.Name = "reqNoneCheckBox";
            this.reqNoneCheckBox.Size = new System.Drawing.Size(52, 17);
            this.reqNoneCheckBox.TabIndex = 55;
            this.reqNoneCheckBox.Text = "None";
            this.reqNoneCheckBox.UseVisualStyleBackColor = true;
            this.reqNoneCheckBox.CheckedChanged += new System.EventHandler(this.reqNoneCheckBox_CheckedChanged);
            // 
            // reqAllCheckBox
            // 
            this.reqAllCheckBox.AutoSize = true;
            this.reqAllCheckBox.Location = new System.Drawing.Point(265, 161);
            this.reqAllCheckBox.Name = "reqAllCheckBox";
            this.reqAllCheckBox.Size = new System.Drawing.Size(37, 17);
            this.reqAllCheckBox.TabIndex = 56;
            this.reqAllCheckBox.Text = "All";
            this.reqAllCheckBox.UseVisualStyleBackColor = true;
            this.reqAllCheckBox.CheckedChanged += new System.EventHandler(this.reqAllCheckBox_CheckedChanged);
            // 
            // genReportButton
            // 
            this.genReportButton.Location = new System.Drawing.Point(322, 383);
            this.genReportButton.Name = "genReportButton";
            this.genReportButton.Size = new System.Drawing.Size(104, 23);
            this.genReportButton.TabIndex = 57;
            this.genReportButton.Text = "Generate Report";
            this.genReportButton.UseVisualStyleBackColor = true;
            this.genReportButton.Click += new System.EventHandler(this.genReportButton_Click);
            // 
            // reqOpenCheckBox
            // 
            this.reqOpenCheckBox.AutoSize = true;
            this.reqOpenCheckBox.Location = new System.Drawing.Point(374, 54);
            this.reqOpenCheckBox.Name = "reqOpenCheckBox";
            this.reqOpenCheckBox.Size = new System.Drawing.Size(52, 17);
            this.reqOpenCheckBox.TabIndex = 58;
            this.reqOpenCheckBox.Text = "Open";
            this.reqOpenCheckBox.UseVisualStyleBackColor = true;
            // 
            // reqInProgressCheckBox
            // 
            this.reqInProgressCheckBox.AutoSize = true;
            this.reqInProgressCheckBox.Location = new System.Drawing.Point(374, 77);
            this.reqInProgressCheckBox.Name = "reqInProgressCheckBox";
            this.reqInProgressCheckBox.Size = new System.Drawing.Size(79, 17);
            this.reqInProgressCheckBox.TabIndex = 59;
            this.reqInProgressCheckBox.Text = "In Progress";
            this.reqInProgressCheckBox.UseVisualStyleBackColor = true;
            // 
            // reqClosedCheckBox
            // 
            this.reqClosedCheckBox.AutoSize = true;
            this.reqClosedCheckBox.Location = new System.Drawing.Point(374, 100);
            this.reqClosedCheckBox.Name = "reqClosedCheckBox";
            this.reqClosedCheckBox.Size = new System.Drawing.Size(58, 17);
            this.reqClosedCheckBox.TabIndex = 60;
            this.reqClosedCheckBox.Text = "Closed";
            this.reqClosedCheckBox.UseVisualStyleBackColor = true;
            // 
            // bugClosedCheckBox
            // 
            this.bugClosedCheckBox.AutoSize = true;
            this.bugClosedCheckBox.Location = new System.Drawing.Point(374, 250);
            this.bugClosedCheckBox.Name = "bugClosedCheckBox";
            this.bugClosedCheckBox.Size = new System.Drawing.Size(58, 17);
            this.bugClosedCheckBox.TabIndex = 70;
            this.bugClosedCheckBox.Text = "Closed";
            this.bugClosedCheckBox.UseVisualStyleBackColor = true;
            // 
            // bugInProgressCheckBox
            // 
            this.bugInProgressCheckBox.AutoSize = true;
            this.bugInProgressCheckBox.Location = new System.Drawing.Point(374, 227);
            this.bugInProgressCheckBox.Name = "bugInProgressCheckBox";
            this.bugInProgressCheckBox.Size = new System.Drawing.Size(79, 17);
            this.bugInProgressCheckBox.TabIndex = 69;
            this.bugInProgressCheckBox.Text = "In Progress";
            this.bugInProgressCheckBox.UseVisualStyleBackColor = true;
            // 
            // bugOpenCheckBox
            // 
            this.bugOpenCheckBox.AutoSize = true;
            this.bugOpenCheckBox.Location = new System.Drawing.Point(374, 204);
            this.bugOpenCheckBox.Name = "bugOpenCheckBox";
            this.bugOpenCheckBox.Size = new System.Drawing.Size(52, 17);
            this.bugOpenCheckBox.TabIndex = 68;
            this.bugOpenCheckBox.Text = "Open";
            this.bugOpenCheckBox.UseVisualStyleBackColor = true;
            // 
            // bugAllCheckBox
            // 
            this.bugAllCheckBox.AutoSize = true;
            this.bugAllCheckBox.Location = new System.Drawing.Point(265, 311);
            this.bugAllCheckBox.Name = "bugAllCheckBox";
            this.bugAllCheckBox.Size = new System.Drawing.Size(37, 17);
            this.bugAllCheckBox.TabIndex = 67;
            this.bugAllCheckBox.Text = "All";
            this.bugAllCheckBox.UseVisualStyleBackColor = true;
            this.bugAllCheckBox.CheckedChanged += new System.EventHandler(this.bugAllCheckBox_CheckedChanged);
            // 
            // bugNoneCheckBox
            // 
            this.bugNoneCheckBox.AutoSize = true;
            this.bugNoneCheckBox.Location = new System.Drawing.Point(98, 311);
            this.bugNoneCheckBox.Name = "bugNoneCheckBox";
            this.bugNoneCheckBox.Size = new System.Drawing.Size(52, 17);
            this.bugNoneCheckBox.TabIndex = 66;
            this.bugNoneCheckBox.Text = "None";
            this.bugNoneCheckBox.UseVisualStyleBackColor = true;
            this.bugNoneCheckBox.CheckedChanged += new System.EventHandler(this.bugNoneCheckBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(319, 204);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "Status";
            // 
            // bugAddButton
            // 
            this.bugAddButton.Location = new System.Drawing.Point(275, 198);
            this.bugAddButton.Name = "bugAddButton";
            this.bugAddButton.Size = new System.Drawing.Size(27, 27);
            this.bugAddButton.TabIndex = 64;
            this.bugAddButton.Text = "Add";
            this.bugAddButton.UseVisualStyleBackColor = true;
            this.bugAddButton.Click += new System.EventHandler(this.bugAddButton_Click);
            // 
            // bugComboBox
            // 
            this.bugComboBox.FormattingEnabled = true;
            this.bugComboBox.Location = new System.Drawing.Point(98, 202);
            this.bugComboBox.Name = "bugComboBox";
            this.bugComboBox.Size = new System.Drawing.Size(171, 21);
            this.bugComboBox.TabIndex = 63;
            // 
            // bugListBox
            // 
            this.bugListBox.FormattingEnabled = true;
            this.bugListBox.Location = new System.Drawing.Point(98, 235);
            this.bugListBox.Name = "bugListBox";
            this.bugListBox.Size = new System.Drawing.Size(207, 69);
            this.bugListBox.TabIndex = 62;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 61;
            this.label3.Text = "Bug:";
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 424);
            this.Controls.Add(this.bugClosedCheckBox);
            this.Controls.Add(this.bugInProgressCheckBox);
            this.Controls.Add(this.bugOpenCheckBox);
            this.Controls.Add(this.bugAllCheckBox);
            this.Controls.Add(this.bugNoneCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bugAddButton);
            this.Controls.Add(this.bugComboBox);
            this.Controls.Add(this.bugListBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.reqClosedCheckBox);
            this.Controls.Add(this.reqInProgressCheckBox);
            this.Controls.Add(this.reqOpenCheckBox);
            this.Controls.Add(this.genReportButton);
            this.Controls.Add(this.reqAllCheckBox);
            this.Controls.Add(this.reqNoneCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reqAddButton);
            this.Controls.Add(this.reqComboBox);
            this.Controls.Add(this.reqListBox);
            this.Controls.Add(this.label10);
            this.Name = "Reports";
            this.Text = "Reports";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button reqAddButton;
        private System.Windows.Forms.ComboBox reqComboBox;
        private System.Windows.Forms.ListBox reqListBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox reqNoneCheckBox;
        private System.Windows.Forms.CheckBox reqAllCheckBox;
        private System.Windows.Forms.Button genReportButton;
        private System.Windows.Forms.CheckBox reqOpenCheckBox;
        private System.Windows.Forms.CheckBox reqInProgressCheckBox;
        private System.Windows.Forms.CheckBox reqClosedCheckBox;
        private System.Windows.Forms.CheckBox bugClosedCheckBox;
        private System.Windows.Forms.CheckBox bugInProgressCheckBox;
        private System.Windows.Forms.CheckBox bugOpenCheckBox;
        private System.Windows.Forms.CheckBox bugAllCheckBox;
        private System.Windows.Forms.CheckBox bugNoneCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bugAddButton;
        private System.Windows.Forms.ComboBox bugComboBox;
        private System.Windows.Forms.ListBox bugListBox;
        private System.Windows.Forms.Label label3;
    }
}