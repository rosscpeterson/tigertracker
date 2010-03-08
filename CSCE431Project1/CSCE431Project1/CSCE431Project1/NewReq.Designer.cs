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
            this.newReleaseText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ownersCheckedList = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.watchersCheckedList = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.newReqPriorityCombo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.newReqDescText = new System.Windows.Forms.TextBox();
            this.addReqButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.releaseComboBox = new System.Windows.Forms.ComboBox();
            this.releasesListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title";
            // 
            // newTitleText
            // 
            this.newTitleText.Location = new System.Drawing.Point(100, 78);
            this.newTitleText.Name = "newTitleText";
            this.newTitleText.Size = new System.Drawing.Size(202, 20);
            this.newTitleText.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Release(s)";
            // 
            // newReleaseText
            // 
            this.newReleaseText.Location = new System.Drawing.Point(12, 396);
            this.newReleaseText.Multiline = true;
            this.newReleaseText.Name = "newReleaseText";
            this.newReleaseText.Size = new System.Drawing.Size(202, 80);
            this.newReleaseText.TabIndex = 3;
            this.newReleaseText.Text = "Press enter after each release, for all releases type ALL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(320, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Owners";
            // 
            // ownersCheckedList
            // 
            this.ownersCheckedList.FormattingEnabled = true;
            this.ownersCheckedList.Location = new System.Drawing.Point(396, 81);
            this.ownersCheckedList.Name = "ownersCheckedList";
            this.ownersCheckedList.Size = new System.Drawing.Size(202, 94);
            this.ownersCheckedList.TabIndex = 5;
            this.ownersCheckedList.SelectedIndexChanged += new System.EventHandler(this.ownersCheckedList_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(323, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Watchers";
            // 
            // watchersCheckedList
            // 
            this.watchersCheckedList.FormattingEnabled = true;
            this.watchersCheckedList.Location = new System.Drawing.Point(396, 195);
            this.watchersCheckedList.Name = "watchersCheckedList";
            this.watchersCheckedList.Size = new System.Drawing.Size(202, 94);
            this.watchersCheckedList.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 271);
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
            this.newReqPriorityCombo.Location = new System.Drawing.Point(100, 268);
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
            this.releaseComboBox.Location = new System.Drawing.Point(100, 115);
            this.releaseComboBox.Name = "releaseComboBox";
            this.releaseComboBox.Size = new System.Drawing.Size(202, 21);
            this.releaseComboBox.TabIndex = 15;
            // 
            // releasesListBox
            // 
            this.releasesListBox.FormattingEnabled = true;
            this.releasesListBox.Location = new System.Drawing.Point(27, 149);
            this.releasesListBox.Name = "releasesListBox";
            this.releasesListBox.Size = new System.Drawing.Size(275, 95);
            this.releasesListBox.TabIndex = 16;
            // 
            // NewReq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 488);
            this.Controls.Add(this.releasesListBox);
            this.Controls.Add(this.releaseComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addReqButton);
            this.Controls.Add(this.newReqDescText);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.newReqPriorityCombo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.watchersCheckedList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ownersCheckedList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.newReleaseText);
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
        private System.Windows.Forms.TextBox newReleaseText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox ownersCheckedList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox watchersCheckedList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox newReqPriorityCombo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox newReqDescText;
        private System.Windows.Forms.Button addReqButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox releaseComboBox;
        private System.Windows.Forms.ListBox releasesListBox;
    }
}