using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.OleDb;

namespace CSCE431Project1
{
    //To pass data from window to window pass the form as a parameter to the construtor and then
    //make a changeText function or something in the window thats reading.
    public partial class Form1 : Form
    {
        // Connection variable.
        protected MySqlConnection conSQL;
        protected MySqlCommand cmdSQL;
        protected MySqlDataAdapter adpSQL;
        // Bug Data Table.
        protected string connectionString;
        protected string currentUser;
        protected int currentUserID;
        protected int currentProjectID;
        protected int currentUserPermLvl;
        protected DataTable proj_dt, ver_dt, rel_dt;

        public Form1()
        {
            InitializeComponent();
            // Default value for variables.
            conSQL = new MySqlConnection();
            cmdSQL = new MySqlCommand("", conSQL);
            adpSQL = new MySqlDataAdapter();
            adpSQL.SelectCommand = cmdSQL;
            // Log user, get id and level.
            LogUser();

            setProjComboBox();  //sets the project combo box and updates the req and bug table

            setReleaseComboBox();   //sets the release combo box
        }
        // Always close the connection.
        ~Form1()
        {
            adpSQL.Dispose();
            cmdSQL.Dispose();
            conSQL.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {}

        private void LogUser()
        {
            // Hide this window.
            this.Hide();
            // Get log in.
            LogIn logIn = new LogIn(conSQL);
            // Show as dialog box.
            logIn.ShowDialog();
            // Stop if not a valid user.
            if ((currentUserID = logIn.UserID()) < 0)
            {
                System.Environment.Exit(0);
            }
            // Get rest of information.
            this.currentUser = logIn.UserName();
            this.currentUserPermLvl = logIn.UserLevel();
            // Log in sucessful.
            logIn.Close();
            this.Show();
        }

        private void UpdateUserInfo()
        {
            cmdSQL.CommandText = "SELECT username, permissionLevel FROM users WHERE uid = " + currentUserID.ToString() + ";";
        }

        private void setProjComboBox()
        {
            //Set Curr Projects combo box
            proj_dt = new DataTable();
            cmdSQL.CommandText = "SELECT projects.pid, projects.name, userprojectlinks.permissionLevel FROM projects, userprojectlinks " +
                                    "WHERE projects.pid = userprojectlinks.projectid AND userprojectlinks.userid = " + currentUserID.ToString() + ";";

            adpSQL.SelectCommand = cmdSQL;
            adpSQL.Fill(proj_dt);

            if (proj_dt.Rows.Count != 0)
            {
                //if there are valid projects go ahead and update everything
                currentProjectID = (int)proj_dt.Rows[0][0];
                updateReqTable(currentProjectID);
                updateBugTable(currentProjectID);
            }

            //bind the combo box to the table
            this.toolProjCombo.ComboBox.DataSource = proj_dt.DefaultView;   //binds the data table
            this.toolProjCombo.ComboBox.DisplayMember = "name"; //sets the column to display
        }

        private void setReleaseComboBox()
        {
            //DataSet rel_ds = new DataSet();
            rel_dt = new DataTable();

            //establish the requirements grid
            cmdSQL.CommandText = "SELECT version, projectid FROM versions WHERE projectid = " + currentProjectID + ";";
            adpSQL.Fill(rel_dt);

            //this.releaseComboBox.DataSource = rel_dt.DefaultView;
            //this.releaseComboBox.DisplayMember = "version";
        }

        private void updateReqTable(int pid)
        {
            //establish the requirements grid
            cmdSQL.CommandText = "SELECT * FROM requirements, userprojectlinks, userrequirementlinks WHERE userrequirementlinks.userid = " + pid.ToString() + " AND requirements.rid = userrequirementlinks.requirementid AND" +
                                    " userprojectlinks.userid = userrequirementlinks.userid AND userprojectlinks.projectid = " + pid.ToString() + ";";

            DataSet req_ds = new DataSet();
            adpSQL.Fill(req_ds, "req_data");

            reqTable.DataSource = req_ds;
            reqTable.DataMember = "req_data";
        }

        private void updateBugTable(int pid)
        {
            //establish the bugs grid view
            //MAKE THIS EVERY BUG IN PROJECT***********************
            cmdSQL.CommandText = "SELECT * FROM bugs, userprojectlinks, userbuglinks WHERE userbuglinks.userid = " + pid.ToString() + " AND bugs.bid = userbuglinks.bugid AND" +
                                    " userprojectlinks.userid = userbuglinks.userid AND userprojectlinks.projectid = " + pid.ToString() + ";";
            DataSet bugs_dataset = new DataSet();
            adpSQL.Fill(bugs_dataset, "bugs_data");

            bugTable.DataSource = bugs_dataset;
            bugTable.DataMember = "bugs_data";

            updateReqTable(currentProjectID);
        }

        private void reqTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int clickedRow = e.RowIndex;
            Console.WriteLine("clicked this row");
        }

        private void newReqButton_Click(object sender, EventArgs e)
        {
            NewReq newReqWindow = new NewReq(connectionString, currentUser, currentUserID, currentProjectID);
            newReqWindow.Show();
        }

        private void newBugButton_Click(object sender, EventArgs e)
        {
            NewBug newBugWindow = new NewBug(connectionString, currentUser, currentUserID, currentProjectID);
            newBugWindow.Show();
        }

        private void statusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolProjCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentProjectID = (Int32)proj_dt.Rows[this.toolProjCombo.ComboBox.SelectedIndex][0];   //gives the id selected
            updateReqTable(currentProjectID);
            updateBugTable(currentProjectID);
        }

        private void projects_toolstrip_Click(object sender, EventArgs e)
        {
            Projects projWindow = new Projects(conSQL, currentUserID, currentUserPermLvl);
            projWindow.Show();
        }

        private void toolUsersButton_Click(object sender, EventArgs e)
        {
            Users usersWindow = new Users(conSQL, currentUserID);
            usersWindow.Show();
        }

        private void toolStripButtonLogOff_Click(object sender, EventArgs e)
        {
            conSQL.Close();
            LogUser();
        }

        private void addVerButton_Click(object sender, EventArgs e)
        {
            this.releaseListBox.Items.Add(rel_dt.Rows[releaseListBox.SelectedIndex][0].ToString());    //Sets up the watchers combo box
            //remove users from the table so they are no longer shown in the combo box
            rel_dt.Rows[releaseListBox.SelectedIndex].Delete();
            rel_dt.AcceptChanges();
        }
    }
}

//        private void LogUser()
//        {
//            this.Hide();
//            LogIn logForm = new LogIn(conSQL);
//            logForm.ShowDialog();
//            Int32 userProjLinkID = logForm.LinkID();
//            logForm.Close();
//            if (userProjLinkID < 0)
//            {
//                this.Close();
//                Application.ExitThread();
//            }

//            DataSet ProjUserLink = new DataSet();
//            MySqlDataAdapter adapter = new MySqlDataAdapter();
//            MySqlCommand command = new MySqlCommand("", conSQL);
//            adapter.SelectCommand = command;

//            adapter.SelectCommand.CommandText = "SELECT projectid, userid, permissionLevel FROM userprojectlinks WHERE uplid = " + userProjLinkID.ToString();
//            adapter.Fill(ProjUserLink, "First Table");

//            pid = (Int32)ProjUserLink.Tables[0].Rows[0].ItemArray[0];
//            uid = (Int32)ProjUserLink.Tables[0].Rows[0].ItemArray[1];
//            lvl = (Int32)ProjUserLink.Tables[0].Rows[0].ItemArray[2];

//            adapter.SelectCommand.CommandText = "SELECT name FROM projects WHERE pid = " + pid.ToString();
//            adapter.Fill(ProjUserLink, "Second Table");
//            this.Text = (String)ProjUserLink.Tables[1].Rows[0].ItemArray[0];

//            adapter.SelectCommand.CommandText = "SELECT username FROM users WHERE uid = " + uid.ToString();
//            adapter.Fill(ProjUserLink, "Third Table");

//            this.labelUserName.Text = (String)ProjUserLink.Tables[2].Rows[0].ItemArray[0];
//            String title = "Project Manager";
//            if (lvl > 1)
//                title = "End User";
//            else if (lvl > 0)
//                title = "Team Member";
//            this.labelUserTitle.Text = title;

//            command.Dispose();
//            adapter.Dispose();
//            this.Show();
//        }

//        private void BuildChimerrorDB()
//        {
//            MySqlCommand cmdSQL = new MySqlCommand("", conSQL);
//            cmdSQL.CommandText = @"CREATE TABLE `users` (
//                                    `uid` INTEGER AUTO_INCREMENT DEFAULT NULL ,
//                                    `username` VARCHAR(200) DEFAULT NULL ,
//                                    `password` VARCHAR(200) DEFAULT NULL ,
//                                    `permissionLevel` INTEGER DEFAULT NULL ,
//                                    `active` INTEGER DEFAULT NULL ,
//                                    PRIMARY KEY (`uid`)
//                                    );
//
//                                    CREATE TABLE `projects` (
//                                    `pid` INTEGER AUTO_INCREMENT DEFAULT NULL ,
//                                    `name` VARCHAR(200) DEFAULT NULL ,
//                                    PRIMARY KEY (`pid`)
//                                    );
//
//                                    CREATE TABLE `bugs` (
//                                    `bid` INTEGER AUTO_INCREMENT DEFAULT NULL ,
//                                    `bugTitle` VARCHAR(200) DEFAULT NULL ,
//                                    `bugDescription` VARCHAR DEFAULT NULL ,
//                                    `status` VARCHAR(200) DEFAULT NULL ,
//                                    `timeOpen` DATETIME DEFAULT NULL ,
//                                    `timeClosed` DATETIME DEFAULT NULL ,
//                                    `fixDetails` VARCHAR DEFAULT NULL ,
//                                    `priority` INTEGER DEFAULT NULL ,
//                                    `versionid` INTEGER DEFAULT NULL ,
//                                    PRIMARY KEY (`bid`)
//                                    );
//
//                                    CREATE TABLE `requirements` (
//                                    `rid` INTEGER AUTO_INCREMENT DEFAULT NULL ,
//                                    `requirementTitle` VARCHAR(200) DEFAULT NULL ,
//                                    `requirementDescription` MEDIUMTEXT DEFAULT NULL ,
//                                    PRIMARY KEY (`rid`)
//                                    );
//
//                                    CREATE TABLE `versions` (
//                                    `vid` INTEGER AUTO_INCREMENT DEFAULT NULL ,
//                                    `projectid` INTEGER DEFAULT NULL ,
//                                    `version` VARCHAR(200) DEFAULT NULL ,
//                                    `verisonInfo` MEDIUMTEXT DEFAULT NULL ,
//                                    PRIMARY KEY (`vid`)
//                                    );
//
//                                    CREATE TABLE `userprojectlinks` (
//                                    `uplid` INTEGER AUTO_INCREMENT DEFAULT NULL ,
//                                    `projectid` INTEGER DEFAULT NULL ,
//                                    `userid` INTEGER DEFAULT NULL ,
//                                    `permissionLevel` INTEGER DEFAULT NULL ,
//                                    PRIMARY KEY (`uplid`)
//                                    );
//
//                                    CREATE TABLE `userbuglinks` (
//                                    `ublid` INTEGER AUTO_INCREMENT DEFAULT NULL ,
//                                    `userid` INTEGER DEFAULT NULL ,
//                                    `bugid` INTEGER DEFAULT NULL ,
//                                    PRIMARY KEY (`ublid`)
//                                    );
//
//                                    CREATE TABLE `bugnotes` (
//                                    `bnid` INTEGER AUTO_INCREMENT DEFAULT NULL ,
//                                    `userid` INTEGER DEFAULT NULL ,
//                                    `bugid` INTEGER DEFAULT NULL ,
//                                    `note` MEDIUMTEXT DEFAULT NULL ,
//                                    PRIMARY KEY (`bnid`)
//                                    );
//
//                                    CREATE TABLE `requirementversionlink` (
//                                    `rvlid` INTEGER AUTO_INCREMENT DEFAULT NULL ,
//                                    `versionid` INTEGER DEFAULT NULL ,
//                                    `satisfied` INTEGER DEFAULT NULL ,
//                                    `requirementid` INTEGER DEFAULT NULL ,
//                                    PRIMARY KEY (`rvlid`)
//                                    );
//
//                                    CREATE TABLE `bugreqlinks` (
//                                    `brlid` INTEGER AUTO_INCREMENT DEFAULT NULL ,
//                                    `bugid` INTEGER DEFAULT NULL ,
//                                    `requirementid` INTEGER DEFAULT NULL ,
//                                    PRIMARY KEY (`brlid`)
//                                    );
//
//                                    ALTER TABLE `bugs` ADD FOREIGN KEY (versionid) REFERENCES `versions` (`vid`);
//                                    ALTER TABLE `versions` ADD FOREIGN KEY (projectid) REFERENCES `projects` (`pid`);
//                                    ALTER TABLE `userprojectlinks` ADD FOREIGN KEY (projectid) REFERENCES `projects` (`pid`);
//                                    ALTER TABLE `userprojectlinks` ADD FOREIGN KEY (userid) REFERENCES `users` (`uid`);
//                                    ALTER TABLE `userbuglinks` ADD FOREIGN KEY (userid) REFERENCES `users` (`uid`);
//                                    ALTER TABLE `userbuglinks` ADD FOREIGN KEY (bugid) REFERENCES `bugs` (`bid`);
//                                    ALTER TABLE `bugnotes` ADD FOREIGN KEY (userid) REFERENCES `users` (`uid`);
//                                    ALTER TABLE `bugnotes` ADD FOREIGN KEY (bugid) REFERENCES `bugs` (`bid`);
//                                    ALTER TABLE `requirementversionlink` ADD FOREIGN KEY (versionid) REFERENCES `versions` (`vid`);
//                                    ALTER TABLE `requirementversionlink` ADD FOREIGN KEY (requirementid) REFERENCES `requirements` (`rid`);
//                                    ALTER TABLE `bugreqlinks` ADD FOREIGN KEY (bugid) REFERENCES `bugs` (`bid`);
//                                    ALTER TABLE `bugreqlinks` ADD FOREIGN KEY (requirementid) REFERENCES `requirements` (`rid`);";

//            cmdSQL.CommandText = "DROP TABLE IF EXISTS users;" +
//                                  "CREATE TABLE users (" +
//                                     "uid INTEGER NOT NULL AUTO_INCREMENT, " +
//                                     "username VARCHAR(200) DEFAULT NULL ," +
//                                     "password VARCHAR(200) DEFAULT NULL ," +
//                                     "permissionLevel INTEGER DEFAULT NULL ," +
//                                     "active INTEGER DEFAULT NULL ," +
//                                     "PRIMARY KEY (uid));" +
//                                  "INSERT INTO users VALUES(1, 'Joe Bob', 'joebob', 0, 1);" +
//                                  "INSERT INTO users VALUES(2, 'Ty Burba', 'tyburba', 1, 1);" +
//                                  "INSERT INTO users VALUES(3, 'No one', 'lemmein', 2, 1);";
//            cmdSQL.ExecuteNonQuery();

//            cmdSQL.CommandText = "DROP TABLE IF EXISTS projects;" +
//                                 "CREATE TABLE projects (" +
//                                    "pid INTEGER NOT NULL AUTO_INCREMENT, " +
//                                    "name VARCHAR(200) DEFAULT NULL ," +
//                                    "PRIMARY KEY (pid));" +
//                                 "INSERT INTO projects VALUES(1, 'plan A');" +
//                                 "INSERT INTO projects VALUES(2, 'plan B');";
//            cmdSQL.ExecuteNonQuery();

//            cmdSQL.CommandText = "DROP TABLE IF EXISTS userprojectlinks;" +
//                                 "CREATE TABLE userprojectlinks (" +
//                                    "uplid INTEGER NOT NULL AUTO_INCREMENT, " +
//                                    "projectid INTEGER DEFAULT NULL ," +
//                                    "userid INTEGER DEFAULT NULL ," +
//                                    "permissionLevel INTEGER DEFAULT NULL ," +
//                                    "PRIMARY KEY (uplid));" +
//                                  "INSERT INTO userprojectlinks VALUES(1, 1, 1, 0);" +
//                                  "INSERT INTO userprojectlinks VALUES(2, 1, 2, 1);" +
//                                  "INSERT INTO userprojectlinks VALUES(3, 1, 3, 2);" +
//                                  "INSERT INTO userprojectlinks VALUES(4, 2, 1, 0);" +
//                                  "INSERT INTO userprojectlinks VALUES(5, 2, 2, 1);";
//            cmdSQL.ExecuteNonQuery();
//        }

