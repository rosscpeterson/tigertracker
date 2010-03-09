namespace CSCE431Project1
{
    partial class NewReq
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
            this.newTitleText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.newReqPriorityCombo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.newReqDescText = new System.Windows.Forms.TextBox();
            this.addReqButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.releaseComboBox = new System.Windows.Forms.ComboBox();
            this.releasesListBox = new System.Windows.Forms.ListBox();
            this.ownersComboBox = new System.Windows.Forms.ComboBox();
            this.watchersComboBox = new System.Windows.Forms.ComboBox();
            this.ownersListBox = new System.Windows.Forms.ListBox();
            this.watchersListBox = new System.Windows.Forms.ListBox();
            this.watchersAddButton = new System.Windows.Forms.Button();
            this.ownersAddButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title";
            // 
            // newTitleText
            // 
            this.newTitleText.Location = new System.Drawing.Point(100, 66);
            this.newTitleText.Name = "newTitleText";
            this.newTitleText.Size = new System.Drawing.Size(202, 20);
            this.newTitleText.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Release(s)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(320, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Owners";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(323, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Watchers";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 259);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Priority";
            // 
            // newReqPriorityCombo
            // 
            this.newReqPriorityCombo.FormattingEnabled = true;
            this.newReqPriorityCombo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.newReqPriorityCombo.Location = new System.Drawing.Point(100, 256);
            this.newReqPriorityCombo.Name = "newReqPriorityCombo";
            this.newReqPriorityCombo.Size = new System.Drawing.Size(121, 21);
            this.newReqPriorityCombo.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 304);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Description";
            // 
            // newReqDescText
            // 
            this.newReqDescText.Location = new System.Drawing.Point(94, 304);
            this.newReqDescText.Multiline = true;
            this.newReqDescText.Name = "newReqDescText";
            this.newReqDescText.Size = new System.Drawing.Size(504, 120);
            this.newReqDescText.TabIndex = 11;
            // 
            // addReqButton
            // 
            this.addReqButton.Location = new System.Drawing.Point(341, 453);
            this.addReqButton.Name = "addReqButton";
            this.addReqButton.Size = new System.Drawing.Size(120, 23);
            this.addReqButton.TabIndex = 12;
            this.addReqButton.Text = "Add Requirement";
            this.addReqButton.UseVisualStyleBackColor = true;
            this.addReqButton.Click += new System.EventHandler(this.addReqButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(478, 453);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(120, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(183, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(246, 31);
            this.label7.TabIndex = 14;
            this.label7.Text = "New Requirement";
            // 
            // releaseComboBox
            // 
            this.releaseComboBox.FormattingEnabled = true;
            this.releaseComboBox.Location = new System.Drawing.Point(100, 103);
            this.releaseComboBox.Name = "releaseComboBox";
            this.releaseComboBox.Size = new System.Drawing.Size(202, 21);
            this.releaseComboBox.TabIndex = 15;
            this.releaseComboBox.SelectedIndexChanged += new System.EventHandler(this.releaseComboBox_SelectedIndexChanged);
            // 
            // releasesListBox
            // 
            this.releasesListBox.FormattingEnabled = true;
            this.releasesListBox.Location = new System.Drawing.Point(27, 137);
            this.releasesListBox.Name = "releasesListBox";
            this.releasesListBox.Size = new System.Drawing.Size(275, 95);
            this.releasesListBox.TabIndex = 16;
            // 
            // ownersComboBox
            // 
            this.ownersComboBox.FormattingEnabled = true;
            this.ownersComboBox.Location = new System.Drawing.Point(382, 65);
            this.ownersComboBox.Name = "ownersComboBox";
            this.ownersComboBox.Size = new System.Drawing.Size(183, 21);
            this.ownersComboBox.TabIndex = 17;
            // 
            // watchersComboBox
            // 
            this.watchersComboBox.FormattingEnabled = true;
            this.watchersComboBox.Location = new System.Drawing.Point(382, 180);
            this.watchersComboBox.Name = "watchersComboBox";
            this.watchersComboBox.Size = new System.Drawing.Size(183, 21);
            this.watchersComboBox.TabIndex = 18;
            // 
            // ownersListBox
            // 
            this.ownersListBox.FormattingEnabled = true;
            this.ownersListBox.Location = new System.Drawing.Point(323, 93);
            this.ownersListBox.Name = "ownersListBox";
            this.ownersListBox.Size = new System.Drawing.Size(275, 82);
            this.ownersListBox.TabIndex = 19;
            // 
            // watchersListBox
            // 
            this.watchersListBox.FormattingEnabled = true;
            this.watchersListBox.Location = new System.Drawing.Point(323, 207);
            this.watchersListBox.Name = "watchersListBox";
            this.watchersListBox.Size = new System.Drawing.Size(275, 82);
            this.watchersListBox.TabIndex = 20;
            // 
            // watchersAddButton
            // 
            this.watchersAddButton.BackgroundImage = global::CSCE431Project1.Properties.Resources._128px_Nuvola_Green_Plus;
            this.watchersAddButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.watchersAddButton.Image = global::CSCE431Project1.Properties.Resources._128px_Nuvola_Green_Plus1;
            this.watchersAddButton.Location = new System.Drawing.Point(571, 176);
            this.watchersAddButton.Name = "watchersAddButton";
            this.watchersAddButton.Size = new System.Drawing.Size(27, 27);
            this.watchersAddButton.TabIndex = 21;
            this.watchersAddButton.UseVisualStyleBackColor = true;
            this.watchersAddButton.Click += new System.EventHandler(this.watchersAddButton_Click);
            // 
            // ownersAddButton
            // 
            this.ownersAddButton.Image = global::CSCE431Project1.Properties.Resources._128px_Nuvola_Green_Plus1;
            this.ownersAddButton.Location = new System.Drawing.Point(571, 62);
            this.ownersAddButton.Name = "ownersAddButton";
            this.ownersAddButton.Size = new System.Drawing.Size(27, 27);
            this.ownersAddButton.TabIndex = 22;
            this.ownersAddButton.UseVisualStyleBackColor = true;
            this.ownersAddButton.Click += new System.EventHandler(this.ownersAddButton_Click);
            // 
            // NewReq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 488);
            this.Controls.Add(this.ownersAddButton);
            this.Controls.Add(this.watchersAddButton);
            this.Controls.Add(this.watchersListBox);
            this.Controls.Add(this.ownersListBox);
            this.Controls.Add(this.watchersComboBox);
            this.Controls.Add(this.ownersComboBox);
            this.Controls.Add(this.releasesListBox);
            this.Controls.Add(this.releaseComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addReqButton);
            this.Controls.Add(this.newReqDescText);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.newReqPriorityCombo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.newTitleText);
            this.Controls.Add(this.label1);
            this.Name = "NewReq";
            this.Text = "NewReq";
            this.Load += new System.EventHandler(this.NewReq_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox newTitleText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox newReqPriorityCombo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox newReqDescText;
        private System.Windows.Forms.Button addReqButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox releaseComboBox;
        private System.Windows.Forms.ListBox releasesListBox;
        private System.Windows.Forms.ComboBox ownersComboBox;
        private System.Windows.Forms.ComboBox watchersComboBox;
        private System.Windows.Forms.ListBox ownersListBox;
        private System.Windows.Forms.ListBox watchersListBox;
        private System.Windows.Forms.Button watchersAddButton;
        private System.Windows.Forms.Button ownersAddButton;
    }
}