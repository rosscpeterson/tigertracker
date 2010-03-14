namespace CSCE431Project1
{
    partial class Versions
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
            this.comboBoxVersions = new System.Windows.Forms.ComboBox();
            this.richTextBoxProjDesc = new System.Windows.Forms.RichTextBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonNew = new System.Windows.Forms.Button();
            this.textBoxNew = new System.Windows.Forms.TextBox();
            this.buttonDel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxVersions
            // 
            this.comboBoxVersions.FormattingEnabled = true;
            this.comboBoxVersions.Location = new System.Drawing.Point(12, 12);
            this.comboBoxVersions.Name = "comboBoxVersions";
            this.comboBoxVersions.Size = new System.Drawing.Size(111, 21);
            this.comboBoxVersions.TabIndex = 0;
            this.comboBoxVersions.SelectedIndexChanged += new System.EventHandler(this.comboBoxVersions_SelectedIndexChanged);
            // 
            // richTextBoxProjDesc
            // 
            this.richTextBoxProjDesc.Location = new System.Drawing.Point(12, 39);
            this.richTextBoxProjDesc.Name = "richTextBoxProjDesc";
            this.richTextBoxProjDesc.Size = new System.Drawing.Size(301, 126);
            this.richTextBoxProjDesc.TabIndex = 1;
            this.richTextBoxProjDesc.Text = "";
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(202, 12);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(111, 21);
            this.buttonUpdate.TabIndex = 2;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonNew
            // 
            this.buttonNew.Location = new System.Drawing.Point(12, 170);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(111, 21);
            this.buttonNew.TabIndex = 3;
            this.buttonNew.Text = "New Version";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // textBoxNew
            // 
            this.textBoxNew.Location = new System.Drawing.Point(138, 171);
            this.textBoxNew.Name = "textBoxNew";
            this.textBoxNew.Size = new System.Drawing.Size(51, 20);
            this.textBoxNew.TabIndex = 4;
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(202, 170);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(111, 21);
            this.buttonDel.TabIndex = 5;
            this.buttonDel.Text = "Delete Current";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // Versions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 199);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.textBoxNew);
            this.Controls.Add(this.buttonNew);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.richTextBoxProjDesc);
            this.Controls.Add(this.comboBoxVersions);
            this.Name = "Versions";
            this.Text = "Versions";
            this.Load += new System.EventHandler(this.Versions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxVersions;
        private System.Windows.Forms.RichTextBox richTextBoxProjDesc;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.TextBox textBoxNew;
        private System.Windows.Forms.Button buttonDel;
    }
}