using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class TopLevel : Form
    {
        // Connection variable.
        protected MySqlConnection conSQL;
        protected MySqlCommand cmdSQL;
        protected MySqlDataAdapter adpSQL;
        // User/Project variables.
        protected string connectionString;
        protected string currentUser;
        protected int currentUserID;
        protected int currentProjectID;
        protected int currentUserPermLvl;
        // Keep data tables.
        protected DataTable proj_dt, ver_dt, req_dt, bug_dt;
        //protected DataTable user_dt, userreq_dt;

        public TopLevel()
        {
            InitializeComponent();
            // Default value for variables.
            conSQL = new MySqlConnection();
            cmdSQL = new MySqlCommand("", conSQL);
            adpSQL = new MySqlDataAdapter();
            adpSQL.SelectCommand = cmdSQL;
            // Log user, get id and level.
            LogUser();
            UpdateUserDisplay();
            // Sets the project combo box and updates the req and bug table.
            setProjComboBox();
        }
        // Always close the connection.
        ~TopLevel()
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
        // Updates user information changed by other forms (note that id does not change).
        private void UpdateUserInfo()
        {
            try
            {
                cmdSQL.CommandText = "SELECT * FROM users WHERE uid = " + currentUserID.ToString() + ";";
                DataTable myuser_dt = new DataTable();
                adpSQL.Fill(myuser_dt);
                this.currentUser = myuser_dt.Rows[0][1].ToString();
                this.currentUserPermLvl = Convert.ToInt32(myuser_dt.Rows[0][3]);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        // Grey out certain components according to user level.
        private void UpdateUserDisplay()
        {
            this.toolUsersButton.Visible = this.toolStripSeparator2.Visible = (this.currentUserPermLvl == 0);
            this.labelUserName.ForeColor = (this.currentUserPermLvl == 0) ? Color.Red : Color.Black;
            this.labelUserName.Text = this.currentUser;
        }

        private void setProjComboBox()
        {
            currentProjectID = -1;
            this.toolProjCombo.ComboBox.DataSource = null;
            //Set Curr Projects combo box
            proj_dt = new DataTable();
            cmdSQL.CommandText = "SELECT projects.pid, projects.name, userprojectlinks.permissionLevel FROM projects, userprojectlinks " +
                                 "WHERE projects.pid = userprojectlinks.projectid AND userprojectlinks.userid = " + currentUserID.ToString() + ";";
            adpSQL.Fill(proj_dt);
            
            if (proj_dt.Rows.Count != 0)
            {
                //if there are valid projects go ahead and update everything
                currentProjectID = (int)proj_dt.Rows[0][0];
                this.toolProjCombo.ComboBox.DataSource = proj_dt.DefaultView;
                this.toolProjCombo.ComboBox.DisplayMember = "name";
                updateReqTable();
                updateBugTable();
            }

            // Bind the combo box to the table
            this.UpdateProjectDisplay();
            this.setVersionsComboBox();   
        }
        // Display project related things.
        private void UpdateProjectDisplay()
        {
            if (currentProjectID < 0)
            {
                this.labelUserTitle.Text = "[User Title]";
                return;
            }
            switch (Convert.ToInt32(proj_dt.Rows[0][2]))
            {
                case 0:
                    this.labelUserTitle.Text = "Project Manager";
                    break;
                case 10:
                    this.labelUserTitle.Text = "Project Developer";
                    break;
                default:
                    this.labelUserTitle.Text = "End User";
                    break;
            }
        }

        private void setVersionsComboBox()
        {
            ver_dt = new DataTable();

            //establish the requirements grid
            cmdSQL.CommandText = "SELECT * FROM versions WHERE projectid = " + currentProjectID + " ORDER BY vid DESC;";
            adpSQL.Fill(ver_dt);
        }

        private void updateReqTable()
        {
            reqTable.DataSource = null;

            if (currentProjectID < 0)
                return;

            //establish the requirements grid
            
            cmdSQL.CommandText = "SELECT requirements.rid, requirements.requirementTitle, requirements.requirementDescription," +
                                 " requirements.priority, requirements.timeCreated, requirements.timeSatisfied, requirements.status, requirements.notes" +
                                 " FROM requirements, versions WHERE versions.projectid = " + currentProjectID.ToString() +
                                 " ORDER BY rid DESC;";
            req_dt = new DataTable();
            adpSQL.Fill(req_dt);
            // Change names.
            req_dt.PrimaryKey = new DataColumn[]{req_dt.Columns["rid"]};
            req_dt.Columns["rid"].ColumnName                    = "ReqID";
            req_dt.Columns["requirementTitle"].ColumnName       = "Title";
            req_dt.Columns["requirementDescription"].ColumnName = "Description";
            req_dt.Columns["priority"].ColumnName               = "Priority";
            req_dt.Columns["timeCreated"].ColumnName            = "Time Created";
            req_dt.Columns["timeSatisfied"].ColumnName          = "Time Satisfied";
            req_dt.Columns["status"].ColumnName                 = "Status";
            req_dt.Columns["notes"].ColumnName                  = "Notes";
            req_dt.DefaultView.Sort = String.Format("Status DESC, Priority DESC, ReqID DESC");

            reqTable.DataSource = req_dt.DefaultView;
            // Blank out ones we don't want.
            reqTable.Columns["Description"].Visible = false;
            reqTable.Columns["Notes"].Visible       = false;
            // Make others better.
            // reqTable.AutoSizeColumnMode(index) for single column.
            reqTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // reqTable.AutoResizeColumn(index) for single column.
            reqTable.AutoResizeColumns();
            /*
           //update userrequirementlinks table
           cmdSQL.CommandText = "SELECT userrequirementlinks.* FROM userrequirementlinks, versions WHERE versions.projectid = " + currentProjectID.ToString() + ";";
           userreq_dt = new DataTable();
           adpSQL.Fill(userreq_dt);

           
           //updates user_dt
           cmdSQL.CommandText = "SELECT * FROM users;";
           user_dt = new DataTable();
           adpSQL.Fill(user_dt);*/
        }

        private void updateBugTable()
        {
            bugTable.DataSource = null;

            if (currentProjectID < 0)
                return;

            //establish the bugs grid view
            // Get every bug current user is linked to.
            /*cmdSQL.CommandText = "SELECT * FROM bugs, userprojectlinks, userbuglinks WHERE userbuglinks.userid = " + currentProjectID.ToString() + " AND bugs.bid = userbuglinks.bugid AND" +
                                    " userprojectlinks.userid = userbuglinks.userid AND userprojectlinks.projectid = " + currentProjectID.ToString() + ";";*/
            // Get every bug in project.
            cmdSQL.CommandText = "SELECT bugs.bid, bugs.bugTitle, bugs.bugDescription, bugs.status, " +
                                 " bugs.timeOpen, bugs.timeClosed, bugs.notes, bugs.priority" +
                                 " FROM bugs, versions WHERE versions.projectid = " + currentProjectID.ToString() +
                                 " ORDER BY bid DESC;";
            bug_dt = new DataTable();
            adpSQL.Fill(bug_dt);
            // Change names.
            bug_dt.PrimaryKey = new DataColumn[] { bug_dt.Columns["bid"] };
            bug_dt.Columns["bid"].ColumnName            = "BugID";
            bug_dt.Columns["bugTitle"].ColumnName       = "Title";
            bug_dt.Columns["bugDescription"].ColumnName = "Description";
            bug_dt.Columns["priority"].ColumnName       = "Priority";
            bug_dt.Columns["timeOpen"].ColumnName       = "Time Open";
            bug_dt.Columns["timeClosed"].ColumnName     = "Time Closed";
            bug_dt.Columns["status"].ColumnName         = "Status";
            bug_dt.Columns["notes"].ColumnName          = "Notes";
            bug_dt.DefaultView.Sort = String.Format("{0} DESC", bug_dt.Columns["BugID"].ColumnName);

            bugTable.DataSource = bug_dt.DefaultView;
            // Blank out ones we don't want.
            bugTable.Columns["Description"].Visible = false;
            bugTable.Columns["Notes"].Visible       = false;
            // Make others better.
            // reqTable.AutoSizeColumnMode(index) for single column.
            bugTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // reqTable.AutoResizeColumn(index) for single column.
            bugTable.AutoResizeColumns();
        }

        private void reqTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 row = e.RowIndex;
            if (row < 0)
                return;

            //set values found in the requirements table
            idText.Text                     = req_dt.Rows[row][0].ToString(); //get id
            titleText.Text                  = req_dt.Rows[row][1].ToString();  //get title
            descriptionText.Text            = req_dt.Rows[row][2].ToString(); //get Description
            priorityComboBox.SelectedIndex  = Convert.ToInt32(req_dt.Rows[row][3]) - 1;  //changes the priority combo box
            timeOpenText.Text               = req_dt.Rows[row][4].ToString(); //get time created
            timeClosedText.Text             = req_dt.Rows[row][5].ToString(); //get time satisfied
            
            //get status
            switch (req_dt.Rows[row][6].ToString())
            {
                case("Open"):
                    statusComboBox.SelectedIndex = 0;
                    break;
                case("In Progress"):
                    statusComboBox.SelectedIndex = 1;
                    break;
                case("Closed"):
                    statusComboBox.SelectedIndex = 2;
                    break;
            }

            DataSet dsUsrReq = new DataSet();
            adpSQL.SelectCommand = cmdSQL;
            cmdSQL.CommandText = "SELECT users.uid, users.username FROM users, userrequirementlinks " +
                                 "WHERE userrequirementlinks.requirementid = " + req_dt.Rows[row][0].ToString() + 
                                 " AND users.uid = userrequirementlinks.userid " + 
                                 "AND role = 'originator';";
            adpSQL.Fill(dsUsrReq, "Originator");
            cmdSQL.CommandText = "SELECT users.uid, users.username FROM users, userrequirementlinks " +
                                 "WHERE userrequirementlinks.requirementid = " + req_dt.Rows[row][0].ToString() +
                                 " AND users.uid = userrequirementlinks.userid " + 
                                 "AND (role = 'owner' OR role = 'originator');";
            adpSQL.Fill(dsUsrReq, "Owners");
            cmdSQL.CommandText = "SELECT users.uid, users.username FROM users, userrequirementlinks " +
                                 "WHERE userrequirementlinks.requirementid = " + req_dt.Rows[row][0].ToString() +
                                 " AND users.uid = userrequirementlinks.userid " + 
                                 "AND role = 'watcher';";
            adpSQL.Fill(dsUsrReq, "Watchers");
            
            if (dsUsrReq.Tables["Originator"].Rows.Count > 0)
                originatorText.Text = dsUsrReq.Tables["Originator"].Rows[0]["username"].ToString();
            else
                MessageBox.Show("No Originator Found", "SQL DB Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            this.ownerListBox.DataSource = dsUsrReq.Tables["Owners"].DefaultView;
            this.ownerListBox.DisplayMember = "username";

            this.watchersListBox.DataSource = dsUsrReq.Tables["Watchers"].DefaultView;
            this.watchersListBox.DisplayMember = "username";
            /*
            //owner and watcher array's
            ArrayList ownerIDs = new ArrayList();
            ArrayList watcherIDs = new ArrayList();
            DataTable currOwners_dt = user_dt.Clone();
            DataTable currWatchers_dt = user_dt.Clone();

            //get the origionator, userreq_dt updated in update req table function
            String expression = "requirementid = " + req_dt.Rows[row][0].ToString();
            DataRow[] userreqlinks_dr;
            userreqlinks_dr = userreq_dt.Select(expression);

            int ownerID = -1;

            //here we are also going to construct the arrays of user id's for the owners and watchers list box
            for (int i = 0; i < userreqlinks_dr.Length; i++)
            {
                DataRow newRow;
                switch (userreqlinks_dr[i][3].ToString())
                {
                    case ("originator"):
                        ownerID = Convert.ToInt32(userreqlinks_dr[i][1]);
                        break;
                    case ("owner"):
                        ownerIDs.Add(Convert.ToInt32(userreqlinks_dr[i][1]));
                        //currOwners_dt.ImportRow(userreqlinks_dr[i]);
                        break;
                    case ("watcher"):
                        watcherIDs.Add(Convert.ToInt32(userreqlinks_dr[i][1]));
                        //currWatchers_dt.ImportRow(userreqlinks_dr[i]);
                        break;
                    default:
                        MessageBox.Show("No user associated with selected requirement", "SQL DB Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                }
            }

            //we have owner id go get owners name from user_dt
            expression = "uid = " + ownerID.ToString();
            DataRow[] user_dr;
            user_dr = user_dt.Select(expression);

            if (user_dr.Length > 0)
            {
                originatorText.Text = user_dr[0][1].ToString();
            }
            else
            {
                MessageBox.Show("No Origionator Found", "SQL DB Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            //populate the owners and watchers list boxes
            //owners list box
            for (int i = 0; i < ownerIDs.Count; i++)
            {
                expression = "uid = " + ownerIDs[i].ToString();
                DataRow[] owners_dr;
                owners_dr = user_dt.Select(expression);

                currOwners_dt.ImportRow(owners_dr[0]);
            }

            this.ownerListBox.DataSource = currOwners_dt.DefaultView;
            this.ownerListBox.DisplayMember = "username";

            //Watchers List Box
            for (int i = 0; i < watcherIDs.Count; i++)
            {
                expression = "uid = " + watcherIDs[i].ToString();
                DataRow[] watchers_dr;
                watchers_dr = user_dt.Select(expression);

                currWatchers_dt.ImportRow(watchers_dr[0]);
            }

            this.watchersListBox.DataSource = currWatchers_dt.DefaultView;
            this.watchersListBox.DisplayMember = "username";*/
        }

        private void bugTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 row = e.RowIndex;
            if (row < 0)
                return;

            //set values found in the requirements table
            idText.Text                     = bug_dt.Rows[row]["BugID"].ToString(); //get id
            titleText.Text                  = bug_dt.Rows[row]["Title"].ToString();  //get title
            descriptionText.Text            = bug_dt.Rows[row]["Description"].ToString(); //get Description
            priorityComboBox.SelectedIndex  = Convert.ToInt32(bug_dt.Rows[row]["Priority"]) - 1;  //changes the priority combo box
            timeOpenText.Text               = bug_dt.Rows[row]["Time Open"].ToString(); //get time created
            timeClosedText.Text             = bug_dt.Rows[row]["Time Closed"].ToString(); //get time satisfied

            //get status
            switch (req_dt.Rows[row]["Status"].ToString())
            {
                case ("Open"):
                    statusComboBox.SelectedIndex = 0;
                    break;
                case ("In Progress"):
                    statusComboBox.SelectedIndex = 1;
                    break;
                case ("Closed"):
                    statusComboBox.SelectedIndex = 2;
                    break;
            }

            DataSet dsUsrBug = new DataSet();
            adpSQL.SelectCommand = cmdSQL;
            cmdSQL.CommandText = "SELECT users.uid, users.username FROM users, userbuglinks " +
                                 "WHERE userbuglinks.bugid = " + bug_dt.Rows[row]["BugID"].ToString() +
                                 " AND users.uid = userbuglinks.userid " +
                                 "AND role = 'originator';";
            adpSQL.Fill(dsUsrBug, "Originator");
            cmdSQL.CommandText = "SELECT users.uid, users.username FROM users, userbuglinks " +
                                 "WHERE userbuglinks.bugid = " + bug_dt.Rows[row]["BugID"].ToString() +
                                 " AND users.uid = userbuglinks.userid " +
                                 "AND (role = 'owner' OR role = 'originator');";
            adpSQL.Fill(dsUsrBug, "Owners");
            cmdSQL.CommandText = "SELECT users.uid, users.username FROM users, userbuglinks " +
                                 "WHERE userbuglinks.bugid = " + bug_dt.Rows[row]["BugID"].ToString() +
                                 " AND users.uid = userbuglinks.userid " +
                                 "AND role = 'watcher';";
            adpSQL.Fill(dsUsrBug, "Watchers");

            if (dsUsrBug.Tables["Originator"].Rows.Count > 0)
                originatorText.Text = dsUsrBug.Tables["Originator"].Rows[0]["username"].ToString();
            else
                MessageBox.Show("No Origionator Found", "SQL DB Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            this.ownerListBox.DataSource    = dsUsrBug.Tables["Owners"].DefaultView;
            this.ownerListBox.DisplayMember = "username";

            this.watchersListBox.DataSource     = dsUsrBug.Tables["Watchers"].DefaultView;
            this.watchersListBox.DisplayMember  = "username";
        }

        private void newReqButton_Click(object sender, EventArgs e)
        {
            NewReq newReqWindow = new NewReq(conSQL, currentUser, currentUserID, currentProjectID, ver_dt, req_dt/*, userreq_dt*/);
            newReqWindow.ShowDialog();
        }

        private void newBugButton_Click(object sender, EventArgs e)
        {
            NewBug newBugWindow = new NewBug(conSQL, currentUser, currentUserID, currentProjectID, ver_dt, bug_dt, req_dt);
            newBugWindow.ShowDialog();
        }

        private void statusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolProjCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Got an error here when I logged out and tried to log in with different user****
            //got error here from adding ross to a project as a developer.  When I closed user boxes it gave problem ****
            currentProjectID = (Int32)proj_dt.Rows[this.toolProjCombo.ComboBox.SelectedIndex][0];   //gives the id selected
            updateReqTable();
            updateBugTable();
        }

        private void projects_toolstrip_Click(object sender, EventArgs e)
        {
            Projects projWindow = new Projects(conSQL, currentUserID, currentUserPermLvl);
            projWindow.ShowDialog();
            setProjComboBox();
        }

        private void toolUsersButton_Click(object sender, EventArgs e)
        {
            Users usersWindow = new Users(conSQL, currentUserID);
            usersWindow.ShowDialog();
            UpdateUserInfo();
            UpdateUserDisplay();
        }

        private void toolStripButtonLogOff_Click(object sender, EventArgs e)
        {
            conSQL.Close();
            LogUser();
            UpdateUserDisplay();
            setProjComboBox();
        }

        private void addVerButton_Click(object sender, EventArgs e)
        {
            this.releaseListBox.Items.Add(ver_dt.Rows[releaseListBox.SelectedIndex][0].ToString());    //Sets up the watchers combo box
            //remove users from the table so they are no longer shown in the combo box
            ver_dt.Rows[releaseListBox.SelectedIndex].Delete();
            ver_dt.AcceptChanges();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            //sample query from users line 169
            //m_cmdSQL.CommandText = "UPDATE users SET username = '" + name + "', password = '" + pass + "', permissionLevel = '" +
              //                         newLevel + "', active = " + activity + ", email = '" + email + "' WHERE uid = " + m_userID;
        }
    }
}

