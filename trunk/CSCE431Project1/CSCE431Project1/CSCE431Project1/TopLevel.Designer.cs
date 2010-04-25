namespace CSCE431Project1
{
    partial class TopLevel
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopLevel));
            this.label1 = new System.Windows.Forms.Label();
            this.titleText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.statusComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.timeClosedText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.timeOpenText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.descriptionText = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.bugTable = new System.Windows.Forms.DataGridView();
            this.updateButton = new System.Windows.Forms.Button();
            this.newBugButton = new System.Windows.Forms.Button();
            this.newReqButton = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolProjCombo = new System.Windows.Forms.ToolStripComboBox();
            this.reportsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonLogOff = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.projects_toolstrip = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolUsersButton = new System.Windows.Forms.ToolStripButton();
            this.idText = new System.Windows.Forms.TextBox();
            this.originatorText = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.labelUserName = new System.Windows.Forms.Label();
            this.labelUserTitle = new System.Windows.Forms.Label();
            this.TabView = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.reqTable = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.notesLabel = new System.Windows.Forms.Label();
            this.detailedNotesButton = new System.Windows.Forms.Button();
            this.priorityComboBox = new System.Windows.Forms.ComboBox();
            this.releaseListBox = new System.Windows.Forms.ListBox();
            this.addVerButton = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.ownerListBox = new System.Windows.Forms.ListBox();
            this.watchersListBox = new System.Windows.Forms.ListBox();
            this.ownerComboBox = new System.Windows.Forms.ComboBox();
            this.ownerAddButton = new System.Windows.Forms.Button();
            this.watcherComboBox = new System.Windows.Forms.ComboBox();
            this.watchersAddButton = new System.Windows.Forms.Button();
            this.comboBoxRR = new System.Windows.Forms.ComboBox();
            this.comboBoxVer = new System.Windows.Forms.ComboBox();
            this.notesText = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.bugTable)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.TabView.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reqTable)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID:";
            // 
            // titleText
            // 
            this.titleText.Location = new System.Drawing.Point(62, 74);
            this.titleText.Name = "titleText";
            this.titleText.Size = new System.Drawing.Size(215, 20);
            this.titleText.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Title:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(548, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Status";
            // 
            // statusComboBox
            // 
            this.statusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusComboBox.FormattingEnabled = true;
            this.statusComboBox.Items.AddRange(new object[] {
            "Open",
            "In Progress",
            "Closed"});
            this.statusComboBox.Location = new System.Drawing.Point(599, 44);
            this.statusComboBox.Name = "statusComboBox";
            this.statusComboBox.Size = new System.Drawing.Size(188, 21);
            this.statusComboBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Release";
            // 
            // timeClosedText
            // 
            this.timeClosedText.Location = new System.Drawing.Point(361, 79);
            this.timeClosedText.Name = "timeClosedText";
            this.timeClosedText.ReadOnly = true;
            this.timeClosedText.Size = new System.Drawing.Size(146, 20);
            this.timeClosedText.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(285, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Time Closed";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(285, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Time Open";
            // 
            // timeOpenText
            // 
            this.timeOpenText.Location = new System.Drawing.Point(361, 44);
            this.timeOpenText.Name = "timeOpenText";
            this.timeOpenText.ReadOnly = true;
            this.timeOpenText.Size = new System.Drawing.Size(146, 20);
            this.timeOpenText.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 221);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Originator";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(539, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Priority";
            // 
            // descriptionText
            // 
            this.descriptionText.Location = new System.Drawing.Point(361, 115);
            this.descriptionText.Multiline = true;
            this.descriptionText.Name = "descriptionText";
            this.descriptionText.Size = new System.Drawing.Size(574, 117);
            this.descriptionText.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(292, 115);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Description:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 339);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Watcher(s)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 255);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Owner(s)";
            // 
            // bugTable
            // 
            this.bugTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bugTable.Location = new System.Drawing.Point(3, 3);
            this.bugTable.MultiSelect = false;
            this.bugTable.Name = "bugTable";
            this.bugTable.Size = new System.Drawing.Size(954, 220);
            this.bugTable.TabIndex = 23;
            this.bugTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.bugTable_CellClick);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(40, 446);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 24;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // newBugButton
            // 
            this.newBugButton.Location = new System.Drawing.Point(827, 446);
            this.newBugButton.Name = "newBugButton";
            this.newBugButton.Size = new System.Drawing.Size(120, 25);
            this.newBugButton.TabIndex = 25;
            this.newBugButton.Text = "New Bug";
            this.newBugButton.UseVisualStyleBackColor = true;
            this.newBugButton.Click += new System.EventHandler(this.newBugButton_Click);
            // 
            // newReqButton
            // 
            this.newReqButton.Location = new System.Drawing.Point(685, 446);
            this.newReqButton.Name = "newReqButton";
            this.newReqButton.Size = new System.Drawing.Size(120, 25);
            this.newReqButton.TabIndex = 26;
            this.newReqButton.Text = "New Requirement";
            this.newReqButton.UseVisualStyleBackColor = true;
            this.newReqButton.Click += new System.EventHandler(this.newReqButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolProjCombo,
            this.reportsButton,
            this.toolStripSeparator3,
            this.toolStripButtonLogOff,
            this.toolStripSeparator1,
            this.projects_toolstrip,
            this.toolStripSeparator2,
            this.toolUsersButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(992, 25);
            this.toolStrip1.TabIndex = 27;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolProjCombo
            // 
            this.toolProjCombo.Name = "toolProjCombo";
            this.toolProjCombo.Size = new System.Drawing.Size(121, 25);
            this.toolProjCombo.SelectedIndexChanged += new System.EventHandler(this.toolProjCombo_SelectedIndexChanged);
            // 
            // reportsButton
            // 
            this.reportsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.reportsButton.Image = ((System.Drawing.Image)(resources.GetObject("reportsButton.Image")));
            this.reportsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reportsButton.Name = "reportsButton";
            this.reportsButton.Size = new System.Drawing.Size(51, 22);
            this.reportsButton.Text = "Reports";
            this.reportsButton.Click += new System.EventHandler(this.reportsButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonLogOff
            // 
            this.toolStripButtonLogOff.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonLogOff.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLogOff.Image")));
            this.toolStripButtonLogOff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLogOff.Name = "toolStripButtonLogOff";
            this.toolStripButtonLogOff.Size = new System.Drawing.Size(51, 22);
            this.toolStripButtonLogOff.Text = "Log Off";
            this.toolStripButtonLogOff.Click += new System.EventHandler(this.toolStripButtonLogOff_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // projects_toolstrip
            // 
            this.projects_toolstrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.projects_toolstrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.projects_toolstrip.Name = "projects_toolstrip";
            this.projects_toolstrip.Size = new System.Drawing.Size(53, 22);
            this.projects_toolstrip.Text = "Projects";
            this.projects_toolstrip.Click += new System.EventHandler(this.projects_toolstrip_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolUsersButton
            // 
            this.toolUsersButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolUsersButton.Image = ((System.Drawing.Image)(resources.GetObject("toolUsersButton.Image")));
            this.toolUsersButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolUsersButton.Name = "toolUsersButton";
            this.toolUsersButton.Size = new System.Drawing.Size(39, 22);
            this.toolUsersButton.Text = "Users";
            this.toolUsersButton.Click += new System.EventHandler(this.toolUsersButton_Click);
            // 
            // idText
            // 
            this.idText.Location = new System.Drawing.Point(62, 45);
            this.idText.Name = "idText";
            this.idText.ReadOnly = true;
            this.idText.Size = new System.Drawing.Size(215, 20);
            this.idText.TabIndex = 28;
            // 
            // originatorText
            // 
            this.originatorText.Location = new System.Drawing.Point(89, 218);
            this.originatorText.Name = "originatorText";
            this.originatorText.ReadOnly = true;
            this.originatorText.Size = new System.Drawing.Size(208, 20);
            this.originatorText.TabIndex = 29;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(894, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 13);
            this.label12.TabIndex = 30;
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Location = new System.Drawing.Point(796, 44);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(60, 13);
            this.labelUserName.TabIndex = 31;
            this.labelUserName.Text = "User Name";
            this.labelUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelUserTitle
            // 
            this.labelUserTitle.AutoSize = true;
            this.labelUserTitle.Location = new System.Drawing.Point(796, 77);
            this.labelUserTitle.Name = "labelUserTitle";
            this.labelUserTitle.Size = new System.Drawing.Size(52, 13);
            this.labelUserTitle.TabIndex = 32;
            this.labelUserTitle.Text = "User Title";
            this.labelUserTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TabView
            // 
            this.TabView.Controls.Add(this.tabPage2);
            this.TabView.Controls.Add(this.tabPage1);
            this.TabView.Location = new System.Drawing.Point(12, 477);
            this.TabView.Name = "TabView";
            this.TabView.SelectedIndex = 0;
            this.TabView.Size = new System.Drawing.Size(968, 249);
            this.TabView.TabIndex = 33;
            this.TabView.SelectedIndexChanged += new System.EventHandler(this.TabView_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.reqTable);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(960, 223);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Requirements";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // reqTable
            // 
            this.reqTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.reqTable.Location = new System.Drawing.Point(0, 0);
            this.reqTable.MultiSelect = false;
            this.reqTable.Name = "reqTable";
            this.reqTable.Size = new System.Drawing.Size(960, 221);
            this.reqTable.TabIndex = 0;
            this.reqTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.reqTable_CellClick);
            this.reqTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.reqTable_CellContentClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.bugTable);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(960, 223);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Bugs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // notesLabel
            // 
            this.notesLabel.AutoSize = true;
            this.notesLabel.Location = new System.Drawing.Point(314, 252);
            this.notesLabel.Name = "notesLabel";
            this.notesLabel.Size = new System.Drawing.Size(35, 13);
            this.notesLabel.TabIndex = 35;
            this.notesLabel.Text = "Notes";
            // 
            // detailedNotesButton
            // 
            this.detailedNotesButton.Location = new System.Drawing.Point(308, 268);
            this.detailedNotesButton.Name = "detailedNotesButton";
            this.detailedNotesButton.Size = new System.Drawing.Size(47, 23);
            this.detailedNotesButton.TabIndex = 36;
            this.detailedNotesButton.Text = "Details";
            this.detailedNotesButton.UseVisualStyleBackColor = true;
            this.detailedNotesButton.Click += new System.EventHandler(this.detailedNotesButton_Click);
            // 
            // priorityComboBox
            // 
            this.priorityComboBox.FormattingEnabled = true;
            this.priorityComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.priorityComboBox.Location = new System.Drawing.Point(599, 77);
            this.priorityComboBox.Name = "priorityComboBox";
            this.priorityComboBox.Size = new System.Drawing.Size(188, 21);
            this.priorityComboBox.TabIndex = 37;
            // 
            // releaseListBox
            // 
            this.releaseListBox.FormattingEnabled = true;
            this.releaseListBox.Location = new System.Drawing.Point(62, 132);
            this.releaseListBox.Name = "releaseListBox";
            this.releaseListBox.Size = new System.Drawing.Size(215, 69);
            this.releaseListBox.TabIndex = 40;
            // 
            // addVerButton
            // 
            this.addVerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addVerButton.ForeColor = System.Drawing.Color.Green;
            this.addVerButton.Location = new System.Drawing.Point(250, 101);
            this.addVerButton.Name = "addVerButton";
            this.addVerButton.Size = new System.Drawing.Size(27, 27);
            this.addVerButton.TabIndex = 41;
            this.addVerButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.addVerButton.UseVisualStyleBackColor = true;
            this.addVerButton.Click += new System.EventHandler(this.addVerButton_Click);
            // 
            // ownerListBox
            // 
            this.ownerListBox.FormattingEnabled = true;
            this.ownerListBox.Location = new System.Drawing.Point(89, 279);
            this.ownerListBox.Name = "ownerListBox";
            this.ownerListBox.Size = new System.Drawing.Size(208, 43);
            this.ownerListBox.TabIndex = 42;
            // 
            // watchersListBox
            // 
            this.watchersListBox.FormattingEnabled = true;
            this.watchersListBox.Location = new System.Drawing.Point(89, 369);
            this.watchersListBox.Name = "watchersListBox";
            this.watchersListBox.Size = new System.Drawing.Size(207, 43);
            this.watchersListBox.TabIndex = 43;
            // 
            // ownerComboBox
            // 
            this.ownerComboBox.FormattingEnabled = true;
            this.ownerComboBox.Location = new System.Drawing.Point(89, 252);
            this.ownerComboBox.Name = "ownerComboBox";
            this.ownerComboBox.Size = new System.Drawing.Size(171, 21);
            this.ownerComboBox.TabIndex = 44;
            // 
            // ownerAddButton
            // 
            this.ownerAddButton.Location = new System.Drawing.Point(266, 248);
            this.ownerAddButton.Name = "ownerAddButton";
            this.ownerAddButton.Size = new System.Drawing.Size(27, 27);
            this.ownerAddButton.TabIndex = 46;
            this.ownerAddButton.Text = "Add";
            this.ownerAddButton.UseVisualStyleBackColor = true;
            this.ownerAddButton.Click += new System.EventHandler(this.ownerAddButton_Click);
            // 
            // watcherComboBox
            // 
            this.watcherComboBox.FormattingEnabled = true;
            this.watcherComboBox.Location = new System.Drawing.Point(89, 336);
            this.watcherComboBox.Name = "watcherComboBox";
            this.watcherComboBox.Size = new System.Drawing.Size(171, 21);
            this.watcherComboBox.TabIndex = 47;
            // 
            // watchersAddButton
            // 
            this.watchersAddButton.Location = new System.Drawing.Point(266, 332);
            this.watchersAddButton.Name = "watchersAddButton";
            this.watchersAddButton.Size = new System.Drawing.Size(27, 27);
            this.watchersAddButton.TabIndex = 48;
            this.watchersAddButton.Text = "Add";
            this.watchersAddButton.UseVisualStyleBackColor = true;
            this.watchersAddButton.Click += new System.EventHandler(this.watchersAddButton_Click);
            // 
            // comboBoxRR
            // 
            this.comboBoxRR.FormattingEnabled = true;
            this.comboBoxRR.Location = new System.Drawing.Point(62, 105);
            this.comboBoxRR.Name = "comboBoxRR";
            this.comboBoxRR.Size = new System.Drawing.Size(134, 21);
            this.comboBoxRR.TabIndex = 49;
            this.comboBoxRR.SelectedIndexChanged += new System.EventHandler(this.comboBoxRR_SelectedIndexChanged);
            // 
            // comboBoxVer
            // 
            this.comboBoxVer.FormattingEnabled = true;
            this.comboBoxVer.Location = new System.Drawing.Point(202, 105);
            this.comboBoxVer.Name = "comboBoxVer";
            this.comboBoxVer.Size = new System.Drawing.Size(42, 21);
            this.comboBoxVer.TabIndex = 50;
            // 
            // notesText
            // 
            this.notesText.Location = new System.Drawing.Point(361, 268);
            this.notesText.Name = "notesText";
            this.notesText.ReadOnly = true;
            this.notesText.Size = new System.Drawing.Size(574, 144);
            this.notesText.TabIndex = 51;
            this.notesText.Text = "";
            // 
            // TopLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 732);
            this.Controls.Add(this.notesText);
            this.Controls.Add(this.comboBoxVer);
            this.Controls.Add(this.comboBoxRR);
            this.Controls.Add(this.watchersAddButton);
            this.Controls.Add(this.watcherComboBox);
            this.Controls.Add(this.ownerAddButton);
            this.Controls.Add(this.ownerComboBox);
            this.Controls.Add(this.watchersListBox);
            this.Controls.Add(this.ownerListBox);
            this.Controls.Add(this.addVerButton);
            this.Controls.Add(this.releaseListBox);
            this.Controls.Add(this.priorityComboBox);
            this.Controls.Add(this.detailedNotesButton);
            this.Controls.Add(this.notesLabel);
            this.Controls.Add(this.TabView);
            this.Controls.Add(this.labelUserTitle);
            this.Controls.Add(this.labelUserName);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.originatorText);
            this.Controls.Add(this.idText);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.newReqButton);
            this.Controls.Add(this.newBugButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.descriptionText);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.timeClosedText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.timeOpenText);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.statusComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.titleText);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(50, 50);
            this.Name = "TopLevel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Tiger Tracker";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bugTable)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.TabView.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.reqTable)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox titleText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox statusComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox timeClosedText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox timeOpenText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox descriptionText;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView bugTable;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button newBugButton;
        private System.Windows.Forms.Button newReqButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.TextBox idText;
        private System.Windows.Forms.TextBox originatorText;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.Label labelUserTitle;
        private System.Windows.Forms.TabControl TabView;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripButton projects_toolstrip;
        private System.Windows.Forms.DataGridView reqTable;
        private System.Windows.Forms.Label notesLabel;
        private System.Windows.Forms.Button detailedNotesButton;
        private System.Windows.Forms.ComboBox priorityComboBox;
        private System.Windows.Forms.ToolStripComboBox toolProjCombo;
        private System.Windows.Forms.ListBox releaseListBox;
        private System.Windows.Forms.Button addVerButton;
        private System.Windows.Forms.ToolStripButton toolUsersButton;
        private System.Windows.Forms.ToolStripButton toolStripButtonLogOff;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ListBox ownerListBox;
        private System.Windows.Forms.ListBox watchersListBox;
        private System.Windows.Forms.ComboBox ownerComboBox;
        private System.Windows.Forms.Button ownerAddButton;
        private System.Windows.Forms.ComboBox watcherComboBox;
        private System.Windows.Forms.Button watchersAddButton;
        private System.Windows.Forms.ComboBox comboBoxRR;
        private System.Windows.Forms.ComboBox comboBoxVer;
        private System.Windows.Forms.ToolStripButton reportsButton;
        private System.Windows.Forms.RichTextBox notesText;
    }
}

