namespace CSCE431Project1
{
    partial class Projects
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
            this.buttonNewProject = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.comboBoxProject = new System.Windows.Forms.ComboBox();
            this.comboBoxRemoveUser = new System.Windows.Forms.ComboBox();
            this.comboBoxAddUser = new System.Windows.Forms.ComboBox();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.labelIn = new System.Windows.Forms.Label();
            this.labelOut = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.labelProjectName = new System.Windows.Forms.Label();
            this.textBoxProjectName = new System.Windows.Forms.TextBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNew = new System.Windows.Forms.TextBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.comboBoxTitle = new System.Windows.Forms.ComboBox();
            this.buttonVersions = new System.Windows.Forms.Button();
            this.labelNewName = new System.Windows.Forms.Label();
            this.textBoxVersion = new System.Windows.Forms.TextBox();
            this.labelFirstVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonNewProject
            // 
            this.buttonNewProject.Location = new System.Drawing.Point(12, 12);
            this.buttonNewProject.Name = "buttonNewProject";
            this.buttonNewProject.Size = new System.Drawing.Size(89, 45);
            this.buttonNewProject.TabIndex = 0;
            this.buttonNewProject.Text = "New Project";
            this.buttonNewProject.UseVisualStyleBackColor = true;
            this.buttonNewProject.Click += new System.EventHandler(this.buttonNewProject_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(130, 302);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(104, 23);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // comboBoxProject
            // 
            this.comboBoxProject.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxProject.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxProject.FormattingEnabled = true;
            this.comboBoxProject.Location = new System.Drawing.Point(108, 77);
            this.comboBoxProject.Name = "comboBoxProject";
            this.comboBoxProject.Size = new System.Drawing.Size(146, 21);
            this.comboBoxProject.TabIndex = 3;
            this.comboBoxProject.SelectedIndexChanged += new System.EventHandler(this.comboBoxProject_SelectedIndexChanged);
            // 
            // comboBoxRemoveUser
            // 
            this.comboBoxRemoveUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxRemoveUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxRemoveUser.FormattingEnabled = true;
            this.comboBoxRemoveUser.Location = new System.Drawing.Point(12, 132);
            this.comboBoxRemoveUser.Name = "comboBoxRemoveUser";
            this.comboBoxRemoveUser.Size = new System.Drawing.Size(121, 21);
            this.comboBoxRemoveUser.TabIndex = 5;
            this.comboBoxRemoveUser.SelectedIndexChanged += new System.EventHandler(this.comboBoxRemoveUser_SelectedIndexChanged);
            // 
            // comboBoxAddUser
            // 
            this.comboBoxAddUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxAddUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxAddUser.FormattingEnabled = true;
            this.comboBoxAddUser.Location = new System.Drawing.Point(12, 194);
            this.comboBoxAddUser.Name = "comboBoxAddUser";
            this.comboBoxAddUser.Size = new System.Drawing.Size(121, 21);
            this.comboBoxAddUser.TabIndex = 9;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(271, 130);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 7;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // labelIn
            // 
            this.labelIn.AutoSize = true;
            this.labelIn.Location = new System.Drawing.Point(30, 111);
            this.labelIn.Name = "labelIn";
            this.labelIn.Size = new System.Drawing.Size(86, 13);
            this.labelIn.TabIndex = 4;
            this.labelIn.Text = "Project Members";
            // 
            // labelOut
            // 
            this.labelOut.AutoSize = true;
            this.labelOut.Location = new System.Drawing.Point(22, 166);
            this.labelOut.Name = "labelOut";
            this.labelOut.Size = new System.Drawing.Size(94, 13);
            this.labelOut.TabIndex = 8;
            this.labelOut.Text = "Potential Members";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(271, 192);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 11;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // labelProjectName
            // 
            this.labelProjectName.AutoSize = true;
            this.labelProjectName.Location = new System.Drawing.Point(30, 263);
            this.labelProjectName.Name = "labelProjectName";
            this.labelProjectName.Size = new System.Drawing.Size(71, 13);
            this.labelProjectName.TabIndex = 13;
            this.labelProjectName.Text = "Project Name";
            // 
            // textBoxProjectName
            // 
            this.textBoxProjectName.Location = new System.Drawing.Point(221, 263);
            this.textBoxProjectName.Name = "textBoxProjectName";
            this.textBoxProjectName.Size = new System.Drawing.Size(125, 20);
            this.textBoxProjectName.TabIndex = 14;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(246, 302);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(104, 23);
            this.buttonUpdate.TabIndex = 15;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Project Information";
            // 
            // textBoxNew
            // 
            this.textBoxNew.Location = new System.Drawing.Point(125, 37);
            this.textBoxNew.Name = "textBoxNew";
            this.textBoxNew.Size = new System.Drawing.Size(118, 20);
            this.textBoxNew.TabIndex = 1;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(150, 135);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(52, 13);
            this.labelTitle.TabIndex = 6;
            this.labelTitle.Text = "User Title";
            // 
            // comboBoxTitle
            // 
            this.comboBoxTitle.FormattingEnabled = true;
            this.comboBoxTitle.Items.AddRange(new object[] {
            "Project Manager",
            "Project Developer",
            "End User"});
            this.comboBoxTitle.Location = new System.Drawing.Point(153, 194);
            this.comboBoxTitle.Name = "comboBoxTitle";
            this.comboBoxTitle.Size = new System.Drawing.Size(101, 21);
            this.comboBoxTitle.TabIndex = 10;
            // 
            // buttonVersions
            // 
            this.buttonVersions.Location = new System.Drawing.Point(12, 302);
            this.buttonVersions.Name = "buttonVersions";
            this.buttonVersions.Size = new System.Drawing.Size(104, 23);
            this.buttonVersions.TabIndex = 16;
            this.buttonVersions.Text = "Manage Versions";
            this.buttonVersions.UseVisualStyleBackColor = true;
            this.buttonVersions.Click += new System.EventHandler(this.buttonVersions_Click);
            // 
            // labelNewName
            // 
            this.labelNewName.AutoSize = true;
            this.labelNewName.Location = new System.Drawing.Point(164, 12);
            this.labelNewName.Name = "labelNewName";
            this.labelNewName.Size = new System.Drawing.Size(35, 13);
            this.labelNewName.TabIndex = 17;
            this.labelNewName.Text = "Name";
            // 
            // textBoxVersion
            // 
            this.textBoxVersion.Location = new System.Drawing.Point(268, 37);
            this.textBoxVersion.Name = "textBoxVersion";
            this.textBoxVersion.Size = new System.Drawing.Size(78, 20);
            this.textBoxVersion.TabIndex = 18;
            // 
            // labelFirstVersion
            // 
            this.labelFirstVersion.AutoSize = true;
            this.labelFirstVersion.Location = new System.Drawing.Point(273, 12);
            this.labelFirstVersion.Name = "labelFirstVersion";
            this.labelFirstVersion.Size = new System.Drawing.Size(64, 13);
            this.labelFirstVersion.TabIndex = 19;
            this.labelFirstVersion.Text = "First Version";
            // 
            // Projects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 337);
            this.Controls.Add(this.labelFirstVersion);
            this.Controls.Add(this.textBoxVersion);
            this.Controls.Add(this.labelNewName);
            this.Controls.Add(this.buttonVersions);
            this.Controls.Add(this.comboBoxTitle);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.textBoxNew);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.textBoxProjectName);
            this.Controls.Add(this.labelProjectName);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.labelOut);
            this.Controls.Add(this.labelIn);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.comboBoxAddUser);
            this.Controls.Add(this.comboBoxRemoveUser);
            this.Controls.Add(this.comboBoxProject);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonNewProject);
            this.Name = "Projects";
            this.Text = "Projects";
            this.Load += new System.EventHandler(this.Projects_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonNewProject;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.ComboBox comboBoxProject;
        private System.Windows.Forms.ComboBox comboBoxRemoveUser;
        private System.Windows.Forms.ComboBox comboBoxAddUser;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Label labelIn;
        private System.Windows.Forms.Label labelOut;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label labelProjectName;
        private System.Windows.Forms.TextBox textBoxProjectName;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNew;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.ComboBox comboBoxTitle;
        private System.Windows.Forms.Button buttonVersions;
        private System.Windows.Forms.Label labelNewName;
        private System.Windows.Forms.TextBox textBoxVersion;
        private System.Windows.Forms.Label labelFirstVersion;
    }
}